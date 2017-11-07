using ClientSupport;
using ICSWrapper;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;

namespace ClientSupportClient
{
    public partial class ClientSupportForm : Form
    {
        public ClientSupportForm()
        {
            InitializeComponent();
        }

        private SupportProcessor Processor = new SupportProcessor();

        private void toolStripButtonLoadCalendar_Click(object sender, EventArgs e)
        {
            var calendar = new ICSCalendar();
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (!String.IsNullOrEmpty(dialog.FileName))
                    {
                        toolStripMain.Enabled = false;
                        Cursor.Current = Cursors.WaitCursor;
                        try
                        {
                            Processor.LoadCalendar(dialog.FileName);
                        }
                        finally
                        {
                            toolStripMain.Enabled = true;
                            Cursor.Current = Cursors.Default;
                        }
                    }
                }
                var months = Processor.GetMonths();
                listBoxMonths.Items.Clear();
                listBoxMonths.BeginUpdate();
                foreach (var month in months)
                {
                    listBoxMonths.Items.Add(month);
                }
                listBoxMonths.EndUpdate();
            }
        }

        private void listBoxMonths_Click(object sender, EventArgs e)
        {
            string month = listBoxMonths.SelectedItem.ToString();
            richTextBoxReport.Text = Processor.GetMonthReport(month, Properties.Settings.Default.PersonNames);
        }
    }
}
