#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Purchase
{
    public partial class MainForm : Syncfusion.Windows.Forms.MetroForm
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";

        public MainForm(string username)
        {
            this.username = username;
            InitializeComponent();
            GridSettings();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (CheckAuth())
            {
                Grid_Load();
                txtFilterValue.Focus();
            }
            else
            {
                MessageBox.Show("Anda tidak mempunyai Otoritas untuk mengakses menu ini!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }

        private bool CheckAuth()
        {
            bool isOk = true;

            string query = "SELECT c.[active], c.[new], c.[edit], c.[view], c.[delete], c.[print], c.[refresh] " +
                "FROM masterUser a " +
                "INNER JOIN masterRoles b ON a.rolesCode = b.code " +
                "INNER JOIN masterRolesDetail c ON b.ID = c.rolesID " +
                "INNER JOIN masterMenu d ON c.menuCode = d.code " +
                "WHERE a.code = @username AND c.menuCode = 'B0003' ";
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
                                if (reader["active"].ToString() != "1")
                                    isOk = false;
                                if (reader["new"].ToString() != "1")
                                    btnNew.Enabled = false;
                                if (reader["edit"].ToString() != "1")
                                    btnEdit.Enabled = false;
                                if (reader["view"].ToString() != "1")
                                    btnView.Enabled = false;
                                if (reader["delete"].ToString() != "1")
                                    btnDelete.Enabled = false;
                                if (reader["refresh"].ToString() != "1")
                                    btnRefresh.Enabled = false;
                                if (reader["print"].ToString() != "1")
                                    btnPrint.Enabled = false;
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
            return isOk;
        }

        void Form_Closed(object sender, FormClosedEventArgs e)
        {
            Grid_Load();
        }

        #region Actions
        private void btnNew_Click(object sender, EventArgs e)
        {
            NewForm newForm = new NewForm(username);
            newForm.FormClosed += new FormClosedEventHandler(Form_Closed);
            newForm.ShowDialog();
            this.SuspendLayout();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string id = "";

            if (gridView.SelectedCells.Count != 0)
            {
                try
                {
                    DataGridViewCell cell = gridView.SelectedCells[0];
                    DataGridViewRow row = cell.OwningRow;
                    id = row.Cells[0].Value.ToString();

                    ViewForm viewForm = new ViewForm(id, username);
                    viewForm.FormClosed += new FormClosedEventHandler(Form_Closed);
                    viewForm.ShowDialog();
                    this.SuspendLayout();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = "";

            if (gridView.SelectedCells.Count != 0)
            {
                try
                {
                    DataGridViewCell cell = gridView.SelectedCells[0];
                    DataGridViewRow row = cell.OwningRow;
                    id = row.Cells[0].Value.ToString();

                    EditForm editForm = new EditForm(id, username);
                    editForm.FormClosed += new FormClosedEventHandler(Form_Closed);
                    editForm.ShowDialog();
                    this.SuspendLayout();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string id = "";

            if (gridView.SelectedCells.Count != 0)
            {
                try
                {
                    DataGridViewCell cell = gridView.SelectedCells[0];
                    DataGridViewRow row = cell.OwningRow;
                    id = row.Cells[0].Value.ToString();

                    rptPurchaseForm report = new rptPurchaseForm(id);
                    report.ShowDialog();
                    this.SuspendLayout();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Grid_Load();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = "";
            string code = "";

            if (gridView.SelectedCells.Count != 0)
            {
                try
                {
                    DataGridViewCell cell = gridView.SelectedCells[0];
                    DataGridViewRow row = cell.OwningRow;
                    id = row.Cells[0].Value.ToString();
                    code = row.Cells[1].Value.ToString();

                    DialogResult dialogResult = MessageBox.Show("Are you sure want to delete " + code + " ?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            using (SqlConnection con = new SqlConnection(connString))
                            {
                                con.Open();
                                SqlTransaction tran = con.BeginTransaction();
                                SqlCommand cmd;

                                cmd = new SqlCommand("DELETE FROM purchaseDetails WHERE purchaseID = @purchaseID", con, tran);
                                cmd.Parameters.Add("@purchaseID", SqlDbType.VarChar).Value = id;
                                cmd.ExecuteNonQuery();

                                cmd = new SqlCommand("DELETE FROM purchase WHERE ID = @ID", con, tran);
                                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = id;
                                cmd.ExecuteNonQuery();

                                try
                                {
                                    tran.Commit();
                                }
                                catch (Exception ex)
                                {
                                    tran.Rollback();
                                    MessageBox.Show(ex.Message.ToString());
                                }
                                Grid_Load();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            } 
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string code = "";
            string note = "";
            
            if (cmbFilterColumn.SelectedItem.ToString() == "No. Transaksi")
            {
                code = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "Keterangan")
            {
                note = txtFilterValue.Text;
            }

            Grid_Load(code, note);
        }
        #endregion

        #region Grid

        public void Grid_Load(string code = "", string note = "")
        {
            try
            {
                String sqlString = "SELECT a.id, a.code, a.date, b.code AS relationCode, b.name AS relationName, a.bruto, a.bonus, a.downpayment, a.total, a.note, a.status, a.updated, a.created, a.username " +
                    "FROM purchase a LEFT JOIN masterRelation b " +
                    "ON a.relationCode = b.code WHERE " +
                    "(a.code like @code OR a.code IS NULL) " +
                    "AND (a.note like @note OR a.note IS NULL) " +
                    "AND (a.date between @dateBegin AND @dateEnd) " +
                    "AND (1=1) " +
                    "ORDER BY a.status ASC, a.code DESC";

                using (SqlDataAdapter da = new SqlDataAdapter(sqlString, connString))
                {
                    da.SelectCommand.Parameters.Add("@code", SqlDbType.VarChar).Value = code + '%';
                    da.SelectCommand.Parameters.Add("@note", SqlDbType.VarChar).Value = note + '%';
                    da.SelectCommand.Parameters.Add("@dateBegin", SqlDbType.Date).Value = dtpDateBegin.Value;
                    da.SelectCommand.Parameters.Add("@dateEnd", SqlDbType.Date).Value = dtpDateEnd.Value;

                    using (DataSet ds = new DataSet())
                    {
                        da.Fill(ds, "data");

                        DataTable dt = new DataTable();
                        dt = ds.Tables["data"];

                        gridView.DataSource = dt;

                        gridView.Columns["id"].Visible = false;
                        gridView.Columns["id"].HeaderText = "ID";
                        gridView.Columns["code"].HeaderText = "No. Transaksi";
                        gridView.Columns["date"].HeaderText = "Tanggal";
                        gridView.Columns["relationCode"].HeaderText = "Kode Mitra";
                        gridView.Columns["relationName"].HeaderText = "Nama Mitra";
                        gridView.Columns["bruto"].HeaderText = "Bruto";
                        gridView.Columns["bonus"].HeaderText = "Bonus";
                        gridView.Columns["downpayment"].HeaderText = "D.P.";
                        gridView.Columns["total"].HeaderText = "Total";
                        gridView.Columns["note"].HeaderText = "Keterangan";
                        gridView.Columns["status"].HeaderText = "Status";
                        gridView.Columns["updated"].HeaderText = "Diubah";
                        gridView.Columns["created"].HeaderText = "Dibuat";
                        gridView.Columns["username"].HeaderText = "User";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void GridSettings()
        {   
            cmbFilterColumn.Items.Add("No. Transaksi");
            cmbFilterColumn.Items.Add("Keterangan");
            cmbFilterColumn.SelectedItem = "No. Transaksi";

            dtpDateBegin.Value = DateTime.Today;
            dtpDateEnd.Value = DateTime.Today;            
        }
        #endregion

        private void btnPrint2_Click(object sender, EventArgs e)
        {
            string id = "";

            if (gridView.SelectedCells.Count != 0)
            {
                try
                {
                    DataGridViewCell cell = gridView.SelectedCells[0];
                    DataGridViewRow row = cell.OwningRow;
                    id = row.Cells[0].Value.ToString();

                    rptPurchase2Form report = new rptPurchase2Form(id);
                    report.ShowDialog();
                    this.SuspendLayout();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
