#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.IO;
using System.Data;

namespace Dashboard
{
    public partial class DeleteDataForm : Syncfusion.Windows.Forms.MetroForm
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";

        public DeleteDataForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void DeleteDataForm_Load(object sender, EventArgs e)
        {
            
        }
        

        private void btnDelete_Click(object sender, EventArgs e)
        {
            txtStatus.AppendText("Deleting Data...\n\n");
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    SqlCommand cmd;
                    
                    if (cbPayment.Checked == true)
                    {
                        txtStatus.AppendText("Deleting Payment...\n\n");
                        cmd = new SqlCommand("DELETE FROM paymentDetails", con, tran);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("DELETE FROM payment", con, tran);
                        cmd.ExecuteNonQuery();
                    }

                    if (cbPurchase.Checked == true)
                    {
                        txtStatus.AppendText("Deleting Purchase...\n\n");
                        cmd = new SqlCommand("DELETE FROM purchaseDetails", con, tran);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("DELETE FROM purchase", con, tran);
                        cmd.ExecuteNonQuery();
                    }

                    if (cbRepayment.Checked == true)
                    {
                        txtStatus.AppendText("Deleting Repayment...\n\n");
                        cmd = new SqlCommand("DELETE FROM repaymentDetails", con, tran);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("DELETE FROM repayment", con, tran);
                        cmd.ExecuteNonQuery();
                    }

                    if (cbSales.Checked == true)
                    {
                        txtStatus.AppendText("Deleting Sales...\n\n");
                        cmd = new SqlCommand("DELETE FROM sales", con, tran);
                        cmd.ExecuteNonQuery();
                    }

                    if (cbReceipt.Checked == true)
                    {
                        txtStatus.AppendText("Deleting Receipt...\n\n");
                        cmd = new SqlCommand("DELETE FROM receipt", con, tran);
                        cmd.ExecuteNonQuery();
                    }

                    if (cbMasterRelation.Checked == true)
                    {
                        txtStatus.AppendText("Deleting Master Relation...\n\n");
                        cmd = new SqlCommand("DELETE FROM MasterRelation", con, tran);
                        cmd.ExecuteNonQuery();
                    }

                    try
                    {
                        tran.Commit();
                        txtStatus.AppendText("All Data Deleted Succesfully...\n\n");
                        MessageBox.Show("Penghapusan Selesai!");
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

        private void cbMasterRelation_CheckedChanged(object sender, EventArgs e)
        {
            if(cbMasterRelation.Checked == true)
            {
                cbReceipt.Checked = true;
                cbReceipt.Enabled = false;

                cbSales.Checked = true;
                cbSales.Enabled = false;

                cbRepayment.Checked = true;
                cbRepayment.Enabled = false;

                cbPurchase.Checked = true;
                cbPurchase.Enabled = false;

                cbPayment.Checked = true;
                cbPayment.Enabled = false;
            }
            else
            {
                cbReceipt.Checked = false;
                cbReceipt.Enabled = true;

                cbSales.Checked = false;
                cbSales.Enabled = true;

                cbRepayment.Checked = false;
                cbRepayment.Enabled = true;

                cbPurchase.Checked = false;
                cbPurchase.Enabled = true;

                cbPayment.Checked = false;
                cbPayment.Enabled = true;
            }
        }

        private void cbReceipt_CheckedChanged(object sender, EventArgs e)
        {
            if (cbReceipt.Checked == true)
            {
                cbSales.Checked = true;
                cbSales.Enabled = false;

                cbRepayment.Checked = true;
                cbRepayment.Enabled = false;

                cbPurchase.Checked = true;
                cbPurchase.Enabled = false;

                cbPayment.Checked = true;
                cbPayment.Enabled = false;
            }
            else
            {
                cbSales.Checked = false;
                cbSales.Enabled = true;

                cbRepayment.Checked = false;
                cbRepayment.Enabled = true;

                cbPurchase.Checked = false;
                cbPurchase.Enabled = true;

                cbPayment.Checked = false;
                cbPayment.Enabled = true;
            }
        }

        private void cbSales_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSales.Checked == true)
            {
                cbRepayment.Checked = true;
                cbRepayment.Enabled = false;

                cbPurchase.Checked = true;
                cbPurchase.Enabled = false;

                cbPayment.Checked = true;
                cbPayment.Enabled = false;
            }
            else
            {
                cbRepayment.Checked = false;
                cbRepayment.Enabled = true;

                cbPurchase.Checked = false;
                cbPurchase.Enabled = true;

                cbPayment.Checked = false;
                cbPayment.Enabled = true;
            }
        }

        private void cbPurchase_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPurchase.Checked == true)
            {
                cbPayment.Checked = true;
                cbPayment.Enabled = false;
            }
            else
            {
                cbPayment.Checked = false;
                cbPayment.Enabled = true;
            }
        }
    }
}

