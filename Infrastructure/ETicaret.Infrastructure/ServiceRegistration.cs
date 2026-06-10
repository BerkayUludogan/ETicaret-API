using ETicaret.Application.Common.Abstractions.Caching;
using ETicaret.Application.Shared.Abstractions.Token;
using ETicaret.Infrastructor.Services.Token;
using ETicaret.Infrastructure.Cache.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaret.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>(); 
            services.AddMemoryCache();
            services.AddScoped<ICacheService, MemoryCacheService>();


            services.Configure<TokenSettings>(configuration.GetSection("JWT"));
        }
    }
}
