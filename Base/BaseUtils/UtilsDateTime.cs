using System;
using System.Collections.Generic;
using System.Text;

namespace Base.BaseUtils
{
    public class UtilsDateTime
    {
        public enum DateInterval
        {
            Year,
            Month,
            Weekday,
            Day,
            Hour,
            Minute,
            Second
        }

        public enum DateTimePart
        {
            MinTime,
            MaxTime,
            None
        }

        public static long DateDiff(DateInterval interval, DateTime date1, DateTime date2)
        {

            TimeSpan ts = ts = date2 - date1;

            switch (interval)
            {
                case DateInterval.Year:
                    return date2.Year - date1.Year;
                case DateInterval.Month:
                    return (date2.Month - date1.Month) + (12 * (date2.Year - date1.Year));
                case DateInterval.Weekday:
                    return Fix(ts.TotalDays) / 7;
                case DateInterval.Day:
                    return Fix(ts.TotalDays);
                case DateInterval.Hour:
                    return Fix(ts.TotalHours);
                case DateInterval.Minute:
                    return Fix(ts.TotalMinutes);
                default:
                    return Fix(ts.TotalSeconds);
            }
        }

        public static bool DateAdd(DateTime dt1, int nrZile, ref DateTime dt2)
        {
            try
            {
                dt2 = dt1.AddDays(nrZile);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private static long Fix(double Number)
        {
            if (Number >= 0)
            {
                return (long)Math.Floor(Number);
            }
            return (long)Math.Ceiling(Number);
        }

        public static string ToInvariantString(object obj)
        {
            return ToInvariantString(obj, DateTimePart.None);
        }

        public static string ToInvariantString(object obj, DateTimePart part)
        {
            DateTime data;
            if (part == DateTimePart.None)
                try
                {
                    return (Convert.ToDateTime(obj)).ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    return "01/01/01";
                }
            else
                try
                {
                    DateTime dt = Convert.ToDateTime(obj);
                    if (part == DateTimePart.MinTime)
                        data = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
                    else
                        data = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                    return data.ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    return "01/01/01";
                }
        }

        public static DateTime ToDateTime(DateTime dataInput, DateTimePart part)
        {
            if (part == DateTimePart.MinTime)
                return new DateTime(dataInput.Year, dataInput.Month, dataInput.Day, 0, 0, 0);
            else if (part == DateTimePart.MaxTime)
                return new DateTime(dataInput.Year, dataInput.Month, dataInput.Day, 23, 59, 59);
            else
                return dataInput;
        }

        public static DateTime GetLastDayOfMonth(int year, int month)
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return new DateTime(year, month, 31);
                case 4:
                case 6:
                case 9:
                case 11:
                    return new DateTime(year, month, 30);
                case 2:
                    if (year % 4 == 0)
                        return new DateTime(year, month, 29);
                    else
                        return new DateTime(year, month, 28);

            }

            return DateTime.Today;
        }
    }
}
