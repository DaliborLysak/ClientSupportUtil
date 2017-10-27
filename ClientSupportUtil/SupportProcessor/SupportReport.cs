using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ClientSupport
{
    public class SupportReport
    {
        private Dictionary<string, List<SupportEvent>> DaysByPerson = new Dictionary<string, List<SupportEvent>>();
        private Dictionary<SupportEvent.SupportDayType, Dictionary<string, int>> DaysBySupportDayType;
        private int WorkdaySummary = 0;
        private int WeekendSummary = 0;
        private int HolidaySummary = 0;

        public SupportReport()
        {

        }

        private void Init()
        {
            DaysByPerson.Clear();
            DaysBySupportDayType = new Dictionary<ClientSupport.SupportEvent.SupportDayType, Dictionary<string, int>>()
            {
                [SupportEvent.SupportDayType.WorkDay] = new Dictionary<string, int>(),
                [SupportEvent.SupportDayType.Holiday] = new Dictionary<string, int>(),
                [SupportEvent.SupportDayType.Weekend] = new Dictionary<string, int>(),
            };
            WorkdaySummary = 0;
            WeekendSummary = 0;
            HolidaySummary = 0;
    }

        public string Get(List<SupportEvent> days, int numberOfSupportMonth)
        {
            var report = "Missing data.";
            Init();
            if (days.Count > 0)
            {
                var dayOne = days[0];
                var cultureInfo = new CultureInfo("cz-CZ");
                System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
                string[] CzechMonths = cultureInfo.DateTimeFormat.MonthNames;
                foreach (var supportEvent in days)
                {
                    var person = supportEvent.Person;
                    if (!DaysByPerson.ContainsKey(person))
                    {
                        DaysByPerson[person] = new List<SupportEvent>();
                    }

                    DaysByPerson[supportEvent.Person].Add(supportEvent);
                    CountDays(supportEvent);
                }

                report =
                    $"{CzechMonths[dayOne.Date.Month - 1]} {dayOne.Date.Year}{Environment.NewLine}{Environment.NewLine}" +
                    $"VÍKEND - 13H + 48, SVÁTEK - 13H + 24,  PRACOVNÍ DEN - 13H{Environment.NewLine}{Environment.NewLine}" +
                    $"PAUŠÁLNI NÁHRADA ZA POHOTOVOST = 15% PRŮMĚRNÉ HODINOVÉ MZDY.{Environment.NewLine}{Environment.NewLine}" +
                    $"========================================================================= {Environment.NewLine}{Environment.NewLine}" +
                    $"{numberOfSupportMonth}. MĚSÍC POHOTOVOSTI KLIENTSKÉHO TÝMU.{Environment.NewLine}{Environment.NewLine}" +
                    $"{GetPersons()}{Environment.NewLine}" +
                    $"---------------------------------------------{Environment.NewLine}" +
                    $"CELKEM             {GetHours(WorkdaySummary, WeekendSummary, HolidaySummary)}h // {WeekendSummary}X VÍKEND,  {WorkdaySummary}X PRACOVNÍ DEN, {HolidaySummary}X SVÁTEK{Environment.NewLine}" +
                    $"                                    // {WeekendSummary}X(13H + 48)={GetWeekendHours(WeekendSummary)}, {WorkdaySummary}X13={GetWorkdayHours(WeekendSummary)}, {HolidaySummary}X(13H+ 24)={GetHolidayHours(HolidaySummary)}";
            }

            return report;
        }

        private string GetPersons()
        {
            var report = String.Empty;

            foreach (var record in DaysByPerson)
            {
                var workdays = 0;
                var weekends = 0;
                var holidays = 0;
                (workdays, weekends, holidays) = GetDays(record.Key);
                WorkdaySummary = WorkdaySummary + workdays;
                WeekendSummary = WeekendSummary + weekends;
                HolidaySummary = HolidaySummary + holidays;
                report += $"{record.Key}  {GetHours(workdays, weekends, holidays)} // {weekends}X VÍKEND, {workdays}X PRACOVNÍ DEN, {holidays}X SVÁTEK>{Environment.NewLine}";
            }

            return report;
        }

        private int GetHours(int workdays, int weekends, int holidays)
        {
            // VÍKEND - 13H + 48, SVÁTEK - 13H + 24,  PRACOVNÍ DEN - 13H
            return GetWeekendHours(weekends) + GetHolidayHours(holidays) + GetWorkdayHours(workdays);
        }

        private int GetWorkdayHours(int days)
        {
            // PRACOVNÍ DEN - 13H
            return days * 13;
        }

        private int GetWeekendHours(int days)
        {
            // VÍKEND - 13H + 48
            return days * (13 + 48);
        }

        private int GetHolidayHours(int days)
        {
            // SVÁTEK - 13H + 24
            return days * (13 + 24);
        }

        private (int workdays, int weekends, int holidays) GetDays(string person)
        {
            return (
                GetDayCount(person, SupportEvent.SupportDayType.WorkDay), 
                GetDayCount(person, SupportEvent.SupportDayType.Weekend), 
                GetDayCount(person, SupportEvent.SupportDayType.Holiday));
        }

        private int GetDayCount(string person, SupportEvent.SupportDayType supportDayType)
        {
            var days = DaysBySupportDayType[supportDayType];
            return days.ContainsKey(person) ? days[person] : 0;
        }

        private void CountDays(SupportEvent supportEvent)
        {
            var supportType = supportEvent.SupportType;
            if (supportEvent.SupportType == supportType)
            {
                var daysBy = DaysBySupportDayType[supportType];
                if (!daysBy.ContainsKey(supportEvent.Person))
                {
                    daysBy[supportEvent.Person] = 1;
                }
                else
                {
                    daysBy[supportEvent.Person]++;
                }
            }
        }
    }
}
