using ETicaret.Application.Abstractions.RedisCache;
using ETicaret.Redis.Services;
using ETicaret.Redis.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaret.Redis
{
    public static class ServiceRegistration
    {
        public static void AddRedisServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RedisCacheSettings>(configuration.GetSection("RedisCacheSettings"));

            services.AddTransient<IRedisCacheService, RedisCacheService>();

            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = configuration["RedisCacheSettings:ConnectionString"];
                opt.InstanceName = configuration["RedisCacheSettings:InstanceName"];
            });
        }
    }
}