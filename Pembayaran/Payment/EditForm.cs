#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Payment
{
    public partial class EditForm : Syncfusion.Windows.Forms.MetroForm 
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";
        string id = "";
        string code = "";
        int row = 0;
        int column = 1;

        public EditForm(string id, string username)
        {
            this.username = username;
            this.id = id;
            InitializeComponent();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            Load_Data();
            Grid_Load();
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(SaveData())
                this.Close();
        }

        private bool SaveData()
        {
            bool status = false;
            if (isValid())
            {
                try
                {
                    string query = "UPDATE payment SET " +
                               "code = @code, " +
                               "date = @date, " +
                               "total = (SELECT SUM(ammount) FROM paymentDetails WHERE paymentID = @ID), " +
                               "note = @note, " +
                               "updated = getdate(), " +
                               "username = @username " +
                               "WHERE ID = @ID";

                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = txtCode.Text;
                            cmd.Parameters.Add("@date", SqlDbType.Date).Value = dtpDate.Value;
                            cmd.Parameters.Add("@note", SqlDbType.VarChar).Value = txtNote.Text;
                            cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = this.id;

                            con.Open();
                            cmd.ExecuteNonQuery();
                            status = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            return status;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveData())
                {
                    rptPaymentForm report = new rptPaymentForm(id);
                    report.ShowDialog();
                    this.SuspendLayout();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private Boolean isValid()
        {
            Boolean isValidated = true;

            if (!(String.IsNullOrEmpty(txtCode.Text)))
            {
                if (txtCode.Text.Length < 10)
                {
                    isValidated = false;
                    MessageBox.Show("No. Transaksi harus lebih dari 10 karakter!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Select();
                }
                else if (!txtCode.Text.Substring(txtCode.Text.Length - 10).All(char.IsDigit))
                {
                    isValidated = false;
                    MessageBox.Show("10 Karakter terakhir harus terdiri dari angka dengan format YYMMDDNNNN!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Select();
                }
            }
            return isValidated;
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
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

            try
            {
                gridView.CurrentCell = gridView.Rows[this.row].Cells[this.column];
            }
            catch { }
        }

        private void gridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string id = gridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            string code = gridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            int.TryParse(gridView.Rows[e.RowIndex].Cells[4].Value.ToString(), out int ammount);

            if (!string.IsNullOrEmpty(code))
                if (checkData(code, ammount, id))
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        try
                        {
                            string query = "UPDATE paymentDetails SET purchaseCode = @code, ammount = @ammount WHERE ID = @ID ";

                            using (SqlConnection con = new SqlConnection(connString))
                            {
                                using (SqlCommand cmd = new SqlCommand(query, con))
                                {
                                    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = id;
                                    cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = code;
                                    cmd.Parameters.Add("@ammount", SqlDbType.Int).Value = ammount;

                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    updateTotal();
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
                        try
                        {
                            string query = "INSERT INTO paymentDetails (paymentID, purchaseCode, ammount) " +
                                "SELECT TOP 1 @paymentID, code, total FROM purchase WHERE code = @code";

                            using (SqlConnection con = new SqlConnection(connString))
                            {
                                using (SqlCommand cmd = new SqlCommand(query, con))
                                {
                                    cmd.Parameters.Add("@paymentID", SqlDbType.VarChar).Value = this.id;
                                    cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = code;

                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    updateTotal();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invoice tidak ditemukan atau sudah lunas!. Pastikan nilai pembayaran tidak melebihi sisa Invoice.");
                }

            try
            {
                BeginInvoke(new MethodInvoker(() =>
                    Grid_Load()
                ));
            }
            catch { }
        }

        private void gridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string code = e.FormattedValue.ToString();
            string headerText = gridView.Columns[e.ColumnIndex].HeaderText;
            this.row = e.RowIndex;
            this.column = e.ColumnIndex;

            if (headerText.Equals("No. Invoice"))
            {
                if (string.IsNullOrEmpty(code)) return;
            }
            else if (headerText.Equals("Pembayaran (Rp)"))
            {
                if (string.IsNullOrEmpty(code)) return;
                else
                {
                    int.TryParse(code.Replace(".", "").Replace(",", ""), out int price);
                    if (price <= 0)
                    {
                        MessageBox.Show("Pembayaran harus lebih dari nol!");
                        e.Cancel = true;
                    }
                }
            }
            else
                return;
        }

        private void gridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string headerText = gridView.Columns[e.ColumnIndex].HeaderText;

            if (!headerText.Equals("No. Invoice")) return;

            Form dialogForm = new dialogPurchaseForm(username, "EditForm", this.id);
            dialogForm.ShowDialog(this);
        }
        
        public void callWhenChildClick(List<string> codes)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    SqlCommand cmd;

                    foreach (string code in codes)
                    {
                        if (!string.IsNullOrEmpty(code))
                        {
                            cmd = new SqlCommand("INSERT INTO paymentDetails (paymentID, purchaseCode, ammount) " +
                            "SELECT TOP 1 @paymentID, code, (total-paid) FROM View_PurchasePayment WHERE code = @code", con, tran);
                            cmd.Parameters.Add("@paymentID", SqlDbType.VarChar).Value = this.id;
                            cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = code;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    try
                    {
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                updateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            gridView.CellValidating -= gridView_CellValidating;
            Grid_Load();
            gridView.CellValidating += gridView_CellValidating;
        }

        private void gridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string code = e.Row.Cells[1].Value.ToString();
            string id = e.Row.Cells[0].Value.ToString();

            DialogResult dialogResult = MessageBox.Show("Are you sure want to delete " + code + " ?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM paymentDetails WHERE ID = @id", con))
                        {
                            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            updateTotal();
                        }
                    }
                }
                catch (Exception ex)
                {
                    e.Cancel = true;
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private Boolean checkData(string code, int ammount, string id)
        {
            bool isValid = false;

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 code FROM View_PurchasePayment WHERE " +
                        "((total-paid+ISNULL((SELECT ammount FROM paymentDetails WHERE ID = @ID),0)) > 0) AND " +
                        "((total-paid-@ammount+ISNULL((SELECT ammount FROM paymentDetails WHERE ID = @ID),0)) >= 0) " +
                        "AND code = @code ", con))
                    {
                        cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = code;
                        cmd.Parameters.Add("@ammount", SqlDbType.Int).Value = ammount;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
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

        private void updateTotal()
        {
            try
            {
                string query = "UPDATE payment SET total = " +
                    "(SELECT SUM(ammount) FROM paymentDetails WHERE paymentID = @ID) OUTPUT INSERTED.total " +
                    "WHERE ID = @ID ";

                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = this.id;

                        con.Open();
                        int.TryParse(cmd.ExecuteScalar().ToString(), out int total);

                        try
                        {
                            txtTotal.Value = total;
                        }
                        catch { }
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
