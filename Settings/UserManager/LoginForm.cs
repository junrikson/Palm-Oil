#region Copyright Syncfusion Inc. 2001-2017.
// Copyright Syncfusion Inc. 2001-2017. All rights reserved.
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
    public partial class LoginForm : Syncfusion.Windows.Forms.MetroForm
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            FormReset();
            this.AcceptButton = BtnSubmit;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            FormReset();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 code, password FROM masterUser WHERE code = @code AND status = 1", con))
                        {
                            cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = TxtUsername.Text;
                            con.Open();

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Security security = new Security();
                                    if (security.checkHashPassword(TxtPassword.Text, reader["password"].ToString()))
                                    {
                                        MainForm main = new MainForm(reader["code"].ToString());
                                        this.Hide();
                                        main.ShowDialog();
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Password or Username is wrong!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Password or Username is wrong!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void FormReset()
        {
            this.TxtUsername.Text = "";
            this.TxtPassword.Text = "";
            this.TxtUsername.Select();
        }

        private Boolean isValid()
        {
            Boolean isValidated = true;

            if (String.IsNullOrEmpty(TxtUsername.Text))
            {
                isValidated = false;
                MessageBox.Show("Username can not be blank!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtUsername.Focus();
            }
            else if (String.IsNullOrEmpty(TxtPassword.Text))
            {
                isValidated = false;
                MessageBox.Show("Password can not be blank!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtPassword.Focus();
            }

            return isValidated;
        }
    }
}
