
using Microsoft.AspNetCore.Mvc;

using MiniSolution;
using MiniSolution.Endpoints.HttpApi.Conventions;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Mini Solution 的扩展。
/// </summary>
public static class MiniSolutionDependencyInjectionExtensions
{
    /// <summary>
    /// 添加 Swagger 功能服务。
    /// </summary>
    /// <param name="builder"><see cref="MiniSolutionBuilder"/> 实例。</param>
    /// <param name="configure">Swagger 配置。</param>
    /// <returns></returns>
    public static MiniSolutionBuilder AddSwagger(this MiniSolutionBuilder builder, Action<SwaggerGenOptions> configure = default)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(configure);

        return builder;
    }

    /// <summary>
    /// 添加可自动识别实现 <see cref="MiniSolution.Business.Contracts.IRemotingService"/> 的类型作为 HTTP API 。
    /// </summary>
    /// <param name="builder"><see cref="MiniSolutionBuilder"/> 实例。</param>
    public static MiniSolutionBuilder AddRemotingServiceHttpApi(this MiniSolutionBuilder builder)
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
