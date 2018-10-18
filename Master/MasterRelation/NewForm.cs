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

namespace MasterRelation
{
    public partial class NewForm : Syncfusion.Windows.Forms.MetroForm 
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";

        public NewForm(string username)
        {
            this.username = username;
            InitializeComponent();
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
                    string query = "";

                    if (String.IsNullOrEmpty(txtCode.Text))
                    {
                        query = "INSERT INTO masterRelation" +
                            "(code, name, address, phone, handphone, fax, email, note, status, updated, created, username) VALUES " +
                            "(@code + (SELECT RIGHT('0000' + CAST((ISNULL((SELECT TOP 1 RIGHT(code,5) " +
                            "   FROM masterRelation " +
                            "   WHERE LEFT(code,2) = 'MT' " +
                            "   ORDER BY RIGHT(code,5) DESC),0) + 1) AS VARCHAR(5)),5))," +
                            "@name, @address, @phone, @handphone, @fax, @email, @note, @status, getdate(), getdate(), @username)";
                    }
                    else
                    {
                        query = "INSERT INTO masterRelation" +
                            "(code, name, address, phone, handphone, fax, email, note, status, updated, created, username) VALUES " +
                            "(@code, @name, @address, @phone, @handphone, @fax, @email, @note, @status, getdate(), getdate(), @username)";
                    }

                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            if (String.IsNullOrEmpty(txtCode.Text))
                            {
                                cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = "MT";
                            }
                            else
                            {
                                cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = txtCode.Text;
                            }

                            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = txtName.Text;
                            cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = txtAddress.Text;
                            cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = txtPhone.Text;
                            cmd.Parameters.Add("@handphone", SqlDbType.VarChar).Value = txtHandphone.Text;
                            cmd.Parameters.Add("@fax", SqlDbType.VarChar).Value = txtFax.Text;
                            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = txtEmail.Text;
                            cmd.Parameters.Add("@note", SqlDbType.VarChar).Value = txtNote.Text;
                            cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;

                            int status = 0;

                            if(chkStatus.Checked == true)
                            {
                                status = 1;
                            }

                            cmd.Parameters.Add("@status", SqlDbType.SmallInt).Value = status;

                            con.Open();
                            cmd.ExecuteNonQuery();
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
            txtCode.ReadOnly = true;
            txtName.Text = "";
            txtAddress.Text = "";
            txtHandphone.Text = "";
            txtPhone.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtNote.Text = "";
            chkStatus.Checked = true;
            chkManual.Checked = false;
            txtName.Select();
        }

        private Boolean isValid()
        {
            Boolean isValidated = true;

            if (!(String.IsNullOrEmpty(txtCode.Text)))
            {
                if (txtCode.Text.Length < 5)
                {
                    isValidated = false;
                    MessageBox.Show("No. Transaksi harus lebih dari 5 karakter!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Select();
                }
                else if (!txtCode.Text.Substring(txtCode.Text.Length - 5).All(char.IsDigit))
                {
                    isValidated = false;
                    MessageBox.Show("5 Karakter terakhir harus terdiri dari angka!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Select();
                }
            }

            if (isValidated == true)
            {
                if (String.IsNullOrEmpty(txtName.Text))
                {
                    isValidated = false;
                    MessageBox.Show("Nama Mitra harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Select();
                }
                else if (String.IsNullOrEmpty(txtAddress.Text))
                {
                    isValidated = false;
                    MessageBox.Show("Alamat harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAddress.Select();
                }
            }

            return isValidated;
        }

        private void chkManual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManual.Checked == true)
            {
                txtCode.ReadOnly = false;
            }
            else
            {
                txtCode.ReadOnly = true;
            }
        }
    }
}
