using AvvaMobile.Core.Caching;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AvvaMobile.Core.Extensions
{
    public static class StringExtension
    {
        public static AppSettingsKeys _appSettingsKeys;
        public static bool IsNullOrEmtpy(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNotNull(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static string ToEmptyStringIndicator(this string str)
        {
            return string.IsNullOrEmpty(str) ? "--" : str;
        }

        public static string ReplaceENTER(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            return str.Replace(Environment.NewLine, "<br/>");
        }

        public static string ToStringData(this object str)
        {
            return str?.ToString() ?? string.Empty;
        }

        public static string ToPhoneNumber(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            else if (str.Length == 10)
            {
                return Regex.Replace(str, @"^\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*$", "0 ($1$2$3) $4$5$6 $7$8 $9$10");
            }
            else if (str.Length == 11)
            {
                return Regex.Replace(str, @"^\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*$", "$1 ($2$3$4) $5$6$7 $8$9 $10$11");
            }
            else if (str.Length == 12)
            {
                return Regex.Replace(str, @"^\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*$", "+$1$2 ($3$4$5) $6$7$8 $9$10 $11$12");
            }
            else
            {
                return str;
            }
        }

        public static string ClearPhoneNumber(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            str = Regex.Replace(str, "[^0-9]+", string.Empty);

            if (str.Length.Equals(10)) // 10 = '532 111 22 33'
            {
                str = "90" + str;
            }
            else if (str.Length.Equals(11)) // 11 = '0 532 111 22 33'
            {
                str = "9" + str;
            }

            return str;
        }

        [Obsolete("This method is deprecated. Please use 'ClearPhoneNumber' class. (Öcal Esmer)", true)]
        public static string ClearPhoneNumber_LeadingCountrCode(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            str = Regex.Replace(str, "[^0-9]+", string.Empty);

            if (str.Length.Equals(10)) // 10 = '532 111 22 33'
            {
                str = "90" + str;
            }
            else if (str.Length.Equals(11)) // 11 = '0 532 111 22 33'
            {
                str = "9" + str;
            }

            return str;
        }

        [Obsolete("This method is deprecated. Please use 'ClearPhoneNumber' class. (Öcal Esmer)", true)]
        public static string ClearPhoneNumber_LeadingZero(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            str = Regex.Replace(str, "[^0-9]+", string.Empty);

            if (str.Length.Equals(10)) // 10 = '532 111 22 33'
            {
                str = "0" + str;
            }
            else if (str.Length.Equals(12)) // 12 = '90 532 111 22 33'
            {
                str = str.TrimStart('9');
            }

            return str;
        }

        [Obsolete("This method is deprecated. Please use 'ClearPhoneNumber' class. (Öcal Esmer)", true)]
        public static string ClearPhoneNumber_NoLeadingNumber(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            str = Regex.Replace(str, "[^0-9]+", string.Empty);

            if (str.Length.Equals(11)) // 11 = '0 532 111 22 33'
            {
                str = str.TrimStart('0');
            }
            else if (str.Length.Equals(12)) // 12 = '90 532 111 22 33'
            {
                str = str.TrimStart('9');
                str = str.TrimStart('0');
            }

            return str;
        }

        public static string ClearHTMLTags(this string str)
        {
            return string.IsNullOrEmpty(str) ? str : Regex.Replace(str, "<.*?>", " ");
        }

        private const string Dash = "-";

        private const string Dot = ".";

        public static string ToKeyword(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            str = str.Trim();
            str = str.ToLower(new CultureInfo("en-US"));
            str = str.Replace("ğ", "g");
            str = str.Replace("ü", "u");
            str = str.Replace("ş", "s");
            str = str.Replace("ı", "i");
            str = str.Replace("ö", "o");
            str = str.Replace("ç", "c");
            str = str.Replace("Ğ", "g");
            str = str.Replace("Ü", "u");
            str = str.Replace("Ş", "s");
            str = str.Replace("İ", "i");
            str = str.Replace("Ö", "o");
            str = str.Replace("Ç", "c");
            str = str.Replace("+", Dash);
            str = str.Replace("'", string.Empty);
            str = str.Replace("(", string.Empty);
            str = str.Replace(")", string.Empty);
            str = str.Replace(" ", Dash);
            str = str.Replace("/", Dash);
            str = str.Replace("&", Dash);
            str = str.Replace("!", string.Empty);
            str = str.Replace("?", string.Empty);
            str = str.Replace(".", string.Empty);
            str = str.Replace(":", string.Empty);
            str = str.Replace(@"\", Dash);
            str = str.Replace("---", Dash);
            str = str.Replace("--", Dash);
            str = str.Replace("\"", Dash);
            str = str.Replace("%", string.Empty);
            return str;
        }

        /// <summary>
        /// Waits a date sting as formatted "dd.MM.yyyy" and return datetime object.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime DateTimeParseExact(this string str)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            return DateTime.ParseExact(str, "dd.MM.yyyy", provider);
        }

        /// <summary>
        /// Return initial characters of first two words of a string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetInitials(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            var words = str.Split(' ');
            if (words.Length == 1)
            {
                return words[0].Substring(0, 1);
            }
            else if (words.Length >= 2)
            {
                return words[0].Substring(0, 1) + words[1].Substring(0, 1);
            }
            else
            {
                return string.Empty;
            }
        }

        public static bool ToBool(this string str)
        {
            return bool.Parse(str);
        }

        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }

        public static decimal ToDecimal(this string str)
        {
            return decimal.Parse(str);
        }

        public static long ToLong(this string str)
        {
            return long.Parse(str);
        }
        public static string PrepareCDNUrl(this string imageUrl, string folder)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return null;
            }

            return string.Format("{0}{1}{2}", _appSettingsKeys.CDN_BaseUrl, folder, imageUrl);
        }
        public static string PrepareS3Url(this string imageUrl, string bucketName)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return null;
            }
            return string.Format(_appSettingsKeys.S3CDNBaseUrl, bucketName, imageUrl);
            //                   https://{0}.s3.eu-central-1.amazonaws.com/{1}
        }
        public static string CreditCardMaskify(this string str)
        {
            return "**** **** **** " + str;
        }

        public static string CreditCardLast4Digits(this string str)
        {
            return str.Substring(str.Length - 4);
        }

        public static bool ValidateDateFormat(string value)
        {
            if (Regex.Match(value, @"^\s*(3[01]|[12][0-9]|0?[1-9])\.(1[012]|0?[1-9])\.((?:19|20)\d{2})\s*$").Success)
            {
                return true;
            }

            return false;
        }

        public static string FormatToIBAN(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            return Regex.Replace(str, ".{4}", "$0 ").Trim();
        }
    }
}