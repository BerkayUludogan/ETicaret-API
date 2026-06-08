using ETicaret.Application.Abstractions.RedisCache;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis; 

namespace ETicaret.Infrastructure.RedisCache
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private readonly IDatabase _database;
        private readonly RedisCacheSettings settings;

        public RedisCacheService(IOptions<RedisCacheSettings> options)
        {
            settings = options.Value;
            var opt = ConfigurationOptions.Parse(settings.ConnectionString);
            _redisConnection = ConnectionMultiplexer.Connect(opt);
            _database = _redisConnection.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (!value.HasValue) 
             return default;
            return JsonConvert.DeserializeObject<T>(value.ToString());

        }

        public async Task SetAsync<T>(string key, T value, DateTime? expirationTime = null)
        {
            TimeSpan timeUnitExpiration = expirationTime.Value
                - DateTime.UtcNow;
            await _database.StringSetAsync(key, JsonConvert.SerializeObject(value), timeUnitExpiration);
        }
    }
}
