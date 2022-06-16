
using MiniSolution;

namespace Microsoft.Extensions.DependencyInjection;

public static class MiniSolutionDependencyInjectionExtensions
{
    public static IServiceCollection AddMiniSolution(this IServiceCollection services,Action<IMiniSolutionBuilder> configure)
    {
        configure( new MiniSolutionBuilder(services));
        return services;
    }
}
