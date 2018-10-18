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
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Purchase
{
    public partial class rptPurchase2Form : Syncfusion.Windows.Forms.MetroForm
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string id = "";
        string reportName = "rptPurchase2.rpt";

        public rptPurchase2Form(string id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void rptPurchase2Form_Load(object sender, EventArgs e)
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
            crDoc.SetParameterValue("terbilang", " ## " + Terbilang(getTotal(id)) + " ## ");
            crTables = crDoc.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
            {
                crTableLogonInfo = crTable.LogOnInfo;
                crTableLogonInfo.ConnectionInfo = crConnectionInfo;
                crTable.ApplyLogOnInfo(crTableLogonInfo);
            }
            return crDoc;
        }

        private Decimal getTotal(string id)
        {
            Decimal total = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 total FROM purchase WHERE ID = @ID ", con))
                    {
                        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = id;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                Decimal.TryParse(reader["total"].ToString(), out total);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            return total;
        }

        string[] satuan = new string[10] { "nol", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan" };
        string[] belasan = new string[10] { "sepuluh", "sebelas", "dua belas", "tiga belas", "empat belas", "lima belas", "enam belas", "tujuh belas", "delapan belas", "sembilan belas" };
        string[] puluhan = new string[10] { "", "", "dua puluh", "tiga puluh", "empat puluh", "lima puluh", "enam puluh", "tujuh puluh", "delapan puluh", "sembilan puluh" };
        string[] ribuan = new string[5] { "", "ribu", "juta", "milyar", "triliyun" };

        string Terbilang(Decimal d)
        {
            string strHasil = "";
            Decimal frac = d - Decimal.Truncate(d);

            if (Decimal.Compare(frac, 0.0m) != 0)
                strHasil = Terbilang(Decimal.Round(frac * 100)) + " sen";
            else
                strHasil = "rupiah";
            int xDigit = 0;
            int xPosisi = 0;

            string strTemp = Decimal.Truncate(d).ToString();
            for (int i = strTemp.Length; i > 0; i--)
            {
                string tmpx = "";
                xDigit = Convert.ToInt32(strTemp.Substring(i - 1, 1));
                xPosisi = (strTemp.Length - i) + 1;
                switch (xPosisi % 3)
                {
                    case 1:
                        bool allNull = false;
                        if (i == 1)
                            tmpx = satuan[xDigit] + " ";
                        else if (strTemp.Substring(i - 2, 1) == "1")
                            tmpx = belasan[xDigit] + " ";
                        else if (xDigit > 0)
                            tmpx = satuan[xDigit] + " ";
                        else
                        {
                            allNull = true;
                            if (i > 1)
                                if (strTemp.Substring(i - 2, 1) != "0")
                                    allNull = false;
                            if (i > 2)
                                if (strTemp.Substring(i - 3, 1) != "0")
                                    allNull = false;
                            tmpx = "";
                        }

                        if ((!allNull) && (xPosisi > 1))
                            if ((strTemp.Length == 4) && (strTemp.Substring(0, 1) == "1"))
                                tmpx = "se" + ribuan[(int)Decimal.Round(xPosisi / 3m)] + " ";
                            else
                                tmpx = tmpx + ribuan[(int)Decimal.Round(xPosisi / 3)] + " ";
                        strHasil = tmpx + strHasil;
                        break;
                    case 2:
                        if (xDigit > 0)
                            strHasil = puluhan[xDigit] + " " + strHasil;
                        break;
                    case 0:
                        if (xDigit > 0)
                            if (xDigit == 1)
                                strHasil = "seratus " + strHasil;
                            else
                                strHasil = satuan[xDigit] + " ratus " + strHasil;
                        break;
                }
            }
            strHasil = strHasil.Trim().ToLower();
            if (strHasil.Length > 0)
            {
                strHasil = strHasil.Substring(0, 1).ToUpper() +
                  strHasil.Substring(1, strHasil.Length - 1);
            }
            return strHasil;
        }
    }
}
