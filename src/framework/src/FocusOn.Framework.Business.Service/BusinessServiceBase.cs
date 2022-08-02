using AutoMapper;
using System.Security.Claims;
using FocusOn.Framework.Security;
using Microsoft.Extensions.Logging;
using FocusOn.Framework.Business.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace FocusOn.Framework.Business.Service;

/// <summary>
/// 表示业务服务的基类。
/// </summary>
public abstract class BusinessServiceBase : IBusinessSerivce
{
    /// <summary>
    /// 初始化 <see cref="BusinessServiceBase"/> 类的新实例。
    /// </summary>
    /// <param name="serviceProvider">服务注册提供者。</param>
    protected BusinessServiceBase(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// 获取 <see cref="IMapper"/> 实例。
    /// </summary>
    protected IMapper Mapper => ServiceProvider.GetRequiredService<IMapper>();
    /// <summary>
    /// 获取注册的 <see cref="ILoggerFactory"/> 实例。
    /// </summary>
    protected ILoggerFactory LoggerFactory => ServiceProvider.GetRequiredService<ILoggerFactory>();

    /// <summary>
    /// 获取 <see cref="ILogger"/> 实例。
    /// </summary>
    protected virtual ILogger? Logger => LoggerFactory.CreateLogger(GetType().Name);

    /// <summary>
    /// 获取当前访问主体。
    /// </summary>
    protected ICurrentPrincipalAccessor? CurrentPrincipal => ServiceProvider.GetService<ICurrentPrincipalAccessor>();
    /// <summary>
    /// 获取当前用户主体。
    /// </summary>
    protected ClaimsPrincipal? CurrentUser => CurrentPrincipal?.CurrentPrincipal;

    /// <summary>
    /// 获取可取消异步操作的令牌，默认是1分钟。
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
