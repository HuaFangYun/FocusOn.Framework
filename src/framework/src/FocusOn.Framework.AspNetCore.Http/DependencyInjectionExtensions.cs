using System.Reflection;
using FocusOn.Framework;
using Microsoft.AspNetCore.Mvc;
using FocusOn.Framework.Security;
using NSwag.Generation.AspNetCore;
using FocusOn.Framework.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using FocusOn.Framework.AspNetCore.Http.Conventions;

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
    public static FocusOnBuilder AddSwagger(this FocusOnBuilder builder, Action<AspNetCoreOpenApiDocumentGeneratorSettings>? configure = default)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApiDocument(configure);

        return builder;
    }

    /// <summary>
    /// 添加动态 WEB API 的功能。只有实例实现了 <see cref="FocusOn.Framework.Business.Contract.IRemotingService"/> 接口才会被视为动态 API。
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="assemblies">要作为动态 WEB API 添加的程序集范围。</param>
    /// <returns></returns>
    public static FocusOnBuilder AddDynamicWebApi(this FocusOnBuilder builder, params Assembly[] assemblies)
    {
        Checker.NotNull(assemblies, nameof(assemblies));

        builder.Services.AddControllers(options =>
        {
            options.Conventions.Add(new DynamicHttpApiConvention());
            options.Filters.Add(new ProducesAttribute("application/json"));
        })

        .ConfigureApplicationPartManager(applicationPart =>
        {
            foreach (var assembly in assemblies)
            {
                applicationPart.ApplicationParts.Add(new AssemblyPart(assembly));
            }
            applicationPart.FeatureProviders.Add(new DynamicHttpApiControllerFeatureProvider());
        })
            ;

        return builder;
    }

    /// <summary>
    /// 添加对 AllowAnyHeader, AllowAnyMethod, AllowAnyOrigin 任意跨域的服务。并在中间件部分调用 <c>UseAnyCors</c> 方法应用该策略。
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static FocusOnBuilder AddAnyCors(this FocusOnBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(HttpUtility.CORS_POLICY_FOR_ANY, cors => cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        });
        return builder;
    }

    /// <summary>
    /// 添加对 <see cref="ICurrentPrincipalAccessor"/> 的服务。
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static FocusOnBuilder AddCurrentPrincipalAccessor(this FocusOnBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ICurrentPrincipalAccessor, HttpContentCurrentUserAccessor>();
        return builder;
    }
}
