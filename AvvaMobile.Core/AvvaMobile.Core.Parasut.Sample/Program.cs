using AvvaMobile.Core.Parasut;
using AvvaMobile.Core.Parasut.Models.Common;
using AvvaMobile.Core.Parasut.Models.Requests;

//var parasut = new Parasut
//{
//    CompanyID = "Company ID",
//    Username = "Username",
//    Password = "Password",
//    ClientID = "Client ID",
//    ClientSecret = "Client Secret"
//};

var parasut = new Parasut
{
    CompanyID = "211042",
    Username = "murat.yilmaz@avvamobile.com",
    Password = "Avva2024059",
    ClientID = "GPw92--FTE_ep2pcS6GJgyno4zoOtazUjy6ISp1K1bA",
    ClientSecret = "q3XV2umX9bLeeGyOKDgXKQKurZt1oAjCuM2togBUyxo"
};



//await GetToken();
//await CustomerCreate();
//await CustomerEdit();
//await CustomerList();
//await ProductCreate();
//await ProductCreate();
await SalesInvoiceCreate();


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
    var customer = new Contact();
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
    var customer = new Contact();
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

async Task SalesInvoiceCreate()
{
    var invoice = new SalesInvoice();

    invoice.relationships.contact = new Envelope<ContactBase>() { data = new ContactBase() { id = "99257873" } };

    invoice.attributes.description = "Yeni Fatura Açıklaması";
    invoice.attributes.issue_date = DateTime.Now;
    invoice.attributes.due_date = DateTime.Now.AddDays(7);
    invoice.attributes.invoice_series = "F";
    invoice.attributes.invoice_id = 1234;
    invoice.attributes.currency = Currencies.TRL;
    invoice.attributes.exchange_rate = 18.5m;
    invoice.attributes.order_no = "1234567890";
    invoice.attributes.order_date = DateTime.Now;
    invoice.attributes.shipment_included = true;
    invoice.attributes.is_abroad = false;
    invoice.attributes.cash_sale = true;
    invoice.attributes.payment_account_id = 1009901;
    invoice.attributes.payment_date = DateTime.Now;
    invoice.attributes.payment_description = "Peşin Satış Ödemesi";

    var response = await parasut.SalesInvoiceCreate(invoice);

    if (response.IsSuccess)
    {
        Console.WriteLine("Invoice ID: " + response.Data.data.id);
    }
    else
    {
        Console.WriteLine("ERROR: " + response.Message);
    }
}