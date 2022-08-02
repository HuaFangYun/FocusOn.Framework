using FocusOn.Framework;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 对于 <see cref="IServiceCollection"/> 服务注册的扩展。
/// </summary>
public static class FocusOnDependencyInjectionExtensions
{
    /// <summary>
    /// 添加 FocusOn 的指定配置到 <see cref="IServiceCollection"/> 中。
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> 实例。</param>
    /// <param name="configure">一个可以配置 <see cref="FocusOnBuilder"/> 的委托。</param>
    /// <exception cref="ArgumentNullException"><paramref name="configure"/> 是 <c>null</c>。</exception>
    public static IServiceCollection AddFocusOn(this IServiceCollection services, Action<FocusOnBuilder> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        configure(new FocusOnBuilder(services));
        return services;
    }
}
