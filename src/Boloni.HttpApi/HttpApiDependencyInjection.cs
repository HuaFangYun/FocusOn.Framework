using Boloni.Data;
using Boloni.HttpApi.Users;

using Microsoft.EntityFrameworkCore;

namespace Boloni.HttpApi;

public static class HttpApiDependencyInjection
{
    public static IServiceCollection AddHttpApi(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddAuthorization();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAutoMapper(typeof(Program).Assembly);

        services.AddDbContext<BoloniDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });

        services.AddUser();
        return services;
    }
}
