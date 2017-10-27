using System;
using System.Collections.Generic;
using System.Text;
using ICSWrapper;

namespace ClientSupport
{
    public class SupportEvent
    {
        public SupportEvent(ICSEvent icsEvent, bool holiday)
        {
            Date = icsEvent.StartDate;
            SupportType =
                (icsEvent.StartDate.DayOfWeek == DayOfWeek.Friday) && (icsEvent.EndDate.DayOfWeek == DayOfWeek.Monday)
                ? SupportDayType.Weekend
                : holiday ? SupportDayType.Holiday : SupportDayType.WorkDay;
            Person = icsEvent.Summary.Split('(')[0].Trim();
        }

        public DateTime Date { get; private set; }

        public string Person { get; private set; }

        public SupportDayType SupportType { get; private set; } = SupportDayType.Undefined;

        public enum SupportDayType
        {
            WorkDay,
            Weekend,
            Holiday,
            Undefined
        }
    }
}
