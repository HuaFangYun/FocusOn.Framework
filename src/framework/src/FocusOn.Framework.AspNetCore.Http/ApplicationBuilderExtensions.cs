namespace Microsoft.AspNetCore.Builder;
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// 使用 FocusOn 快速集成的中间件。
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseFocusOn(this IApplicationBuilder app)
    {
        app.UseStaticFiles();

        app
            .UseOpenApi()
            .UseSwaggerUi3()
            .UseCors()
            ;


        return app;
    }
}
