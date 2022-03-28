using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AvvaMobile.Core.Extensions
{
    public static class StringExtension
    {
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
            else if (str.Length == 7)
            {
                return Regex.Replace(str, @"^\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*$", "$1$2$3 $4 $5$6$7");
            }
            else if (str.Length == 10)
            {
                return Regex.Replace(str, @"^\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*$", "0($1$2$3) $4$5$6 $7$8 $9$10");
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

        public static string ClearHTMLTags(this string str)
        {
            return string.IsNullOrEmpty(str) ? str : Regex.Replace(str, "<.*?>", " ");
        }

        public static string ClearPhoneNumber(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            str = Regex.Replace(str, "[^0-9]+", string.Empty);
            if (str.Length.Equals(11))
            {
                str = str.TrimStart('0');
            }

            if (str.Length.Equals(10))
            {
                str = "90" + str;
            }

            return str;
        }

        private const string Dash = "-";

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

        public static DateTime DateTimeParseExact(this string str)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            return DateTime.ParseExact(str, "dd.MM.yyyy", provider);
        }

        public static string ParseDateToDefaultStringFormat(this DateTime date)
        {
            return date.ToString("dd.MM.yyyy");
        }
    }
}