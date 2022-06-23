using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

using MiniSolution.Business.Contracts;

namespace MiniSolution.Endpoints.HttpApi.Conventions;

public class DynamicHttpApiConvention : IApplicationModelConvention
{
    public DynamicHttpApiConvention()
    {
    }

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
        controller.ApiExplorer.IsVisible = !controller.ApiExplorer.IsVisible.HasValue;

        foreach (var action in controller.Actions)
        {
            action.ApiExplorer.IsVisible = !action.ApiExplorer.IsVisible.HasValue;
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

    private void ConfigureParameters(ControllerModel controller)
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

                    if (httpMethods.Contains("GET") ||
                        httpMethods.Contains("DELETE") ||
                        httpMethods.Contains("TRACE") ||
                        httpMethods.Contains("HEAD"))
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
        var controllerName = ResolveControllerName(action);

        routeTemplateBuilder.Append($"/{controllerName}s");

        if (action.Parameters.Any(m => m.ParameterName.ToLower() == "id"))
        {
            routeTemplateBuilder.Append("/{id}");
        }

        var actionName = action.ActionName;
        if (actionName.EndsWith("Async"))
        {
            actionName = actionName.Replace("Async", string.Empty);
        }


        var trimPrefixes = new[]
{
    "GetBy","GetList","Get",
    "Post","Create","Add","Insert",
    "Put","Update",
    "Delete","Remove",
    "Patch"
};
        foreach (var trimPrefix in trimPrefixes)
        {
            if (actionName.StartsWith(trimPrefix))
            {
                actionName = actionName[trimPrefix.Length..];
                break;
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
        var actionName = action.ActionName;
        if (actionName.StartsWith("Get"))
        {
            return "GET";
        }

        if (actionName.StartsWith("Put") || actionName.StartsWith("Update"))
        {
            return "PUT";
        }

        if (actionName.StartsWith("Delete") || actionName.StartsWith("Remove"))
        {
            return "DELETE";
        }

        if (actionName.StartsWith("Patch"))
        {
            return "PATCH";
        }

        return "POST";
    }

    protected virtual string ResolveControllerName(ActionModel action)
    {
        var controllerName = action.Controller.ControllerName;

        foreach (var name in new[] { "ApplicationService", "AppService" })
        {
            if (controllerName.EndsWith(name))
            {
                controllerName = controllerName.Substring(0,controllerName.IndexOf(name));
            }
        }
        return controllerName;
    }
}
