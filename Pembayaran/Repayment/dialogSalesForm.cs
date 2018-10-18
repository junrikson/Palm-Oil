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

namespace Repayment
{
    public partial class dialogSalesForm : Syncfusion.Windows.Forms.MetroForm
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";
        string typeForm = "";
        string id = "";

        public dialogSalesForm(string username, string typeForm, string id)
        {
            this.username = username;
            this.typeForm = typeForm;
            this.id = id;
            InitializeComponent();
            GridSettings();
            Grid_Load();
        }

        private void dialogPurchaseForm_Load(object sender, EventArgs e)
        {
            txtFilterValue.Select();
        }

        #region Actions        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string code = "";
            string relationCode = "";
            string relationName = "";

            if (cmbFilterColumn.SelectedItem.ToString() == "No. Ticket")
            {
                code = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "Kode Mitra")
            {
                relationCode = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "Nama Mitra")
            {
                relationName = txtFilterValue.Text;
            }

            Grid_Load(code, relationCode, relationName);
        }
        #endregion

        #region Grid

        public void Grid_Load(string code = "", string relationCode = "", string relationName = "")
        {
            try
            {
                String sqlString = "SELECT id, code, DOCode, receiptCode, relationCode, relationName, total, (total-paid) as remaining " +
                    "FROM View_SalesRepayment WHERE " +
                    "(total-paid) > 0 " +
                    "AND (code like @code OR code IS NULL) " +
                    "AND (relationCode like @relationCode OR relationCode IS NULL) " +
                    "AND (relationName like @relationName OR relationName IS NULL) " +
                    "AND (1=1)";

                SqlDataAdapter da = new SqlDataAdapter(sqlString, connString);
                da.SelectCommand.Parameters.Add("@code", SqlDbType.VarChar).Value = code + '%';
                da.SelectCommand.Parameters.Add("@relationCode", SqlDbType.VarChar).Value = relationCode + '%';
                da.SelectCommand.Parameters.Add("@relationName", SqlDbType.VarChar).Value = relationName + '%';

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
                gridView.Columns[4].HeaderText = "Kode Mitra";
                gridView.Columns[5].HeaderText = "Nama Mitra";
                gridView.Columns[6].HeaderText = "Jumlah Invoice";
                gridView.Columns[7].HeaderText = "Sisa Invoice";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void GridSettings()
        {
            cmbFilterColumn.Items.Add("No. Ticket");
            cmbFilterColumn.Items.Add("Kode Mitra");
            cmbFilterColumn.Items.Add("Nama Mitra");
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
