using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using FocusOn.Business.Contracts;

namespace FocusOn.Endpoints.HttpApi.Conventions;

/// <summary>
/// 动态 HTTP API 的约定。
/// </summary>
internal class DynamicHttpApiConvention : IApplicationModelConvention
{
    /// <summary>
    /// 识别并替换的 controller 后缀关键字。
    /// </summary>
    readonly static string[] Controller_Replace_Sufix_Key_Words = new string[] { "ApplicationService", "AppService", "BusinessService","BizService" };
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
        ["Insert"]= HttpMethods.Post,
        ["Update"] = HttpMethods.Put,
        ["Edit"] = HttpMethods.Put,
        ["Patch"]= HttpMethods.Put,
        ["Delete"] = HttpMethods.Delete,
        ["Remove"] = HttpMethods.Delete,
        ["Get"] = HttpMethods.Get,
        ["Find"] = HttpMethods.Get,
    };
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
{
            if (typeof(IRemotingService).IsAssignableFrom(controller.ControllerType))
            {
                ConfigureApiExplorer(controller);
                ConfigureSelector(controller);
                ConfigureParameters(controller);
            }
        }
    }

    void ConfigureApiExplorer(ControllerModel controller)
    {
        if(TryGetRemotingServiceAttribute(controller, out RemotingServiceAttribute? remotingServiceAttribute))
        {
            controller.ApiExplorer.IsVisible = !remotingServiceAttribute?.Disabled;
        }
        else
        {
            controller.ApiExplorer.IsVisible = true;
        }

        foreach (var action in controller.Actions)
        {
            if (TryGetRemotingServiceAttribute(action,out RemotingServiceAttribute? actionRemotingServiceAttribute))
            {
                action.ApiExplorer.IsVisible = !actionRemotingServiceAttribute?.Disabled;
            }
            else
            {
                action.ApiExplorer.IsVisible = true;
            }
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
            AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(GenerateRouteTemplate(action)))
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
                selector.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(GenerateRouteTemplate(action)));
            }

            if (!selector.ActionConstraints.Any())
            {
                selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { GetHttpMethod(action) }));
            }
        }
    }

    string GenerateRouteTemplate(ActionModel action)
    {
        var routeTemplateBuilder = new StringBuilder("api");
        var controllerName = GetControllerName(action);

        routeTemplateBuilder.Append($"/{controllerName}");

        if (action.Parameters.Any(m => m.ParameterName.ToLower() == "id"))
        {
            routeTemplateBuilder.Append("/{id}");
        }

        var actionName = action.ActionName;

        if (TryGetRemotingServiceAttribute(action, out var remotingServiceAttribute) && !remotingServiceAttribute.Template.IsNullOrEmpty())
        {
            actionName = remotingServiceAttribute.Template;
        }
        else
        {
            if (actionName.EndsWith("Async"))
            {
                actionName = actionName.Replace("Async", string.Empty);
            }

            foreach (var trimPrefix in Action_Replace_Prefix_Key_Words)
            {
                if (actionName.StartsWith(trimPrefix))
                {
                    actionName = actionName[trimPrefix.Length..];
                    break;
                }
            }
        }

        if (!actionName.IsNullOrEmpty())
        {
            routeTemplateBuilder.Append($"/{actionName}");
        }
        return routeTemplateBuilder.ToString();
    }

    string GetHttpMethod(ActionModel action)
    {
        foreach (var item in Action_HttpMethod_Mapping.Keys)
        {
            if (action.ActionName.Contains(item))
            {
                return Action_HttpMethod_Mapping[item];
            }
        }
        return HttpMethods.Post;
    }

    /// <summary>
    /// 获取 controller 的名称。
    /// </summary>
    /// <param name="action">Action</param>
    /// <returns></returns>
    protected virtual string GetControllerName(ActionModel action)
    {
        if(TryGetRemotingServiceAttribute(action.Controller,out var controllerRemotingServiceAttribute) && !controllerRemotingServiceAttribute.Template.IsNullOrEmpty())
        {
            return controllerRemotingServiceAttribute.Template;
        }

        var controllerName = action.Controller.ControllerName;
        
        foreach (var name in Controller_Replace_Sufix_Key_Words)
        {
            if (controllerName.EndsWith(name))
            {
                controllerName = controllerName.Substring(0,controllerName.IndexOf(name));
            }
        }
        return controllerName;
    }

    bool TryGetRemotingServiceAttribute(ControllerModel controller,out RemotingServiceAttribute? attribute)
    {
        return controller.ControllerType.TryGetCustomAttribute(out attribute);
    }

    bool TryGetRemotingServiceAttribute(ActionModel action, out RemotingServiceAttribute? attribute)
    {
        var actionRemotingServiceAttribute = action.Attributes.Where(m=>m is RemotingServiceAttribute).Select(m => m as RemotingServiceAttribute).FirstOrDefault();
        attribute = actionRemotingServiceAttribute;
        return actionRemotingServiceAttribute != null;
    }
}
