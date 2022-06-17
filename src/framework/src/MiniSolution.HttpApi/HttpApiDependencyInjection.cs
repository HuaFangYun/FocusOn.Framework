using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace MiniSolution.HttpApi;

public static class HttpApiDependencyInjection
{
    public static MiniSolutionBuilder AddSwagger(this MiniSolutionBuilder builder,Action<SwaggerGenOptions> configure=default)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(configure);

        return builder;
    }
}
