using Newtonsoft.Json;

namespace ETicaret.Redis.Models
{
    public class BaseRedisModel<T>
    {
        public required string Id { get; set; }
        public required T Value { get; set; }
        public TimeSpan Time { get; set; }
    }
}
