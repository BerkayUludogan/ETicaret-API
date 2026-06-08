using ETicaret.Application.Abstractions.RedisCache;
using ETicaret.Redis.Models;
using ETicaret.Redis.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ETicaret.Redis.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _database;
        private readonly RedisCacheSettings _settings;

        public RedisCacheService(IOptions<RedisCacheSettings> settings)
        {
            _settings = settings.Value;
            var opt = ConfigurationOptions.Parse(_settings.ConnectionString);
            _connectionMultiplexer = ConnectionMultiplexer.Connect(opt);
            _database = _connectionMultiplexer.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);

            if (!value.HasValue)
                return default;

            var cachedData = JsonConvert.DeserializeObject<BaseRedisModel<T>>(value!);
            return cachedData != null ? cachedData.Value : default!;
        }

        public async Task SetAsync<T>(string key, T value, DateTime expirationTime)
        {
            BaseRedisModel<T> cacheModel = new()
            {
                Id = key,
                Value = value,
                Time = expirationTime - DateTime.Now
            };
            await _database.StringSetAsync(cacheModel.Id, JsonConvert.SerializeObject(cacheModel), cacheModel.Time);
        }
        public async Task RemoveAsync(string key)
            => await _database.KeyDeleteAsync(key);

        public async Task RemoveByPrefixAsync(string prefix)
        {
            var endpoints = _connectionMultiplexer.GetEndPoints();
            foreach (var endpoint in endpoints)
            {
                var server = _connectionMultiplexer.GetServer(endpoint);
                var keys = server.Keys(pattern: $"{prefix}*").ToArray();
                foreach (var key in keys)
                    await _database.KeyDeleteAsync(key);
            }
        }
    }
}
