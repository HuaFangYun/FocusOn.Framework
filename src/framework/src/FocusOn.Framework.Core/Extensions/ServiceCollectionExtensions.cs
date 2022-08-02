using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FocusOn.Framework;
/// <summary>
/// <see cref="IServiceCollection"/> 的扩展。
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 获取指定服务的实例。
    /// </summary>
    /// <typeparam name="TService">服务类型。</typeparam>
    /// <param name="services"><see cref="IServiceCollection"/> 实例。</param>
    /// <returns>指定服务的实例或 null。</returns>
    public static TService? GetServiceInstance<TService>(this IServiceCollection services) where TService : notnull
        => (TService?)services.FirstOrDefault(m => m.ServiceType == typeof(TService))?.ImplementationInstance;

    /// <summary>
    /// 获取 <see cref="IConfiguration"/> 服务。
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> 实例。</param>
    /// <returns><see cref="IConfiguration"/> 服务的实例或 null。</returns>
    public static IConfiguration? GetConfiguration(this IServiceCollection services)
    {
        var hostBuilderContext = services.GetServiceInstance<HostBuilderContext>();
        if (hostBuilderContext?.Configuration != null)
        {
            return hostBuilderContext.Configuration as IConfigurationRoot;
        }

        return services.GetServiceInstance<IConfiguration>();
    }
}
