
using System.Reflection;

using Microsoft.AspNetCore.Mvc;

using MiniSolution;
using MiniSolution.Endpoints.HttpApi.Conventions;

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

    public static MiniSolutionBuilder AddAutoHttpApi(this MiniSolutionBuilder builder,params Assembly[] assemblies)
    {
        var mvcBuilder=builder.Services.AddMvcCore().ConfigureApplicationPartManager(applicationPart =>
        {
            applicationPart.FeatureProviders.Add(new DynamicHttpApiControllerFeatureProvider());
        })
            ;
        mvcBuilder.Services.Configure<MvcOptions>(options =>
        {

            options.Conventions.Add(new DynamicHttpApiConvention());
        });
        return builder;
    }
}
