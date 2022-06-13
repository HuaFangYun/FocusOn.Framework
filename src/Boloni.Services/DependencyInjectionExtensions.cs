
using Boloni.Services.Users;

using Microsoft.Extensions.DependencyInjection;

namespace Boloni.Services
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyInjectionExtensions).Assembly);
            services.AddTransient<UserAppService>().AddUserPasswordHasher<DefaultPasswordHasher>();
            return services;
        }

        public static IServiceCollection AddUserPasswordHasher<TPasswordHasher>(this IServiceCollection services) where TPasswordHasher :class, IPasswordHasher
            => services.AddSingleton<IPasswordHasher, TPasswordHasher>();
    }
}
