
using Microsoft.Extensions.DependencyInjection;

namespace MiniSolution.Services
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyInjectionExtensions).Assembly);
            return services;
        }
    }
}
