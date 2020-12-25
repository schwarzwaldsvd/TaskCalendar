using System;

namespace TaskCalendar
{
    public class Task
    {
        public DateTime StartDate { get; set; }
        public int MinutesToWork { get; set; }

        public Task()
        {
            StartDate = DateTime.Now;
            MinutesToWork = 60 * 8;
        }
        public Task(int minutes) : this()
        {
            MinutesToWork = minutes;
        }
        public Task(DateTime start, int minutes) :this(minutes)
        {
            StartDate = start;
            MinutesToWork = minutes;
        }
        
        public DateTime GetEndDate()
        {
            var w = new Work();
            w.MinuteTolerance = 0.01;
            return w.GetEndDate(StartDate, MinutesToWork);
        }
    }
}
