using Microsoft.Extensions.DependencyInjection;

namespace FocusOn.Framework;
public static class ServiceCollectionExtensions
{
    public static TService? GetServiceInstance<TService>(this IServiceCollection services) where TService : notnull
        => (TService?)services.FirstOrDefault(m => m.ServiceType == typeof(TService))?.ImplementationInstance;
}
