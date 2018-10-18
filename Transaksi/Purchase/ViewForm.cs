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

namespace Purchase
{
    public partial class ViewForm : Syncfusion.Windows.Forms.MetroForm 
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";
        string id = "";
        string code = "";

        public ViewForm(string id, string username)
        {
            InitializeComponent();
            this.id = id;
            this.username = username;
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            Load_Data();
            Grid_Load();
        }

        private void Load_Data()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 code, date, relationCode, bruto, bonus, downpayment, total, note FROM purchase WHERE id = @id", con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                txtCode.Text = code = reader["code"].ToString();
                                dtpDate.Value = DateTime.Parse(reader["date"].ToString());
                                txtRelationCode.Text = reader["relationCode"].ToString();
                                int.TryParse(reader["bruto"].ToString(), out int bruto);
                                txtBruto.Value = bruto;
                                int.TryParse(reader["downpayment"].ToString(), out int downpayment);
                                txtDP.Value = downpayment;
                                int.TryParse(reader["bonus"].ToString(), out int bonus);
                                txtBonus.Value = bonus;
                                int.TryParse(reader["total"].ToString(), out int total);
                                txtTotal.Value = total;
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

        private void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 id, code, date, relationCode, bonus, bruto, downpayment, total, note FROM purchase WHERE id < @id ORDER BY id DESC", con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id = reader["id"].ToString();
                                txtCode.Text = code = reader["code"].ToString();
                                dtpDate.Value = DateTime.Parse(reader["date"].ToString());
                                txtRelationCode.Text = reader["relationCode"].ToString();
                                int.TryParse(reader["bruto"].ToString(), out int bruto);
                                txtBruto.Value = bruto;
                                int.TryParse(reader["downpayment"].ToString(), out int downpayment);
                                txtDP.Value = downpayment;
                                int.TryParse(reader["bonus"].ToString(), out int bonus);
                                txtBonus.Value = bonus;
                                int.TryParse(reader["total"].ToString(), out int total);
                                txtTotal.Value = total;
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
            Grid_Load();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 id, code, date, relationCode, bonus, bruto, downpayment, total, note FROM purchase WHERE id > @id ORDER BY id ASC", con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                this.id = reader["id"].ToString();
                                txtCode.Text = code = reader["code"].ToString();
                                dtpDate.Value = DateTime.Parse(reader["date"].ToString());
                                txtRelationCode.Text = reader["relationCode"].ToString();
                                int.TryParse(reader["bruto"].ToString(), out int bruto);
                                txtBruto.Value = bruto;
                                int.TryParse(reader["downpayment"].ToString(), out int downpayment);
                                txtDP.Value = downpayment;
                                int.TryParse(reader["bonus"].ToString(), out int bonus);
                                txtBonus.Value = bonus;
                                int.TryParse(reader["total"].ToString(), out int total);
                                txtTotal.Value = total;
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
            Grid_Load();
        }

        public void Grid_Load()
        {
            try
            {
                String sqlString = "SELECT ID, salesCode, netto, price, total " +
                    "FROM purchaseDetails " +
                    "WHERE purchaseID = @purchaseID " +
                    "AND (1=1) " +
                    "ORDER BY ID";

                using (SqlDataAdapter da = new SqlDataAdapter(sqlString, connString))
                {
                    da.SelectCommand.Parameters.Add("@purchaseID", SqlDbType.VarChar).Value = this.id;

                    using (DataSet ds = new DataSet())
                    {
                        da.Fill(ds, "data");

                        DataTable dt = new DataTable();
                        dt = ds.Tables["data"];

                        gridView.DataSource = dt;
                        gridView.Columns[0].Visible = false;
                        gridView.Columns[0].HeaderText = "ID";
                        gridView.Columns[1].HeaderText = "No. Ticket";
                        gridView.Columns[2].DefaultCellStyle.Format = "N0";
                        gridView.Columns[2].HeaderText = "Netto (Kg)";
                        gridView.Columns[2].ReadOnly = true;
                        gridView.Columns[3].DefaultCellStyle.Format = "N0";
                        gridView.Columns[3].HeaderText = "Harga (Rp)";
                        gridView.Columns[4].DefaultCellStyle.Format = "N0";
                        gridView.Columns[4].HeaderText = "Total";
                        gridView.Columns[4].ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                rptPurchaseForm report = new rptPurchaseForm(id);
                report.ShowDialog();
                this.SuspendLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
