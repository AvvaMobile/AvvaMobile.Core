﻿namespace AvvaMobile.Core.Caching;

public class AppSettingsKeys
{
    private readonly ICacheManager _cacheManager;

    public AppSettingsKeys(ICacheManager cacheManager)
    {
        _cacheManager = cacheManager;
    }

    public string Common_AppName => _cacheManager.Get("Common_AppName").Result;
    public string Common_CMSAppUrl => _cacheManager.Get("Common_CMSAppUrl").Result;
    public string Common_ClientName => _cacheManager.Get("Common_ClientName").Result;
    public string Common_ClientWebSite => _cacheManager.Get("Common_ClientWebSite").Result;

    public string CDN_FTP_Url => _cacheManager.Get("CDN_FTP_Url").Result;
    public string CDN_FTP_UseSSL => _cacheManager.Get("CDN_FTP_UseSSL").Result;
    public string CDN_FTP_Username => _cacheManager.Get("CDN_FTP_Username").Result;
    public string CDN_FTP_Password => _cacheManager.Get("CDN_FTP_Password").Result;
    public string CDN_FTP_Port => _cacheManager.Get("CDN_FTP_Port").Result;

    public string SMTP_Url => _cacheManager.Get("SMTP_Url").Result;
    public string SMTP_UseSSL => _cacheManager.Get("SMTP_UseSSL").Result;
    public string SMTP_Sender => _cacheManager.Get("SMTP_Sender").Result;
    public string SMTP_SenderDisplayName => _cacheManager.Get("SMTP_SenderDisplayName").Result;
    public string SMTP_Username => _cacheManager.Get("SMTP_Username").Result;
    public string SMTP_Password => _cacheManager.Get("SMTP_Password").Result;
    public string SMTP_Port => _cacheManager.Get("SMTP_Port").Result;

    public string OneSignal_APIKey => _cacheManager.Get("OneSignal_APIKey").Result;
    public string OneSignal_AppID => _cacheManager.Get("OneSignal_AppID").Result;
    
    public string SMS_Url => _cacheManager.Get("SMS_Url").Result;
    public string SMS_Username => _cacheManager.Get("SMS_Username").Result;
    public string SMS_Password => _cacheManager.Get("SMS_Password").Result;
    public string SMS_Sender => _cacheManager.Get("SMS_Sender").Result;

    public string CDN_BaseUrl => _cacheManager.Get("CDN_BaseUrl").Result;
    public string CDN_UsersImageFolder => _cacheManager.Get("CDN_UsersImageFolder").Result;
    public string CDN_CustomersImageFolder => _cacheManager.Get("CDN_CustomersImageFolder").Result;

    public string AwsAccessKeyID => _cacheManager.Get("AwsAccessKeyID").Result;
    public string AwsSecretAccessKey => _cacheManager.Get("AwsSecretAccessKey").Result;
    public string S3CDNBaseUrl => _cacheManager.Get("S3CDNBaseUrl").Result;
    
    public string Parasut_ProductID => _cacheManager.Get("Parasut_ProductID").Result;
    public string Parasut_CompanyID => _cacheManager.Get("Parasut_CompanyID").Result;
    public string Parasut_Username => _cacheManager.Get("Parasut_Username").Result;
    public string Parasut_Password => _cacheManager.Get("Parasut_Password").Result;
    public string Parasut_ClientID => _cacheManager.Get("Parasut_ClientID").Result;
    public string Parasut_ClientSecret => _cacheManager.Get("Parasut_ClientSecret").Result;
    public string Parasut_AccountID => _cacheManager.Get("Parasut_AccountID").Result;

    public string ElasticIndex => _cacheManager.Get("ElasticIndex").Result;
    public string ElasticPassword => _cacheManager.Get("ElasticPassword").Result;
    public string ElasticUrl => _cacheManager.Get("ElasticUrl").Result;
    public string ElasticUserName => _cacheManager.Get("ElasticUserName").Result;
    
    public string MobileApp_GooglePlayUrl => _cacheManager.Get("MobileApp_GooglePlayUrl").Result;
    public string MobileApp_AppStoreUrl => _cacheManager.Get("MobileApp_AppStoreUrl").Result;
    public string MobileApp_AppGalleryUrl => _cacheManager.Get("MobileApp_AppGalleryUrl").Result;
    public string MobileApp_iPhoneVersion => _cacheManager.Get("MobileApp_iPhoneVersion").Result;
    public string MobileApp_iPadVersion => _cacheManager.Get("MobileApp_iPadVersion").Result;
    public string MobileApp_VersionVersion => _cacheManager.Get("MobileApp_VersionVersion").Result;
    public string MobileApp_HuaweiVersion => _cacheManager.Get("MobileApp_HuaweiVersion").Result;
    
    public string Social_Facebook => _cacheManager.Get("Social_Facebook").Result;
    public string Social_Instagram => _cacheManager.Get("Social_Instagram").Result;
    public string Social_Twitter => _cacheManager.Get("Social_Twitter").Result;
    public string Social_Linkedin => _cacheManager.Get("Social_Linkedin").Result;
    public string Social_Youtube => _cacheManager.Get("Social_Youtube").Result;
    public string Social_TikTok => _cacheManager.Get("Social_TikTok").Result;
}