using System;

namespace TaskCalendar
{
    public class Work
    {
        private double _minutesToWork;
        private double _minutesWorkedTotal;
        private const int FullWorkingDay = (8 * 60);
        private const int HalfWorkingDay = FullWorkingDay / 2;
        private const int Lunch = 60;
        
    private DateTime CurrentMoment { get; set; }
    public double MinuteTolerance { get; set; }

        public DateTime GetEndDate(DateTime start, int minutes)
        {
            var maxMinutes = 365 * 24 * 60; 
            if (DateTime.IsLeapYear(start.Year))
                maxMinutes += 24 * 60;
            CurrentMoment = start;

            if (minutes <= maxMinutes)
            {
                _minutesToWork = minutes;
                _minutesWorkedTotal = 0;
                
                while (_minutesToWork > 0)
                {
                    var didWork = false;
                    var minutesWorkedToday = 0;
                    if (CurrentMoment.IsWeekend() || CurrentMoment.IsHoliday())
                        CurrentMoment = CurrentMoment.AddDays(1).TimeAt(8, 0); // is weekend or holiday
                    else
                    {
                        if (CurrentMoment.IsAfter(17, 0))
                        {
                            CurrentMoment = CurrentMoment.AddDays(1).TimeAt(8,0);
                            if (CurrentMoment.IsWeekend() || CurrentMoment.IsHoliday())
                            {
                                Logger.PrintMinutes(CurrentMoment, _minutesToWork, minutesWorkedToday, _minutesWorkedTotal, false);
                                continue;
                            }
                        }

                        if (CurrentMoment.IsBefore(8, 0))
                            CurrentMoment = CurrentMoment.TimeAt(8,0);

                        if (CurrentMoment.TimeIsBetween(12,0,13,0)) // Is Lunch Time
                        {
                            if (_minutesToWork >= HalfWorkingDay)
                            {
                                minutesWorkedToday = HalfWorkingDay;
                                CurrentMoment = CurrentMoment.TimeAt(13, 0).AddMinutes(minutesWorkedToday);
                                _minutesWorkedTotal += minutesWorkedToday;
                                _minutesToWork -= minutesWorkedToday;
                            }

                            if (_minutesToWork > 0 && _minutesToWork < HalfWorkingDay)
                            {
                                minutesWorkedToday = (int)_minutesToWork;
                                CurrentMoment = CurrentMoment.TimeAt(13, 0).AddMinutes(minutesWorkedToday);
                                _minutesWorkedTotal += minutesWorkedToday;
                                _minutesToWork -= minutesWorkedToday;

                            }
                            Logger.PrintMinutes(CurrentMoment, _minutesToWork, minutesWorkedToday, _minutesWorkedTotal, true);
                            continue;
                        }

                        // TODO: To review this logic
                        if (_minutesToWork / HalfWorkingDay < 1)
                        {
                            minutesWorkedToday = (int)_minutesToWork;
                            CurrentMoment = CurrentMoment.AddMinutes(minutesWorkedToday);
                            _minutesWorkedTotal += minutesWorkedToday;
                            _minutesToWork -= minutesWorkedToday;
                            didWork = true;
                        }
                        else if (Math.Abs(_minutesToWork / HalfWorkingDay - 1) < MinuteTolerance) // _minutesToWork/HalfWorkingDay ==1
                        {
                            minutesWorkedToday = HalfWorkingDay;
                            CurrentMoment = CurrentMoment.AddMinutes(minutesWorkedToday);
                            _minutesWorkedTotal += minutesWorkedToday;
                            _minutesToWork -= minutesWorkedToday;
                            didWork = true;
                        }
                        else if (_minutesToWork / HalfWorkingDay > 1 && _minutesToWork / HalfWorkingDay < 2)
                        {
                            minutesWorkedToday = HalfWorkingDay;
                            CurrentMoment = CurrentMoment.AddMinutes(minutesWorkedToday).TimeAt(13, 0);
                            _minutesWorkedTotal += minutesWorkedToday;
                            _minutesToWork -= minutesWorkedToday;
                            didWork = true;
                        }
                        else if (_minutesToWork / FullWorkingDay >= 1)
                        {
                            if (CurrentMoment.TimeIsBetween(13,0,17,0))
                            {
                                
                                var remainingMinutesToWorkToday = CurrentMoment.Date.TimeAt(17,0) - CurrentMoment;
                                minutesWorkedToday = (int)remainingMinutesToWorkToday.TotalMinutes;
                                CurrentMoment = CurrentMoment.AddMinutes(remainingMinutesToWorkToday.TotalMinutes);
                                _minutesWorkedTotal += minutesWorkedToday;
                                _minutesToWork -= minutesWorkedToday;
                                didWork = true;
                            }
                            else
                            {
                                if (CurrentMoment.TimeIs(8,0))
                                {
                                    minutesWorkedToday = FullWorkingDay;
                                    CurrentMoment = CurrentMoment.AddMinutes(minutesWorkedToday + Lunch);
                                    _minutesWorkedTotal += minutesWorkedToday;
                                    _minutesToWork -= minutesWorkedToday;
                                    didWork = true;
                                }
                                else
                                {
                                    
                                    var remainingMinutesToWorkToday = CurrentMoment.Date.TimeAt(17, 0) - CurrentMoment;
                                    minutesWorkedToday = (int)remainingMinutesToWorkToday.TotalMinutes;
                                    CurrentMoment = CurrentMoment.AddMinutes(remainingMinutesToWorkToday.TotalMinutes);
                                    _minutesWorkedTotal += minutesWorkedToday;
                                    _minutesToWork -= minutesWorkedToday;
                                    didWork = true;
                                }
                            }
                        }
                    }
                    Logger.PrintMinutes(CurrentMoment, _minutesToWork, minutesWorkedToday, _minutesWorkedTotal, didWork);
                }
            }
            else Console.WriteLine("Working minutes exceeded one year.");

            return CurrentMoment;
        }
    }
}
