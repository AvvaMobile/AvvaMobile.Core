namespace AvvaMobile.Core.Parasut.Models.Common
{
    /// <summary>
    /// Fatura bilgilerini doldurmanın haricinde;
    /// 1- Relationships içindeki Customer objesine id atamasını yapmayı unutmayınız.
    /// </summary>
    public class SalesInvoiceAttributes
    {
        /// <summary>
        /// Fatura türü.
        /// </summary>
        public string item_type { get; set; } = "invoice";

        /// <summary>
        /// Fatura açıklaması.
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Düzenlenme tarihi.
        /// </summary>
        public DateTime issue_date { get; set; }

        /// <summary>
        /// Son tahsilat tarihi.
        /// </summary>
        public DateTime due_date { get; set; }

        /// <summary>
        /// Farura seri.
        /// </summary>
        public string invoice_series { get; set; }

        /// <summary>
        /// Fatura no.
        /// </summary>
        public int invoice_id { get; set; }

        /// <summary>
        /// Para birimi. "Currencies.TRL" olarak tüm para birimleri kullanılabilir. Varsayılan olarak "TRL" ayarlanmıştır.
        /// </summary>
        public string currency { get; set; } = Currencies.TRL;

        /// <summary>
        /// Döviz kuru.
        /// </summary>
        public decimal exchange_rate { get; set; }

        /// <summary>
        /// Gönderim ve fatura adresi.
        /// </summary>
        public string billing_address { get; set; }

        /// <summary>
        /// Gönderim telefonu.
        /// </summary>
        public string billing_phone { get; set; }

        /// <summary>
        /// Müşteri vergi dairesi.
        /// </summary>
        public string tax_office { get; set; }

        /// <summary>
        /// Müşteri vergi numarası.
        /// </summary>
        public string tax_number { get; set; }

        /// <summary>
        /// Ülke.
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// Şehir.
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// İlçe.
        /// </summary>
        public string district { get; set; }

        /// <summary>
        /// Alıcı yurt dışı bilgisi.
        /// </summary>
        public bool is_abroad { get; set; } = false;

        /// <summary>
        /// Sipariş numarası.
        /// </summary>
        public string order_no { get; set; }

        /// <summary>
        /// Sipariş tarihi.
        /// </summary>
        public DateTime order_date { get; set; }

        /// <summary>
        /// İrsaliyeli fatura.
        /// </summary>
        public bool shipment_included { get; set; } = false;

        /// <summary>
        /// Peşin satış. Fatura ile aynı anda ödeme alındığında kullanılır.
        /// </summary>
        public bool cash_sale { get; set; } = false;

        /// <summary>
        /// Eğer peşin satış ise ödemenin hangi hesaba alındığını belirtir. Kullanılabilir hesapların id bilgilerini Paraşüt web ekranındaki "Nakit > Kasalar ve Bankalar" listesindeki kullanılacak olan kasanın id bilgisi alınabilir. Örnek olarak "https://uygulama.parasut.com/123/kasa-ve-bankalar/456" adresindeki "456" yazan yer kullanılan kasanın id'sidir.
        /// </summary>
        public int? payment_account_id { get; set; }

        /// <summary>
        /// Peşinde ödeme yapılacak ise ödemenin tarihi.
        /// </summary>
        public DateTime? payment_date { get; set; }

        /// <summary>
        /// Peşin ödeme işleminin açıklaması.
        /// </summary>
        public string payment_description { get; set; }
    }

    public struct Currencies
    {
        public static string TRL = "TRL";
        public static string USD = "USD";
        public static string EUR = "EUR";
        public static string GBP = "GBP";
    }
}