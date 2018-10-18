#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Dashboard
{
    public partial class ChangePasswordForm : Syncfusion.Windows.Forms.MetroForm 
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";

        public ChangePasswordForm(string username)
        {
            InitializeComponent();
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
                        using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 code, password FROM masterUser WHERE code = @code AND status = 1", con))
                        {
                            cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = username;
                            con.Open();

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Security security = new Security();
                                    if (security.checkHashPassword(txtOldPassword.Text, reader["password"].ToString()))
                                    {
                                        try
                                        {
                                            using (SqlConnection con2 = new SqlConnection(connString))
                                            {
                                                using (SqlCommand cmd2 = new SqlCommand("UPDATE masterUser SET password = @password, updated = getdate(), username = @username WHERE code = @username", con2))
                                                {
                                                    Security security2 = new Security();
                                                    cmd2.Parameters.Add("@password", SqlDbType.VarChar).Value = security2.getHashPassword(txtNewPassword.Text);
                                                    cmd2.Parameters.Add("@username", SqlDbType.VarChar).Value = username;

                                                    con2.Open();
                                                    cmd2.ExecuteNonQuery();

                                                    if (con2.State == System.Data.ConnectionState.Open)
                                                    {
                                                        con2.Close();
                                                    }
                                                    this.Close();
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message.ToString());
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Password lama salah!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        txtOldPassword.Select();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Password lama salah!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    txtOldPassword.Select();
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

        private Boolean isValid()
        {
            Boolean isValidated = true;


            if (String.IsNullOrEmpty(txtOldPassword.Text))
            {
                isValidated = false;
                MessageBox.Show("Password lama harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
            }
            else if (String.IsNullOrEmpty(txtNewPassword.Text) || String.IsNullOrEmpty(txtConfirm.Text) || (txtConfirm.Text != txtNewPassword.Text))
            {
                isValidated = false;
                MessageBox.Show("Password dan Konfirmasi tidak sama!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
            }

            return isValidated;
        }
    }
}
