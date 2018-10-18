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

namespace Sales
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
                        using (SqlCommand cmd = new SqlCommand("UPDATE sales " +
                            "SET code = @code, " +
                            "receiptCode = @receiptCode, " +
                            "DOCode = @DOCode, " +
                            "arrived = @arrived, " +
                            "finished = @finished, " +
                            "netto = @netto, " +
                            "price = @price, " +
                            "note = @note, " +
                            "updated = getdate(), " +
                            "username = @username " +
                            "WHERE id = @id", con))
                        {
                            cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = txtCode.Text;
                            cmd.Parameters.Add("@receiptCode", SqlDbType.VarChar).Value = txtReceiptCode.Text;
                            cmd.Parameters.Add("@DOCode", SqlDbType.VarChar).Value = txtDOCode.Text;
                            cmd.Parameters.Add("@arrived", SqlDbType.DateTime).Value = dtpArrived.Value;
                            cmd.Parameters.Add("@finished", SqlDbType.DateTime).Value = dtpFinished.Value;
                            cmd.Parameters.Add("@netto", SqlDbType.Int).Value = txtNetto.Value;
                            cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = txtPrice.Value;
                            cmd.Parameters.Add("@note", SqlDbType.VarChar).Value = txtNote.Text;
                            cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = this.id;

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

            if (String.IsNullOrEmpty(txtCode.Text))
            {
                isValidated = false;
                MessageBox.Show("Nomor Ticket harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Select();
            }
            else if (String.IsNullOrEmpty(txtDOCode.Text))
            {
                isValidated = false;
                MessageBox.Show("Nomor DO harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDOCode.Select();
            }
            else if (String.IsNullOrEmpty(txtReceiptCode.Text))
            {
                isValidated = false;
                MessageBox.Show("Nomor Tanda Terima harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReceiptCode.Select();
            }
            else if (dtpArrived.Value == null)
            {
                isValidated = false;
                MessageBox.Show("Waktu tiba harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpArrived.Select();
            }
            else if (dtpFinished.Value == null)
            {
                isValidated = false;
                MessageBox.Show("Waktu kembali harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFinished.Select();
            }
            else if (String.IsNullOrEmpty(txtNetto.Text))
            {
                isValidated = false;
                MessageBox.Show("Netto harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNetto.Select();
            }
            else if (String.IsNullOrEmpty(txtPrice.Text))
            {
                isValidated = false;
                MessageBox.Show("Harga harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Select();
            }

            if (isValidated == true)
            {
                if (checkCode(txtCode.Text))
                {
                    isValidated = false;
                    MessageBox.Show("Nomor Ticket ini sudah pernah digunakan!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Select();
                }
            }

            return isValidated;
        }

        private Boolean checkCode(string code)
        {
            bool isValid = false;

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 code FROM sales WHERE code = @code AND id <> @id", con))
                    {
                        cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = code;
                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = this.id;
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
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 code, receiptCode, DOCode, arrived, finished, netto, price, note FROM sales WHERE id = @id", con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                txtCode.Text = reader["code"].ToString();
                                txtReceiptCode.Text = reader["receiptCode"].ToString();
                                txtDOCode.Text = reader["DOCode"].ToString();
                                dtpArrived.Value = DateTime.Parse(reader["arrived"].ToString());
                                dtpFinished.Value = DateTime.Parse(reader["finished"].ToString());
                                txtNetto.Value = int.Parse(reader["netto"].ToString());
                                txtPrice.Value = Decimal.Parse(reader["price"].ToString());
                                txtNote.Text = reader["note"].ToString();
                                txtTotal.Value = Math.Round(txtNetto.Value * txtPrice.Value,0);
                                refreshReceipt();
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

        public void btnReceiptClick(string receiptCode)
        {
            txtReceiptCode.Text = receiptCode;
            checkReceipt();
        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            Form dialogForm = new dialogReceiptForm(username, "EditForm", this.id);
            dialogForm.ShowDialog(this);
        }

        private void checkReceipt()
        {
            if (!string.IsNullOrEmpty(txtReceiptCode.Text))
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 a.date, b.name as relationName, a.licensePlate, a.driver, a.note " +
                            "FROM receipt a INNER JOIN masterRelation b " +
                            "ON a.relationCode = b.code " +
                            "LEFT JOIN sales c ON a.code = c.receiptCode WHERE " +
                            "c.receiptCode IS NULL " +
                            "AND a.code = @code", con))
                        {
                            cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = txtReceiptCode.Text;
                            con.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    dtpReceiptDate.Value = DateTime.Parse(reader["date"].ToString());
                                    txtReceiptRelation.Text = reader["relationName"].ToString();
                                    txtReceiptLicensePlate.Text = reader["licensePlate"].ToString();
                                    txtReceiptDriver.Text = reader["driver"].ToString();
                                    txtReceiptNote.Text = txtNote.Text = reader["note"].ToString();

                                    txtCode.Select();
                                }
                                else
                                {
                                    dtpReceiptDate.Value = DateTime.Now;
                                    txtReceiptRelation.Text = "";
                                    txtReceiptLicensePlate.Text = "";
                                    txtReceiptDriver.Text = "";
                                    txtReceiptNote.Text = "";

                                    MessageBox.Show("Tanda terima tidak ditemukan atau sudah pernah digunakan!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    txtReceiptCode.Select();
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
        }

        private void refreshReceipt()
        {
            if (!string.IsNullOrEmpty(txtReceiptCode.Text))
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 a.date, b.name as relationName, a.licensePlate, a.driver, a.note " +
                            "FROM receipt a INNER JOIN masterRelation b " +
                            "ON a.relationCode = b.code WHERE a.code = @code", con))
                        {
                            cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = txtReceiptCode.Text;
                            con.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    dtpReceiptDate.Value = DateTime.Parse(reader["date"].ToString());
                                    txtReceiptRelation.Text = reader["relationName"].ToString();
                                    txtReceiptLicensePlate.Text = reader["licensePlate"].ToString();
                                    txtReceiptDriver.Text = reader["driver"].ToString();
                                    txtReceiptNote.Text = txtNote.Text = reader["note"].ToString();

                                    txtCode.Select();
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
        }

        private void txtNetto_ValueChanged(object sender, EventArgs e)
        {
            txtTotal.Value = Math.Round(txtNetto.Value * txtPrice.Value, 0);
        }

        private void txtPrice_ValueChanged(object sender, EventArgs e)
        {
            txtTotal.Value = Math.Round(txtNetto.Value * txtPrice.Value, 0);
        }

        private void txtReceiptCode_Leave(object sender, EventArgs e)
        {
            checkReceipt();
        }
    }
}
