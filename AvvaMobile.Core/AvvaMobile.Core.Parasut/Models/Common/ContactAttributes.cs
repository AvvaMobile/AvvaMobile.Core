namespace AvvaMobile.Core.Parasut.Models.Common
{
    public class ContactAttributes
    {
        /// <summary>
        /// Müşterinin ticari adı.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Müşterinin e-posta adresi.
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Müşterinın kısa adı veya tabela adı.
        /// </summary>
        public string short_name { get; set; }

        /// <summary>
        /// Müşteri için bu alan olduğu gibi bırakılmalıdır.
        /// </summary>
        public string contact_type { get; set; } = "company";

        /// <summary>
        /// Müşteri için bu alan olduğu gibi bırakılmalıdır.
        /// </summary>
        public string account_type { get; set; } = "customer";

        /// <summary>
        /// Eğer yurt dışı müşterisi ise true olarak gönderilmelidir.
        /// </summary>
        public bool is_abroad { get; set; } = false;

        /// <summary>
        /// Eğer müşteri arşivlenmek isteniyorsa true gönderilmelidir.
        /// </summary>
        public bool archived { get; set; } = false;

        /// <summary>
        /// Olduğu gibi bırakılmalıdır.
        /// </summary>
        public bool untrackable { get; set; } = false;

        public string tax_number { get; set; }
        public string tax_office { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string country { get; set; }
        public string iban { get; set; }
    }
}