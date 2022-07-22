using FocusOn;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using FocusOn.Framework.Endpoint.HttpApi.Identity;
using FocusOn.Framework.Endpoint.HttpApi.Conventions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// FocusOn 的扩展。
/// </summary>
public static class FocusOnDependencyInjectionExtensions
{


    /// <summary>
    /// 添加 Swagger 功能服务。
    /// </summary>
    /// <param name = "builder" ><see cref="FocusOnBuilder"/> 实例。</param>
    /// <param name = "configure"> Swagger 配置。</param>
    public static FocusOnBuilder AddSwagger(this FocusOnBuilder builder, Action<SwaggerGenOptions>? configure = default)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(configure);

        return builder;
    }
    public static FocusOnBuilder AddAutoWebApi(this FocusOnBuilder builder)
    {
        builder.Services.AddMvcCore(options =>
        {
            options.Conventions.Add(new DynamicHttpApiConvention());
            options.Filters.Add(new ProducesAttribute("application/json"));
        })
            .ConfigureApplicationPartManager(applicationPart => applicationPart.FeatureProviders.Add(new DynamicHttpApiControllerFeatureProvider())
            )
            ;
        return builder;
    }

}
