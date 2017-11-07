using System;
using System.Collections.Generic;
using System.Linq;
using HolidaysCZ;
using ICSWrapper;

namespace ClientSupport
{
    public class SupportProcessor
    {
        public Dictionary<string, List<SupportEvent>> Calendar { get; private set; } = new Dictionary<string, List<SupportEvent>>();
        public Dictionary<string, int> ReportMonths { get; private set; } = new Dictionary<string, int>();

        public void LoadCalendar(string path)
        {
            var wrapper = new ICSCalendar();
            var calendar = wrapper.Get(path);
            Parse(calendar);
        }

        public List<string> GetMonths()
        {
            return Calendar.Keys.ToList();
        }

        public string GetMonthReport(string month, string correctionNamesPath)
        {
            return new SupportReport(correctionNamesPath).Get(Calendar[month], ReportMonths[month]);
        }

        public void Parse(ICSCalendar calendar)
        {
            Calendar.Clear();
            //2017.09.01 je prvni den pocitani supportu
            var validCalendar = calendar.Events.Where(e => e.StartDate > new DateTime(2017, 8, 31)).OrderBy(e => e.StartDate.Month);
            var reportMonth = 2; // predtim se pocitaly jinak reporty
            foreach (ICSEvent icsEvent in validCalendar)
            {
                var supportEvent = new SupportEvent(icsEvent, Holidays.IsHoliday(icsEvent.StartDate));
                var key = $"{supportEvent.Date.Year}/{supportEvent.Date.Month}";
                if (!Calendar.ContainsKey(key))
                {
                    Calendar[key] = new List<SupportEvent>();
                    reportMonth++;
                    ReportMonths[key] = reportMonth;
                }
                Calendar[key].Add(supportEvent);
            }
        }

        private Holidays Holidays = new Holidays();
    }
}
