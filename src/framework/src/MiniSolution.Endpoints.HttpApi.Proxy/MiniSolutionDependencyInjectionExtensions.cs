
using MiniSolution;
using MiniSolution.Business.Contracts;
using MiniSolution.Endpoints.HttpApi.Proxy;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Mini Solution 扩展。
/// </summary>
public static class MiniSolutionDependencyInjectionExtensions
{
    /// <summary>
    /// 添加 HTTP 客户端代理的服务。
    /// </summary>
    /// <typeparam name="TService">业务服务类型。</typeparam>
    /// <typeparam name="TClientProxy">HTTP 代理类型。</typeparam>
    /// <param name="builder"><see cref="MiniSolutionBuilder"/> 实例。</param>
    public static MiniSolutionBuilder AddHttpClientProxy<TService, TClientProxy>(this MiniSolutionBuilder builder)
        where TService : class, IBusinessSerivce
        where TClientProxy : class, IHttpApiClientProxy, TService
    {
        builder.Services.AddScoped<TService, TClientProxy>();
        return builder;
    }
}
