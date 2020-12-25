using System;

namespace TaskCalendar
{
    class Program
    {
        private enum Months
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }

        static void Main()
        {

            //Sample 1
            var task1 = new Task(new DateTime(2018, (int)Months.January, 3, 6, 0, 0), 55);
            Logger.PrintTask(task1);

            // Sample 2
            var task2 = new Task(new DateTime(2018, (int)Months.July, 3, 21, 0, 0), 720);
            Logger.PrintTask(task2);

            // Sample 3
            var task3 = new Task(new DateTime(2018, (int)Months.August, 21, 5, 0, 0), 4800);
            Logger.PrintTask(task3);

            // Sample 4
            var task4 = new Task(new DateTime(2020, (int)Months.December, 22, 16, 10, 0), 750);
            Logger.PrintTask(task4);

            // Sample 5
            var task5 = new Task(new DateTime(2020, (int)Months.December, 24, 10, 11, 59), 8*60);
            Logger.PrintTask(task5);

            // Sample 6
            var task6 = new Task(8 * 60);
            Logger.PrintTask(task6);

        }
    }
}
