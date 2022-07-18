using FocusOn;
using Swashbuckle.AspNetCore.SwaggerGen;
using FocusOn.Framework.Business.Services.Mvc;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// <see cref="FocusOnBuilder"/> 的扩展。
/// </summary>
public static class BusinessServiceDependencyInjectionExtensions
{
    /// <summary>
    /// 向 <see cref="FocusOnBuilder"/> 添加契约与业务的服务。
    /// </summary>
    /// <typeparam name="TContractService">契约类型。</typeparam>
    /// <typeparam name="TBusinessService">业务服务类型。</typeparam>
    /// <param name="builder"><see cref="FocusOnBuilder"/> 实例。</param>
    public static FocusOnBuilder AddBusinessService<TContractService, TBusinessService>(this FocusOnBuilder builder)
        where TContractService : class
        where TBusinessService : class, TContractService
    {
        builder.Services.AddScoped<TContractService, TBusinessService>();
        return builder;
    }

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
        builder.Services.AddMvcCore(options => options.Conventions.Add(new DynamicHttpApiConvention()))
            .ConfigureApplicationPartManager(applicationPart => applicationPart.FeatureProviders.Add(new DynamicHttpApiControllerFeatureProvider())
            )
            ;
        return builder;
    }
}
