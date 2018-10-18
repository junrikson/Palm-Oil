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
    public partial class dialogReceiptForm : Syncfusion.Windows.Forms.MetroForm
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";
        string typeForm = "";
        string id = "";

        public dialogReceiptForm(string username, string typeForm, string id = "")
        {
            this.typeForm = typeForm;
            this.username = username;
            this.id = id;
            InitializeComponent();
            GridSettings();
            Grid_Load();
        }

        private void dialogReceiptForm_Load(object sender, EventArgs e)
        {
            txtFilterValue.Select();
        }

        #region Actions     
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string code = "";
            string relationName = "";
            string relationCode = "";
            string note = "";

            if (cmbFilterColumn.SelectedItem.ToString() == "Nama Mitra")
            {
                relationName = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "Kode Tanda Terima")
            {
                code = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "Kode Mitra")
            {
                relationCode = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "Keterangan")
            {
                note = txtFilterValue.Text;
            }

            Grid_Load(code, relationCode, relationName, note);
        }
        #endregion

        #region Grid

        public void Grid_Load(string code = "", string relationCode = "", string relationName = "", string note = "")
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Kode Tanda Terima", typeof(String));
            dt.Columns.Add("Waktu", typeof(String));
            dt.Columns.Add("Kode Mitra", typeof(String));
            dt.Columns.Add("Nama Mitra", typeof(String));
            dt.Columns.Add("Nomor Kendaraan", typeof(String));
            dt.Columns.Add("Supir", typeof(String));
            dt.Columns.Add("Keterangan", typeof(String));

            try
            {
                String sqlString = "SELECT a.id, a.code, a.date, b.code as relationCode, b.name as relationName, a.licensePlate, a.driver, a.note " +
                    "FROM receipt a INNER JOIN masterRelation b " +
                    "ON a.relationCode = b.code " +
                    "LEFT JOIN sales c ON a.code = c.receiptCode WHERE " +
                    "c.receiptCode IS NULL " +
                    "AND (a.code like @code OR a.code IS NULL) " +
                    "AND (b.code like @relationCode OR b.code IS NULL) " +
                    "AND (b.name like @relationName OR b.name IS NULL) " +
                    "AND (a.note like @note OR a.note IS NULL) " +
                    "AND (1=1)";

                using (SqlDataAdapter da = new SqlDataAdapter(sqlString, connString))
                {
                    da.SelectCommand.Parameters.Add("@code", SqlDbType.VarChar).Value = code + '%';
                    da.SelectCommand.Parameters.Add("@relationCode", SqlDbType.VarChar).Value = relationCode + '%';
                    da.SelectCommand.Parameters.Add("@relationName", SqlDbType.VarChar).Value = relationName + '%';
                    da.SelectCommand.Parameters.Add("@note", SqlDbType.VarChar).Value = note + '%';

                    using (DataSet ds = new DataSet())
                    {
                        da.Fill(ds, "data");

                        DataTable tDT = new DataTable();
                        tDT = ds.Tables["data"];

                        for (int i = 0; i < tDT.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            DataRow tDR = tDT.Rows[i];

                            dr["ID"] = tDR["id"];
                            dr["Kode Tanda Terima"] = tDR["code"];
                            dr["Waktu"] = tDR["date"];
                            dr["Kode Mitra"] = tDR["relationCode"];
                            dr["Nama Mitra"] = tDR["relationName"];
                            dr["Nomor Kendaraan"] = tDR["licensePlate"];
                            dr["Supir"] = tDR["driver"];
                            dr["Keterangan"] = tDR["note"];

                            dt.Rows.Add(dr);
                        }

                        gridView.DataSource = dt;
                        gridView.Columns["ID"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void GridSettings()
        {
            cmbFilterColumn.Items.Add("Kode Tanda Terima");
            cmbFilterColumn.Items.Add("Kode Mitra");
            cmbFilterColumn.Items.Add("Nama Mitra");
            cmbFilterColumn.Items.Add("Keterangan");
            cmbFilterColumn.SelectedItem = "Kode Tanda Terima";
        }
        #endregion

        private void gridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridView.SelectedRows.Count > 0)
            {
                if (typeForm == "NewForm")
                {
                    string code = gridView.SelectedRows[0].Cells[1].Value.ToString();
                    NewForm parentForm = new NewForm(username);
                    ((NewForm)this.Owner).btnReceiptClick(code);
                    this.Close();
                }
                else if (typeForm == "EditForm")
                {
                    string code = gridView.SelectedRows[0].Cells[1].Value.ToString();
                    EditForm parentForm = new EditForm(username, this.id);
                    ((EditForm)this.Owner).btnReceiptClick(code);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Pilih transaksi yang akan ditambahkan!");
            }
        }

        private void sfButton1_Click(object sender, EventArgs e)
        {
            if (gridView.SelectedRows.Count > 0)
            {
                if (typeForm == "NewForm")
                {
                    string code = gridView.SelectedRows[0].Cells[1].Value.ToString();
                    NewForm parentForm = new NewForm(username);
                    ((NewForm)this.Owner).btnReceiptClick(code);
                    this.Close();
                }
                else if (typeForm == "EditForm")
                {
                    string code = gridView.SelectedRows[0].Cells[1].Value.ToString();
                    EditForm parentForm = new EditForm(username, this.id);
                    ((EditForm)this.Owner).btnReceiptClick(code);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Pilih transaksi yang akan ditambahkan!");
            }
        }
    }
}
