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



//await GetToken();
//await CustomerCreate();
//await CustomerEdit();
//await CustomerList();
//await ProductCreate();
//await ProductCreate();


Console.ReadKey();


async Task GetToken()
{
    var response = await parasut.GetTokenAsync();
    if (response.IsSuccess)
    {
        Console.WriteLine("access_token: " + response.Data.access_token);
    }
    else
    {
        Console.WriteLine("ERROR: " + response.Message);
    }
}

async Task CustomerCreate()
{
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
}

async Task CustomerEdit()
{
    var customer = new Customer();
    customer.id = "1234567890";
    customer.attributes.email = "opensource@avvamobile.com";
    customer.attributes.name = "AVVA MOBILE KURUMSAL ÇÖZÜMLER YAZILIM VE DANIŞMANLIK TİC. LTD. ŞTİ.";
    customer.attributes.short_name = "Avva Mobile Enterprise Solutions";
    customer.attributes.tax_office = "Büyük Mükellefler Vergi Dairesi";
    customer.attributes.tax_number = "1234567890";
    customer.attributes.iban = "TR00 0000 0000 0000 0000 0000 00";

    var response = await parasut.CustomerEdit(customer);

    if (response.IsSuccess)
    {
        Console.WriteLine("Customer ID: " + response.Data.data.id);
    }
    else
    {
        Console.WriteLine("ERROR: " + response.Message);
    }
}

async Task CustomerList()
{
    var customerListRequest = new CustomerListRequest
    {
        name = "AVVA MOBILE KURUMSAL ÇÖZÜMLER YAZILIM VE DANIŞMANLIK TİC. LTD. ŞTİ."
    };

    var response = await parasut.CustomerList(customerListRequest);

    foreach (var item in response.Data.data)
    {
        Console.WriteLine("Customer: " + item.attributes.name);
    }
}

async Task ProductCreate()
{
    var product = new Product();
    product.attributes.name = "Product Name";
    product.attributes.code = "Product Code";
    product.attributes.vat_rate = 18;
    product.attributes.unit = "Adet";
    product.attributes.list_price = 222;
    product.attributes.buying_price = 111;
    product.attributes.barcode = "1234567890";

    var response = await parasut.ProductCreate(product);

    if (response.IsSuccess)
    {
        Console.WriteLine("Product ID: " + response.Data.data.id);
    }
    else
    {
        Console.WriteLine("ERROR: " + response.Message);
    }
}

async Task ProductEdit()
{
    var product = new Product();
    product.id = "1234567890";
    product.attributes.name = "Product Name";
    product.attributes.code = "Product Code";
    product.attributes.vat_rate = 18;
    product.attributes.unit = "Adet";
    product.attributes.list_price = 222;
    product.attributes.buying_price = 111;
    product.attributes.barcode = "1234567890";

    var response = await parasut.ProductEdit(product);

    if (response.IsSuccess)
    {
        Console.WriteLine("Product ID: " + response.Data.data.id);
    }
    else
    {
        Console.WriteLine("ERROR: " + response.Message);
    }
}