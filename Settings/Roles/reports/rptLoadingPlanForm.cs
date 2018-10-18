#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Roles
{
    public partial class rptLoadingPlanForm : Syncfusion.Windows.Forms.MetroForm
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string id = "";
        string reportName = "rptLoadingPlan.rpt";

        public rptLoadingPlanForm(string id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void rptLoadingPlanForm_Load(object sender, EventArgs e)
        {
            LoadReport(this.reportName);
        }        

        private void LoadReport(string name)
        {
            {
                var report = new ReportDocument();
                string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) +"\\reports\\" + name;

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connString);
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = builder.DataSource;
                connectionInfo.DatabaseName = builder.InitialCatalog;
                connectionInfo.UserID = builder.UserID;
                connectionInfo.Password = builder.Password;

                crystalReportViewer.ReportSource = ReportSourceSetup(path, connectionInfo);
                crystalReportViewer.Refresh();
            }
        }

        ReportDocument ReportSourceSetup(string crFileInfo, ConnectionInfo crConnectionInfo)
        {
            ReportDocument crDoc = new ReportDocument();
            TableLogOnInfos crTableLogonInfos = new TableLogOnInfos();
            TableLogOnInfo crTableLogonInfo = new TableLogOnInfo();
            Tables crTables;
            crDoc.Load(crFileInfo);
            crDoc.SetParameterValue("id", id);
            crTables = crDoc.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
            {
                crTableLogonInfo = crTable.LogOnInfo;
                crTableLogonInfo.ConnectionInfo = crConnectionInfo;
                crTable.ApplyLogOnInfo(crTableLogonInfo);
            }
            return crDoc;
        }

        private void btnChangeReport_Click(object sender, EventArgs e)
        {
            if(this.reportName == "rptLoadingPlan.rpt")
            {
                this.reportName = "rptLoadingPlanPerSender.rpt";
            }
            else
            {
                this.reportName = "rptLoadingPlan.rpt";
            }
            LoadReport(this.reportName);
        }
    }
}
