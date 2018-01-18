using ICSWrapper;
using System;

namespace ClientSupport
{
    public class SupportEvent
    {
        public SupportEvent(ICSEvent icsEvent, bool holiday)
        {
            Event = icsEvent;
            Date = icsEvent.StartDate;
            SupportType =
                IsWeekend(icsEvent.StartDate, icsEvent.EndDate) ? SupportDayType.Weekend : holiday ? SupportDayType.Holiday : SupportDayType.WorkDay;
            Person = icsEvent.Summary.Split('(')[0].Trim();
        }

        private bool IsWeekend(DateTime startDate, DateTime endDate)
        {
            bool isWeekend = false;
            if (((startDate.DayOfWeek == DayOfWeek.Saturday) || (startDate.DayOfWeek == DayOfWeek.Friday)) && (endDate.DayOfWeek == DayOfWeek.Monday))
            {
                OldWeekendDefinition = startDate.DayOfWeek == DayOfWeek.Friday;
                isWeekend = true;
            }

            return isWeekend;
        }

        public ICSEvent Event { get; set; }

        public bool OldWeekendDefinition { get; private set; } = false;

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
