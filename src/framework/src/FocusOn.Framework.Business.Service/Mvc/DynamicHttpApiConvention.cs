using FocusOn;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FocusOn.Framework.Business.Contract;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FocusOn.Framework.Business.Contract.Http;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace FocusOn.Framework.Business.Services.Mvc;

/// <summary>
/// 动态 HTTP API 的约定。
/// </summary>
internal class DynamicHttpApiConvention : IApplicationModelConvention
{
    /// <summary>
    /// 识别并替换的 controller 后缀关键字。
    /// </summary>
    readonly static string[] Controller_Replace_Sufix_Key_Words = new string[] { "BusinessService", "AppService", "BusinessService", "BizService" };
    /// <summary>
    /// 识别并替换的 action 前缀关键字。
    /// </summary>
    readonly static string[] Action_Replace_Prefix_Key_Words = new[] {
    "GetBy","GetList","Get",
    "Post","Create","Add","Insert",
    "Put","Update",
    "Delete","Remove",
    "Patch"};
    readonly static Dictionary<string, string> Action_HttpMethod_Mapping = new()
    {
        ["Create"] = HttpMethods.Post,
        ["Add"] = HttpMethods.Post,
        ["Insert"] = HttpMethods.Post,
        ["Update"] = HttpMethods.Put,
        ["Edit"] = HttpMethods.Put,
        ["Patch"] = HttpMethods.Put,
        ["Delete"] = HttpMethods.Delete,
        ["Remove"] = HttpMethods.Delete,
        ["Get"] = HttpMethods.Get,
        ["Find"] = HttpMethods.Get,
    };
    private Type? _controllerType;

    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            if (typeof(IRemotingService).IsAssignableFrom(controller.ControllerType))
            {
                _controllerType = GetOnlyServiceType(controller.ControllerType);
                if (_controllerType is null)
                {
                    return;
                }

                ConfigureApiExplorer(controller);
                ConfigureSelector(controller);
                ConfigureParameters(controller);
            }
        }
    }

    void ConfigureApiExplorer(ControllerModel controller)
    {
        controller.ApiExplorer.IsVisible = true;

        foreach (var action in controller.Actions)
        {
            var actionMethodAttribute = FindHttpMethodFromAction(action);
            if (actionMethodAttribute is null)
            {
                continue;
            }
            action.ApiExplorer.IsVisible = true;
        }
    }

    private void ConfigureSelector(ControllerModel controller)
    {
        RemoveEmptySelectors(controller.Selectors);

        if (controller.Selectors.Any(temp => temp.AttributeRouteModel != null))
        {
            return;
        }

        foreach (var action in controller.Actions)
        {
            ConfigureSelector(action);
        }

        void ConfigureSelector(ActionModel action)
        {
            RemoveEmptySelectors(action.Selectors);

            if (action.Selectors.Count <= 0)
            {
                AddBusinessServiceSelector(action);
            }
            else
            {
                NormalizeSelectorRoutes(action);
            }
        }

        static void RemoveEmptySelectors(IList<SelectorModel> selectors)
            => selectors.Where(selector => selector.AttributeRouteModel == null
                                                                                && !selector.ActionConstraints.Any()
                                                                                && !selector.EndpointMetadata.Any())
                .ToList().ForEach(s => selectors.Remove(s));
    }

    private static void ConfigureParameters(ControllerModel controller)
    {
        foreach (var action in controller.Actions)
        {
            foreach (var parameter in action.Parameters)
            {
                if (parameter.BindingInfo != null)
                {
                    continue;
                }

                parameter.ParameterType.GetInterfaces().Where(m => m.IsDefined(typeof(HttpParameterAttribute)));

                if (parameter.ParameterType.IsClass &&
                    parameter.ParameterType != typeof(string) &&
                    parameter.ParameterType != typeof(IFormFile))
                {
                    var httpMethods = action.Selectors.SelectMany(temp => temp.ActionConstraints).OfType<HttpMethodActionConstraint>().SelectMany(temp => temp.HttpMethods).ToList();

                    if (httpMethods.Contains(HttpMethods.Get) ||
                        httpMethods.Contains(HttpMethods.Delete) ||
                        httpMethods.Contains(HttpMethods.Trace) ||
                        httpMethods.Contains(HttpMethods.Head))
                    {
                        continue;
                    }
                    parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                }
            }
        }
    }

    void AddBusinessServiceSelector(ActionModel action)
    {
        var selector = new SelectorModel
        {
            AttributeRouteModel = new AttributeRouteModel(new Microsoft.AspNetCore.Mvc.RouteAttribute(GenerateRouteTemplate(action)))
        };

        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { GetHttpMethod(action) }));

        action.Selectors.Add(selector);
    }
    private void NormalizeSelectorRoutes(ActionModel action)
    {
        foreach (var selector in action.Selectors)
        {
            if (selector.AttributeRouteModel == null)
            {
                selector.AttributeRouteModel = new AttributeRouteModel(new Microsoft.AspNetCore.Mvc.RouteAttribute(GenerateRouteTemplate(action)));
            }

            if (!selector.ActionConstraints.Any())
            {
                selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { GetHttpMethod(action) }));
            }
        }
    }

    string GenerateRouteTemplate(ActionModel action)
    {
        var routeTemplateBuilder = new StringBuilder();
        var controllerName = GetControllerTemplate(action);

        routeTemplateBuilder.Append($"/{controllerName}");

        var attr = FindHttpMethodFromAction(action);

        routeTemplateBuilder.Append($"{attr?.Template}");

        //else
        //{
        //    if (actionName.EndsWith("Async"))
        //    {
        //        actionName = actionName.Replace("Async", string.Empty);
        //    }

        //    foreach (var trimPrefix in Action_Replace_Prefix_Key_Words)
        //    {
        //        if (actionName.StartsWith(trimPrefix))
        //        {
        //            actionName = actionName[trimPrefix.Length..];
        //            break;
        //        }
        //    }
        //}

        return routeTemplateBuilder.ToString();
    }

    private HttpMethodAttribute? FindHttpMethodFromAction(ActionModel action)
    {
        var actionName = action.ActionName;
        var actionMethod = _controllerType.GetMethod(actionName);
        if (actionMethod is null)
        {
            throw new InvalidOperationException($"找不到 {actionName} 方法");
        }

        var attr = actionMethod.GetCustomAttribute<HttpMethodAttribute>();
        if (attr is null)
        {

        }

        return attr;
    }

    string GetHttpMethod(ActionModel action)
    {
        if (!TryGetAttribute<HttpMethodAttribute>(action, out var httpMethod))
        {
            throw new InvalidOperationException($"方法必须定义 {nameof(HttpMethodAttribute)} 特性");
        }

        return httpMethod.Method.Method;

        //foreach (var item in Action_HttpMethod_Mapping.Keys)
        //{
        //    if (action.ActionName.Contains(item))
        //    {
        //        return Action_HttpMethod_Mapping[item];
        //    }
        //}
        //return HttpMethods.Post;
    }

    /// <summary>
    /// 获取 controller 的名称。
    /// </summary>
    /// <param name="action">Action</param>
    /// <returns></returns>
    protected virtual string GetControllerTemplate(ActionModel action)
    {
        if (_controllerType is null)
        {
            throw new InvalidOperationException($"不能识别成 Controller");
        }

        var routeAttribute = _controllerType.GetCustomAttribute<Contract.Http.RouteAttribute>();

        return routeAttribute.Template;
    }

    private static Type? GetOnlyServiceType(TypeInfo controller)
    {
        return controller.GetInterfaces().SingleOrDefault(m => m.IsDefined(typeof(Contract.Http.RouteAttribute)));
    }


    bool TryGetAttribute<TAttribute>(ActionModel action, out TAttribute? attribute) where TAttribute : Attribute
    {
        var actionRemotingServiceAttribute = action.Attributes.OfType<TAttribute>().FirstOrDefault();
        attribute = actionRemotingServiceAttribute;
        return actionRemotingServiceAttribute != null;
    }
}
