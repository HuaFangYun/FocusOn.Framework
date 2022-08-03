using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FocusOn.Framework.Business.Contract;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Authorization;
using FocusOn.Framework.Business.Contract.Http;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace FocusOn.Framework.AspNetCore.Http.Conventions;

/// <summary>
/// 动态 HTTP API 的约定。
/// </summary>
internal class DynamicHttpApiConvention : IApplicationModelConvention
{
    ///// <summary>
    ///// 识别并替换的 controller 后缀关键字。
    ///// </summary>
    //readonly static string[] Controller_Replace_Sufix_Key_Words = new string[] { "BusinessService", "AppService", "BusinessService", "BizService" };
    ///// <summary>
    ///// 识别并替换的 action 前缀关键字。
    ///// </summary>
    //readonly static string[] Action_Replace_Prefix_Key_Words = new[] {
    //"GetBy","GetList","Get",
    //"Post","Create","Add","Insert",
    //"Put","Update",
    //"Delete","Remove",
    //"Patch"};
    //readonly static Dictionary<string, string> Action_HttpMethod_Mapping = new()
    //{
    //    ["Create"] = HttpMethods.Post,
    //    ["Add"] = HttpMethods.Post,
    //    ["Insert"] = HttpMethods.Post,
    //    ["Update"] = HttpMethods.Put,
    //    ["Edit"] = HttpMethods.Put,
    //    ["Patch"] = HttpMethods.Put,
    //    ["Delete"] = HttpMethods.Delete,
    //    ["Remove"] = HttpMethods.Delete,
    //    ["Get"] = HttpMethods.Get,
    //    ["Find"] = HttpMethods.Get,
    //};
    private Type? _interfaceAsControllerType;

    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            if (typeof(IRemotingService).IsAssignableFrom(controller.ControllerType))
            {
                _interfaceAsControllerType = GetOnlyServiceType(controller.ControllerType);
                if (_interfaceAsControllerType is null)
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
        if (_interfaceAsControllerType.TryGetCustomAttribute<Business.Contract.Http.RouteAttribute>(out var routeAttribute) && !routeAttribute.Name.IsNullOrEmpty())
        {
            controller.ControllerName = routeAttribute.Name;
        }

        //if (controller.ControllerType.TryGetCustomAttribute<Business.Contract.Authorizations.AuthorizeAttribute>(out var authorizeAttribute))
        //{
        //    controller.Filters.Add(new AuthorizeFilter());
        //}

        foreach (var action in controller.Actions)
        {
            if (action is null)
            {
                continue;
            }

            var actionMethodAttribute = FindHttpMethodFromAction(action);
            if (actionMethodAttribute is null)
            {
                action.ApiExplorer.IsVisible = false;
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
            if (FindHttpMethodFromAction(action) is null)
            {
                return;
            }

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

    private void ConfigureParameters(ControllerModel controller)
    {
        foreach (var action in controller.Actions)
        {
            var method = FindInterfaceMethod(action);
            if (method is null)
            {
                continue;
            }

            foreach (var parameter in action.Parameters)
            {
                if (parameter.BindingInfo != null)
                {
                    continue;
                }

                var methodParameter = method.GetParameters().SingleOrDefault(m => m.Name == parameter.Name);

                if (methodParameter is null)
                {
                    continue;
                }

                var httpParameterAttribute = methodParameter.GetCustomAttribute<HttpParameterAttribute>();
                if (httpParameterAttribute is null)
                {
                    parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromRouteAttribute() });
                    continue;
                }
                switch (httpParameterAttribute.Type)
                {
                    case HttpParameterType.FromBody:
                        parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                        break;
                    case HttpParameterType.FromQuery:
                        parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromQueryAttribute() { Name = httpParameterAttribute.Name } });
                        break;
                    case HttpParameterType.FromHeader:
                        parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromHeaderAttribute() { Name = httpParameterAttribute.Name } });
                        break;
                    default:
                        parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromRouteAttribute() { Name = httpParameterAttribute.Name } });
                        break;
                }

                //parameter.ParameterType.GetInterfaces().Where(m => m.IsDefined(typeof(HttpParameterAttribute)));

                //if (parameter.ParameterType.IsClass &&
                //    parameter.ParameterType != typeof(string) &&
                //    parameter.ParameterType != typeof(IFormFile))
                //{
                //    var httpMethods = action.Selectors.SelectMany(temp => temp.ActionConstraints).OfType<HttpMethodActionConstraint>().SelectMany(temp => temp.HttpMethods).ToList();

                //    if (httpMethods.Contains(HttpMethods.Get) ||
                //        httpMethods.Contains(HttpMethods.Delete) ||
                //        httpMethods.Contains(HttpMethods.Trace) ||
                //        httpMethods.Contains(HttpMethods.Head))
                //    {
                //        continue;
                //    }
                //    parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                //}
            }
        }
    }

    void AddBusinessServiceSelector(ActionModel action)
    {
        var selector = new SelectorModel
        {
            AttributeRouteModel = new AttributeRouteModel(GenerateRoute(action))
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
                selector.AttributeRouteModel = new AttributeRouteModel(GenerateRoute(action));
            }

            if (!selector.ActionConstraints.Any())
            {
                selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { GetHttpMethod(action) }));
            }
        }
    }

    Microsoft.AspNetCore.Mvc.RouteAttribute GenerateRoute(ActionModel action)
    {
        if (_interfaceAsControllerType is null)
        {
            throw new InvalidOperationException($"不能识别成 Controller");
        }

        if (!_interfaceAsControllerType.TryGetCustomAttribute<Business.Contract.Http.RouteAttribute>(out var routeAttribute))
        {

        }

        var routeTemplateBuilder = new StringBuilder();
        var template = routeAttribute?.Template;

        routeTemplateBuilder.Append($"/{template}");

        var httpMethodAttribute = FindHttpMethodFromAction(action);
        if (httpMethodAttribute is null)
        {
            throw new InvalidOperationException($"Action {action.ActionMethod.Name} 必须设置 {nameof(HttpMethodAttribute)} 特性才能生成路由");
        }

        routeTemplateBuilder.Append($"/{httpMethodAttribute?.Template}");

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

        return new(routeTemplateBuilder.ToString()) { Name = routeAttribute.Name ?? httpMethodAttribute.Name, Order = routeAttribute.Order };
    }

    /// <summary>
    /// 从方法的接口处识别出 <see cref="HttpMethodAttribute"/> 特性。
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    private HttpMethodAttribute? FindHttpMethodFromAction(ActionModel action)
    {
        MethodInfo? actionMethod = FindInterfaceMethod(action);

        if (actionMethod is null)
        {
            //Action，有这个方法，但接口没有这个方法，会报错。
            //throw new InvalidOperationException($"找不到 {action.ActionName} 方法");

            return default;
        }

        return actionMethod.GetCustomAttributes<HttpMethodAttribute>().OrderBy(m => m.Order).FirstOrDefault();
    }

    /// <summary>
    /// 从 action 中识别出符合接口的方法。
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    private MethodInfo? FindInterfaceMethod(ActionModel action)
    {
        var allmethods = _interfaceAsControllerType.GetMethods().Concat(_interfaceAsControllerType.GetInterfaces().SelectMany(m => m.GetMethods())).Distinct();

        var methodName = action.ActionMethod.Name;

        var actionMethod = allmethods.SingleOrDefault(m => m.Name == methodName);
        return actionMethod;
    }

    string GetHttpMethod(ActionModel action)
    {
        var httpMethodAttribute = FindHttpMethodFromAction(action);
        if (httpMethodAttribute is null)
        {
            throw new InvalidOperationException($"方法必须定义 {nameof(HttpMethodAttribute)} 特性");
        }

        return httpMethodAttribute.Method.Method;

        //foreach (var item in Action_HttpMethod_Mapping.Keys)
        //{
        //    if (action.ActionName.Contains(item))
        //    {
        //        return Action_HttpMethod_Mapping[item];
        //    }
        //}
        //return HttpMethods.Post;
    }


    private static Type? GetOnlyServiceType(TypeInfo controller)
    {
        return controller.GetInterfaces().SingleOrDefault(m => m.IsDefined(typeof(Business.Contract.Http.RouteAttribute)));
    }


    bool TryGetAttribute<TAttribute>(ActionModel action, out TAttribute? attribute) where TAttribute : Attribute
    {
        var actionRemotingServiceAttribute = action.Attributes.OfType<TAttribute>().FirstOrDefault();
        attribute = actionRemotingServiceAttribute;
        return actionRemotingServiceAttribute != null;
    }
}
