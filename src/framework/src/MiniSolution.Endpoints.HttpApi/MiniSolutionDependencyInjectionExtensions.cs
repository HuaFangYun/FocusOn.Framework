
using MiniSolution;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection;

public static class MiniSolutionDependencyInjectionExtensions
{
    public static MiniSolutionBuilder AddSwagger(this MiniSolutionBuilder builder, Action<SwaggerGenOptions> configure = default)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(configure);

        return builder;
    }
}
