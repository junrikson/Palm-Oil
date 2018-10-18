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
            if (SaveData() != 0)
                this.Close();
        }

        private void doClear()
        {
            txtCode.Text = "";
            dtpDate.Value = DateTime.Now;
            txtRelationCode.Text = "";
            txtLicensePlate.Text = "";
            txtDriver.Text = "";
            txtNote.Text = "T B S (TANDAN BUAH SEGAR)";
            txtCode.Select();
        }

        private Boolean isValid()
        {
            Boolean isValidated = true;
            
            if (isValidated == true)
            {
                if (String.IsNullOrEmpty(txtCode.Text))
                {
                    isValidated = false;
                    MessageBox.Show("Kode Tanda Terima harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Select();
                }
                if (String.IsNullOrEmpty(txtRelationCode.Text))
                {
                    isValidated = false;
                    MessageBox.Show("Kode Mitra harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if(!checkCustomer(txtRelationCode.Text))
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

        private void btnRelation_Click(object sender, EventArgs e)
        {           Form dialogForm = new dialogRelationForm(username, "NewForm");
            dialogForm.ShowDialog(this);
        }

        public void btnRelationClick(string relationCode)
        {
            txtRelationCode.Text = relationCode;
        }
        
        private void btnPrint_Click(object sender, EventArgs e)
        {
            int id = SaveData();
            if (id != 0)
            {
                rptReceiptForm report = new rptReceiptForm(id.ToString());
                report.ShowDialog();
                this.SuspendLayout();
            }
            this.Close();
        }

        private int SaveData()
        {
            int id = 0;

            if (isValid())
            {
                try
                {
                    string query = "INSERT INTO receipt " +
                            "(code, date, relationCode, licensePlate, driver, note, updated, created, username) output INSERTED.ID VALUES " +
                            "(@code, @date, @relationCode, @licensePlate, @driver, @note, getdate(), getdate(), @username)";
                    
                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            if (String.IsNullOrEmpty(txtCode.Text))
                            {
                                cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = "TT/" + dtpDate.Value.ToString("yyMMdd");
                                cmd.Parameters.Add("@today", SqlDbType.VarChar).Value = dtpDate.Value.ToString("yyMMdd");
                            }
                            else
                            {
                                cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = txtCode.Text;
                            }
                            cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = dtpDate.Value;
                            cmd.Parameters.Add("@relationCode", SqlDbType.VarChar).Value = txtRelationCode.Text;
                            cmd.Parameters.Add("@licensePlate", SqlDbType.VarChar).Value = txtLicensePlate.Text;
                            cmd.Parameters.Add("@driver", SqlDbType.VarChar).Value = txtDriver.Text;
                            cmd.Parameters.Add("@note", SqlDbType.VarChar).Value = txtNote.Text;
                            cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;

                            con.Open();
                            id = (int)cmd.ExecuteScalar();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

            return id;
        }
    }
}
