
using MiniSolution;
using MiniSolution.Business.Contracts;
using MiniSolution.Endpoints.HttpApi.Proxy;

namespace Microsoft.Extensions.DependencyInjection;

public static class MiniSolutionDependencyInjectionExtensions
{
    public static MiniSolutionBuilder AddHttpClientProxy<TService, TClientProxy>(this MiniSolutionBuilder builder)
        where TService : class, IApplicationSerivce
        where TClientProxy : class, IHttpApiClientProxy, TService
    {
        builder.Services.AddScoped<TService, TClientProxy>();
        return builder;
    }
}
