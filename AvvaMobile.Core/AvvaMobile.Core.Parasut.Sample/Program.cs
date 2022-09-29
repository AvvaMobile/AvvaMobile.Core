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

#region Get Token
var getTokenResponse = await parasut.GetTokenAsync();
if (getTokenResponse.IsSuccess)
{
    Console.WriteLine("access_token: " + getTokenResponse.Data.access_token);
}
else
{
    Console.WriteLine("ERROR: " + getTokenResponse.Message);
}
#endregion

#region Customer Create
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
#endregion

#region Customer List
var customerListRequest = new CustomerListRequest
{
    name = "AVVA MOBILE KURUMSAL ÇÖZÜMLER YAZILIM VE DANIŞMANLIK TİC. LTD. ŞTİ."
};

var customerListResponse = await parasut.CustomerList(customerListRequest);

foreach (var item in customerListResponse.Data.data)
{
    Console.WriteLine("Customer: " + item.attributes.name);
}
#endregion


Console.ReadKey();