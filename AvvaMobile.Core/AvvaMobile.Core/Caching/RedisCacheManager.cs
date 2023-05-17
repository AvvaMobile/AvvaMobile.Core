using AvvaMobile.Core.Redis;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace AvvaMobile.Core.Caching;

public class RedisCacheManager : ICacheManager
{
    private readonly IRedisClient _cache;

    public RedisCacheManager(IRedisClient cache)
    {
        _cache = cache;
    }

    public async Task<bool> IsExists(string key)
    {
        return await _cache.IsExists(key);
    }

    public async Task<T> Get<T>(string key)
    {
        return await _cache.Get_Deserialized<T>(key);
    }

    public async Task<string> Get(string key)
    {
        return await _cache.Get_String(key);
    }

    public async Task<bool> Set(string key, object value)
    {
        return value switch
        {
            string => await _cache.Set(key, value.ToString()),
            int or long or decimal => await _cache.Set(key, value.ToString()),
            _ => await _cache.Set(key, JsonSerializer.Serialize(value))
        };
    }

    public async Task<bool> Set(string key, object value, TimeSpan expiry)
    {
        return await _cache.Set(key, value.ToString(), expiry);
    }

    public async Task<List<SelectListItem>> Get_SelectListItems(string key)
    {
        return await _cache.Get_SelectListItems(key);
    }

    public async Task<bool> Set_SelectListItems(string key, List<SelectListItem> value)
    {
        return await _cache.Set_SelectListItems(key, value);
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