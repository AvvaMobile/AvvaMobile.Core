using AvvaMobile.Core.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;

namespace AvvaMobile.Core.Caching;

public class MemoryCacheManager : ICacheManager
{
    private readonly IMemoryCache _cache;

    public MemoryCacheManager(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task<bool> IsExists(string key)
    {
        return Task.FromResult(_cache.TryGetValue(key, out _));
    }

    public Task<T> Get<T>(string key)
    {
        return Task.FromResult(_cache.Get<T>(key));
    }

    public Task<string> Get(string key)
    {
        return Task.FromResult(_cache.Get(key).ToStringData());
    }

    public Task<bool> Set(string key, object value)
    {
        _cache.Set(key, value);
        return Task.FromResult(true);
    }

    public Task<bool> Set(string key, object value, TimeSpan expiry)
    {
        _cache.Set(key, value, expiry);
        return Task.FromResult(true);
    }

    public Task<List<SelectListItem>> Get_SelectListItems(string key)
    {
        return Task.FromResult(_cache.Get<List<SelectListItem>>(key));
    }

    public Task<bool> Set_SelectListItems(string key, List<SelectListItem> value)
    {
        _cache.Set(key, value);
        return Task.FromResult(true);
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