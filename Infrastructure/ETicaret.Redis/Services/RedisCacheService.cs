using ETicaret.Application.Common.Abstractions.Caching;
using ETicaret.Redis.Models;
using ETicaret.Redis.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ETicaret.Redis.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _database;
        private readonly RedisCacheSettings _settings;

        public RedisCacheService(IOptions<RedisCacheSettings> settings)
        {
            _settings = settings.Value;

            var options = ConfigurationOptions.Parse(_settings.ConnectionString);
            _connectionMultiplexer = ConnectionMultiplexer.Connect(options);
            _database = _connectionMultiplexer.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(
            string key,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var value = await _database.StringGetAsync(key);

            if (!value.HasValue)
                return default;

            var cachedData = JsonConvert.DeserializeObject<BaseRedisModel<T>>(value!);

            return cachedData is not null
                ? cachedData.Value
                : default;
        }

        public async Task SetAsync<T>(
      string key,
      T value,
      TimeSpan expiration,
      CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var cacheModel = new BaseRedisModel<T>
            {
                Id = key,
                Value = value,
                Time = expiration
            };

            await _database.StringSetAsync(
                cacheModel.Id,
                JsonConvert.SerializeObject(cacheModel),
                cacheModel.Time);
        }

        public async Task RemoveAsync(
            string key,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _database.KeyDeleteAsync(key);
        }

        public async Task RemoveByPrefixAsync(
            string prefix,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var endpoints = _connectionMultiplexer.GetEndPoints();

            foreach (var endpoint in endpoints)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var server = _connectionMultiplexer.GetServer(endpoint);

                var keys = server.Keys(pattern: $"{prefix}*").ToArray();

                foreach (var key in keys)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    await _database.KeyDeleteAsync(key);
                }
            }
        }
    }
}