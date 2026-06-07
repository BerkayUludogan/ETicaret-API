using ETicaret.Application.Abstractions.Token;
using ETicaret.Infrastructor.Services.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaret.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.Configure<TokenSettings>(configuration.GetSection("JWT"));

        }
    }
}
