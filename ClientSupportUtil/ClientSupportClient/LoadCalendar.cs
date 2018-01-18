using System;
using System.Windows.Forms;

namespace ClientSupportClient
{
    public class LoadCalendar : MethodWithUX
    {
        protected override void Process()
        {
            base.Process();

            var monthsControl = Data?.MonthsControl;
            if (monthsControl != null)
            {
                var fileName = (Dialog as OpenFileDialog)?.FileName;
                if (!String.IsNullOrEmpty(fileName))
                {
                    Processor.LoadCalendar(fileName);

                    var months = Processor.GetMonths();
                    monthsControl.Items.Clear();
                    monthsControl.BeginUpdate();
                    foreach (var month in months)
                    {
                        monthsControl.Items.Add(month);
                    }
                    monthsControl.EndUpdate();
                }
            }
        }

        protected override CommonDialog GetDialog()
        {
            return new OpenFileDialog();
        }
    }
}
