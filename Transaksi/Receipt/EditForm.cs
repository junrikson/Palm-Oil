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

namespace Receipt
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
                        using (SqlCommand cmd = new SqlCommand("UPDATE receipt " +
                            "SET date = @date, " +
                            "relationCode = @relationCode, " +
                            "licensePlate = @licensePlate, " +
                            "driver = @driver, " +
                            "note = @note, " +
                            "updated = getdate(), " +
                            "username = @username " +
                            "WHERE id = @id", con))
                        {
                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = this.id;
                            cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = dtpDate.Value;
                            cmd.Parameters.Add("@relationCode", SqlDbType.VarChar).Value = txtRelationCode.Text;
                            cmd.Parameters.Add("@licensePlate", SqlDbType.VarChar).Value = txtLicensePlate.Text;
                            cmd.Parameters.Add("@driver", SqlDbType.VarChar).Value = txtDriver.Text;
                            cmd.Parameters.Add("@note", SqlDbType.VarChar).Value = txtNote.Text;
                            cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;

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

        private Boolean isValid()
        {
            Boolean isValidated = true;
            
            if (isValidated == true)
            {
                if (String.IsNullOrEmpty(txtRelationCode.Text))
                {
                    isValidated = false;
                    MessageBox.Show("Kode Relasi harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRelationCode.Select();
                }
                else if (dtpDate.Value == null)
                {
                    isValidated = false;
                    MessageBox.Show("Waktu harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpDate.Select();
                }
            }
            
            if (isValidated == true)
            {
                if (!checkCustomer(txtRelationCode.Text))
                {
                    isValidated = false;
                    MessageBox.Show("Kode Mitra tidak terdaftar!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRelationCode.Select();
                }
            }

            return isValidated;
        }

        private Boolean checkCustomer(string code)
        {
            bool isValid = false;

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 code FROM masterRelation WHERE code = @code", con))
                    {
                        cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = code;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!string.IsNullOrEmpty(reader["code"].ToString()))
                                    isValid = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            return isValid;
        }

        private void Load_Data()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 code, date, relationCode, licensePlate, driver, note FROM receipt WHERE id = @id", con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = this.id;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                txtCode.Text = reader["code"].ToString();
                                dtpDate.Value = DateTime.Parse(reader["date"].ToString());
                                txtRelationCode.Text = reader["relationCode"].ToString();
                                txtLicensePlate.Text = reader["licensePlate"].ToString();
                                txtDriver.Text = reader["driver"].ToString();
                                txtNote.Text = reader["note"].ToString();
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

        public void btnRelationClick(string relationCode)
        {
            txtRelationCode.Text = relationCode;
        }

        private void btnRelation_Click(object sender, EventArgs e)
        {
            Form dialogForm = new dialogRelationForm(username, "EditForm", this.id);
            dialogForm.ShowDialog(this);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            rptReceiptForm report = new rptReceiptForm(this.id.ToString());
            report.ShowDialog();
            this.SuspendLayout();
            this.Close();
        }
    }
}
