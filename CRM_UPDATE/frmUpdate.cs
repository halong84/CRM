using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.IO;

namespace CRM_UPDATE
{
    public partial class frmUpdate : Form
    {
        public frmUpdate()
        {
            InitializeComponent();
        }
        //private void Update()
        //{
        //    string ftp_path = "ftp://192.168.1.150/CRM/";
        //    string user_ftp = "khach";
        //    string pass_ftp = "123456a@";
        //    FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftp_path);
        //    ftpRequest.Credentials = new NetworkCredential(user_ftp, pass_ftp);
        //    ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
        //    FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
        //    StreamReader streamReader = new StreamReader(response.GetResponseStream());
        //    List<string> directories = new List<string>();

        //    string line = streamReader.ReadLine();
        //    while (!string.IsNullOrEmpty(line))
        //    {
        //        directories.Add(line);
        //        line = streamReader.ReadLine();
        //    }
        //    streamReader.Close();


        //    using (WebClient ftpClient = new WebClient())
        //    {
        //        ftpClient.Credentials = new System.Net.NetworkCredential(user_ftp, pass_ftp);

        //        for (int i = 0; i <= directories.Count - 1; i++)
        //        {
        //            if (directories[i].Contains("."))
        //            {
        //                string path = ftp_path + directories[i].ToString();
        //                string trnsfrpth = Application.StartupPath.ToString() + @"\" + directories[i].ToString();
        //                ftpClient.DownloadFile(path, trnsfrpth);
        //            }
        //        }
        //    }
        //    MessageBox.Show("Cập nhật thành công.");
        //    //Mở chương trình đã cập nhật
        //    //Process.Start(Application.StartupPath.ToString() + @"\CRM.exe");
        //    //Đóng màn hình cập nhật
        //    Application.Exit();
        //}

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //progressBar1.Maximum = 100;
            //progressBar1.Step = 1;
            //progressBar1.Value = 0;
            // Start the BackgroundWorker.
            backgroundWorker1.RunWorkerAsync();            
        }

        private void frmUpdate_Load(object sender, EventArgs e)
        {
            // Start the BackgroundWorker.
            //backgroundWorker1.RunWorkerAsync();
        }

        private void Calculate(int i)
        {
            double pow = Math.Pow(i, i);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //string ftp_path = "ftp://192.168.1.150/CRM/";
            //string user_ftp = "khach";
            //string pass_ftp = "123456a@";

            string ftp_path = "ftp://10.14.0.19/_DIENTOAN/SETUP/CRM_UPDATE/";
            string user_ftp = "hduong";
            string pass_ftp = "9frgd";
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftp_path);
            ftpRequest.Credentials = new NetworkCredential(user_ftp, pass_ftp);
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            List<string> directories = new List<string>();

            string line = streamReader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                directories.Add(line);
                line = streamReader.ReadLine();
            }
            streamReader.Close();


            using (WebClient ftpClient = new WebClient())
            {
                ftpClient.Credentials = new System.Net.NetworkCredential(user_ftp, pass_ftp);

                for (int i = 0; i <= directories.Count - 1; i++)
                {
                    // Wait 100 milliseconds.
                    Thread.Sleep(100);
                    // Report progress.
                    backgroundWorker1.ReportProgress((int)(100/directories.Count) * i, null);
                    if (directories[i].Contains("."))
                    {
                        string path = ftp_path + directories[i].ToString();
                        string trnsfrpth = Application.StartupPath.ToString() + @"\" + directories[i].ToString();
                        ftpClient.DownloadFile(path, trnsfrpth);
                    }
                }
            }
            

            //for (int i = 1; i <= 100; i++)
            //{
            //    // Wait 100 milliseconds.
            //    Thread.Sleep(100); 
            //    // Report progress.
            //    backgroundWorker1.ReportProgress(i);
            //}
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            progressBar1.Value = e.ProgressPercentage;
            // Set the text.
            this.Text = "CRM UPDATE " + e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Cập nhật thành công.");
            //Mở chương trình đã cập nhật
            Process.Start(Application.StartupPath.ToString() + @"\CRM.exe");
            //Đóng màn hình cập nhật
            Application.Exit();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create("ftp://mywebsite.com/");
        //    ftpRequest.Credentials = new NetworkCredential("user345", "pass234");
        //    ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
        //    FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
        //    StreamReader streamReader = new StreamReader(response.GetResponseStream());
        //    List<string> directories = new List<string>();

        //    string line = streamReader.ReadLine();
        //    while (!string.IsNullOrEmpty(line))
        //    {
        //        directories.Add(line);
        //        line = streamReader.ReadLine();
        //    }
        //    streamReader.Close();


        //    using (WebClient ftpClient = new WebClient())
        //    {
        //        ftpClient.Credentials = new System.Net.NetworkCredential("user345", "pass234");

        //        for (int i = 0; i <= directories.Count - 1; i++)
        //        {
        //            if (directories[i].Contains("."))
        //            {

        //                string path = "ftp://mywebsite.com/" + directories[i].ToString();
        //                string trnsfrpth = @"D:\\Test\" + directories[i].ToString();
        //                ftpClient.DownloadFile(path, trnsfrpth);
        //            }
        //        }
        //    }
            
            //String RemoteFtpPath = "ftp://ftp.csidata.com:21/Futures.20150305.gz";
            //String LocalDestinationPath = "Futures.20150305.gz";
            //String Username="yourusername";
            //String Password = "yourpassword";
            //Boolean UseBinary = true; // use true for .zip file or false for a text file
            //Boolean UsePassive = false;
 
            //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(RemoteFtpPath);
            //request.Method = WebRequestMethods.Ftp.DownloadFile;
            //request.KeepAlive = true;
            //request.UsePassive = UsePassive;
            //request.UseBinary = UseBinary;
 
            //request.Credentials = new NetworkCredential(Username, Password);
 
            //FtpWebResponse response = (FtpWebResponse)request.GetResponse();
 
            //Stream responseStream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(responseStream);
 
            //using (FileStream writer = new FileStream(LocalDestinationPath, FileMode.Create))
            //{
 
            //    long length = response.ContentLength;
            //    int bufferSize = 2048;
            //    int readCount;
            //    byte[] buffer = new byte[2048];
 
            //    readCount = responseStream.Read(buffer, 0, bufferSize);
            //    while (readCount > 0)
            //    {
            //        writer.Write(buffer, 0, readCount);
            //        readCount = responseStream.Read(buffer, 0, bufferSize);
            //    }
            //}
 
            //reader.Close();
            //response.Close();

            //Process.Start(Application.StartupPath.ToString() + @"\CRM.exe");
            //Application.Exit();

            //Process[] runningProcesses = Process.GetProcesses();
            //foreach (Process process in runningProcesses)
            //{
            //    // now check the modules of the process
            //    foreach (ProcessModule module in process.Modules)
            //    {
            //        if (module.FileName.Equals("MyProcess.exe"))
            //        {
            //            process.Kill();
            //        } 
            //        else 
            //        {
            //            //enter code here if process not found
            //        }
            //    }
            //}

            //Process[] GetPArry = Process.GetProcesses();
            //foreach (Process testProcess in GetPArry)
            //{
            //    string ProcessName = testProcess.ProcessName;

            //    ProcessName = ProcessName.ToLower();
            //    if (ProcessName.CompareTo("winword") == 0)
            //        testProcess.Kill();
            //} 

            //Process[] proc = Process.GetProcessesByName("CRM");
            //foreach (Process prs in proc)
            //{
            //    prs.Kill();
            //}
        //}
    }
}
