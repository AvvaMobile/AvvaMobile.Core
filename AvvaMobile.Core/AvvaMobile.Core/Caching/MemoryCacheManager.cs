using AvvaMobile.Core.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace AvvaMobile.Core.Caching;

public class MemoryCacheManager : ICacheManager
{
    private readonly IMemoryCache _cache;

    public MemoryCacheManager(IMemoryCache cache)
    {
        _cache = cache;
    }

    public T Get<T>(string key)
    {
        return _cache.Get<T>(key);
    }

    public string Get(string key)
    {
        return _cache.Get(key).ToStringData();
    }
    
    public void Set(string key, object value)
    {
        _cache.Set(key, value);
    }
    
    public void Set(string key, object value, TimeSpan expiry)
    {
        _cache.Set(key, value, expiry);
    }
    

    [Obsolete("This method is obsolete. Use Set method with timespan parameters instead.")]
    public void SetHours(string key, object data, int hours)
    {
        SetMinutes(key, data, hours * 60);
    }

    [Obsolete("This method is obsolete. Use Set method with timespan parameters instead.")]
    public void SetMinutes(string key, object data, int minutes)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(minutes));
        _cache.Set(key, data, cacheEntryOptions);
    }

    [Obsolete("This method is obsolete. Use Set method without timespan parameters instead.")]
    public void SetNeverRemove(string key, object data)
    {
        var options = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove);
        _cache.Set(key, data, options);
    }
}