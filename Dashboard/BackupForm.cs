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
using System.IO.Compression;

namespace Dashboard
{
    public partial class BackupForm : Syncfusion.Windows.Forms.MetroForm 
    {
        string connString = ConfigurationManager.ConnectionStrings["stowageConnection"].ToString();
        string username = "";

        public BackupForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            DeclareDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                BackupDB();
            }
        }

        private Boolean IsValid()
        {
            Boolean isValidated = true;


            if (String.IsNullOrEmpty(txtFileName.Text))
            {
                isValidated = false;
                MessageBox.Show("Nama File harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFileName.Select();
            }
            else if (String.IsNullOrEmpty(txtLocation.Text))
            {
                isValidated = false;
                MessageBox.Show("Lokasi Penyimpanan harus diisi!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLocation.Select();
            }

            return isValidated;
        }

        private void DeclareDialog()
        {
            txtFileName.Text = "POS" + DateTime.Now.ToString("yyyyMMddHHmmss");
            txtLocation.Text = folderBrowserDialog.SelectedPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\backup";            
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtLocation.Text = folderBrowserDialog.SelectedPath;
                Environment.SpecialFolder root = folderBrowserDialog.RootFolder;
            }
        }

        private void BackupDB()
        {
            try
            {
                txtStatus.AppendText("Backup operation started...\n");
                using (var con = new SqlConnection(connString))
                {
                    string destination = txtLocation.Text;
                    var fileName = Path.Combine(destination, String.Format("{0}.bak", txtFileName.Text));
                    if (!Directory.Exists(destination))
                        Directory.CreateDirectory(destination);
                    var sqlServer = new Server(new ServerConnection(con));
                    var bkpDatabase = new Backup
                    {
                        Action = BackupActionType.Database,
                        Database = "POS"
                    };
                    var bkpDevice = new BackupDeviceItem(fileName, DeviceType.File);
                    bkpDatabase.Devices.Add(bkpDevice);
                    bkpDatabase.Checksum = true;
                    bkpDatabase.ContinueAfterError = true;
                    bkpDatabase.Incremental = false;
                    bkpDatabase.Initialize = true;
                    //bkpDatabase.CompressionOption = BackupCompressionOptions.On;
                    bkpDatabase.SqlBackup(sqlServer);

                    try
                    {
                        txtStatus.AppendText("Compressing File...\n");
                        Compress(new FileInfo(fileName));
                        if (File.Exists(fileName))
                        {
                            File.Delete(fileName);
                        }
                        txtStatus.AppendText("Compression succeeded!\n");
                    }
                    catch (Exception ex)
                    {
                        txtStatus.AppendText("Compression failed :\n");
                        txtStatus.AppendText(ex.Message + "\n\n");
                        MessageBox.Show(ex.Message.ToString());
                    }

                    txtStatus.AppendText("Backup operation succeeded!\n");
                    MessageBox.Show("Backup Selesai!", "Finished!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                txtStatus.AppendText("Backup operation failed :\n");
                txtStatus.AppendText(ex.Message + "\n\n");
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public static void Compress(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Prevent compressing hidden and 
                // already compressed files.
                if ((File.GetAttributes(fi.FullName)
                    & FileAttributes.Hidden)
                    != FileAttributes.Hidden & fi.Extension != ".gz")
                {
                    // Create the compressed file.
                    using (FileStream outFile =
                                File.Create(fi.FullName + ".gz"))
                    {
                        using (GZipStream Compress =
                            new GZipStream(outFile,
                            CompressionMode.Compress))
                        {
                            // Copy the source file into 
                            // the compression stream.
                            inFile.CopyTo(Compress);

                            //Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                            //    fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                        }
                    }
                }
            }
        }
    }
}
