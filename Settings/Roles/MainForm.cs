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

namespace Roles
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
                "WHERE a.code = @username AND c.menuCode = 'D0002' ";
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

                    rptLoadingPlanForm report = new rptLoadingPlanForm(id);
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

                                cmd = new SqlCommand("DELETE FROM loadingPlanDetail WHERE loadingID = @loadingID", con, tran);
                                cmd.Parameters.Add("@loadingID", SqlDbType.VarChar).Value = id;
                                cmd.ExecuteNonQuery();

                                cmd = new SqlCommand("DELETE FROM loadingPlan WHERE ID = @ID", con, tran);
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
            string name = "";
            
            if (cmbFilterColumn.SelectedItem.ToString() == "Kode Role")
            {
                code = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "Nama Role")
            {
                name = txtFilterValue.Text;
            }

            Grid_Load(code, name);
        }
        #endregion

        #region Grid

        public void Grid_Load(string code = "", string name = "")
        {
            try
            {
                String sqlString = "SELECT id, code, name, status, updated, created, username " +
                    "FROM masterRoles " +
                    "WHERE status = 1 " +
                    "AND (code like @code OR code IS NULL) " +
                    "AND (name like @name OR name IS NULL) " +
                    "AND (1=1)";

                using (SqlDataAdapter da = new SqlDataAdapter(sqlString, connString))
                {
                    da.SelectCommand.Parameters.Add("@code", SqlDbType.VarChar).Value = code + '%';
                    da.SelectCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = name + '%';

                    using (DataSet ds = new DataSet())
                    {
                        da.Fill(ds, "data");

                        DataTable dt = new DataTable();
                        dt = ds.Tables["data"];

                        gridView.DataSource = dt;

                        gridView.Columns["id"].Visible = false;
                        gridView.Columns["id"].HeaderText = "ID";
                        gridView.Columns["code"].HeaderText = "Kode Role";
                        gridView.Columns["name"].HeaderText = "Nama Role";
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
            cmbFilterColumn.Items.Add("Kode Role");
            cmbFilterColumn.Items.Add("Nama Role");
            cmbFilterColumn.SelectedItem = "Kode Role";   
        }
        #endregion

    }
}
