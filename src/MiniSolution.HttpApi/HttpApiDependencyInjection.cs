using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace MiniSolution.HttpApi;

public static class HttpApiDependencyInjection
{
    public static IServiceCollection AddHttpApi(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddAuthorization();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
