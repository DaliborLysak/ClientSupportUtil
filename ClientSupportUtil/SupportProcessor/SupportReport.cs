using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace ClientSupport
{
    public class SupportReport : SupportReportObject
    {
        private List<SupportPersonReport> SupportPersonReports = new List<SupportPersonReport>();
        private SupportHelper SupportHelper;

        public SupportReport(string correctionNamesPath)
        {
            SupportHelper = new SupportHelper(correctionNamesPath);
        }

        protected override void Init()
        {
            base.Init();
            SupportPersonReports.Clear();
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
                    var supportPersonReport = SupportPersonReports.FirstOrDefault(p => p.Name.Equals(person));
                    if (supportPersonReport == null)
                    {
                        supportPersonReport = new SupportPersonReport() { Name = person, SupportHelper = SupportHelper };
                        SupportPersonReports.Add(supportPersonReport);
                    }
                    supportPersonReport.AddEvents(supportEvent);
                }

                var textWeekendHours = IsOldWeekendDefinition() ? "13h + 48h" : $"{2* NotWorkingDaySupportHours}h";
                var personText = GetPersons();
                var textHolidays = Holidays > 0 ? $", {Holidays}x({NotWorkingDaySupportHours}h)={HolidayHours}" : String.Empty;
                report =
                    $"{CzechMonths[dayOne.Date.Month - 1]} {dayOne.Date.Year}{Environment.NewLine}{Environment.NewLine}" +
                    $"{Vikend} - {textWeekendHours}, {Svatek} - {NotWorkingDaySupportHours}h, {PracovniDen} - {WorkingDaySupportHours}h{Environment.NewLine}{Environment.NewLine}" +
                    $"Paušálni náhrada za pohotovost = 15% průměrné hodinové mzdy.{Environment.NewLine}{Environment.NewLine}" +
                    $"{new string('=',70)}{Environment.NewLine}{Environment.NewLine}" +
                    $"{numberOfSupportMonth}. měsíc pohotovosti klientského týmu.{Environment.NewLine}{Environment.NewLine}" +
                    $"{personText}{Environment.NewLine}" +
                    $"{new string('-', 70)}{ Environment.NewLine}" +
                    $"Celkem{new string(' ', 15)}{HoursSummary}h{NumberDataSplitter}{GetTypeSummary()}{Environment.NewLine}" +
                    $"{new string(' ', 25)}{NumberDataSplitter}{Weekends}x({textWeekendHours})={WeekendHours}, {Workdays}x13h={WorkdayHours}{textHolidays}";
            }

            return report;
        }

        private string GetPersons()
        {
            var report = String.Empty;
            var reportList = new List<string>();
            foreach (var supportPersonReport in SupportPersonReports)
            {
                DaysByType[SupportEvent.SupportDayType.WorkDay] = DaysByType[SupportEvent.SupportDayType.WorkDay] + supportPersonReport.Workdays;
                DaysByType[SupportEvent.SupportDayType.Weekend] = DaysByType[SupportEvent.SupportDayType.Weekend] + supportPersonReport.Weekends;
                DaysByType[SupportEvent.SupportDayType.Holiday] = DaysByType[SupportEvent.SupportDayType.Holiday] + supportPersonReport.Holidays;
                var reportLine = supportPersonReport.Get();
                reportList.Add(reportLine);
            }
            reportList.ForEach(i => report += i);
            return report;
        }

        public override bool IsOldWeekendDefinition()
        {
            return SupportPersonReports.Where(r => r.IsOldWeekendDefinition() == true).ToList().Count > 0;
        }
    }
}
