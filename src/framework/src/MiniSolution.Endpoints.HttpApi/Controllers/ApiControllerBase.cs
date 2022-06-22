﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MiniSolution.Endpoints.HttpApi.Controllers;

/// <summary>
/// 表示具备 HTTP API 功能的控制器基类。
/// </summary>
[Route("[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    /// <summary>
    /// 获取 <see cref="IServiceProvider"/> 实例。
    /// </summary>
    public IServiceProvider ServiceProvider => HttpContext.RequestServices;

    /// <summary>
    /// 获取 <see cref="ILoggerFactory"/> 实例。
    /// </summary>
    protected ILoggerFactory LoggerFactory => ServiceProvider.GetRequiredService<ILoggerFactory>();
    /// <summary>
    /// 获取 <see cref="ILogger"/> 实例。
    /// </summary>
    protected virtual ILogger Logger => LoggerFactory.CreateLogger(GetType().Name);
}
