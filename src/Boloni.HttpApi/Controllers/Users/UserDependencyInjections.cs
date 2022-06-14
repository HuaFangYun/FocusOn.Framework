using Boloni.HttpApi.Users;
using Boloni.Services.Users;

namespace Boloni.HttpApi.Users
{
    public static class UserDependencyInjections
    {
        public static IServiceCollection AddUser(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher, DefaultPasswordHasher>();
            services.AddTransient<UserAppService>();
            return services;
        }

    }
}
