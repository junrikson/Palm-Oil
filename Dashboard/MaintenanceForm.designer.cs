#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace Dashboard
{
    partial class MaintenanceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaintenanceForm));
            this.btnEdit = new Syncfusion.WinForms.Controls.SfButton();
            this.pnlCommand = new System.Windows.Forms.Panel();
            this.Run = new Syncfusion.WinForms.Controls.SfButton();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.grpInput = new System.Windows.Forms.GroupBox();
            this.cbDeleteLog = new System.Windows.Forms.CheckBox();
            this.cbMaintenanceIndex = new System.Windows.Forms.CheckBox();
            this.pnlCommand.SuspendLayout();
            this.grpInput.SuspendLayout();
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
            // pnlCommand
            // 
            this.pnlCommand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.pnlCommand.Controls.Add(this.Run);
            this.pnlCommand.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCommand.Location = new System.Drawing.Point(0, 0);
            this.pnlCommand.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCommand.Name = "pnlCommand";
            this.pnlCommand.Size = new System.Drawing.Size(430, 73);
            this.pnlCommand.TabIndex = 27;
            // 
            // Run
            // 
            this.Run.AccessibleName = "btnRun";
            this.Run.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.Run.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Run.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Run.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.Run.Image = ((System.Drawing.Image)(resources.GetObject("Run.Image")));
            this.Run.ImageMargin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.Run.ImageSize = new System.Drawing.Size(28, 28);
            this.Run.Location = new System.Drawing.Point(11, 5);
            this.Run.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(56, 58);
            this.Run.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.Run.Style.DisabledImage = ((System.Drawing.Image)(resources.GetObject("Run.Style.DisabledImage")));
            this.Run.Style.FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(229)))), ((int)(((byte)(243)))));
            this.Run.Style.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(229)))), ((int)(((byte)(243)))));
            this.Run.Style.Image = ((System.Drawing.Image)(resources.GetObject("Run.Style.Image")));
            this.Run.Style.PressedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(199)))), ((int)(((byte)(213)))));
            this.Run.TabIndex = 22;
            this.Run.Text = "&Run";
            this.Run.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Run.TextMargin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.Run.UseVisualStyleBackColor = false;
            this.Run.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.SelectedPath = "C:\\";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(20, 92);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(384, 129);
            this.txtStatus.TabIndex = 5;
            // 
            // grpInput
            // 
            this.grpInput.Controls.Add(this.txtStatus);
            this.grpInput.Controls.Add(this.cbDeleteLog);
            this.grpInput.Controls.Add(this.cbMaintenanceIndex);
            this.grpInput.Location = new System.Drawing.Point(3, 76);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(420, 235);
            this.grpInput.TabIndex = 26;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "Maintenance Tasks";
            // 
            // cbDeleteLog
            // 
            this.cbDeleteLog.AutoSize = true;
            this.cbDeleteLog.Checked = true;
            this.cbDeleteLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDeleteLog.Location = new System.Drawing.Point(20, 55);
            this.cbDeleteLog.Name = "cbDeleteLog";
            this.cbDeleteLog.Size = new System.Drawing.Size(202, 17);
            this.cbDeleteLog.TabIndex = 8;
            this.cbDeleteLog.Text = "Delete and Shrink Database and Log";
            this.cbDeleteLog.UseVisualStyleBackColor = true;
            // 
            // cbMaintenanceIndex
            // 
            this.cbMaintenanceIndex.AutoSize = true;
            this.cbMaintenanceIndex.Checked = true;
            this.cbMaintenanceIndex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMaintenanceIndex.Location = new System.Drawing.Point(20, 32);
            this.cbMaintenanceIndex.Name = "cbMaintenanceIndex";
            this.cbMaintenanceIndex.Size = new System.Drawing.Size(169, 17);
            this.cbMaintenanceIndex.TabIndex = 7;
            this.cbMaintenanceIndex.Text = "Reorganize and Rebuild Index";
            this.cbMaintenanceIndex.UseVisualStyleBackColor = true;
            // 
            // MaintenanceForm
            // 
            this.AcceptButton = this.Run;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.CaptionAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.CaptionBarHeight = 35;
            this.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ClientSize = new System.Drawing.Size(430, 321);
            this.Controls.Add(this.pnlCommand);
            this.Controls.Add(this.grpInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.MinimizeBox = false;
            this.Name = "MaintenanceForm";
            this.ShowInTaskbar = false;
            this.ShowMaximizeBox = false;
            this.ShowMinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Maintenance Database";
            this.Load += new System.EventHandler(this.MaintenanceForm_Load);
            this.pnlCommand.ResumeLayout(false);
            this.grpInput.ResumeLayout(false);
            this.grpInput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Syncfusion.WinForms.Controls.SfButton btnEdit;
        private System.Windows.Forms.Panel pnlCommand;
        private Syncfusion.WinForms.Controls.SfButton Run;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.CheckBox cbMaintenanceIndex;
        private System.Windows.Forms.CheckBox cbDeleteLog;
    }
}