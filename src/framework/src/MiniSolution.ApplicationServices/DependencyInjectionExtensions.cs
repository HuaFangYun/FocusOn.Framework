
using Microsoft.Extensions.DependencyInjection;

using MiniSolution.ApplicationContracts;

namespace MiniSolution.ApplicationServices;

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
