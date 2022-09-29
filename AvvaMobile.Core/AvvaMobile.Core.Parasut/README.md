
# Giriş
.Net ile Paraşüt API'ını rahat kullanabilmek için bu paket geliştirilmiştir. Paraşüt tarafından sağlanan tüm özellikler henüz tam implement edilmemiştir. Yeni özellikler eklendikte paket güncellemesi yapılacaktır.

Paraşüt Web API dokümantasyonuna https://apidocs.parasut.com/ adresinden ulaşabilirsiniz.

Bu paket Paraşüt'ün V4 versiyonu için geliştirilmiştir.

# Kaynak Kodları
Paketin kaynak kodları GitHub üzerinden public olarak erişilebilir. 
[AvvaMobile.Core.Parasut](https://github.com/AvvaMobile/AvvaMobile.Core/tree/master/AvvaMobile.Core/AvvaMobile.Core.Parasut)

Ayrıca örnek kullanım için bir Console uygulaması da repoda bulunmaktadır.
[AvvaMobile.Core.Parasut.Sample](https://github.com/AvvaMobile/AvvaMobile.Core/tree/master/AvvaMobile.Core/AvvaMobile.Core.Parasut.Sample)

# Geliştirme Ekibine Katılım
Eğer geliştirme ekibine katılmak ve katkıda bulunmak isterseniz opensource@avvamobile.com adresinden bizimle iletişime geçerek repo ekibine katılabilir veya paket içerisinde geliştirilmesini istediğiniz konularda talepte bulunabilirsiniz.

# Başlamadan Önce
Geliştirmeye başlamadan önce Paraşüt destek ekibi ile iletişime geçerek firmanız/hesabınız ile ilgili bazı bilgileri temin etmeniz gerekmektedir.

- **Username**: Bu bilgi halihazırda Paraşüt'e giriş yapmak için kullandığınız kullanıcı olabilir ancak önerimiz sadece API için ayrı bir kullanıcı olurşturmanızdır.
- **Password**: API bağlantısında kullanacağınız kullanıcının parolasıdır.
- **Client ID**: Bu bilgiyi Paraşüt destek ekibinden edinebilirsiniz.
- **Client Secret**: Bu bilgiyi Paraşüt destek ekibinden edinebilirsiniz.
- **Firma No**: Bu bilgiyi kendinizde Paraşüt ekranlarındaki adres satırından edinebilirsiniz. Örnek olarak "https://uygulama.parasut.com/123456/" adresindeki "123456" sizin firma numaranızdır. Eğer numarayı bulmakta zorluk yaşıyorsanız yine Paraşüt destek ekibi size bu bilgiyi verecektir.

# using
Yapılacak tüm işlemlerinizde aşağıdaki using bloklarını kullanmalısınız.
```csharp
using AvvaMobile.Core.Parasut;
using AvvaMobile.Core.Parasut.Models.Common;
using AvvaMobile.Core.Parasut.Models.Requests;
```
# Parasut objesi
Tüm işlemler için ortak bir noktada objesi oluşturarak kullanabilirsiniz.

```csharp
var parasut = new Parasut
{
    CompanyID = "Company ID",
    Username = "Username",
    Password = "Password",
    ClientID = "Client ID",
    ClientSecret = "Client Secret"
};
```

# Token Almak
```csharp
using AvvaMobile.Core.Parasut;
using AvvaMobile.Core.Parasut.Models.Common;
using AvvaMobile.Core.Parasut.Models.Requests;

var parasut = new Parasut
{
    CompanyID = "Company ID",
    Username = "Username",
    Password = "Password",
    ClientID = "Client ID",
    ClientSecret = "Client Secret"
};

var getTokenResponse = await parasut.GetTokenAsync();
if (getTokenResponse.IsSuccess)
{
    Console.WriteLine("access_token: " + getTokenResponse.Data.access_token);
}
else
{
    Console.WriteLine("ERROR: " + getTokenResponse.Message);
}
```

# Yeni Müşteri Yaratmak
```csharp
using AvvaMobile.Core.Parasut;
using AvvaMobile.Core.Parasut.Models.Common;
using AvvaMobile.Core.Parasut.Models.Requests;

var parasut = new Parasut
{
    CompanyID = "Company ID",
    Username = "Username",
    Password = "Password",
    ClientID = "Client ID",
    ClientSecret = "Client Secret"
};

var customer = new Customer();
customer.attributes.email = "opensource@avvamobile.com";
customer.attributes.name = "AVVA MOBILE KURUMSAL ÇÖZÜMLER YAZILIM VE DANIŞMANLIK TİC. LTD. ŞTİ.";
customer.attributes.short_name = "Avva Mobile Enterprise Solutions";
customer.attributes.tax_office = "Büyük Mükellefler Vergi Dairesi";
customer.attributes.tax_number = "1234567890";
customer.attributes.iban = "TR00 0000 0000 0000 0000 0000 00";

var customerCreateResponse = await parasut.CustomerCreate(customer);

if (customerCreateResponse.IsSuccess)
{
    Console.WriteLine("Customer ID: " + customerCreateResponse.Data.data.id);
}
else
{
    Console.WriteLine("ERROR: " + customerCreateResponse.Message);
}
```
# Müşterileri Listeleme
Kayıtlı olan tüm müşterileri, fitreleme seçenekleri ile listeler. Filtreleme yaparken bilgileri kayıtlardaki ile birebir olarak göndermelisiniz. Paraşüt LIKE veya CONTAINS gibi arama yapmamaktadır.

```csharp
using AvvaMobile.Core.Parasut;
using AvvaMobile.Core.Parasut.Models.Common;
using AvvaMobile.Core.Parasut.Models.Requests;

var parasut = new Parasut
{
    CompanyID = "Company ID",
    Username = "Username",
    Password = "Password",
    ClientID = "Client ID",
    ClientSecret = "Client Secret"
};

var customerListRequest = new CustomerListRequest
{
    name = "AVVA MOBILE KURUMSAL ÇÖZÜMLER YAZILIM VE DANIŞMANLIK TİC. LTD. ŞTİ."
};

var customerListResponse = await parasut.CustomerList(customerListRequest);

foreach (var item in customerListResponse.Data.data)
{
    Console.WriteLine("Customer: " + item.attributes.name);
}
```