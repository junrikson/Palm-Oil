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
    public partial class MaintenanceForm : Syncfusion.Windows.Forms.MetroForm
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";

        public MaintenanceForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void MaintenanceForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            txtStatus.AppendText("Maintenance Started...\n\n");
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlCommand cmd;

                    if (cbMaintenanceIndex.Checked == true)
                    {
                        txtStatus.AppendText("Rebuilding and Reorganizing Index...\n\n");
                        cmd = new SqlCommand("exec maintenanceIndex", con);
                        cmd.ExecuteNonQuery();
                        txtStatus.AppendText("Rebuilding and Reorganizing Success...\n\n");
                    }

                    if (cbDeleteLog.Checked == true)
                    {
                        txtStatus.AppendText("Deleting Log...\n\n");
                        cmd = new SqlCommand("exec deleteLog", con);
                        cmd.ExecuteNonQuery();
                        txtStatus.AppendText("Deleting Success...\n\n");
                    }

                    txtStatus.AppendText("Maintenance Finished...\n\n");
                    MessageBox.Show("Maintenance Selesai!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}

