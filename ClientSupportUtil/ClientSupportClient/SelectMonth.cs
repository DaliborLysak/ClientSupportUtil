using System;

namespace ClientSupportClient
{
    public class SelectMonth : MethodWithUX
    {
        protected override void Process()
        {
            base.Process();

            var month = Data?.MonthsControl?.SelectedItem.ToString();
            if (!String.IsNullOrEmpty(month))
            {
                var reportControl = Data?.ReportControl;
                if (reportControl != null)
                {
                    reportControl.Text = Processor.GetMonthReport(month, Properties.Settings.Default.PersonNames);
                }
            }
        }
    }
}
