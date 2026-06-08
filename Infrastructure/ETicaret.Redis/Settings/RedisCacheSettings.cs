namespace ETicaret.Redis.Settings
{
    public class RedisCacheSettings
    {
        public required string ConnectionString { get; set; }
        public required string InstanceName { get; set; }
    }
}
