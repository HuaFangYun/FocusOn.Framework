using System;
using System.Linq;
using System.Text;
using System.Reflection;
using Castle.DynamicProxy;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using FocusOn.Framework.Business.Contract;
using FocusOn.Framework.Endpoint.HttpProxy.Dynamic;
using FocusOn.Framework;

namespace Microsoft.Extensions.DependencyInjection;
public static class DypendencyInjectionExtensions
{
    static readonly ProxyGenerator Generator = new();

    public static FocusOnBuilder AddDynamicHttpProxy(this FocusOnBuilder builder, Assembly assembly, Action<DynamicHttpProxyConfiguration> configure)
    {
        var serviceTypes = assembly.GetTypes().Where(IsSubscribeToHttpProxy);

        foreach (var type in serviceTypes)
        {
            builder.AddDynamicHttpProxy(type, configure);
        }

        return builder;

        static bool IsSubscribeToHttpProxy(Type type)
            => type.IsInterface
            && type.IsPublic
            && !type.IsGenericType
            && typeof(IRemotingService).IsAssignableFrom(type);
    }

    public static FocusOnBuilder AddDynamicHttpProxy<TService>(this FocusOnBuilder builder, Action<DynamicHttpProxyConfiguration> configure) where TService : class
    {
        var type = typeof(TService);

        Type interceptorType = AddCommonConfiguration(builder, type, configure);

        builder.Services.AddScoped(provider =>
        {
            return Generator.CreateInterfaceProxyWithoutTarget<TService>(((IAsyncInterceptor)provider.GetRequiredService(interceptorType)).ToInterceptor());
        });
        return builder;
    }

    public static FocusOnBuilder AddDynamicHttpProxy(this FocusOnBuilder builder, Type type, Action<DynamicHttpProxyConfiguration> configure)
    {
        Type interceptorType = AddCommonConfiguration(builder, type, configure);

        builder.Services.AddScoped(provider =>
        {
            return Generator.CreateInterfaceProxyWithoutTarget(type, (IInterceptor)provider.GetRequiredService(interceptorType));
        });
        return builder;
    }

    private static Type AddCommonConfiguration(FocusOnBuilder builder, Type type, Action<DynamicHttpProxyConfiguration> configure)
    {
        var configuration = new DynamicHttpProxyConfiguration
        {
            Name = DynamicHttpProxyConfiguration.Default
        };

        configure(configuration);

        builder.Services.Configure<DynamicHttpProxyOptions>(options =>
        {
            options.HttpProxies[type] = configuration;
        });

        builder.Services.AddHttpClient();

        builder.Services.AddTransient(typeof(DynamicHttpClientProxy<>).MakeGenericType(type));
        var interceptorType = typeof(DynamicHttpInterceptor<>).MakeGenericType(type);
        builder.Services.AddTransient(interceptorType);
        return interceptorType;
    }
}
