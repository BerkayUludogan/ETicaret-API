using ETicaret.Application.Abstractions.RedisCache;
using ETicaret.Application.Abstractions.Token;
using ETicaret.Infrastructor.Services.Token;
using ETicaret.Infrastructure.Services.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaret.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>(); 
            services.AddScoped<ICacheService, CacheService>();


            services.Configure<TokenSettings>(configuration.GetSection("JWT"));
        }
    }
}
