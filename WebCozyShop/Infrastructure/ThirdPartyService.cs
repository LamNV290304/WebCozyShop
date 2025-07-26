using WebCozyShop.Services;

namespace WebCozyShop.Infrastructure
{
    public static class ThirdPartyService
    {
        public static IServiceCollection AddThirdPartyIntegrations(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // 1. Bind options
            services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));
            return services;
        }
    }
}
