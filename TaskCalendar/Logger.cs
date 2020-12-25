using System;

namespace TaskCalendar
{
    public static class Logger
    {
        public static void PrintTask(Task t)
        {
            Console.WriteLine($"Start = {t.StartDate.ToLongString()}");
            Console.WriteLine($"Minutes = {t.MinutesToWork} ({t.MinutesToWork/60} hours)");
            Console.WriteLine($"End = {t.GetEndDate().ToLongString()}");
            Console.WriteLine();
        }

        public static void PrintMinutes(DateTime now, double minToWork, double minWorkedToday, double minWorkedTotal, bool didWork)
        {
            if (didWork)
            {
                Console.WriteLine($" ■ {now.ToShortDateString()} {now.ToShortTimeString()}," +
                                  $" worked: {minWorkedToday} (total {minWorkedTotal}m) ({Math.Round(minWorkedTotal / 60, 2)}h), " +
                                  $"remaining: {minToWork}m ({Math.Round(minToWork / 60, 2)}h)");
            }
            else
            {
                Console.WriteLine($" x {now.ToShortDateString()} {now.ToShortTimeString()} - holiday");
            }
        }
    }
}
