namespace Microsoft.AspNetCore.Builder;
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// 使用 FocusOn 快速集成的中间件。
    /// <list type="bullet">
    /// <item><c>UseOpenApi</c></item>
    /// <item><c>UseSwaggerUi3</c></item>
    /// <item>UseCors</item>
    /// </list>
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseFocusOn(this IApplicationBuilder app)
    {
        app
            .UseOpenApi()
            .UseSwaggerUi3()
            .UseCors()
            ;


        return app;
    }
}
