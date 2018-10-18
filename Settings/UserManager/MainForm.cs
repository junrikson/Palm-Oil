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

namespace UserManager
{
    public partial class MainForm : Syncfusion.Windows.Forms.MetroForm
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";

        public MainForm(string username)
        {
            InitializeComponent();
            GridSettings();
            this.username = username;
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
                "WHERE a.code = @username AND c.menuCode = 'D0003' ";
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
                    name = row.Cells["Name"].Value.ToString();

                    DialogResult dialogResult = MessageBox.Show("Are you sure want to delete " + name + " ?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        using (SqlConnection con = new SqlConnection(connString))
                        {
                            using (SqlCommand cmd = new SqlCommand("DELETE FROM masterUser WHERE id = @id", con))
                            {

                                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                                con.Open();
                                cmd.ExecuteNonQuery();

                                if (con.State == System.Data.ConnectionState.Open)
                                {
                                    con.Close();
                                }
                                Grid_Load();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            } 
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            string id = "";

            if (gridView.SelectedCells.Count != 0)
            {
                try
                {
                    DataGridViewCell cell = gridView.SelectedCells[0];
                    DataGridViewRow row = cell.OwningRow;
                    id = row.Cells[0].Value.ToString();

                    ChangePasswordForm changePasswordForm = new ChangePasswordForm(id, username);
                    changePasswordForm.FormClosed += new FormClosedEventHandler(Form_Closed);
                    changePasswordForm.ShowDialog();
                    this.SuspendLayout();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int status = 1;
            string code = "";
            string name = "";

            if (cmbStatus.SelectedItem.ToString() == "Not Active")
            {
                status = 0;
            }

            if (cmbFilterColumn.SelectedItem.ToString() == "Name")
            {
                name = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "Username")
            {
                code = txtFilterValue.Text;
            }

            Grid_Load(status, code, name);
        }
        #endregion

        #region Grid

        public void Grid_Load(int status = 1, string code = "", string name = "")
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Username", typeof(String));
            dt.Columns.Add("Name", typeof(String));
            dt.Columns.Add("Role Code", typeof(String));
            dt.Columns.Add("Updated", typeof(DateTime));
            dt.Columns.Add("Created", typeof(DateTime));
            dt.Columns.Add("User Update", typeof(String));
            dt.Columns.Add("Status", typeof(String));

            try
            {
                String sqlString = "SELECT id, code, name, rolesCode, status, username, updated, created FROM masterUser WHERE " +
                    "status = (@status) " +
                    "AND (code like @code OR code IS NULL) " +
                    "AND (name like @name OR name IS NULL) " +
                    "AND (1=1)";

                SqlDataAdapter da = new SqlDataAdapter(sqlString, connString);
                da.SelectCommand.Parameters.Add("@status", SqlDbType.Int).Value = status;
                da.SelectCommand.Parameters.Add("@code", SqlDbType.VarChar).Value = code + '%';
                da.SelectCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = name + '%';

                DataSet ds = new DataSet();
                da.Fill(ds, "data");

                DataTable tDT = new DataTable();
                tDT = ds.Tables["data"];
                
                for (int i = 0; i < tDT.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    DataRow tDR = tDT.Rows[i];
                    string isActive = "Active";

                    dr["ID"] = tDR["id"];
                    dr["Username"] = tDR["code"];
                    dr["Name"] = tDR["name"];
                    dr["Role Code"] = tDR["rolesCode"];
                    dr["Updated"] = tDR["updated"];
                    dr["Created"] = tDR["created"];
                    dr["User Update"] = tDR["username"];

                    if (tDR["status"].ToString() != "1")
                    {
                        isActive = "Not Active";
                    }                    
                    dr["Status"] = isActive;

                    dt.Rows.Add(dr);
                }
                
                gridView.DataSource = dt;
                gridView.Columns["ID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void GridSettings()
        {
            cmbFilterColumn.Items.Add("Username");
            cmbFilterColumn.Items.Add("Name");
            cmbFilterColumn.SelectedItem = "Username";

            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Not Active");
            cmbStatus.SelectedItem = "Active";
        }
        #endregion

    }
}
