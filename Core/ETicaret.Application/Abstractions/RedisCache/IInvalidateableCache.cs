namespace ETicaret.Application.Abstractions.RedisCache
{
    public interface IInvalidateableCache
    {
        public string InvalidateCacheKeyPrefix { get; }
    }
}
