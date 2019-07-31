#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.WinForms.Controls;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace Dashboard
{
    public partial class MainForm : Syncfusion.Windows.Forms.MetroForm
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";

        public MainForm(string username)
        {
            this.username = username;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Grid_Load();
            Menu_Load();
        }

        private void Menu_Load()
        {
            string query = "SELECT d.code FROM masterUser a " +
                "INNER JOIN masterRoles b ON a.rolesCode = b.code " +
                "INNER JOIN masterRolesDetail c ON b.ID = c.rolesID " +
                "INNER JOIN masterMenu d ON c.menuCode = d.code " +
                "WHERE a.code = @username AND c.[active] = 0";
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = this.username;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DisableMenu(reader["code"].ToString());
                            }
                        }

                        if (con.State == System.Data.ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        
        private void DisableMenu(string code)
        {
            switch(code)
            {
                //A0000 Master
                case "A0000":
                    masterToolStripMenuItem.Visible = false;
                    break;
                //A0001 Master Relasi
                case "A0001":
                    masterRelasiToolStripMenuItem.Visible = false;
                    btnMasterRelation.Enabled = false;
                    break;
                //A0002 Master Relasi
                case "A0002":
                    masterMillToolStripMenuItem.Visible = false;
                    btnMasterMill.Enabled = false;
                    break;
                //B0000 Transaksi
                case "B0000":
                    transaksiToolStripMenuItem.Visible = false;
                    break;
                //B0001 Tanda Terima
                case "B0001":
                    tandaTerimaToolStripMenuItem.Visible = false;
                    btnReceipt.Enabled = false;
                    break;
                //B0002 Penjualan Ke Pabrik
                case "B0002":
                    penjualanKePabrikToolStripMenuItem.Visible = false;
                    btnSales.Enabled = false;
                    break;
                //B0003 Invoice Pembelian
                case "B0003":
                    invoicePembelianToolStripMenuItem.Visible = false;
                    btnPurchase.Enabled = false;
                    break;
                //C0000 Pembayaran
                case "C0000":
                    pembayaranToolStripMenuItem.Visible = false;
                    break;
                //C0001 Pembayaran Invoice
                case "C0001":
                    pembayaranInvoiceToolStripMenuItem.Visible = false;
                    btnPurchase.Enabled = false;
                    break;
                //C0002 Pelunasan (Pabrik)
                case "C0002":
                    pelunasanPabrikToolStripMenuItem.Visible = false;
                    btnRepayment.Enabled = false;
                    break;
                //D0000 Settings
                case "D0000":
                    settingsToolStripMenuItem.Visible = false;
                    break;
                //D0001 Menu Manager
                case "D0001":
                    menuManagerToolStripMenuItem.Visible = false;
                    break;
                //D0002 Roles
                case "D0002":
                    rolesToolStripMenuItem.Visible = false;
                    break;
                //D0003 User Manager
                case "D0003":
                    userManagerToolStripMenuItem.Visible = false;
                    break;
                //E0000 Tools
                case "E0000":
                    toolsToolStripMenuItem.Visible = false;
                    break;
                //E0001 BackupDatabase
                case "E0001":
                    backupDatabaseToolStripMenuItem.Visible = false;
                    btnBackupDatabase.Enabled = false;
                    break;
                //E0002 Maintenance Database
                case "E0002":
                    maintenanceDatabaseToolStripMenuItem.Visible = false;
                    break;
                //E0003 Hapus Data
                case "E0003":
                    hapusDataToolStripMenuItem.Visible = false;
                    break;
                //E0004 Ganti Password
                case "E0004":
                    gantiPasswordToolStripMenuItem.Visible = false;
                    btnChangePassword.Enabled = false;
                    break;
                //F0000 Reports
                case "F0000":
                    reportsToolStripMenuItem.Visible = false;
                    break;
                //F0001 Penjualan Per Tanggal
                case "F0001":
                    penjualanPerTanggalToolStripMenuItem.Visible = false;
                    break;
                //F0002 Pembelian Per Tanggal
                case "F0002":
                    pembelianPerTanggalToolStripMenuItem.Visible = false;
                    break;
                //F0003 Pembayaran Per Tanggal
                case "F0003":
                    pembayaranPerTanggalToolStripMenuItem.Visible = false;
                    break;
                //F0004 Pelunasan Per Tanggal
                case "F0004":
                    pelunasanPerTanggalToolStripMenuItem.Visible = false;
                    break;
                //F0005 Keuntungan (Bruto)
                case "F0005":
                    keuntunganBrutoToolStripMenuItem.Visible = false;
                    break;
            }

        }

        private void btnMasterRelation_Click(object sender, EventArgs e)
        {
            try
            {
                Process exe = new Process();
                exe.StartInfo.FileName = "MasterRelation.exe";
                exe.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnMasterMill_Click(object sender, EventArgs e)
        {
            try
            {
                Process exe = new Process();
                exe.StartInfo.FileName = "MasterMill.exe";
                exe.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                Process exe = new Process();
                exe.StartInfo.FileName = "Receipt.exe";
                exe.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            try
            {
                Process exe = new Process();
                exe.StartInfo.FileName = "Sales.exe";
                exe.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        
        private void btnPurchase_Click(object sender, EventArgs e)
        {
            try
            {
                Process exe = new Process();
                exe.StartInfo.FileName = "Purchase.exe";
                exe.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                Process exe = new Process();
                exe.StartInfo.FileName = "Payment.exe";
                exe.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        
        private void btnRepayment_Click(object sender, EventArgs e)
        {
            try
            {
                Process exe = new Process();
                exe.StartInfo.FileName = "Repayment.exe";
                exe.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void menuManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process exe = new Process();
                exe.StartInfo.FileName = "MenuManager.exe";
                exe.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        
        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process exe = new Process();
                exe.StartInfo.FileName = "Roles.exe";
                exe.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void userManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process exe = new Process();
                exe.StartInfo.FileName = "UserManager.exe";
                exe.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void masterRelasiToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void masterMillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process exe = new Process();
                exe.StartInfo.FileName = "MasterMill.exe";
                exe.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void tandaTerimaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process exe = new Process();
                exe.StartInfo.FileName = "Receipt.exe";
                exe.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void penjualanKePabrikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process exe = new Process();
                exe.StartInfo.FileName = "Sales.exe";
                exe.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void invoicePembelianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process exe = new Process();
                exe.StartInfo.FileName = "Purchase.exe";
                exe.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void pembayaranInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process exe = new Process();
                exe.StartInfo.FileName = "Payment.exe";
                exe.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void pelunasanPabrikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process exe = new Process();
                exe.StartInfo.FileName = "Repayment.exe";
                exe.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void gantiPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePasswordForm = new ChangePasswordForm(username);
            changePasswordForm.ShowDialog();
            this.SuspendLayout();
        }

        private void sfButton5_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePasswordForm = new ChangePasswordForm(username);
            changePasswordForm.ShowDialog();
            this.SuspendLayout();
        }

        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void maximizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void aboutStowagePlanSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm(username);
            aboutForm.ShowDialog();
            this.SuspendLayout();
        }

        private void backupDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupForm backupForm = new BackupForm(username);
            backupForm.ShowDialog();
            this.SuspendLayout();
        }

        private void btnBackupDatabase_Click(object sender, EventArgs e)
        { 
            BackupForm backupForm = new BackupForm(username);
            backupForm.ShowDialog();
            this.SuspendLayout();
        }

        private void hapusDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteDataForm deleteDataForm = new DeleteDataForm(username);
            deleteDataForm.ShowDialog();
            this.SuspendLayout();
        }

        private void maintenanceDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaintenanceForm maintenanceForm = new MaintenanceForm(username);
            maintenanceForm.ShowDialog();
            this.SuspendLayout();
        }

        private void penjualanPerTanggalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            choosePenjualanPerTanggalForm obj = new choosePenjualanPerTanggalForm();
            obj.ShowDialog();
            this.SuspendLayout();
        }

        private void pembelianPerTanggalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            choosePembelianPerTanggalForm obj = new choosePembelianPerTanggalForm();
            obj.ShowDialog();
            this.SuspendLayout();
        }

        private void pembayaranPerTanggalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            choosePembayaranPerTanggalForm obj = new choosePembayaranPerTanggalForm();
            obj.ShowDialog();
            this.SuspendLayout();
        }

        private void pelunasanPerTanggalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            choosePelunasanPerTanggalForm obj = new choosePelunasanPerTanggalForm();
            obj.ShowDialog();
            this.SuspendLayout();
        }

        private void keuntunganBrutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chooseKeuntunganBrutoForm obj = new chooseKeuntunganBrutoForm();
            obj.ShowDialog();
            this.SuspendLayout();
        }

        public void Grid_Load(string code = "", string container = "", string seal = "", string sender = "", string receiver = "", string type = "", string brand = "", string note = "")
        {
            try
            {
                String sqlString = "SELECT id, date, code, relationCode, relationName, total, (total-paid) as remaining " +
                    "FROM View_PurchasePayment WHERE " +
                    "(total-paid) > 0 " +
                    "ORDER BY date DESC, code DESC ";

                SqlDataAdapter da = new SqlDataAdapter(sqlString, connString);

                DataSet ds = new DataSet();
                da.Fill(ds, "data");

                DataTable dt = new DataTable();
                dt = ds.Tables["data"];

                gridView2.DataSource = dt;

                gridView2.Columns[0].Visible = false;
                gridView2.Columns[0].HeaderText = "ID";
                gridView2.Columns[1].HeaderText = "Tanggal";
                gridView2.Columns[2].HeaderText = "No. Invoice";
                gridView2.Columns[3].HeaderText = "Kode Mitra";
                gridView2.Columns[4].HeaderText = "Nama Mitra";
                gridView2.Columns[5].HeaderText = "Jumlah Invoice";
                gridView2.Columns[6].HeaderText = "Sisa Invoice";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            try
            {
                String sqlString = "SELECT id, arrived, code, DOCode, receiptCode, relationCode, relationName, total, (total-paid) as remaining " +
                    "FROM View_SalesRepayment WHERE " +
                    "(total-paid) > 0 " +
                    "ORDER BY arrived DESC, code DESC ";

                SqlDataAdapter da = new SqlDataAdapter(sqlString, connString);

                DataSet ds = new DataSet();
                da.Fill(ds, "data");

                DataTable dt = new DataTable();
                dt = ds.Tables["data"];

                gridView.DataSource = dt;

                gridView.Columns[0].Visible = false;
                gridView.Columns[0].HeaderText = "ID";
                gridView.Columns[1].HeaderText = "Waktu Tiba";
                gridView.Columns[2].HeaderText = "No. Ticket";
                gridView.Columns[3].HeaderText = "No. DO";
                gridView.Columns[4].HeaderText = "No. Tanda Terima";
                gridView.Columns[5].HeaderText = "Kode Mitra";
                gridView.Columns[6].HeaderText = "Nama Mitra";
                gridView.Columns[7].HeaderText = "Jumlah Invoice";
                gridView.Columns[8].HeaderText = "Sisa Invoice";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public static bool CloseCancel()
        {
            const string message = "Apakah anda yakin mau menutup aplikasi?";
            const string caption = "Menutup Aplikasi";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseCancel() == false)
            {
                e.Cancel = true;
            };
        }
    }
}
