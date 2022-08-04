using NSwag.AspNetCore;
using FocusOn.Framework.AspNetCore.Http;

namespace Microsoft.AspNetCore.Builder;
/// <summary>
/// 中间件配置。
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// 使用 FocusOn 快速集成的中间件。
    /// <list type="bullet">
    /// <item><c>UseSwaggerUI</c></item>
    /// <item>UseAnyCors</item>
    /// </list>
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseFocusOn(this IApplicationBuilder app)
        => app.UseSwaggerUI().UseAnyCors();

    /// <summary>
    /// 使用结合 AddSwagger 服务的 Swagger UI 界面的中间件。
    /// </summary>
    /// <param name="app"></param>
    /// <param name="settings">Swagger 的设置。</param>
    /// <returns></returns>
    public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app, Action<SwaggerUi3Settings> settings = default)
        => app.UseOpenApi().UseSwaggerUi3(settings);

    /// <summary>
    /// 应用允许任意跨域策略的配置。配合 AddCors 使用。
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseAnyCors(this IApplicationBuilder app) => app.UseCors(HttpUtility.CORS_POLICY_FOR_ANY);
}
