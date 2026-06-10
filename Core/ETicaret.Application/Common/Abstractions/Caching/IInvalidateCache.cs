namespace ETicaret.Application.Common.Abstractions.Caching
{
    public interface IInvalidateCache
    {
        public string InvalidateCacheKeyPrefix { get; }
    }
}
