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
using System.Windows.Forms;

namespace Purchase
{
    public partial class dialogSalesForm : Syncfusion.Windows.Forms.MetroForm
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";
        string typeForm = "";
        string id = "";
        string relationCode = "";

        public dialogSalesForm(string username, string typeForm, string id, string relationCode)
        {
            this.username = username;
            this.typeForm = typeForm;
            this.id = id;
            this.relationCode = relationCode;
            InitializeComponent();
            GridSettings();
            Grid_Load();
        }

        private void dialogSalesForm_Load(object sender, EventArgs e)
        {
            txtFilterValue.Select();
        }

        #region Actions        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string code = "";
            string name = "";
            string DOCode = "";
            string receiptCode = "";
            string relationName = "";
            string note = "";

            if (cmbFilterColumn.SelectedItem.ToString() == "No. Ticket")
            {
                code = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "No. DO")
            {
                DOCode = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "No. Tanda Terima")
            {
                receiptCode = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "Mitra")
            {
                relationName = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "Keterangan")
            {
                note = txtFilterValue.Text;
            }

            Grid_Load(code, DOCode, receiptCode, relationName, note);
        }
        #endregion

        #region Grid

        public void Grid_Load(string code = "", string DOCode = "", string receiptCode = "", string relationName = "", string note = "")
        {
            try
            {
                String sqlString = "SELECT a.id, a.code, a.DOCode, b.code AS receiptCode, c.name as relationName, a.arrived, a.finished, a.netto, a.price, a.total, a.note " +
                    "FROM sales a INNER JOIN receipt b ON a.receiptCode = b.code " +
                    "INNER JOIN masterRelation c ON b.relationCode = c.code " +
                    "LEFT JOIN purchaseDetails d ON a.code = d.salesCode WHERE " +
                    "d.ID IS NULL " +
                    "AND c.code = @relationCode " +
                    "AND (a.code like @code OR a.code IS NULL) " +
                    "AND (a.DOCode like @DOCode OR a.DOCode IS NULL) " +
                    "AND (b.code like @receiptCode OR b.code IS NULL) " +
                    "AND (c.name like @relationName OR c.name IS NULL) " +
                    "AND (a.note like @note OR a.note IS NULL) " +
                    "AND (1=1)";

                SqlDataAdapter da = new SqlDataAdapter(sqlString, connString);
                da.SelectCommand.Parameters.Add("@code", SqlDbType.VarChar).Value = code + '%';
                da.SelectCommand.Parameters.Add("@DOCode", SqlDbType.VarChar).Value = DOCode + '%';
                da.SelectCommand.Parameters.Add("@receiptCode", SqlDbType.VarChar).Value = receiptCode + '%';
                da.SelectCommand.Parameters.Add("@relationCode", SqlDbType.VarChar).Value = relationCode;
                da.SelectCommand.Parameters.Add("@relationName", SqlDbType.VarChar).Value = relationName + '%';
                da.SelectCommand.Parameters.Add("@note", SqlDbType.VarChar).Value = note + '%';

                DataSet ds = new DataSet();
                da.Fill(ds, "data");

                DataTable dt = new DataTable();
                dt = ds.Tables["data"];

                gridView.DataSource = dt;

                gridView.Columns[0].Visible = false;
                gridView.Columns[0].HeaderText = "ID";
                gridView.Columns[1].HeaderText = "No. Ticket";
                gridView.Columns[2].HeaderText = "No. DO";
                gridView.Columns[3].HeaderText = "No. Tanda Terima";
                gridView.Columns[4].HeaderText = "Mitra";
                gridView.Columns[5].HeaderText = "Waktu Tiba";
                gridView.Columns[6].HeaderText = "Waktu Kembali";
                gridView.Columns[7].HeaderText = "Netto (Kg)";
                gridView.Columns[8].HeaderText = "Harga (Rp)";
                gridView.Columns[9].HeaderText = "Total (Rp)";
                gridView.Columns[10].HeaderText = "Keterangan";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void GridSettings()
        {
            cmbFilterColumn.Items.Add("No. Ticket");
            cmbFilterColumn.Items.Add("No. DO");
            cmbFilterColumn.Items.Add("No. Tanda Terima");
            cmbFilterColumn.Items.Add("Mitra");
            cmbFilterColumn.Items.Add("Keterangan");
            cmbFilterColumn.SelectedItem = "No. Ticket";
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (gridView.SelectedRows.Count > 0)
            {
                if (typeForm == "EditForm")
                {
                    List<string> codes = new List<string>();
                    foreach (DataGridViewRow row in gridView.SelectedRows)
                    {
                        codes.Add(row.Cells[1].Value.ToString());
                    }

                    EditForm parentForm = new EditForm(id, username);
                    ((EditForm)this.Owner).callWhenChildClick(codes);
                }
                else
                {
                    List<string> codes = new List<string>();
                    foreach (DataGridViewRow row in gridView.SelectedRows)
                    {
                        codes.Add(row.Cells[1].Value.ToString());
                    }

                    NewForm parentForm = new NewForm(username);
                    ((NewForm)this.Owner).callWhenChildClick(codes);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Pilih transaksi yang akan ditambahkan!");
            }
        }

        private void gridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridView.SelectedRows.Count > 0)
            {
                if (typeForm == "EditForm")
                {
                    List<string> codes = new List<string>();
                    foreach (DataGridViewRow row in gridView.SelectedRows)
                    {
                        codes.Add(row.Cells[1].Value.ToString());
                    }

                    EditForm parentForm = new EditForm(id, username);
                    ((EditForm)this.Owner).callWhenChildClick(codes);
                }
                else
                {
                    List<string> codes = new List<string>();
                    foreach (DataGridViewRow row in gridView.SelectedRows)
                    {
                        codes.Add(row.Cells[1].Value.ToString());
                    }

                    NewForm parentForm = new NewForm(username);
                    ((NewForm)this.Owner).callWhenChildClick(codes);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Pilih transaksi yang akan ditambahkan!");
            }
        }
    }
}
