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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Dashboard
{
    public partial class chooseKeuntunganBrutoForm : Syncfusion.Windows.Forms.MetroForm
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string id = "";
        string reportName = "rptKeuntunganBruto.rpt";

        public chooseKeuntunganBrutoForm()
        {
            InitializeComponent();
        }

        private void chooseKeuntunganBrutoForm_Load(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                LoadReport(reportName, dtpBegin.Value.Date, dtpEnd.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(58));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void LoadReport(string name, DateTime begin, DateTime end)
        {
            {
                var report = new ReportDocument();
                string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\reports\\" + name;

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connString);
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = builder.DataSource;
                connectionInfo.DatabaseName = builder.InitialCatalog;
                connectionInfo.UserID = builder.UserID;
                connectionInfo.Password = builder.Password;

                crystalReportViewer.ReportSource = ReportSourceSetup(path, connectionInfo, begin, end);
                crystalReportViewer.Refresh();
            }
        }

        ReportDocument ReportSourceSetup(string crFileInfo, ConnectionInfo crConnectionInfo, DateTime begin, DateTime end)
        {
            ReportDocument crDoc = new ReportDocument();
            TableLogOnInfos crTableLogonInfos = new TableLogOnInfos();
            TableLogOnInfo crTableLogonInfo = new TableLogOnInfo();
            Tables crTables;
            crDoc.Load(crFileInfo);
            crDoc.SetParameterValue("dateBegin", begin);
            crDoc.SetParameterValue("dateEnd", end);
            crTables = crDoc.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
            {
                crTableLogonInfo = crTable.LogOnInfo;
                crTableLogonInfo.ConnectionInfo = crConnectionInfo;
                crTable.ApplyLogOnInfo(crTableLogonInfo);
            }
            return crDoc;
        }
    }
}

