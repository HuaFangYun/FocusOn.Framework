﻿//using System.Reflection;

//using Microsoft.AspNetCore.Mvc.Controllers;

//using FocusOn.Framework.Business.Contract;
//using FocusOn.Framework.Framework.Business.Contract;

//namespace FocusOn.Framework.Endpoint.HttpApi.Conventions;

///// <summary>
///// 表示自动化识别 HTTP API 的特性提供器。
///// </summary>
//internal class DynamicHttpApiControllerFeatureProvider : ControllerFeatureProvider
//{
//    protected override bool IsController(TypeInfo typeInfo)
//    {
//        if (typeof(IRemotingService).IsAssignableFrom(typeInfo))
//        {
//            return !typeInfo.IsAbstract && !typeInfo.IsInterface && !typeInfo.IsGenericType && typeInfo.IsPublic;
//        }

//        return typeInfo.TryGetCustomAttribute(out RemotingServiceAttribute attribute);
//    }
//}