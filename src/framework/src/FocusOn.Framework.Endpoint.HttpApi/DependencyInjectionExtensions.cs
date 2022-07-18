using FocusOn;
using Swashbuckle.AspNetCore.SwaggerGen;
using FocusOn.Framework.Endpoint.HttpApi.Identity;

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

    /// <summary>
    /// 向<see cref="FocusOnBuilder"/> 添加契约与业务的服务。
    /// </summary>
    /// <typeparam name = "TContractService" > 契约类型。</typeparam>
    /// <typeparam name = "TBusinessService" > 业务服务类型。</typeparam>
    /// <param name = "builder" ><see cref="FocusOnBuilder"/> 实例。</param>
    public static FocusOnBuilder AddBusinessService<TContractService, TBusinessService>(this FocusOnBuilder builder)
        where TContractService : class
        where TBusinessService : class, TContractService
    {
        builder.Services.AddScoped<TContractService, TBusinessService>();
        return builder;
    }

    /// <summary>
    /// 添加 Identity 模块。
    /// </summary>
    /// <param name = "builder" >< see cref="FocusOnBuilder"/> 实例。</param>
    public static IdentityFocusOnBuilder AddIdentity(this FocusOnBuilder builder)
        => new(builder);
    ///// <summary>
    ///// 添加可自动识别实现 <see cref="FocusOn.Framework.Business.Contract.IRemotingService"/> 的类型作为 HTTP API 。
    ///// </summary>
    ///// <param name="builder"><see cref="FocusOnBuilder"/> 实例。</param>
    //public static FocusOnBuilder AddRemotingServiceHttpApi(this FocusOnBuilder builder)
    //{
    //    var mvcBuilder = builder.Services.AddMvcCore().ConfigureApplicationPartManager(applicationPart =>
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
