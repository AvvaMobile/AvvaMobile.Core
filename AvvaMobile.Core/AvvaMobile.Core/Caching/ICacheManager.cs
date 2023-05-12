namespace AvvaMobile.Core.Caching;

public interface ICacheManager
{
    public T Get<T>(string key);
    
    public string Get(string key);

    public void Set(string key, object value);
    
    public void Set(string key, object value, TimeSpan expiry);
    
    
    
    [Obsolete("This method is obsolete. Use Set method with timespan parameters instead.")]
    public void SetHours(string key, object data, int hours);
    
    [Obsolete("This method is obsolete. Use Set method with timespan parameters instead.")]
    public void SetMinutes(string key, object data, int minutes);
    
    [Obsolete("This method is obsolete. Use Set method without timespan parameters instead.")]
    public void SetNeverRemove(string key, object data);
}