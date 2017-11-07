﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientSupport
{
    public class SupportReportObject
    {
        protected const string NumberDataSplitter = " // ";
        protected const int WorkingDaySupportHours = 13;
        protected const int NotWorkingDaySupportHours = 24;

        protected const string Vikend = "víkend";
        protected const string PracovniDen = "pracovní den";
        protected const string Svatek = "svátek";

        protected Dictionary<SupportEvent.SupportDayType, int> DaysByType;

        protected virtual void Init()
        {
            DaysByType = new Dictionary<SupportEvent.SupportDayType, int>()
            {
                [SupportEvent.SupportDayType.WorkDay] = 0,
                [SupportEvent.SupportDayType.Holiday] = 0,
                [SupportEvent.SupportDayType.Weekend] = 0,
            };
        }

        public int Workdays { get { return DaysByType[SupportEvent.SupportDayType.WorkDay]; } }
        public int WorkdayHours { get { return Workdays * WorkingDaySupportHours; } }
        public int Weekends { get { return DaysByType[SupportEvent.SupportDayType.Weekend]; } }
        public int WeekendHours { get { return Weekends * ((IsOldWeekendDefinition() ? 13 : 0) + 2 * NotWorkingDaySupportHours); } }
        public int Holidays { get { return DaysByType[SupportEvent.SupportDayType.Holiday]; } }
        public int HolidayHours { get { return Holidays * NotWorkingDaySupportHours; } }
        protected int HoursSummary { get { return WeekendHours + HolidayHours + WorkdayHours; } }

        protected string GetTypeReport(int count, string item)
        {
            return count > 0 ? $"{count}x {item}" : String.Empty;
        }

        protected string GetTypeSummary()
        {
            var records = new List<string> { GetTypeReport(Weekends, Vikend), GetTypeReport(Workdays, PracovniDen), GetTypeReport(Holidays, Svatek) };
            var summary = records.Where(r => !String.IsNullOrEmpty(r)).ToList().Aggregate((f, s) => $"{f}, {s}");

            return summary;
        }

        public virtual bool IsOldWeekendDefinition()
        {
            return false;
        }
    }
}
