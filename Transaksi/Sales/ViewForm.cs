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
    public partial class ViewForm : Syncfusion.Windows.Forms.MetroForm 
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string id = "";
        string username = "";

        public ViewForm(string id, string username)
        {
            InitializeComponent();
            this.id = id;
            this.username = username;
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void Load_Data()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 code, receiptCode, DOCode, millCode, arrived, finished, netto, price, note FROM sales WHERE id = @id", con))
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
                                txtMillCode.Text = reader["millCode"].ToString();
                                dtpArrived.Value = DateTime.Parse(reader["arrived"].ToString());
                                dtpFinished.Value = DateTime.Parse(reader["finished"].ToString());
                                txtNetto.Value = int.Parse(reader["netto"].ToString());
                                txtPrice.Value = Decimal.Parse(reader["price"].ToString());
                                txtNote.Text = reader["note"].ToString();
                                txtTotal.Value = Math.Round(txtNetto.Value * txtPrice.Value, 0);
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

        private void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 id, code, receiptCode, DOCode, millCode, arrived, finished, netto, price, note FROM sales WHERE id < @id ORDER BY id DESC", con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                this.id = reader["id"].ToString();
                                txtCode.Text = reader["code"].ToString();
                                txtReceiptCode.Text = reader["receiptCode"].ToString();
                                txtDOCode.Text = reader["DOCode"].ToString();
                                txtMillCode.Text = reader["millCode"].ToString();
                                dtpArrived.Value = DateTime.Parse(reader["arrived"].ToString());
                                dtpFinished.Value = DateTime.Parse(reader["finished"].ToString());
                                txtNetto.Value = int.Parse(reader["netto"].ToString());
                                txtPrice.Value = Decimal.Parse(reader["price"].ToString());
                                txtNote.Text = reader["note"].ToString();
                                txtTotal.Value = Math.Round(txtNetto.Value * txtPrice.Value, 0);
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

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 id, code, receiptCode, DOCode, millCode, arrived, finished, netto, price, note FROM sales WHERE id > @id ORDER BY id ASC", con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                this.id = reader["id"].ToString();
                                txtCode.Text = reader["code"].ToString();
                                txtReceiptCode.Text = reader["receiptCode"].ToString();
                                txtDOCode.Text = reader["DOCode"].ToString();
                                txtMillCode.Text = reader["millCode"].ToString();
                                dtpArrived.Value = DateTime.Parse(reader["arrived"].ToString());
                                dtpFinished.Value = DateTime.Parse(reader["finished"].ToString());
                                txtNetto.Value = int.Parse(reader["netto"].ToString());
                                txtPrice.Value = Decimal.Parse(reader["price"].ToString());
                                txtNote.Text = reader["note"].ToString();
                                txtTotal.Value = Math.Round(txtNetto.Value * txtPrice.Value, 0);
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
    }
}
