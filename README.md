# AvvaMobile.Core
.Net ile geliştrilen tüm backend projelerimizde kullanılmak üzere hazırlanan sınıfları içerir.

# SMS Sender
SMS gönderimi için kullanılır. En popüler SMS gönderim şirketlerinin entegrasyonlarını içerir.

**Kullanım Şekli**
İlk olarak gönderim için kullanılan bilgiler appsettings.json içerisinde aşağıdaki bilgiler ile yazılmalıdır.

    "SMSSender": {
	    "Username": "5312345678",
	    "Password": "OrnekParola",
	    "Sender": "SenderNameGoesHere"
    }

Daha sonra Startup.cs içerisindeki ConfigureServices içerisinde Dependency Injection kurulumu yapılmalıdır.

    services.AddScoped<ISMSSender, IletiMerkezi>();

 Daha sonra kullanılacak yerde instance erişimi sağlanır.

    private readonly ISMSSender _smsSender;
    public BusinessClass (ISMSSender smsSender)
    {
	    _smsSender= smsSender;
    }
Son olarak gönderim işlemi yapılır.

    var smsSentResult= _smsService.Send("5312345678", "Bu bir deneme mesajıdır.");

Kullanılabilir SMS gönderim şirketleri:
| Firma | Web Site | Class Adı |
|--|--|
| İleti Merkezi | https://www.iletimerkezi.com | IletiMerkezi |