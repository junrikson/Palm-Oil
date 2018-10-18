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

namespace MenuManager
{
    public partial class ViewForm : Syncfusion.Windows.Forms.MetroForm 
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string id = "";

        public ViewForm(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            Load_Data();
            disable_actions();
        }

        private Boolean isValid()
        {
            Boolean isValidated = true;
            
            if (String.IsNullOrEmpty(txtCode.Text))
            {
                isValidated = false;
                MessageBox.Show("Code is required!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Focus();
            }
            else if (String.IsNullOrEmpty(txtName.Text))
            {
                isValidated = false;
                MessageBox.Show("Name is required!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
            }

            return isValidated;
        }

        private void Load_Data()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT code, name, status FROM masterMenu WHERE id = @id", con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                txtCode.Text = reader["code"].ToString();
                                txtName.Text = reader["name"].ToString();
                                if (reader["status"].ToString() == "1")
                                    chkStatus.Checked = true;
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

        private void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 id, code, name, status FROM masterMenu WHERE id < @id ORDER BY id DESC", con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id = reader["id"].ToString();
                                txtCode.Text = reader["code"].ToString();
                                txtName.Text = reader["name"].ToString();
                                if (reader["status"].ToString() == "1")
                                    chkStatus.Checked = true;
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

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 id, code, name, status FROM masterMenu WHERE id > @id ORDER BY id ASC", con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id = reader["id"].ToString();
                                txtCode.Text = reader["code"].ToString();
                                txtName.Text = reader["name"].ToString();
                                if (reader["status"].ToString() == "1")
                                    chkStatus.Checked = true;
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

        private void disable_actions()
        {
            txtCode.ReadOnly = true;
            txtName.ReadOnly = true;
            chkStatus.Enabled = false;
        }
    }
}
