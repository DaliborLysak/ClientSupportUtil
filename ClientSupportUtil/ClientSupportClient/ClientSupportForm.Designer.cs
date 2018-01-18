namespace ClientSupportClient
{
    partial class ClientSupportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientSupportForm));
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.listBoxMonths = new System.Windows.Forms.ListBox();
            this.richTextBoxReport = new System.Windows.Forms.RichTextBox();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonLoadCalendar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExportCalendars = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMain.Location = new System.Drawing.Point(0, 28);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.listBoxMonths);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.richTextBoxReport);
            this.splitContainerMain.Size = new System.Drawing.Size(927, 495);
            this.splitContainerMain.SplitterDistance = 155;
            this.splitContainerMain.TabIndex = 1;
            // 
            // listBoxMonths
            // 
            this.listBoxMonths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMonths.FormattingEnabled = true;
            this.listBoxMonths.Location = new System.Drawing.Point(0, 0);
            this.listBoxMonths.Name = "listBoxMonths";
            this.listBoxMonths.Size = new System.Drawing.Size(155, 495);
            this.listBoxMonths.TabIndex = 0;
            this.listBoxMonths.Click += new System.EventHandler(this.listBoxMonths_Click);
            // 
            // richTextBoxReport
            // 
            this.richTextBoxReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxReport.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxReport.Name = "richTextBoxReport";
            this.richTextBoxReport.Size = new System.Drawing.Size(768, 495);
            this.richTextBoxReport.TabIndex = 0;
            this.richTextBoxReport.Text = "";
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonLoadCalendar,
            this.toolStripButtonExportCalendars});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(927, 25);
            this.toolStripMain.TabIndex = 2;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripButtonLoadCalendar
            // 
            this.toolStripButtonLoadCalendar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonLoadCalendar.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLoadCalendar.Image")));
            this.toolStripButtonLoadCalendar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoadCalendar.Name = "toolStripButtonLoadCalendar";
            this.toolStripButtonLoadCalendar.Size = new System.Drawing.Size(87, 22);
            this.toolStripButtonLoadCalendar.Text = "Load Calendar";
            this.toolStripButtonLoadCalendar.Click += new System.EventHandler(this.toolStripButtonLoadCalendar_Click);
            // 
            // toolStripButtonExportCalendars
            // 
            this.toolStripButtonExportCalendars.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonExportCalendars.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExportCalendars.Image")));
            this.toolStripButtonExportCalendars.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportCalendars.Name = "toolStripButtonExportCalendars";
            this.toolStripButtonExportCalendars.Size = new System.Drawing.Size(99, 22);
            this.toolStripButtonExportCalendars.Text = "Export Calendars";
            this.toolStripButtonExportCalendars.Click += new System.EventHandler(this.toolStripButtonExportCalendars_Click);
            // 
            // ClientSupportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 523);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.splitContainerMain);
            this.Name = "ClientSupportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Client Support";
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoadCalendar;
        private System.Windows.Forms.ToolStripButton toolStripButtonExportCalendars;
        private System.Windows.Forms.ListBox listBoxMonths;
        private System.Windows.Forms.RichTextBox richTextBoxReport;
    }
}

