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
using System.Linq;
using System.Windows.Forms;

namespace Roles
{
    public partial class EditForm : Syncfusion.Windows.Forms.MetroForm
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";
        string id = "";
        string code = "";

        public string ConnString { get => connString; set => connString = value; }
        public string Username { get => username; set => username = value; }
        public string Id { get => id; set => id = value; }
        public string Code { get => code; set => code = value; }

        public EditForm(string id, string username)
        {
            this.Username = username;
            InitializeComponent();
            this.Id = id;
        }

        private void NewForm_Load(object sender, EventArgs e)
        {
            doClear();
            Load_Data();
            Grid_Load();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            doClear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
                this.Close();
        }

        private bool SaveData()
        {
            bool status = false;
            if (isValid())
            {
                try
                {
                    string query = "UPDATE masterRoles SET " +
                               "name = @name, " +
                               "updated = getdate(), " +
                               "username = @username " +
                               "WHERE id = @id";

                    using (SqlConnection con = new SqlConnection(ConnString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = txtName.Text;
                            cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = this.Username;
                            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = this.Id;

                            con.Open();
                            cmd.ExecuteNonQuery();
                            status = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            return status;
        }

        private void doClear()
        {
            txtCode.ReadOnly = true;
            txtName.Text = "-";
            txtCode.Select();
        }

        private Boolean isValid()
        {
            Boolean isValidated = true;

            if (String.IsNullOrEmpty(txtCode.Text))
            {
                isValidated = false;
                MessageBox.Show("Kode Roles harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Select();
            }
            else if (String.IsNullOrEmpty(txtName.Text))
            {
                isValidated = false;
                MessageBox.Show("Nama Roles harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Select();
            }

            return isValidated;
        }
        
        private void Load_Data()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 code, name FROM masterRoles WHERE id = @id", con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = Id;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                txtCode.Text = Code = reader["code"].ToString();
                                txtName.Text = reader["name"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void Grid_Load()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Kode Menu", typeof(String));
            dt.Columns.Add("Nama Menu", typeof(String));
            dt.Columns.Add("Aktif", typeof(Boolean));
            dt.Columns.Add("New", typeof(Boolean));
            dt.Columns.Add("Edit", typeof(Boolean));
            dt.Columns.Add("View", typeof(Boolean));
            dt.Columns.Add("Delete", typeof(Boolean));
            dt.Columns.Add("Print", typeof(Boolean));
            dt.Columns.Add("Refresh", typeof(Boolean));

            try
            {
                String sqlString = "SELECT a.menuCode, b.name, a.[active], a.[new], a.[edit], a.[view], a.[delete], a.[print], a.[refresh] " +
                    "FROM masterRolesDetail a LEFT JOIN masterMenu b " +
                    "ON a.menuCode = b.code " +
                    "WHERE a.rolesID = @rolesID " +
                    "AND (1=1) " +
                    "ORDER BY a.menuCode";

                using (SqlDataAdapter da = new SqlDataAdapter(sqlString, ConnString))
                {
                    da.SelectCommand.Parameters.Add("@rolesID", SqlDbType.VarChar).Value = this.Id;

                    using (DataSet ds = new DataSet())
                    {
                        da.Fill(ds, "data");

                        DataTable tDT = new DataTable();
                        tDT = ds.Tables["data"];

                        for (int i = 0; i < tDT.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            DataRow tDR = tDT.Rows[i];

                            dr["Kode Menu"] = tDR["menuCode"];
                            dr["Nama Menu"] = tDR["name"];
                            dr["Aktif"] = tDR["active"];
                            dr["New"] = tDR["new"];
                            dr["Edit"] = tDR["edit"];
                            dr["View"] = tDR["view"];
                            dr["Delete"] = tDR["delete"];
                            dr["Print"] = tDR["print"];
                            dr["Refresh"] = tDR["refresh"];

                            dt.Rows.Add(dr);
                        }

                        gridView.DataSource = dt;
                        gridView.Columns["Kode Menu"].ReadOnly = true;
                        gridView.Columns["Nama Menu"].ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void gridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string MenuCode = gridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            Boolean Active = (bool)gridView.Rows[e.RowIndex].Cells[2].Value;
            Boolean New = (bool)gridView.Rows[e.RowIndex].Cells[3].Value;
            Boolean Edit = (bool)gridView.Rows[e.RowIndex].Cells[4].Value;
            Boolean View = (bool)gridView.Rows[e.RowIndex].Cells[5].Value;
            Boolean Delete = (bool)gridView.Rows[e.RowIndex].Cells[6].Value;
            Boolean Print = (bool)gridView.Rows[e.RowIndex].Cells[7].Value;
            Boolean Refresh = (bool)gridView.Rows[e.RowIndex].Cells[8].Value;

            try
            {
                string query = "UPDATE masterRolesDetail " +
                            "SET [active] = @Active, [new] = @New, [edit] = @Edit, [view] = @View, [delete] = @Delete, [print] = @Print, [refresh] = @Refresh " +
                            "WHERE rolesID = @ID AND menuCode = @MenuCode";

                using (SqlConnection con = new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = this.Id;
                        cmd.Parameters.Add("@MenuCode", SqlDbType.VarChar).Value = MenuCode;
                        cmd.Parameters.Add("@Active", SqlDbType.SmallInt).Value = Active;
                        cmd.Parameters.Add("@New", SqlDbType.SmallInt).Value = New;
                        cmd.Parameters.Add("@Edit", SqlDbType.SmallInt).Value = Edit;
                        cmd.Parameters.Add("@View", SqlDbType.SmallInt).Value = View;
                        cmd.Parameters.Add("@Delete", SqlDbType.SmallInt).Value = Delete;
                        cmd.Parameters.Add("@Print", SqlDbType.SmallInt).Value = Print;
                        cmd.Parameters.Add("@Refresh", SqlDbType.SmallInt).Value = Refresh;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "INSERT INTO masterRolesDetail ([rolesID], menuCode, [active], [new], [edit], [view], [delete], [print], [refresh]) " +
                            "SELECT @ID, code, 1, 1, 1, 1, 1, 1, 1 FROM masterMenu WHERE code NOT IN ( " +
                            "	SELECT menuCode FROM masterRolesDetail WHERE rolesID = @ID)";

                using (SqlConnection con = new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = this.Id;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            Grid_Load();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE masterRolesDetail " +
                            "SET [active] = 1, [new] = 1, [edit] = 1, [view] = 1, [delete] = 1, [print] = 1, [refresh] = 1 " +
                            "WHERE rolesID = @ID";

                using (SqlConnection con = new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = this.Id;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            Grid_Load();
        }

        private void btnUncheck_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE masterRolesDetail " +
                            "SET [active] = 0, [new] = 0, [edit] = 0, [view] = 0, [delete] = 0, [print] = 0, [refresh] = 0 " +
                            "WHERE rolesID = @ID";

                using (SqlConnection con = new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = this.Id;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            Grid_Load();
        }
    }
}
