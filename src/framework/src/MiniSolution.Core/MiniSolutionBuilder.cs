using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace MiniSolution;

public class MiniSolutionBuilder
{
    internal MiniSolutionBuilder(IServiceCollection services)
    {
        Services = services;
    }
    public IServiceCollection Services { get; }

    public MiniSolutionBuilder AddAutoMapper(params Assembly[] assemblies)
    {
        Services.AddAutoMapper(assemblies);
        return this;
    }
}
