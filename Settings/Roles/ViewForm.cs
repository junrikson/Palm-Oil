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
    public partial class ViewForm : Syncfusion.Windows.Forms.MetroForm 
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";
        string id = "";
        string code = "";

        public string ConnString { get => connString; set => connString = value; }
        public string Username { get => username; set => username = value; }
        public string Id { get => id; set => id = value; }
        public string Code { get => code; set => code = value; }

        public ViewForm(string id, string username)
        {
            InitializeComponent();
            this.Id = id;
            this.Username = username;
        }

        private void ViewForm_Load(object sender, EventArgs e)
        {
            Load_Data();
            Grid_Load();
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

        private void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 id, code, name FROM masterRoles WHERE id < @id ORDER BY id DESC", con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = Id;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Id = reader["id"].ToString();
                                txtCode.Text = reader["code"].ToString();
                                txtName.Text = reader["name"].ToString();
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
            Grid_Load();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 id, code, name FROM masterRoles WHERE id > @id ORDER BY id ASC", con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = Id;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                this.Id = reader["id"].ToString();
                                txtCode.Text = reader["code"].ToString();
                                txtName.Text = reader["name"].ToString();
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
            Grid_Load();
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
