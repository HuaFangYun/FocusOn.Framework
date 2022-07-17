using AutoMapper;

using FocusOn.Framework.Business.Contract;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FocusOn.Framework.Business.Services;

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
