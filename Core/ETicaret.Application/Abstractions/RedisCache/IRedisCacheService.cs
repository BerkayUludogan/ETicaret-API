namespace ETicaret.Application.Abstractions.RedisCache
{
    public interface IRedisCacheService
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, DateTime expirationTime);
        Task RemoveAsync(string key);
        Task RemoveByPrefixAsync(string prefix);
    }
}
