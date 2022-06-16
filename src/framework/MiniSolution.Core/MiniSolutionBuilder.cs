using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace MiniSolution;

public interface IMiniSolutionBuilder
{
    public IServiceCollection Services { get; }
}

internal class MiniSolutionBuilder : IMiniSolutionBuilder
{
    public MiniSolutionBuilder(IServiceCollection services)
    {
        Services = services;
    }
    public IServiceCollection Services { get; }

    public IMiniSolutionBuilder AddAutoMapper(params Assembly[] assemblies)
    {
        Services.AddAutoMapper(assemblies);
        return this;
    }
}
