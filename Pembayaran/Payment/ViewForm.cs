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

namespace Payment
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
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 code, date, total, note FROM payment WHERE id = @id", con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                txtCode.Text = code = reader["code"].ToString();
                                dtpDate.Value = DateTime.Parse(reader["date"].ToString());
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
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 id, code, date, total, note FROM payment WHERE id < @id ORDER BY id DESC", con))
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
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 id, code, date, total, note FROM payment WHERE id > @id ORDER BY id ASC", con))
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
                String sqlString = "SELECT a.ID, b.code as purchaseCode, c.name as relationName, b.total as purchaseTotal, a.ammount as paymentAmmount " +
                    "FROM paymentDetails a INNER JOIN purchase b " +
                    "ON a.purchaseCode = b.code " +
                    "INNER JOIN masterRelation c ON b.relationCode = c.code " +
                    "WHERE paymentID = @paymentID " +
                    "AND (1=1) " +
                    "ORDER BY a.ID";

                using (SqlDataAdapter da = new SqlDataAdapter(sqlString, connString))
                {
                    da.SelectCommand.Parameters.Add("@paymentID", SqlDbType.VarChar).Value = this.id;

                    using (DataSet ds = new DataSet())
                    {
                        da.Fill(ds, "data");

                        DataTable dt = new DataTable();
                        dt = ds.Tables["data"];

                        gridView.DataSource = dt;
                        gridView.Columns[0].Visible = false;
                        gridView.Columns[0].HeaderText = "ID";
                        gridView.Columns[1].HeaderText = "No. Invoice";
                        gridView.Columns[2].HeaderText = "Mitra";
                        gridView.Columns[2].ReadOnly = true;
                        gridView.Columns[3].DefaultCellStyle.Format = "N0";
                        gridView.Columns[3].HeaderText = "Nilai Invoice (Rp)";
                        gridView.Columns[3].ReadOnly = true;
                        gridView.Columns[4].DefaultCellStyle.Format = "N0";
                        gridView.Columns[4].HeaderText = "Pembayaran (Rp)";
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
                rptPaymentForm report = new rptPaymentForm(id);
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
