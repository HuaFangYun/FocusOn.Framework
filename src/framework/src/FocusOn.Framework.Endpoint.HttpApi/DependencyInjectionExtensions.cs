
using Microsoft.AspNetCore.Mvc;

using FocusOn;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Mini Solution 的扩展。
/// </summary>
public static class FocusOnDependencyInjectionExtensions
{
    /// <summary>
    /// 添加 Swagger 功能服务。
    /// </summary>
    /// <param name="builder"><see cref="FocusOnBuilder"/> 实例。</param>
    /// <param name="configure">Swagger 配置。</param>
    /// <returns></returns>
    public static FocusOnBuilder AddSwagger(this FocusOnBuilder builder, Action<SwaggerGenOptions> configure = default)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(configure);

        return builder;
    }

    ///// <summary>
    ///// 添加可自动识别实现 <see cref="FocusOn.Framework.Business.Contract.IRemotingService"/> 的类型作为 HTTP API 。
    ///// </summary>
    ///// <param name="builder"><see cref="FocusOnBuilder"/> 实例。</param>
    //public static FocusOnBuilder AddRemotingServiceHttpApi(this FocusOnBuilder builder)
    //{
    //    var mvcBuilder=builder.Services.AddMvc().ConfigureApplicationPartManager(applicationPart =>
    //    {
    //        applicationPart.FeatureProviders.Add(new DynamicHttpApiControllerFeatureProvider());
    //    })
    //        ;
    //    mvcBuilder.Services.Configure<MvcOptions>(options =>
    //    {
    //        options.Conventions.Add(new DynamicHttpApiConvention());
    //    });
    //    return builder;
    //}
}
