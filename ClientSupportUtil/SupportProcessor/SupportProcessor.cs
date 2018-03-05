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

        private Dictionary<string, string> CalendarDefinitions = new Dictionary<string, string>();

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
            return new SupportReport(correctionNamesPath).Get(Calendar[month]);
        }

        public void ExportCalendar(string path, string month)
        {
            var events = Calendar[month];
            foreach (var person in events.Select(e => e.Person).Distinct())
            {
                var personEvents = events.Where(e => e.Person.Equals(person)).ToList();
                new ICSCalendar() { Events = Prepare(personEvents).ToList(), Definition = CalendarDefinitions }.Set($"{path}\\{person}.ics");
            }
        }

        public IEnumerable<ICSEvent> Prepare(IEnumerable<SupportEvent> events)
        {
            var result = events.Select(e => e.Event);
            result.ToList().ForEach(e => e.Summary = $"{e.Summary} Support");
            return result;
        }

        private void Parse(ICSCalendar calendar)
        {
            Calendar.Clear();
            //2017.09.01 je prvni den pocitani supportu
            var validCalendar = calendar.Events.Where(e => e.StartDate > new DateTime(2017, 8, 31)).OrderBy(e => e.StartDate.Year).ThenBy(e => e.StartDate.Month);
            foreach (ICSEvent icsEvent in validCalendar)
            {
                var supportEvent = new SupportEvent(icsEvent, Holidays.IsHoliday(icsEvent.StartDate));
                var key = $"{supportEvent.Date.Year}/{supportEvent.Date.Month}";
                if (!Calendar.ContainsKey(key))
                {
                    Calendar[key] = new List<SupportEvent>();
                }
                Calendar[key].Add(supportEvent);
            }

            CalendarDefinitions = calendar.Definition;
        }

        private Holidays Holidays = new Holidays();
    }
}
