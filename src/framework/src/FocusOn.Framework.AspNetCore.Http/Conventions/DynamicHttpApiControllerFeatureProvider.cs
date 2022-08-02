using System.Reflection;

using FocusOn.Framework.Business.Contract;

using Microsoft.AspNetCore.Mvc.Controllers;

namespace FocusOn.Framework.AspNetCore.Http.Conventions;

/// <summary>
/// 表示自动化识别 HTTP API 的特性提供器。
/// </summary>
public class DynamicHttpApiControllerFeatureProvider : ControllerFeatureProvider
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
