#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace Dashboard
{
    partial class choosePenjualanPerTanggalForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(choosePenjualanPerTanggalForm));
            this.btnEdit = new Syncfusion.WinForms.Controls.SfButton();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOpen = new Syncfusion.WinForms.Controls.SfButton();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.crystalReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEdit
            // 
            this.btnEdit.AccessibleName = "btnEdit";
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnEdit.ImageMargin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.btnEdit.ImageSize = new System.Drawing.Size(28, 28);
            this.btnEdit.Location = new System.Drawing.Point(75, 5);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(56, 58);
            this.btnEdit.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.btnEdit.Style.FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(229)))), ((int)(((byte)(243)))));
            this.btnEdit.Style.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(229)))), ((int)(((byte)(243)))));
            this.btnEdit.Style.PressedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(199)))), ((int)(((byte)(213)))));
            this.btnEdit.TabIndex = 23;
            this.btnEdit.Text = "Edit";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.TextMargin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.SelectedPath = "C:\\";
            // 
            // dtpBegin
            // 
            this.dtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBegin.Location = new System.Drawing.Point(58, 6);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(109, 20);
            this.dtpBegin.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOpen);
            this.panel1.Controls.Add(this.dtpEnd);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpBegin);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(873, 35);
            this.panel1.TabIndex = 1;
            // 
            // btnOpen
            // 
            this.btnOpen.AccessibleName = "btnSearch";
            this.btnOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnOpen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOpen.ImageMargin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.btnOpen.ImageSize = new System.Drawing.Size(28, 28);
            this.btnOpen.Location = new System.Drawing.Point(323, 0);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(78, 31);
            this.btnOpen.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnOpen.Style.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnOpen.Style.DisabledForeColor = System.Drawing.Color.Silver;
            this.btnOpen.Style.FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnOpen.Style.FocusedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOpen.Style.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOpen.Style.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(229)))), ((int)(((byte)(243)))));
            this.btnOpen.Style.PressedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnOpen.Style.PressedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "Open";
            this.btnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOpen.TextMargin = new System.Windows.Forms.Padding(0);
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(202, 6);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(109, 20);
            this.dtpEnd.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "s/d";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Periode :";
            // 
            // crystalReportViewer
            // 
            this.crystalReportViewer.ActiveViewIndex = -1;
            this.crystalReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer.EnableRefresh = false;
            this.crystalReportViewer.EnableToolTips = false;
            this.crystalReportViewer.Location = new System.Drawing.Point(0, 35);
            this.crystalReportViewer.Name = "crystalReportViewer";
            this.crystalReportViewer.ShowCloseButton = false;
            this.crystalReportViewer.ShowCopyButton = false;
            this.crystalReportViewer.ShowParameterPanelButton = false;
            this.crystalReportViewer.ShowRefreshButton = false;
            this.crystalReportViewer.Size = new System.Drawing.Size(873, 220);
            this.crystalReportViewer.TabIndex = 2;
            this.crystalReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // choosePenjualanPerTanggalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.CaptionAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.CaptionBarHeight = 35;
            this.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ClientSize = new System.Drawing.Size(873, 255);
            this.Controls.Add(this.crystalReportViewer);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.MinimizeBox = false;
            this.Name = "choosePenjualanPerTanggalForm";
            this.ShowInTaskbar = false;
            this.ShowMaximizeBox = false;
            this.ShowMinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Penjualan Per Tanggal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.choosePenjualanPerTanggalForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Syncfusion.WinForms.Controls.SfButton btnEdit;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Syncfusion.WinForms.Controls.SfButton btnOpen;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer;
    }
}