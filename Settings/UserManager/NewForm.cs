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
    public partial class NewForm : Syncfusion.Windows.Forms.MetroForm 
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";

        public NewForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void NewForm_Load(object sender, EventArgs e)
        {
            doClear();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            doClear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO masterUser" +
                            "(code, name, rolesCode, password, status, updated, created, username) VALUES " +
                            "(@code, @name, @rolesCode, @password, @status, getdate(), getdate(), @username)", con))
                        {
                            cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = txtCode.Text;
                            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = txtName.Text;
                            cmd.Parameters.Add("@rolesCode", SqlDbType.VarChar).Value = txtRolesCode.Text;
                            cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;

                            Security security = new Security();
                            cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = security.getHashPassword();

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

        private void doClear()
        {
            txtCode.Text = "";
            txtName.Text = "";
            chkStatus.Checked = true;
            txtCode.Focus();
        }

        private Boolean isValid()
        {
            Boolean isValidated = true;
            
            if (String.IsNullOrEmpty(txtCode.Text))
            {
                isValidated = false;
                MessageBox.Show("Code is required!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Select();
            }
            else if (String.IsNullOrEmpty(txtName.Text))
            {
                isValidated = false;
                MessageBox.Show("Name is required!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Select();
            }
            else if (String.IsNullOrEmpty(txtRolesCode.Text))
            {
                isValidated = false;
                MessageBox.Show("Role Code is required!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRolesCode.Select();
            }

            return isValidated;
        }

        private void btnRole_Click(object sender, EventArgs e)
        {
            Form dialogForm = new dialogRolesForm(username, "NewForm");
            dialogForm.ShowDialog(this);
        }

        public void btnRoleClick(string rolesCode)
        {
            txtRolesCode.Text = rolesCode;
        }
    }
}
