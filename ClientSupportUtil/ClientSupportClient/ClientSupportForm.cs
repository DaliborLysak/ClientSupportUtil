using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Windows.Forms;

namespace ClientSupportClient
{
    public partial class ClientSupportForm : Form
    {
        public ClientSupportForm()
        {
            InitializeComponent();
            Processor = new UXActionProcessor()
            {
                Data = new UXData() { MonthsControl = listBoxMonths, ReportControl = richTextBoxReport },
                Actions = new Dictionary<object, MethodWithUX>()
                {
                    [toolStripButtonLoadCalendar] = new LoadCalendar(),
                    [listBoxMonths] = new SelectMonth(),
                    [toolStripButtonExportCalendars] = new ExportCalendars()
                }
            };

            EnableUX();
        }

        private UXActionProcessor Processor;

        private void toolStripButtonLoadCalendar_Click(object sender, EventArgs e)
        {
            Processor.Process(sender);
            EnableUX();
        }

        private void listBoxMonths_Click(object sender, EventArgs e)
        {
            Processor.Process(sender);
        }

        private void toolStripButtonExportCalendars_Click(object sender, EventArgs e)
        {
            Processor.Process(sender);
        }

        private void EnableUX()
        {
            toolStripButtonExportCalendars.Enabled = listBoxMonths.Items.Count > 0;
        }
    }
}
