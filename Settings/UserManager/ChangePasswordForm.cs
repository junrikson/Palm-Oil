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
    public partial class ChangePasswordForm : Syncfusion.Windows.Forms.MetroForm 
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string id = "";
        string username = "";

        public ChangePasswordForm(string id, string username)
        {
            InitializeComponent();
            this.id = id;
            this.username = username;
        }

        private void EditForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE masterUser SET password = @password, updated = getdate(), username = @username WHERE id = @id", con))
                        {
                            Security security = new Security();
                            cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = security.getHashPassword(txtPassword.Text);
                            cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;

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

            if (String.IsNullOrEmpty(txtPassword.Text) || String.IsNullOrEmpty(txtConfirm.Text) || (txtConfirm.Text != txtPassword.Text))
            {
                isValidated = false;
                MessageBox.Show("Password and Confirmation not match!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
            }

            return isValidated;
        }
    }
}
