using System;

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
    }
}