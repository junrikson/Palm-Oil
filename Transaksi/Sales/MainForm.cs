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

namespace Sales
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
                "WHERE a.code = @username AND c.menuCode = 'B0002' ";
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Grid_Load();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = "";
            string name = "";

            if (gridView.SelectedCells.Count != 0)
            {
                try
                {
                    DataGridViewCell cell = gridView.SelectedCells[0];
                    DataGridViewRow row = cell.OwningRow;
                    id = row.Cells[0].Value.ToString();
                    name = row.Cells["No. Ticket"].Value.ToString();

                    DialogResult dialogResult = MessageBox.Show("Are you sure want to delete " + name + " ?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            using (SqlConnection con = new SqlConnection(connString))
                            {
                                using (SqlCommand cmd = new SqlCommand("DELETE FROM sales WHERE id = @id", con))
                                {

                                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                                    con.Open();
                                    cmd.ExecuteNonQuery();

                                    Grid_Load();
                                }
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
            string name = "";
            string DOCode = "";
            string receiptCode = "";
            string relationName = "";
            string note = "";

            if (cmbFilterColumn.SelectedItem.ToString() == "No. Ticket")
            {
                code = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "No. DO")
            {
                DOCode = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "No. Tanda Terima")
            {
                receiptCode = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "Mitra")
            {
                relationName = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "Keterangan")
            {
                note = txtFilterValue.Text;
            }

            Grid_Load(code, DOCode, receiptCode, relationName, note);
        }
        #endregion

        #region Grid

        public void Grid_Load(string code = "", string DOCode = "", string receiptCode = "", string relationName = "", string note = "")
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("No. Ticket", typeof(String));
            dt.Columns.Add("No. DO", typeof(String));
            dt.Columns.Add("No. Tanda Terima", typeof(String));
            dt.Columns.Add("Mitra", typeof(String));
            dt.Columns.Add("Waktu Tiba", typeof(DateTime));
            dt.Columns.Add("Waktu Kembali", typeof(DateTime));
            dt.Columns.Add("Netto (Kg)", typeof(int));
            dt.Columns.Add("Harga (Rp)", typeof(Decimal));
            dt.Columns.Add("Total (Rp)", typeof(Decimal));
            dt.Columns.Add("Keterangan", typeof(String));
            dt.Columns.Add("Diubah", typeof(DateTime));
            dt.Columns.Add("Dibuat", typeof(DateTime));
            dt.Columns.Add("User", typeof(String));

            try
            {
                String sqlString = "SELECT a.id, a.code, a.DOCode, b.code AS receiptCode, c.name as relationName, a.arrived, a.finished, a.netto, a.price, a.total, a.note, a.updated, a.created, a.username " +
                    "FROM sales a INNER JOIN receipt b ON a.receiptCode = b.code " +
                    "INNER JOIN masterRelation c ON b.relationCode = c.code WHERE " +
                    "(a.code like @code OR a.code IS NULL) " +
                    "AND (a.DOCode like @DOCode OR a.DOCode IS NULL) " +
                    "AND (b.code like @receiptCode OR b.code IS NULL) " +
                    "AND (c.name like @relationName OR c.name IS NULL) " +
                    "AND (a.note like @note OR a.note IS NULL) " +
                    "AND (a.arrived between @dateBegin AND @dateEnd) " +
                    "AND (1=1)";

                using (SqlDataAdapter da = new SqlDataAdapter(sqlString, connString))
                {
                    da.SelectCommand.Parameters.Add("@code", SqlDbType.VarChar).Value = code + '%';
                    da.SelectCommand.Parameters.Add("@DOCode", SqlDbType.VarChar).Value = DOCode + '%';
                    da.SelectCommand.Parameters.Add("@receiptCode", SqlDbType.VarChar).Value = receiptCode + '%';
                    da.SelectCommand.Parameters.Add("@relationName", SqlDbType.VarChar).Value = relationName + '%';
                    da.SelectCommand.Parameters.Add("@note", SqlDbType.VarChar).Value = note + '%';
                    da.SelectCommand.Parameters.Add("@dateBegin", SqlDbType.DateTime).Value = dtpDateBegin.Value;
                    da.SelectCommand.Parameters.Add("@dateEnd", SqlDbType.DateTime).Value = dtpDateEnd.Value;

                    using (DataSet ds = new DataSet())
                    {
                        da.Fill(ds, "data");

                        DataTable tDT = new DataTable();
                        tDT = ds.Tables["data"];

                        for (int i = 0; i < tDT.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            DataRow tDR = tDT.Rows[i];
                            
                            dr["ID"] = tDR["id"];
                            dr["No. Ticket"] = tDR["code"];
                            dr["No. DO"] = tDR["DOCode"];
                            dr["No. Tanda Terima"] = tDR["receiptCode"];
                            dr["Mitra"] = tDR["relationName"];
                            dr["Waktu Tiba"] = tDR["arrived"];
                            dr["Waktu Kembali"] = tDR["finished"];
                            dr["Netto (Kg)"] = tDR["netto"];
                            dr["Harga (Rp)"] = tDR["price"];
                            dr["Total (Rp)"] = tDR["total"];
                            dr["Keterangan"] = tDR["note"];
                            dr["Diubah"] = tDR["updated"];
                            dr["Dibuat"] = tDR["created"];
                            dr["User"] = tDR["username"];
                            
                            dt.Rows.Add(dr);
                        }

                        gridView.DataSource = dt;
                        gridView.Columns["ID"].Visible = false;
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
            cmbFilterColumn.Items.Add("No. Ticket");
            cmbFilterColumn.Items.Add("No. DO");
            cmbFilterColumn.Items.Add("No. Tanda Terima");
            cmbFilterColumn.Items.Add("Mitra");
            cmbFilterColumn.Items.Add("Keterangan");
            cmbFilterColumn.SelectedItem = "No. Ticket";
            dtpDateBegin.Value = DateTime.Today;
            dtpDateEnd.Value = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
        }
        #endregion
    }
}
