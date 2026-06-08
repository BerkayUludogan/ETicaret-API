namespace ETicaret.Application.Consts
{
    internal sealed class CacheKeySettings
    {
        internal string Key { get; init; }
        internal double Time { get; init; }
        internal CacheKeySettings(string key, double time)
        {
            Key = key;
            Time = time;
        }
    }
    internal static class CacheKeys
    {
        internal static readonly CacheKeySettings AllUsers = new("get_all_users", 5);
    }
}
