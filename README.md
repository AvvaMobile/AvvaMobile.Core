# AvvaMobile.Core
This package includes most of common and needed core classes and ready use for .Net projects.

# SMS Sender
Helps to send SMS to users with already integrated companies.

**How To**
First, you will need to setup account information in the appsettings.json file.

    "SMSSender": {
	    "Username": "5312345678",
	    "Password": "SampePassword",
	    "Sender": "SenderNameGoesHere"
    }

Then you need to add to DI service with instance type.

    services.AddScoped<ISMSSender, IletiMerkezi>();

It's ready now and you can start using in contructor.

    private readonly ISMSSender _smsSender;
    public BusinessClass (ISMSSender smsSender)
    {
	    _smsSender = smsSender;
    }

Finally send the SMS.

    var smsSentResult = _smsService.Send("5312345678", "This is a test message.");

## Integration Ready Companies

Company | Web Site | Type
--- | --- | --- 
İleti Merkezi | https://www.iletimerkezi.com | IletiMerkezi