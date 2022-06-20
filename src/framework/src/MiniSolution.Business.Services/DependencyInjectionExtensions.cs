
using Microsoft.Extensions.DependencyInjection;

using MiniSolution.Business.Contracts;

namespace MiniSolution.Business.Services;

public static class DependencyInjectionExtensions
{
    public static MiniSolutionBuilder AddApplicationService<TService, TImplementation>(this MiniSolutionBuilder builder)
        where TService : class, IApplicationSerivce
        where TImplementation : class, TService
    {
        builder.Services.AddScoped<TService, TImplementation>();
        return builder;
    }
}
