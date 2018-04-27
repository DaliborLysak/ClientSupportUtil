using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientSupport
{
    public class SupportPersonReport : SupportReportObject
    {
        public SupportPersonReport()
        {
            Init();
        }

        public string Name { get; set; }

        public SupportHelper SupportHelper { get; set; }

        private List<SupportEvent> Events { get; set; } = new List<SupportEvent>();

        private bool OldWeekendDefinition = false;

        public void AddEvents(SupportEvent supportEvent)
        {
            Events.Add(supportEvent);
            if (supportEvent.OldWeekendDefinition)
            {
                OldWeekendDefinition = supportEvent.OldWeekendDefinition; // predpoklad je ze v celem mesici je definice stejna !!!
            }
            var supportType = supportEvent.SupportType;
            if (DaysByType.ContainsKey(supportType))
            {
                DaysByType[supportType]++;
            }
        }

        public string Get()
        {
            var fullName = SupportHelper?.TranslateNames(Name);
            string hoursSummary = HoursSummary > 99 ? HoursSummary.ToString() : $" {HoursSummary}";
            return $"{fullName} {new string(' ', SupportHelper.LineLengthCorrection - fullName.Length)}{hoursSummary}{NumberDataSplitter}{GetTypeSummary(false)}{Environment.NewLine}";
        }

        public override bool IsOldWeekendDefinition()
        {
            return OldWeekendDefinition;
        }
    }
}
