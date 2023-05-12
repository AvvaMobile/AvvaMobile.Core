namespace AvvaMobile.Core.Caching;

public abstract class AppSettingsKeys
{
    private readonly ICacheManager _cacheManager;

    protected AppSettingsKeys(ICacheManager cacheManager)
    {
        _cacheManager = cacheManager;
    }

    public string Common_AppName => _cacheManager.Get("Common_AppName");
    public string Common_CMSAppUrl => _cacheManager.Get("Common_CMSAppUrl");
    public string Common_CookieName => _cacheManager.Get("Common_CookieName");
    public string Common_NoImageUrl => _cacheManager.Get("Common_NoImageUrl");

    public string CDN_FTP_Url => _cacheManager.Get("CDN_FTP_Url");
    public string CDN_FTP_UseSSL => _cacheManager.Get("CDN_FTP_UseSSL");
    public string CDN_FTP_Username => _cacheManager.Get("CDN_FTP_Username");
    public string CDN_FTP_Password => _cacheManager.Get("CDN_FTP_Password");
    public string CDN_FTP_Port => _cacheManager.Get("CDN_FTP_Port");

    public string SMTP_Url => _cacheManager.Get("SMTP_Url");
    public string SMTP_UseSSL => _cacheManager.Get("SMTP_UseSSL");
    public string SMTP_Sender => _cacheManager.Get("SMTP_Sender");
    public string SMTP_SenderDisplayName => _cacheManager.Get("SMTP_SenderDisplayName");
    public string SMTP_Username => _cacheManager.Get("SMTP_Username");
    public string SMTP_Password => _cacheManager.Get("SMTP_Password");
    public string SMTP_Port => _cacheManager.Get("SMTP_Port");

    public string OneSignal_APIKey => _cacheManager.Get("OneSignal_APIKey");
    public string OneSignal_AppID => _cacheManager.Get("OneSignal_AppID");
    

    public string SMS_Url => _cacheManager.Get("SMS_Url");
    public string SMS_Username => _cacheManager.Get("SMS_Username");
    public string SMS_Password => _cacheManager.Get("SMS_Password");
    public string SMS_Sender => _cacheManager.Get("SMS_Sender");
    

    public string CDN_BaseUrl => _cacheManager.Get("CDN_BaseUrl");
    public string CDN_UsersImageFolder => _cacheManager.Get("CDN_UsersImageFolder");
    public string CDN_CustomersImageFolder => _cacheManager.Get("CDN_CustomersImageFolder");

    public string AwsAccessKeyID => _cacheManager.Get("AwsAccessKeyID");
    public string AwsSecretAccessKey => _cacheManager.Get("AwsSecretAccessKey");

    public string S3CDNBaseUrl => _cacheManager.Get("S3CDNBaseUrl");
    

    public string Parasut_ProductID => _cacheManager.Get("Parasut_ProductID");

    public string Parasut_CompanyID => _cacheManager.Get("Parasut_CompanyID");

    public string Parasut_Username => _cacheManager.Get("Parasut_Username");

    public string Parasut_Password => _cacheManager.Get("Parasut_Password");

    public string Parasut_ClientID => _cacheManager.Get("Parasut_ClientID");

    public string Parasut_ClientSecret => _cacheManager.Get("Parasut_ClientSecret");

    public string Parasut_AccountID => _cacheManager.Get("Parasut_AccountID");
}