﻿using System;
using System.Xml;

namespace TaskCalendar
{
    public class Work
    {
        private int _minutesToWork;
        private const int FullWorkingDay = (8 * 60);
        private const int HalfWorkingDay = FullWorkingDay / 2;
        private const int Lunch = 60;
        
    private DateTime CurrentMoment { get; set; }

        public DateTime GetEndDate(DateTime start, int minutes)
        {
            // check if minutes exceed one year
            // regular year -> 365 days
            var maxMinutes = 365 * 24 * 60; 
            if (DateTime.IsLeapYear(start.Year))
                maxMinutes += 24 * 60; // if leap year -> add one more day

            CurrentMoment = start;

            if (minutes <= maxMinutes)
            {
                _minutesToWork = minutes;
                while (_minutesToWork > 0)
                {
                    if (CurrentMoment.IsWeekend() || CurrentMoment.IsHoliday())
                        CurrentMoment = CurrentMoment.AddDays(1).SetTime8OClock(); // is weekend or holiday
                    else
                    {
                        if (CurrentMoment.IsAfterHours())
                        {
                            CurrentMoment = CurrentMoment.AddDays(1).SetTime8OClock();
                            if (CurrentMoment.IsWeekend() || CurrentMoment.IsHoliday())
                            {
                                Logger.PrintMinutes(CurrentMoment, _minutesToWork);
                                continue;
                            }
                        }

                        if (CurrentMoment.IsBeforeHours())
                            CurrentMoment = CurrentMoment.SetTime8OClock();

                        if (CurrentMoment.IsLunchTime())
                        {
                            if (_minutesToWork >= HalfWorkingDay)
                            {
                                CurrentMoment = CurrentMoment.AtTime(13,0).AddMinutes(HalfWorkingDay);
                                _minutesToWork -= HalfWorkingDay;
                            }

                            if (_minutesToWork > 0 && _minutesToWork < HalfWorkingDay)
                            {
                                CurrentMoment = CurrentMoment.AtTime(13, 0).AddMinutes(_minutesToWork);
                                _minutesToWork -= _minutesToWork;
                            }
                            Logger.PrintMinutes(CurrentMoment, _minutesToWork);
                            continue;
                        }

                        if (_minutesToWork / HalfWorkingDay < 1)
                        {
                            CurrentMoment = CurrentMoment.AddMinutes(_minutesToWork);
                            _minutesToWork -= _minutesToWork;
                        }
                        else if (_minutesToWork / HalfWorkingDay == 1)
                        {
                            CurrentMoment = CurrentMoment.AddMinutes(HalfWorkingDay);
                            _minutesToWork -= HalfWorkingDay;
                        }
                        else if (_minutesToWork / HalfWorkingDay > 1 && _minutesToWork / HalfWorkingDay < 2)
                        {
                            CurrentMoment = CurrentMoment.AddMinutes(HalfWorkingDay).AtTime(13, 0);
                            _minutesToWork -= HalfWorkingDay;
                        }
                        else if (_minutesToWork / FullWorkingDay >= 1)
                        {
                            if (CurrentMoment.IsAfterLunchTime()) 
                            {
                                var remainingMinutesToWorkToday = CurrentMoment.Date.AtTime(17,0) - CurrentMoment;
                                CurrentMoment = CurrentMoment.AddMinutes(remainingMinutesToWorkToday.TotalMinutes);
                                _minutesToWork -= (int)remainingMinutesToWorkToday.TotalMinutes;
                            }
                            else
                            {
                                if (CurrentMoment.Is8AmSharp())
                                {
                                    CurrentMoment = CurrentMoment.AddMinutes(FullWorkingDay + Lunch);
                                    _minutesToWork -= FullWorkingDay;
                                }
                                else
                                {
                                    var remainingMinutesToWorkToday = CurrentMoment.Date.AtTime(17, 0) - CurrentMoment;
                                    CurrentMoment = CurrentMoment.AddMinutes(remainingMinutesToWorkToday.TotalMinutes);
                                    _minutesToWork -= (int)remainingMinutesToWorkToday.TotalMinutes;
                                }
                            }
                        }
                    }
                    Logger.PrintMinutes(CurrentMoment, _minutesToWork);
                }
            }
            else Console.WriteLine("Working minutes exceeded one year.");

            return CurrentMoment;
        }
    }
}
