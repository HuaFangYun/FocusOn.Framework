using System.Reflection;

using Microsoft.AspNetCore.Mvc.Controllers;

using MiniSolution.Business.Contracts;

namespace MiniSolution.Endpoints.HttpApi.Conventions;

internal class DynamicHttpApiControllerFeatureProvider : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo)
    {
        if (typeof(IRemotingService).IsAssignableFrom(typeInfo))
        {
            return !typeInfo.IsAbstract && !typeInfo.IsInterface && !typeInfo.IsGenericType && typeInfo.IsPublic;
        }
        return false;
    }
}
