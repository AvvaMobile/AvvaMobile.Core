using AvvaMobile.Core.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace AvvaMobile.Core.Caching
{
    public class CacheManager : ICacheManager
    {
        private IMemoryCache _cache;

        public CacheManager(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public string Get(string key)
        {
            return _cache.Get(key).ToStringData();
        }

        public void SetHours(string key, object data, int hours)
        {
            SetMinutes(key, data, hours * 60);
        }

        public void SetMinutes(string key, object data, int minutes)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(minutes));
            _cache.Set(key, data, cacheEntryOptions);
        }

        public void SetNeverRemove(string key, object data)
        {
            var options = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove);
            _cache.Set(key, data, options);
        }
    }

    public interface ICacheManager
    {
        public T Get<T>(string key);
        public void SetHours(string key, object data, int hours);
        public void SetMinutes(string key, object data, int minutes);
        public void SetNeverRemove(string key, object data);
    }

    public class CacheManagerKeys
    {
        public const string Common_MinPasswordLength = "Common_MinPasswordLength";
        public const string Common_AppName = "Common_AppName";
        public const string Common_CMSAppUrl = "Common_CMSAppUrl";

        public const string CDN_FTP_Url = "CDN_FTP_Url";
        public const string CDN_FTP_UseSSL = "CDN_FTP_UseSSL";
        public const string CDN_FTP_Username = "CDN_FTP_Username";
        public const string CDN_FTP_Password = "CDN_FTP_Password";
        public const string CDN_FTP_Port = "CDN_FTP_Port";

        public const string SMTP_Url = "SMTP_Url";
        public const string SMTP_UseSSL = "SMTP_UseSSL";
        public const string SMTP_Sender = "SMTP_Sender";
        public const string SMTP_Username = "SMTP_Username";
        public const string SMTP_Password = "SMTP_Password";
        public const string SMTP_Port = "SMTP_Port";

        public const string OneSignal_APIKey = "OneSignal_APIKey";
        public const string OneSignal_AppID = "OneSignal_AppID";

        public const string SMS_Url = "SMS_Url";
        public const string SMS_Username = "SMS_Username";
        public const string SMS_Password = "SMS_Password";

        public const string CDN_BaseUrl = "CDN_BaseUrl";
        public const string CDN_UsersImageFolder = "CDN_UsersImageFolder";
        public const string CDN_CustomersImageFolder = "CDN_CustomersImageFolder";
    }
}