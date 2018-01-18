using System;
using System.Windows.Forms;

namespace ClientSupportClient
{
    public class ExportCalendars : MethodWithUX
    {
        protected override void Process()
        {
            base.Process();

            var month = Data?.MonthsControl?.SelectedItem.ToString();
            if (!String.IsNullOrEmpty(month))
            {
                var selectedPath = (Dialog as FolderBrowserDialog)?.SelectedPath;
                if (!String.IsNullOrEmpty(selectedPath))
                {
                    Processor.ExportCalendar(selectedPath, month);
                }
            }
        }

        protected override CommonDialog GetDialog()
        {
            return new FolderBrowserDialog();
        }
    }
}
