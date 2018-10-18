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
    public partial class EditForm : Syncfusion.Windows.Forms.MetroForm 
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string id = "";
        string username = "";

        public EditForm(string id, string username)
        {
            InitializeComponent();
            this.id = id;
            this.username = username;
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE masterMenu SET code = @code, name = @name, status = @status, updated = getdate(), username = @username WHERE id = @id", con))
                        {
                            cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = txtCode.Text;
                            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = txtName.Text;
                            cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = this.username;
                            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;

                            int status = 0;

                            if(chkStatus.Checked == true)
                            {
                                status = 1;
                            }

                            cmd.Parameters.Add("@status", SqlDbType.SmallInt).Value = status;

                            con.Open();
                            cmd.ExecuteNonQuery();

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
                this.Close();
            }
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
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 code, name, status FROM masterMenu WHERE id = @id", con))
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
    }
}
