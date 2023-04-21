using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace _2023MUYGCS
{
    public partial class Form4 : Form
    {

        private BackgroundWorker worker;
        public Form4()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
        }
       
     

        private void Form4_Load(object sender, EventArgs e)
        {
             
            CheckForIllegalCrossThreadCalls = false;


        }
        private long GetFileSize(string ftpUrl, string username, string password)
        {
            // FtpWebRequest nesnesi oluştur ve kimlik doğrulama bilgilerini ayarla
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            request.Credentials = new NetworkCredential(username, password);

            // FtpWebResponse nesnesini al ve dosyanın boyutunu döndür
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                return response.ContentLength;
            }
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {

            string ftpUrl = textBoxftpurl.Text + textBoxDosyaAdi.Text;
            string username = textBoxSunucuadi.Text;
            string password = textBoxPassword.Text;

            try
            {
                // WebClient nesnesi oluştur ve kimlik doğrulama bilgilerini ayarla
                WebClient client = new WebClient();
                client.Credentials = new NetworkCredential(username, password);

               labelAlmaDurumu.Text = "Dosya Alınıyor...";
                progressBar1.Value = 90;
                client.DownloadFile(ftpUrl, textBoxDosyaAdi.Text);
               

                e.Result = "Video downloaded successfully.";
            }
            catch (Exception ex)
            {
                labelAlmaDurumu.Text = "Error: " + ex.Message;
                e.Result = "Error: " + ex.Message;
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            labelAlmaDurumu.Text = "Dosya alındı";
            labelAlmaDurumu.ForeColor = Color.DarkGreen;
            progressBar1.Value = 100;
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        { 
            Console.WriteLine("asdasas"+e.ProgressPercentage);

           // progressBarFtp.Value = e.ProgressPercentage;
           // progressBarFtp.Update();
        }  

        private void buttonFtpAl_Click(object sender, EventArgs e)
        {

            if (!worker.IsBusy)
            {
                // İlerleme çubuğunu sıfırla ve göster
                
                

                // BackgroundWorker'ı başlat
                worker.RunWorkerAsync();
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            string ftpUrl = textBoxftpurl.Text + textBoxDosyaAdi.Text;
            string username = textBoxSunucuadi.Text;
            string password = textBoxPassword.Text;

            long fileSize = GetFileSize(ftpUrl, username, password);


            Console.WriteLine("FileSize" + fileSize);
            labelFtpDurum.Text = "Test  başarılı, alınacak dosya boyutu: " + fileSize;
            labelFtpDurum.ForeColor = Color.DarkGreen;
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }

   
}
