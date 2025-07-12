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
            services.AddScoped<ITokenRepository, TokenRepository>();

            // Register services
            services.AddScoped<IAuthenService, AuthenService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
