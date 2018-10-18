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

namespace UserManager
{
    public partial class dialogRolesForm : Syncfusion.Windows.Forms.MetroForm
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";
        string typeForm = "";
        string id = "";

        public dialogRolesForm(string username, string typeForm, string id = "")
        {
            this.typeForm = typeForm;
            this.username = username;
            this.id = id;
            InitializeComponent();
            GridSettings();
            Grid_Load();
        }

        private void dialogRolesForm_Load(object sender, EventArgs e)
        {
            txtFilterValue.Select();
        }

        #region Actions        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string code = "";
            string name = "";

            if (cmbFilterColumn.SelectedItem.ToString() == "Kode Role")
            {
                code = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "Nama Role")
            {
                name = txtFilterValue.Text;
            }

            Grid_Load(code, name);
        }
        #endregion

        #region Grid

        public void Grid_Load(string code = "", string name = "")
        {
            try
            {
                String sqlString = "SELECT id, code, name, updated, created, username FROM masterRoles WHERE " +
                    "(code like @code OR code IS NULL) " +
                    "AND (name like @name OR name IS NULL) " +
                    "AND status = 1 " +
                    "AND (1=1)";

                SqlDataAdapter da = new SqlDataAdapter(sqlString, connString);
                da.SelectCommand.Parameters.Add("@code", SqlDbType.VarChar).Value = code + '%';
                da.SelectCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = name + '%';

                DataSet ds = new DataSet();
                da.Fill(ds, "data");

                DataTable dt = new DataTable();
                dt = ds.Tables["data"];

                gridView.DataSource = dt;

                gridView.Columns["id"].Visible = false;
                gridView.Columns["id"].HeaderText = "ID";
                gridView.Columns["code"].HeaderText = "Kode Role";
                gridView.Columns["name"].HeaderText = "Nama Role";
                gridView.Columns["updated"].HeaderText = "Diubah";
                gridView.Columns["created"].HeaderText = "Dibuat";
                gridView.Columns["username"].HeaderText = "User";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void GridSettings()
        {
            cmbFilterColumn.Items.Add("Kode Role");
            cmbFilterColumn.Items.Add("Nama Role");
            cmbFilterColumn.Items.Add("Keterangan");
            cmbFilterColumn.SelectedItem = "Kode Role";
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
                    ((NewForm)this.Owner).btnRoleClick(code);
                    this.Close();
                }
                else if (typeForm == "EditForm")
                {
                    string code = gridView.SelectedRows[0].Cells[1].Value.ToString();
                    EditForm parentForm = new EditForm(username, this.id);
                    ((EditForm)this.Owner).btnRoleClick(code);
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
                    ((NewForm)this.Owner).btnRoleClick(code);
                    this.Close();
                }
                else if (typeForm == "EditForm")
                {
                    string code = gridView.SelectedRows[0].Cells[1].Value.ToString();
                    EditForm parentForm = new EditForm(username, this.id);
                    ((EditForm)this.Owner).btnRoleClick(code);
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
