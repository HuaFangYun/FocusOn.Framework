
using Microsoft.Extensions.DependencyInjection;

using MiniSolution;
using MiniSolution.Business.Contracts;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// <see cref="MiniSolutionBuilder"/> 的扩展。
/// </summary>
public static class BusinessServiceDependencyInjectionExtensions
{
    /// <summary>
    /// 向 <see cref="IServiceCollection"/> 添加业务服务的注入并使用 Scoped 生命周期模式。
    /// </summary>
    /// <typeparam name="TService">服务类型。</typeparam>
    /// <typeparam name="TImplementation">服务的实现类型。</typeparam>
    /// <param name="builder"><see cref="MiniSolutionBuilder"/> 实现。</param>
    public static MiniSolutionBuilder AddBusinessService<TService, TImplementation>(this MiniSolutionBuilder builder)
        where TService : class, IBusinessSerivce
        where TImplementation : class, TService
    {
        builder.Services.AddScoped<TService, TImplementation>();
        return builder;
    }
}
