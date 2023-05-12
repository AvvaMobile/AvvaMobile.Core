using AvvaMobile.Core.Redis;

namespace AvvaMobile.Core.Caching;

public class RedisCacheManager : ICacheManager
{
    private readonly IRedisClient _cache;

    public RedisCacheManager(IRedisClient cache)
    {
        _cache = cache;
    }

    public T Get<T>(string key)
    {
        return _cache.Get_Deserialized<T>(key).Result;
    }

    public string Get(string key)
    {
        return _cache.Get_String(key).Result;
    }
    
    public void Set(string key, object value)
    {
        _cache.Set(key, value.ToString());
    }
    
    public void Set(string key, object value, TimeSpan expiry)
    {
        _cache.Set(key, value.ToString(), expiry);
    }

    [Obsolete("This method is obsolete. Use Set method with timespan parameters instead.")]
    public void SetHours(string key, object data, int hours)
    {
        throw new Exception("This method is obsolete. Use Set method without timespan parameters instead.");
    }

    [Obsolete("This method is obsolete. Use Set method with timespan parameters instead.")]
    public void SetMinutes(string key, object data, int minutes)
    {
        throw new Exception("This method is obsolete. Use Set method without timespan parameters instead.");
    }

    [Obsolete("This method is obsolete. Use Set method without timespan parameters instead.")]
    public void SetNeverRemove(string key, object data)
    {
        throw new Exception("This method is obsolete. Use Set method without timespan parameters instead.");
    }
}