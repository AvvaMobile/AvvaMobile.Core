﻿using AvvaMobile.Core.Extensions;
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
        public string Get(string key);
        public void SetHours(string key, object data, int hours);
        public void SetMinutes(string key, object data, int minutes);
        public void SetNeverRemove(string key, object data);
    }

    public class AppSettingsKeys : IAppSettingsKeys
    {
        private readonly ICacheManager _cacheManager;

        public AppSettingsKeys(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public string Common_MinPasswordLength { get { return _cacheManager.Get("Common_MinPasswordLength"); } }
        public string Common_AppName { get { return _cacheManager.Get("Common_AppName"); } }
        public string Common_CMSAppUrl { get { return _cacheManager.Get("Common_CMSAppUrl"); } }

        public string CDN_FTP_Url { get { return _cacheManager.Get("CDN_FTP_Url"); } }
        public string CDN_FTP_UseSSL { get { return _cacheManager.Get("CDN_FTP_UseSSL"); } }
        public string CDN_FTP_Username { get { return _cacheManager.Get("CDN_FTP_Username"); } }
        public string CDN_FTP_Password { get { return _cacheManager.Get("CDN_FTP_Password"); } }
        public string CDN_FTP_Port { get { return _cacheManager.Get("CDN_FTP_Port"); } }

        public string SMTP_Url { get { return _cacheManager.Get("SMTP_Url"); } }
        public string SMTP_UseSSL { get { return _cacheManager.Get("SMTP_UseSSL"); } }
        public string SMTP_Sender { get { return _cacheManager.Get("SMTP_Sender"); } }
        public string SMTP_Username { get { return _cacheManager.Get("SMTP_Username"); } }
        public string SMTP_Password { get { return _cacheManager.Get("SMTP_Password"); } }
        public string SMTP_Port { get { return _cacheManager.Get("SMTP_Port"); } }

        public string OneSignal_APIKey { get { return _cacheManager.Get("OneSignal_APIKey"); } }
        public string OneSignal_AppID { get { return _cacheManager.Get("OneSignal_AppID"); } }

        public string SMS_Url { get { return _cacheManager.Get("SMS_Url"); } }
        public string SMS_Username { get { return _cacheManager.Get("SMS_Username"); } }
        public string SMS_Password { get { return _cacheManager.Get("SMS_Password"); } }
        public string SMS_Sender { get { return _cacheManager.Get("SMS_Sender"); } }

        public string CDN_BaseUrl { get { return _cacheManager.Get("CDN_BaseUrl"); } }
        public string CDN_UsersImageFolder { get { return _cacheManager.Get("CDN_UsersImageFolder"); } }
        public string CDN_CustomersImageFolder { get { return _cacheManager.Get("CDN_CustomersImageFolder"); } }
    }
    public interface IAppSettingsKeys
    {
        public string Common_MinPasswordLength { get; }
        public string Common_AppName { get; }
        public string Common_CMSAppUrl { get; }

        public string CDN_FTP_Url { get; }
        public string CDN_FTP_UseSSL { get; }
        public string CDN_FTP_Username { get; }
        public string CDN_FTP_Password { get; }
        public string CDN_FTP_Port { get; }

        public string SMTP_Url { get; }
        public string SMTP_UseSSL { get; }
        public string SMTP_Sender { get; }
        public string SMTP_Username { get; }
        public string SMTP_Password { get; }
        public string SMTP_Port { get; }

        public string OneSignal_APIKey { get; }
        public string OneSignal_AppID { get; }

        public string SMS_Url { get; }
        public string SMS_Username { get; }
        public string SMS_Password { get; }
        public string SMS_Sender { get; }

        public string CDN_BaseUrl { get; }
        public string CDN_UsersImageFolder { get; }
        public string CDN_CustomersImageFolder { get; }
    }
}