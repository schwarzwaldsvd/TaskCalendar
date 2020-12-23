using System;

namespace TaskCalendar
{
    public static class Logger
    {
        public static void PrintTask(Task t)
        {
            Console.WriteLine($"Start = {t.StartDate.ToLongString()}");
            Console.WriteLine($"Minutes = {t.MinutesToWork} ({t.MinutesToWork/60} hours)");
            Console.WriteLine($"Answer = {t.GetEndDate().ToLongString()}");
            Console.WriteLine();
        }

        public static void PrintMinutes(DateTime now, double minutes)
        {
            Console.WriteLine($" ■ {now.ToShortDateString()} {now.ToShortTimeString()}," +
                              $" remaining : {minutes} minutes ({Math.Round(minutes/60,2)} hours)");
        }
    }
}
