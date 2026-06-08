namespace ETicaret.Application.Abstractions.RedisCache
{
    public interface ICacheService
    {
        void Set<T>(string key, T value, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null);
        T? Get<T>(string key);
        void Remove(string key);
    }
}
