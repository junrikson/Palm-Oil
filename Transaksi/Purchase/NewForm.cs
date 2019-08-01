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

namespace Purchase
{
    public partial class NewForm : Syncfusion.Windows.Forms.MetroForm
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";
        string id = "";
        string code = "";
        int row = 0;
        int column = 1;
        bool onCloseDelete = true;

        public NewForm(string username)
        {
            this.username = username;
            InitializeComponent();
        }

        private void NewForm_Load(object sender, EventArgs e)
        {
            createDataHeader();
            doClear();
            Load_Data();
            Grid_Load();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            doClear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
                this.Close();
        }

        private bool SaveData()
        {
            bool status = false;
            if (isValid())
            {
                try
                {
                    string query = "UPDATE purchase SET " +
                               "code = @code, " +
                               "date = @date, " +
                               "relationCode = @relationCode, " +
                               "bruto = (SELECT SUM(total) FROM purchaseDetails WHERE purchaseID = @ID), " +
                               "downpayment = @downpayment, " +
                               "bonus = @bonus, " +
                               "note = @note, " +
                               "status = 1, " +
                               "updated = getdate(), " +
                               "username = @username " +
                               "WHERE ID = @ID";

                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = txtCode.Text;
                            cmd.Parameters.Add("@date", SqlDbType.Date).Value = dtpDate.Value;
                            cmd.Parameters.Add("@relationCode", SqlDbType.VarChar).Value = txtRelationCode.Text;
                            cmd.Parameters.Add("@downpayment", SqlDbType.Int).Value = txtDP.Value;
                            cmd.Parameters.Add("@bonus", SqlDbType.Int).Value = txtBonus.Value;
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
                onCloseDelete = false;
            }
            return status;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveData())
                {
                    rptPurchaseForm report = new rptPurchaseForm(id);
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

        private void doClear()
        {
            txtCode.ReadOnly = true;
            txtCode.Text = code;
            chkManual.Checked = false;
            dtpDate.Value = DateTime.Today;
            txtNote.Text = "";
            txtBruto.Value = 0;
            txtDP.Value = 0;
            txtTotal.Value = 0;
            txtRelationCode.Text = "";
            txtRelationCode.Select();
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

            if (isValidated == true)
            {
                if (checkCode(txtCode.Text))
                {
                    isValidated = false;
                    MessageBox.Show("Nomor Invoice ini sudah pernah digunakan!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 code FROM purchase WHERE code = @code and ID <> @ID ", con))
                    {
                        cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = code;
                        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = this.id;
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

        private void createDataHeader()
        {
            try
            {
                string query = "INSERT INTO purchase" +
                        "(code, date, bruto, downpayment, bonus, status, updated, created, username) output INSERTED.ID VALUES " +
                        "(@code + (SELECT RIGHT('000000000' + CAST((ISNULL((SELECT TOP 1 RIGHT(code,10) " +
                        "   FROM purchase " +
                        "   WHERE LEFT(code,3) = @code " +
                        "   ORDER BY RIGHT(code,10) DESC),0) + 1) AS VARCHAR(10)),10))," +
                        "@date, 0, 0, 0, 0, getdate(), getdate(), @username)";

                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = "PO/";
                        cmd.Parameters.Add("@date", SqlDbType.Date).Value = dtpDate.Value;
                        cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;

                        con.Open();
                        id = cmd.ExecuteScalar().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Load_Data()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 code FROM purchase WHERE id = @id", con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                txtCode.Text = code = reader["code"].ToString();
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

        private void chkManual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManual.Checked == true)
            {
                txtCode.ReadOnly = false;
            }
            else
            {
                txtCode.ReadOnly = true;
                txtCode.Text = code;
            }
        }

        private void NewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (onCloseDelete)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        con.Open();
                        SqlTransaction tran = con.BeginTransaction();
                        SqlCommand cmd;

                        cmd = new SqlCommand("DELETE FROM purchaseDetails WHERE purchaseID = @purchaseID", con, tran);
                        cmd.Parameters.Add("@purchaseID", SqlDbType.VarChar).Value = this.id;
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("DELETE FROM purchase WHERE ID = @ID", con, tran);
                        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = this.id;
                        cmd.ExecuteNonQuery();

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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
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
            int.TryParse(gridView.Rows[e.RowIndex].Cells[3].Value.ToString(), out int price);

            if (!string.IsNullOrEmpty(code))
                if (checkTicket(code, id))
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        try
                        {
                            string query = "UPDATE purchaseDetails SET salesCode = @code, price = @price WHERE ID = @ID ";

                            using (SqlConnection con = new SqlConnection(connString))
                            {
                                using (SqlCommand cmd = new SqlCommand(query, con))
                                {
                                    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = id;
                                    cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = code;
                                    cmd.Parameters.Add("@price", SqlDbType.Int).Value = price;

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
                            string query = "INSERT INTO purchaseDetails (purchaseID, salesCode, netto, price) " +
                                "SELECT TOP 1 @purchaseID, code, netto, ROUND(price,0) FROM sales WHERE code = @code";

                            using (SqlConnection con = new SqlConnection(connString))
                            {
                                using (SqlCommand cmd = new SqlCommand(query, con))
                                {
                                    cmd.Parameters.Add("@purchaseID", SqlDbType.VarChar).Value = this.id;
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
                    MessageBox.Show("Ticket tidak ditemukan atau sudah pernah digunakan!");
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

            if (headerText.Equals("No. Ticket"))
            {
                if (string.IsNullOrEmpty(code)) return;
            }
            else if (headerText.Equals("Harga (Rp)"))
            {
                if (string.IsNullOrEmpty(code)) return;
                else
                {
                    int.TryParse(code.Replace(".", "").Replace(",", ""), out int price);
                    if (price <= 0)
                    {
                        MessageBox.Show("Harga harus lebih dari nol!");
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

            if (!headerText.Equals("No. Ticket")) return;

            Form dialogForm = new dialogSalesForm(username, "NewForm", this.id, txtRelationCode.Text);
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
                            cmd = new SqlCommand("INSERT INTO purchaseDetails (purchaseID, salesCode, netto, price) " +
                            "SELECT TOP 1 @purchaseID, code, netto, ROUND(price,0) FROM sales WHERE code = @code", con, tran);
                            cmd.Parameters.Add("@purchaseID", SqlDbType.VarChar).Value = this.id;
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
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM purchaseDetails WHERE ID = @id", con))
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

        public void btnRelationClick(string relationCode)
        {
            txtRelationCode.Text = relationCode;
        }

        private void btnRelation_Click(object sender, EventArgs e)
        {
            Form dialogForm = new dialogRelationForm(username, "NewForm");
            dialogForm.ShowDialog(this);
        }

        private Boolean checkTicket(string ticket, string id)
        {
            bool isValid = false;

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 a.code FROM " +
                        "sales a INNER JOIN receipt c " +
                        "ON a.receiptCode = c.code " +
                        "LEFT JOIN purchaseDetails b " +
                        "ON a.code = b.salesCode " +
                        "WHERE a.code = @ticket " +
                        "AND c.relationCode = @relationCode " +
                        "AND (b.id IS NULL OR b.id = @id) ", con))
                    {
                        cmd.Parameters.Add("@ticket", SqlDbType.VarChar).Value = ticket;
                        cmd.Parameters.Add("@relationCode", SqlDbType.VarChar).Value = txtRelationCode.Text;
                        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
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
                string query = "UPDATE purchase SET bruto = " +
                    "(SELECT SUM(total) FROM purchaseDetails WHERE purchaseID = @ID) OUTPUT INSERTED.bruto " +
                    "WHERE ID = @ID ";

                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = this.id;

                        con.Open();
                        int.TryParse(cmd.ExecuteScalar().ToString(), out int bruto);

                        try
                        {
                            txtBruto.Value = bruto;
                            txtTotal.Value = bruto - txtDP.Value + txtBonus.Value;
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

        private void txtDP_ValueChanged(object sender, EventArgs e)
        {
            txtTotal.Value = txtBruto.Value + txtBonus.Value - txtDP.Value;
        }

        private void txtBonus_ValueChanged(object sender, EventArgs e)
        {
            txtTotal.Value = txtBruto.Value + txtBonus.Value - txtDP.Value;
        }
    }
}
