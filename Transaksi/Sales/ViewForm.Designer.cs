#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace Sales
{
    partial class ViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewForm));
            this.pnlCommand = new System.Windows.Forms.Panel();
            this.btnNext = new Syncfusion.WinForms.Controls.SfButton();
            this.btnPrev = new Syncfusion.WinForms.Controls.SfButton();
            this.btnEdit = new Syncfusion.WinForms.Controls.SfButton();
            this.grpInput = new System.Windows.Forms.GroupBox();
            this.txtTotal = new System.Windows.Forms.NumericUpDown();
            this.txtPrice = new System.Windows.Forms.NumericUpDown();
            this.txtNetto = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFinished = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtReceiptCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpArrived = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtDOCode = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtReceiptDriver = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtReceiptLicensePlate = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpReceiptDate = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.txtReceiptNote = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtReceiptRelation = new System.Windows.Forms.TextBox();
            this.pnlCommand.SuspendLayout();
            this.grpInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNetto)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCommand
            // 
            this.pnlCommand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.pnlCommand.Controls.Add(this.btnNext);
            this.pnlCommand.Controls.Add(this.btnPrev);
            this.pnlCommand.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCommand.Location = new System.Drawing.Point(0, 0);
            this.pnlCommand.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCommand.Name = "pnlCommand";
            this.pnlCommand.Size = new System.Drawing.Size(910, 73);
            this.pnlCommand.TabIndex = 27;
            // 
            // btnNext
            // 
            this.btnNext.AccessibleName = "btnNext";
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.ImageMargin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.btnNext.ImageSize = new System.Drawing.Size(28, 28);
            this.btnNext.Location = new System.Drawing.Point(75, 5);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(56, 58);
            this.btnNext.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.btnNext.Style.DisabledImage = ((System.Drawing.Image)(resources.GetObject("btnNext.Style.DisabledImage")));
            this.btnNext.Style.FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(229)))), ((int)(((byte)(243)))));
            this.btnNext.Style.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(229)))), ((int)(((byte)(243)))));
            this.btnNext.Style.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Style.Image")));
            this.btnNext.Style.PressedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(199)))), ((int)(((byte)(213)))));
            this.btnNext.TabIndex = 28;
            this.btnNext.Text = "&Next";
            this.btnNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNext.TextMargin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.AccessibleName = "btnPrev";
            this.btnPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.btnPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnPrev.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.Image")));
            this.btnPrev.ImageMargin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.btnPrev.ImageSize = new System.Drawing.Size(28, 28);
            this.btnPrev.Location = new System.Drawing.Point(11, 5);
            this.btnPrev.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(56, 58);
            this.btnPrev.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.btnPrev.Style.DisabledImage = ((System.Drawing.Image)(resources.GetObject("btnPrev.Style.DisabledImage")));
            this.btnPrev.Style.FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(229)))), ((int)(((byte)(243)))));
            this.btnPrev.Style.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(229)))), ((int)(((byte)(243)))));
            this.btnPrev.Style.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.Style.Image")));
            this.btnPrev.Style.PressedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(199)))), ((int)(((byte)(213)))));
            this.btnPrev.TabIndex = 27;
            this.btnPrev.Text = "&Prev";
            this.btnPrev.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrev.TextMargin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnPrev.UseVisualStyleBackColor = false;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
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
            // grpInput
            // 
            this.grpInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.grpInput.Controls.Add(this.txtTotal);
            this.grpInput.Controls.Add(this.txtPrice);
            this.grpInput.Controls.Add(this.txtNetto);
            this.grpInput.Controls.Add(this.label5);
            this.grpInput.Controls.Add(this.dtpFinished);
            this.grpInput.Controls.Add(this.label2);
            this.grpInput.Controls.Add(this.label7);
            this.grpInput.Controls.Add(this.txtReceiptCode);
            this.grpInput.Controls.Add(this.label6);
            this.grpInput.Controls.Add(this.label4);
            this.grpInput.Controls.Add(this.label1);
            this.grpInput.Controls.Add(this.dtpArrived);
            this.grpInput.Controls.Add(this.label3);
            this.grpInput.Controls.Add(this.txtNote);
            this.grpInput.Controls.Add(this.lblName);
            this.grpInput.Controls.Add(this.lblCode);
            this.grpInput.Controls.Add(this.txtDOCode);
            this.grpInput.Controls.Add(this.txtCode);
            this.grpInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInput.Location = new System.Drawing.Point(3, 76);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(443, 336);
            this.grpInput.TabIndex = 33;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "Input Data";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(128, 238);
            this.txtTotal.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(269, 20);
            this.txtTotal.TabIndex = 8;
            this.txtTotal.ThousandsSeparator = true;
            // 
            // txtPrice
            // 
            this.txtPrice.DecimalPlaces = 2;
            this.txtPrice.Location = new System.Drawing.Point(128, 208);
            this.txtPrice.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.ReadOnly = true;
            this.txtPrice.Size = new System.Drawing.Size(269, 20);
            this.txtPrice.TabIndex = 7;
            this.txtPrice.ThousandsSeparator = true;
            // 
            // txtNetto
            // 
            this.txtNetto.Location = new System.Drawing.Point(128, 178);
            this.txtNetto.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtNetto.Name = "txtNetto";
            this.txtNetto.ReadOnly = true;
            this.txtNetto.Size = new System.Drawing.Size(269, 20);
            this.txtNetto.TabIndex = 6;
            this.txtNetto.ThousandsSeparator = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(82, 240);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 64;
            this.label5.Text = "Total :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFinished
            // 
            this.dtpFinished.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpFinished.Enabled = false;
            this.dtpFinished.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFinished.Location = new System.Drawing.Point(128, 149);
            this.dtpFinished.Name = "dtpFinished";
            this.dtpFinished.Size = new System.Drawing.Size(269, 20);
            this.dtpFinished.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 153);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 62;
            this.label2.Text = "* Waktu Kembali :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 34);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 13);
            this.label7.TabIndex = 60;
            this.label7.Text = "* No. Tanda Terima :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReceiptCode
            // 
            this.txtReceiptCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReceiptCode.Location = new System.Drawing.Point(128, 31);
            this.txtReceiptCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtReceiptCode.MaxLength = 250;
            this.txtReceiptCode.Name = "txtReceiptCode";
            this.txtReceiptCode.ReadOnly = true;
            this.txtReceiptCode.Size = new System.Drawing.Size(269, 20);
            this.txtReceiptCode.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 210);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 58;
            this.label6.Text = "* Harga/Kg (Jual) :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 180);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "* Netto (Kg) :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 123);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "* Waktu Tiba :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpArrived
            // 
            this.dtpArrived.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpArrived.Enabled = false;
            this.dtpArrived.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpArrived.Location = new System.Drawing.Point(128, 119);
            this.dtpArrived.Name = "dtpArrived";
            this.dtpArrived.Size = new System.Drawing.Size(269, 20);
            this.dtpArrived.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 267);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Keterangan :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNote
            // 
            this.txtNote.AccessibleName = "txtNote";
            this.txtNote.Location = new System.Drawing.Point(128, 267);
            this.txtNote.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNote.MaxLength = 250;
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ReadOnly = true;
            this.txtNote.Size = new System.Drawing.Size(269, 48);
            this.txtNote.TabIndex = 9;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(63, 93);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(56, 13);
            this.lblName.TabIndex = 33;
            this.lblName.Text = "* No. DO :";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(49, 64);
            this.lblCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(70, 13);
            this.lblCode.TabIndex = 32;
            this.lblCode.Text = "* No. Ticket :";
            this.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDOCode
            // 
            this.txtDOCode.Location = new System.Drawing.Point(128, 91);
            this.txtDOCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDOCode.MaxLength = 250;
            this.txtDOCode.Name = "txtDOCode";
            this.txtDOCode.ReadOnly = true;
            this.txtDOCode.Size = new System.Drawing.Size(269, 20);
            this.txtDOCode.TabIndex = 3;
            // 
            // txtCode
            // 
            this.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCode.Location = new System.Drawing.Point(128, 61);
            this.txtCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCode.MaxLength = 250;
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(269, 20);
            this.txtCode.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtReceiptDriver);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtReceiptLicensePlate);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.dtpReceiptDate);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtReceiptNote);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtReceiptRelation);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.No;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(452, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 249);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Info Tanda Terima";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(45, 123);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 54;
            this.label9.Text = "Nama Supir :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReceiptDriver
            // 
            this.txtReceiptDriver.Location = new System.Drawing.Point(121, 120);
            this.txtReceiptDriver.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtReceiptDriver.MaxLength = 250;
            this.txtReceiptDriver.Name = "txtReceiptDriver";
            this.txtReceiptDriver.ReadOnly = true;
            this.txtReceiptDriver.Size = new System.Drawing.Size(269, 20);
            this.txtReceiptDriver.TabIndex = 53;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(29, 93);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 13);
            this.label10.TabIndex = 52;
            this.label10.Text = "No. Kendaraan :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReceiptLicensePlate
            // 
            this.txtReceiptLicensePlate.Location = new System.Drawing.Point(121, 90);
            this.txtReceiptLicensePlate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtReceiptLicensePlate.MaxLength = 250;
            this.txtReceiptLicensePlate.Name = "txtReceiptLicensePlate";
            this.txtReceiptLicensePlate.ReadOnly = true;
            this.txtReceiptLicensePlate.Size = new System.Drawing.Size(269, 20);
            this.txtReceiptLicensePlate.TabIndex = 51;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(62, 34);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 13);
            this.label11.TabIndex = 50;
            this.label11.Text = "* Waktu :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpReceiptDate
            // 
            this.dtpReceiptDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpReceiptDate.Enabled = false;
            this.dtpReceiptDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReceiptDate.Location = new System.Drawing.Point(121, 32);
            this.dtpReceiptDate.Name = "dtpReceiptDate";
            this.dtpReceiptDate.Size = new System.Drawing.Size(269, 20);
            this.dtpReceiptDate.TabIndex = 49;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(46, 153);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 13);
            this.label12.TabIndex = 36;
            this.label12.Text = "Keterangan :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReceiptNote
            // 
            this.txtReceiptNote.AccessibleName = "txtNote";
            this.txtReceiptNote.Location = new System.Drawing.Point(121, 150);
            this.txtReceiptNote.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtReceiptNote.MaxLength = 250;
            this.txtReceiptNote.Multiline = true;
            this.txtReceiptNote.Name = "txtReceiptNote";
            this.txtReceiptNote.ReadOnly = true;
            this.txtReceiptNote.Size = new System.Drawing.Size(269, 48);
            this.txtReceiptNote.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(70, 63);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 13);
            this.label13.TabIndex = 32;
            this.label13.Text = "* Mitra :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReceiptRelation
            // 
            this.txtReceiptRelation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReceiptRelation.Location = new System.Drawing.Point(121, 60);
            this.txtReceiptRelation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtReceiptRelation.MaxLength = 250;
            this.txtReceiptRelation.Name = "txtReceiptRelation";
            this.txtReceiptRelation.ReadOnly = true;
            this.txtReceiptRelation.Size = new System.Drawing.Size(269, 20);
            this.txtReceiptRelation.TabIndex = 1;
            // 
            // ViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.CaptionAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.CaptionBarHeight = 35;
            this.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ClientSize = new System.Drawing.Size(910, 446);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpInput);
            this.Controls.Add(this.pnlCommand);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.MinimizeBox = false;
            this.Name = "ViewForm";
            this.ShowInTaskbar = false;
            this.ShowMaximizeBox = false;
            this.ShowMinimizeBox = false;
            this.Text = "Penjualan ke Pabrik - View";
            this.Load += new System.EventHandler(this.EditForm_Load);
            this.pnlCommand.ResumeLayout(false);
            this.grpInput.ResumeLayout(false);
            this.grpInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNetto)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlCommand;
        private Syncfusion.WinForms.Controls.SfButton btnEdit;
        private Syncfusion.WinForms.Controls.SfButton btnPrev;
        private Syncfusion.WinForms.Controls.SfButton btnNext;
        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.NumericUpDown txtTotal;
        private System.Windows.Forms.NumericUpDown txtPrice;
        private System.Windows.Forms.NumericUpDown txtNetto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFinished;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtReceiptCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpArrived;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtDOCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtReceiptDriver;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtReceiptLicensePlate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpReceiptDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtReceiptNote;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtReceiptRelation;
    }
}