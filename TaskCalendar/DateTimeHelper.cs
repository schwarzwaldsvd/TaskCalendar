using System;
using System.ComponentModel;

namespace TaskCalendar
{
    public static class DateTimeHelper
    {
        // Checks if the given date is the last date of week of month
        private static bool IsLastOfMonth(DateTime date)
        {
            var oneWeekAfter = date.AddDays(7);
            return oneWeekAfter.Month != date.Month;
        }

        private static bool IsNthDayOfMonth(DateTime date, DayOfWeek dayOfWeek, int n)
        {
            var d = date.Day;
            return date.DayOfWeek == dayOfWeek && (d - 1) / 7 == (n - 1);
        }
        private static DateTime GetNext(DayOfWeek dayOfWeek, DateTime date)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            var daysUntil = ((int)dayOfWeek - (int)date.DayOfWeek + 7) % 7;
            var next = date.AddDays(daysUntil);
            return next;
        }

        public static int BusinessHoursEnd = 17;
        public static int BusinessHoursStart = 8;

        /*
         * Holidays:
         * 1. New Year’s Day - YYYY/01/01
         * 2. Memorial Day - last Monday in May
         * 3. 4th of July - YYYY/07/04
         * 4. Labor Day - First Monday in September
         * 5. Thanksgiving - fourth Thursday of November
         * 6. Christmas - YYYY/12/25
         * Extra:
         * 7. Martin Luther King Jr. Day - third Monday of January
         * 8. President’s Day - third Monday of February
         * 9. Columbus Day - second Monday in October
         * 10. Veterans Day - November 11, if falls on holiday, then on next Monday.
         * 11. Day after Thanksgiving - next day after Thanksgiving
         */
        public static bool IsHoliday(this DateTime inputDate)
        {
            var isHoliday = false;
            
            switch (inputDate.Month)
            {
                // New Year’s Day
                case 1 when inputDate.Day == 1:
                // Martin Luther King Jr. Day
                case 1 when IsNthDayOfMonth(inputDate, DayOfWeek.Monday, 3): 
                // President’s Day
                case 2 when IsNthDayOfMonth(inputDate, DayOfWeek.Monday, 3): 
                // Memorial Day
                case 5 when IsNthDayOfMonth(inputDate, DayOfWeek.Monday, 4): 
                // 4th of July
                case 7 when inputDate.Day == 4:
                // Labor Day
                case 9 when IsNthDayOfMonth(inputDate, DayOfWeek.Monday, 1):
                // Columbus Day
                case 10 when IsNthDayOfMonth(inputDate, DayOfWeek.Monday, 2):
                // Veterans Day
                case 10 when (inputDate.Day == 11 && !IsWeekend(inputDate)) 
                             || (inputDate.Day > 11 && IsNthDayOfMonth(inputDate, DayOfWeek.Monday, 2)):
                // Thanksgiving 
                case 11 when IsNthDayOfMonth(inputDate, DayOfWeek.Thursday, 4):
                // Day After Thanksgiving 
                case 11 when IsNthDayOfMonth(inputDate, DayOfWeek.Friday, 4):
                // Christmas
                case 12 when inputDate.Day == 25:
                    isHoliday = true;
                    break;
            }
            return isHoliday;
        }

        public static bool IsWeekend(this DateTime inputDate)
        {
            return (inputDate.DayOfWeek == DayOfWeek.Saturday || inputDate.DayOfWeek == DayOfWeek.Sunday);
        }

        public static bool IsAfter(this DateTime inDateTime, int h, int m)
        {
            var s = inDateTime;
            s = s.Date + new TimeSpan(h, m, 0);
            return inDateTime >= s;
        }

        public static bool IsBefore(this DateTime inDateTime, int h, int m)
        {
            var s = inDateTime;
            s = s.Date + new TimeSpan(h, m, 0);
            return inDateTime < s;
        }

        public static bool TimeIsBetween(this DateTime inputTime, int startH, int startM, int endH, int endM)
        {
            var start = inputTime.Date + new TimeSpan(startH, startM, 0);
            var end = inputTime.Date + new TimeSpan(endH, endM, 0);
            return (inputTime >= start && inputTime < end);
        }

        public static bool TimeIs(this DateTime inputTime, int h, int m)
        {
            var s = inputTime;
            s = s.Date + new TimeSpan(h, m, 0);
            return (inputTime == s);
        }
        
        public static string ToLongString(this DateTime date)
        {
            return $"{date.ToLongDateString()} {date.ToLongTimeString()}";
        }

        public static DateTime TimeAt(this DateTime date, int h, int m)
        {
            var s = date;
            s = s.Date + new TimeSpan(h,m,0);
            return s;
        }
    }
}
