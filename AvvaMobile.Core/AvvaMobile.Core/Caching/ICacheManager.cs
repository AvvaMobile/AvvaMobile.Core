using Microsoft.AspNetCore.Mvc.Rendering;

namespace AvvaMobile.Core.Caching;

public interface ICacheManager
{
    public Task<bool> IsExists(string key);
    
    public Task<T> Get<T>(string key);
    
    public Task<string> Get(string key);

    public Task<bool> Set(string key, object value);
    
    public Task<bool> Set(string key, object value, TimeSpan expiry);
    
    public Task<List<SelectListItem>> Get_SelectListItems(string key);
    
    public Task<bool> Set_SelectListItems(string key, List<SelectListItem> value);



    [Obsolete("This method is obsolete. Use Set method with timespan parameters instead.")]
    public void SetHours(string key, object data, int hours);
    
    [Obsolete("This method is obsolete. Use Set method with timespan parameters instead.")]
    public void SetMinutes(string key, object data, int minutes);
    
    [Obsolete("This method is obsolete. Use Set method without timespan parameters instead.")]
    public void SetNeverRemove(string key, object data);
}