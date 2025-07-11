using WebCozyShop.Repositories;
using WebCozyShop.Repositories.Interface;
using WebCozyShop.Services;
using WebCozyShop.Services.Interface;

namespace WebCozyShop.Infrastructure
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register repositories
            services.AddScoped<IUserRepository, UserRepository>();

            // Register services
            services.AddScoped<IAuthenService, AuthenService>();

            return services;
        }
    }
}
