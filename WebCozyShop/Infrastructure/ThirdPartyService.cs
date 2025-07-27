using WebCozyShop.Services;

namespace WebCozyShop.Infrastructure
{
    public static class ThirdPartyService
    {
        public static IServiceCollection AddThirdPartyIntegrations(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));

            services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));

            return services;
        }
    }
}
