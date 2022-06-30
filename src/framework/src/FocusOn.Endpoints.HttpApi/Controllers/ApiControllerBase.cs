
using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FocusOn.Endpoints.HttpApi.Controllers;

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
    /// <summary>
    /// 获取 <see cref="IMapper"/> 实例。
    /// </summary>
    protected IMapper Mapper => ServiceProvider.GetRequiredService<IMapper>();
    /// <summary>
    /// 取消异步操作的令牌，默认是1分钟。
    /// </summary>
    protected virtual CancellationToken CancellationToken
    {
        get
        {
            var tokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(1));
            return tokenSource.Token;
        }
    }
}
