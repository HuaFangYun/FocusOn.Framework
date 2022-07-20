using System;
using FocusOn;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using FocusOn.Framework.Endpoint.HttpProxy.Dynamic;

namespace Microsoft.Extensions.DependencyInjection;
public static class DypendencyInjectionExtensions
{
    public static FocusOnBuilder AddDynamicHttpProxy<TService>(this FocusOnBuilder builder) where TService : class
    {

        builder.Services.AddHttpClient(Microsoft.Extensions.Options.Options.DefaultName);

        builder.Services.Configure<DynamicHttpProxyOptions>(options => { });
        builder.Services.AddScoped<DynamicHttpClientProxy>();
        builder.Services.AddScoped(provider =>
        {
            var proxy = new ProxyGenerator();

            return proxy.CreateInterfaceProxyWithoutTarget<TService>(new DynamicHttpInterceptor<TService>(provider));
        });
        return builder;
    }
}
