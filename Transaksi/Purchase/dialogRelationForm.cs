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
    public partial class dialogRelationForm : Syncfusion.Windows.Forms.MetroForm
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";
        string typeForm = "";
        string id = "";

        public dialogRelationForm(string username, string typeForm, string id = "")
        {
            this.typeForm = typeForm;
            this.username = username;
            this.id = id;
            InitializeComponent();
            GridSettings();
            Grid_Load();
        }

        private void dialogContainerForm_Load(object sender, EventArgs e)
        {
            txtFilterValue.Select();
        }

        #region Actions     
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string code = "";
            string name = "";
            string address = "";
            string note = "";
            
            if (cmbFilterColumn.SelectedItem.ToString() == "Nama Mitra")
            {
                name = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "Kode Mitra")
            {
                code = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "Alamat")
            {
                address = txtFilterValue.Text;
            }
            else if (cmbFilterColumn.SelectedItem.ToString() == "Keterangan")
            {
                note = txtFilterValue.Text;
            }

            Grid_Load(code, name, address, note);
        }
        #endregion

        #region Grid

        public void Grid_Load(string code = "", string name = "", string address = "", string note = "")
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Kode Mitra", typeof(String));
            dt.Columns.Add("Nama Mitra", typeof(String));
            dt.Columns.Add("Alamat", typeof(String));
            dt.Columns.Add("Telepon", typeof(String));
            dt.Columns.Add("Handphone", typeof(String));
            dt.Columns.Add("Fax", typeof(String));
            dt.Columns.Add("Email", typeof(String));
            dt.Columns.Add("Keterangan", typeof(String));

            try
            {
                String sqlString = "SELECT id, code, name, address, phone, handphone, fax, email, note FROM masterRelation WHERE " +
                    "status = 1 " +
                    "AND (code like @code OR code IS NULL) " +
                    "AND (name like @name OR name IS NULL) " +
                    "AND (address like @address OR address IS NULL) " +
                    "AND (note like @note OR note IS NULL) " +
                    "AND (1=1)";

                using (SqlDataAdapter da = new SqlDataAdapter(sqlString, connString))
                {
                    da.SelectCommand.Parameters.Add("@code", SqlDbType.VarChar).Value = code + '%';
                    da.SelectCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = name + '%';
                    da.SelectCommand.Parameters.Add("@address", SqlDbType.VarChar).Value = address + '%';
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
                            dr["Kode Mitra"] = tDR["code"];
                            dr["Nama Mitra"] = tDR["name"];
                            dr["Alamat"] = tDR["address"];
                            dr["Telepon"] = tDR["phone"];
                            dr["Handphone"] = tDR["handphone"];
                            dr["Fax"] = tDR["fax"];
                            dr["Email"] = tDR["email"];
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
            cmbFilterColumn.Items.Add("Nama Mitra");
            cmbFilterColumn.Items.Add("Kode Mitra");
            cmbFilterColumn.Items.Add("Alamat");
            cmbFilterColumn.Items.Add("Keterangan");
            cmbFilterColumn.SelectedItem = "Nama Mitra";
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
                    ((NewForm)this.Owner).btnRelationClick(code);
                    this.Close();
                }
                else if (typeForm == "EditForm")
                {
                    string code = gridView.SelectedRows[0].Cells[1].Value.ToString();
                    EditForm parentForm = new EditForm(username, this.id);
                    ((EditForm)this.Owner).btnRelationClick(code);
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
                    ((NewForm)this.Owner).btnRelationClick(code);
                    this.Close();
                }
                else if (typeForm == "EditForm")
                {
                    string code = gridView.SelectedRows[0].Cells[1].Value.ToString();
                    EditForm parentForm = new EditForm(username, this.id);
                    ((EditForm)this.Owner).btnRelationClick(code);
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
