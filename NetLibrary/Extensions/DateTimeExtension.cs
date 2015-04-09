using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace NetLibrary.Extensions
{
    public static class DateTimeExtension
    {
        public static void ToDayOfWeek(this DateTime date, DayOfWeek dayOfWeek)
        {
            int offset = date.DayOfWeek - dayOfWeek;
            //if (date.Day <= offset)
            //    offset = date.Day - 1;

            date.AddDays(-offset);
        }

        public static void ToFirstDayOfWeek(this DateTime date)
        {
            ToDayOfWeek(date, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        public static void ToFirstDayOfWeek(this DateTime date, string cultureName)
        {
            ToDayOfWeek(date, CultureInfo.GetCultureInfo(cultureName).DateTimeFormat.FirstDayOfWeek);
        }

        public static void ToFirstDayOfWeek(this DateTime date, CultureInfo culture)
        {
            ToDayOfWeek(date, culture.DateTimeFormat.FirstDayOfWeek);
        }

        public static bool Between(this DateTime date, DateTime startDate, DateTime endDate)
        {
            return (date.Ticks >= startDate.Ticks && date.Ticks <= endDate.Ticks);
        }

        public static bool After(this DateTime date, DateTime datetime)
        {
            return (date.Ticks > datetime.Ticks);
        }

        public static bool Before(this DateTime date, DateTime datetime)
        {
            return (date.Ticks < datetime.Ticks);
        }

        public static void ToFirstDayOfMonth(this DateTime date)
        {
            date = new DateTime(date.Year, date.Month, 1);
        }

        public static void ToLastDayOfMonth(this DateTime date)
        {
            date.ToFirstDayOfMonth();
            date.AddDays(DateTime.DaysInMonth(date.Year, date.Month) - 1);
        }

        public static void ToMidnight(this DateTime date)
        {
            date = new DateTime(date.Year, date.Month, date.Day);
        }

        public static void ToLastTime(this DateTime date)
        {
            date = new DateTime(date.Year, date.Month, date.Day).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
        }
    }
}