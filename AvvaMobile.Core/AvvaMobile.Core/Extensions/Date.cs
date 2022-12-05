using Newtonsoft.Json;
using System;
using System.Globalization;

namespace AvvaMobile.Core.Extensions
{
    public static class Date
    {
        public static string ToFormattedDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy HH:mm");
        }

        public static string ToFormattedDate(this DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy");
        }

        public static string ToFormattedTime(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm");
        }

        public static string ToFormattedDateTime(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToFormattedDateTime() : string.Empty;
        }

        public static string ToFormattedDate(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToFormattedDate() : string.Empty;
        }

        public static DateTime StartOfWeek(this DateTime datetime)
        {
            var diff = (7 + (datetime.DayOfWeek - DayOfWeek.Monday)) % 7;
            return datetime.AddDays(-1 * diff).Date;
        }

        public static (DateTime, DateTime) DataTableDateTime(this string date)
        {
            var dateRange = date.ToStringData().Split("-");
            if (dateRange.Length == 2)
            {
                DateTime FilterStartDate = DateTime.ParseExact(dateRange[0].Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                DateTime FilterEndDate = DateTime.ParseExact(dateRange[1].Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                FilterEndDate = FilterEndDate.AddHours(23).AddMinutes(59);
                return (FilterStartDate, FilterEndDate);
            }
            return (DateTime.MinValue, DateTime.MaxValue);
        }

        public static DeserializedDateTime ToDeserializedDateTime(this DateTime dateTime)
        {
            return dateTime.ToDeserializedDateTime("ddd", "MMM", true, true);
        }

        public static DeserializedDateTime ToDeserializedDateTime(
            this DateTime dateTime,
            string dayFormat = "ddd",
            string monthFormat = "MMM",
            bool isMonthUpper = true,
            bool isDayUpper = true)
        {
            var deserializedDateTime = new DeserializedDateTime();

            deserializedDateTime.Day = dateTime.Day.ToString("D2");
            deserializedDateTime.DayName = dateTime.ToString(dayFormat);
            deserializedDateTime.Month = dateTime.Month.ToString("D2");
            deserializedDateTime.MonthName = dateTime.ToString(monthFormat);
            deserializedDateTime.Time = dateTime.ToShortTimeString();
            deserializedDateTime.Hour = dateTime.Hour.ToString();
            deserializedDateTime.Minute = dateTime.Minute.ToString();
            deserializedDateTime.Year = dateTime.Year.ToString();

            if (isDayUpper)
            {
                deserializedDateTime.DayName = deserializedDateTime.DayName.ToUpper();
            }

            if (isMonthUpper)
            {
                deserializedDateTime.MonthName = deserializedDateTime.MonthName.ToUpper();
            }

            return deserializedDateTime;
        }
    }

    public class DeserializedDateTime
    {
        public string Day { get; set; }
        public string DayName { get; set; }
        public string Month { get; set; }
        public string MonthName { get; set; }
        public string Time { get; set; }
        public string Hour { get; set; }
        public string Minute { get; set; }
        public string Year { get; set; }
    }
}