using _2023MUYGCS.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.IO;
using AForge.Video.DirectShow;
using AForge.Video;
using Accord.Video.FFMPEG;

namespace _2023MUYGCS
{

    public partial class Form1 : Form
    {

        private System.Diagnostics.Stopwatch stopWatch = null;
        static TelemetriVerileriModel telemetri = new TelemetriVerileriModel();
        static bool _continue;
        static bool csvVeriKaydedilsinmi = false;
        public static string title;
        static SerialPort _serialPort;
        Thread readThread = new Thread(Read);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            porAdiGetir();


            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            captureDevice = new VideoCaptureDeviceForm();


        }

        public void serialPortHazirla()
        {
            _serialPort = new SerialPort();
            _serialPort.PortName = comboBoxPortName.Text;
            _serialPort.BaudRate = 115200;
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;
            _serialPort.Handshake = Handshake.None;
        }
        public void porAdiGetir()
        {
            Console.WriteLine("Available Ports:");
            foreach (string s in SerialPort.GetPortNames())
            {
                Console.WriteLine("   {0}", s);
                comboBoxPortName.Items.Add(s);
            }
            try
            {
                comboBoxPortName.SelectedIndex = 0;
            }
            catch (Exception err)
            {

                Console.WriteLine("Hata " + err);
            }


        }

        private void buttonSerialPortBaglanti_Click(object sender, EventArgs e)
        {
            if (buttonSerialPortBaglanti.Text == "Bağlan")
            {
                try
                {
                    serialPortHazirla();

                    _serialPort.Open();
                    _continue = true;
                    timer1.Start();
                    buttonSerialPortBaglanti.ForeColor = Color.Red;
                    buttonSerialPortBaglanti.Text = "Durdur";
                    if (readThread.ThreadState == ThreadState.Stopped)
                    {
                        Thread readThread = new Thread(Read);
                        readThread.Start();
                    }
                    else
                    {
                        readThread.Start();
                    }

                }
                catch (Exception err)
                {

                    Console.WriteLine("hata" + err);
                }
            }
            else
            {
                try
                {

                    _serialPort.Close();
                    _continue = false;
                    serialPortHazirla();
                    readThread.Join();
                    buttonSerialPortBaglanti.ForeColor = Color.Green;
                    buttonSerialPortBaglanti.Text = "Bağlan";
                    timer1.Stop();
                    Console.WriteLine("Thread==" + readThread.ThreadState);
                }
                catch (Exception err)
                {

                    Console.WriteLine("Hata " + err);
                }
            }
        }

        public static void Read()
        {
            string gelenTelemetriVerisi;
            while (_continue)
            {
                try
                {
                    gelenTelemetriVerisi = _serialPort.ReadLine();
                    title = gelenTelemetriVerisi;
                    Console.WriteLine(gelenTelemetriVerisi);
                    string gelenTelemetriVerisiRemoveBuyuktur = gelenTelemetriVerisi.Replace('>', ' ');
                    string gelenTelemetriVerisiRemoveKucuktur = gelenTelemetriVerisiRemoveBuyuktur.Replace('<', ' ');
                    try
                    {
                        if (!gelenTelemetriVerisiRemoveKucuktur.Contains('?') || !gelenTelemetriVerisiRemoveKucuktur.Contains('#'))
                        {
                            string[] gelenTelemetriVeriDizisi = gelenTelemetriVerisiRemoveKucuktur.Split(',');

                            telemetri.paketNo = gelenTelemetriVeriDizisi[0];
                            telemetri.uyduStatu = gelenTelemetriVeriDizisi[1];
                            telemetri.hataKodu = gelenTelemetriVeriDizisi[2];
                            telemetri.gondermeSaati = gelenTelemetriVeriDizisi[3] + ", " + gelenTelemetriVeriDizisi[4];

                            telemetri.tarih = gelenTelemetriVeriDizisi[3];
                            telemetri.saat = gelenTelemetriVeriDizisi[4];

                            telemetri.basinc1 = gelenTelemetriVeriDizisi[5];
                            telemetri.basinc2 = gelenTelemetriVeriDizisi[6];
                            telemetri.yukseklik1 = gelenTelemetriVeriDizisi[7];
                            telemetri.yukseklik2 = gelenTelemetriVeriDizisi[8];
                            telemetri.irtifaFarki = gelenTelemetriVeriDizisi[9];
                            telemetri.inisHizi = gelenTelemetriVeriDizisi[10];
                            telemetri.sicaklik = gelenTelemetriVeriDizisi[11];
                            telemetri.pilGerilimi = gelenTelemetriVeriDizisi[12];
                            telemetri.gps1Lat = gelenTelemetriVeriDizisi[13];
                            telemetri.gps1Long = gelenTelemetriVeriDizisi[14];
                            telemetri.gps1Alt = gelenTelemetriVeriDizisi[15];
                            telemetri.pitch = gelenTelemetriVeriDizisi[16];
                            telemetri.roll = gelenTelemetriVeriDizisi[17];
                            telemetri.yaw = gelenTelemetriVeriDizisi[18];
                            telemetri.takimNo = gelenTelemetriVeriDizisi[19];
                            telemetri.tasiyiciInisHizi = gelenTelemetriVeriDizisi[20];

                            if (csvVeriKaydedilsinmi)
                            {
                                try
                                {
                                    StringBuilder csvContent = new StringBuilder();

                                    csvContent.AppendLine(gelenTelemetriVerisiRemoveKucuktur);
                                    string csvPath = "TMUY2023_243868_TLM.csv";
                                    File.AppendAllText(csvPath, csvContent.ToString());
                                }
                                catch (Exception err)
                                {

                                    Console.WriteLine("Hata " + err);
                                }
                            }



                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }



                }
                catch (Exception err)
                {
                    Console.WriteLine("Hata " + err);
                }
                Console.Write("Thread calisiyor");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_serialPort != null)
                {
                    _serialPort.Close();
                }
                CloseCurrentVideoSource();

                _continue = false;
                if (readThread.ThreadState == ThreadState.Suspended)
                {
                    readThread.Resume();
                }

                timer1.Stop();
                timer1.Enabled = false;
                Application.Exit();
            }
            catch (Exception err)
            {

                Console.WriteLine("Hata" + err);
            }

        }
        private void OpenVideoSource(IVideoSource source)
        {
            // set busy cursor
            this.Cursor = Cursors.WaitCursor;

            // stop current video source
            CloseCurrentVideoSource();

            // start new video source
            videoSourcePlayer1.VideoSource = source;
            videoSourcePlayer1.Start();

            // reset stop watch
            stopWatch = null;

            // start timer
            timerKamera.Start();

            this.Cursor = Cursors.Default;
        }

        // Close video source if it is running
        private void CloseCurrentVideoSource()
        {
            if (videoSourcePlayer1.VideoSource != null)
            {
                videoSourcePlayer1.SignalToStop();

                // wait ~ 3 seconds
                for (int i = 0; i < 30; i++)
                {
                    if (!videoSourcePlayer1.IsRunning)
                        break;
                    System.Threading.Thread.Sleep(100);
                }

                if (videoSourcePlayer1.IsRunning)
                {
                    videoSourcePlayer1.Stop();
                }

                videoSourcePlayer1.VideoSource = null;
            }
        }
        private FilterInfoCollection VideoCaptureDevices;

        private VideoCaptureDevice FinalVideo = null;
        private VideoCaptureDeviceForm captureDevice;

        private Bitmap video;
        //private AVIWriter AVIwriter = new AVIWriter();
        public VideoFileWriter FileWriter = new VideoFileWriter();
        private SaveFileDialog saveAvi;
        private void videoSourcePlayer1_NewFrame(object sender, ref Bitmap image)
        {
            DateTime now = DateTime.Now;
            Graphics g = Graphics.FromImage(image);

            // paint current time
            SolidBrush brush = new SolidBrush(Color.Red);
            g.DrawString(now.ToString(), this.Font, brush, new PointF(5, 5));
            brush.Dispose();

            g.Dispose();
        }




        void FinalVideo_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (buttonRecStop.Text == "Kaydı durdur")
            {


                video = (Bitmap)eventArgs.Frame.Clone();

                //AVIwriter.Quality = 0;
                FileWriter.WriteVideoFrame(video);
                //AVIwriter.AddFrame(video);
            }
            else //Stop
            {
                video = (Bitmap)eventArgs.Frame.Clone();

            }
        }




        string fileNamesHowDialog = "yunusVıdeo";

        private void timer1_Tick(object sender, EventArgs e)
        {

            Console.WriteLine("Thread durumu: " + readThread.ThreadState);
            this.Text = " Model uydu takımı Yer istasyonu :  " + title;
            if (_continue)
            {
                GrafikCizdir();
                // sagdaki labeller
                labelPaketNoVALUE.Text = telemetri.paketNo;
                labelUyduStatusuVALUE.Text = telemetri.uyduStatu;
                labelHataKoduVALUE.Text = telemetri.hataKodu;
                labelGondermeSaatiVALUE.Text = telemetri.gondermeSaati;
                labelBasinc1GYVALUE.Text = telemetri.basinc1 + " (Pa)";
                labelBasinc2TVALUE.Text = telemetri.basinc2 + " (Pa)";
                labelYukseklik1GYVALUE.Text = telemetri.yukseklik1 + " m";
                labelYukseklik2TVALUE.Text = telemetri.yukseklik2 + " m";
                labelIrtifaFarkiVALUE.Text = telemetri.irtifaFarki + " m";
                labelGYinisHiziVALUE.Text = telemetri.inisHizi + " (m/s)";
                labelSicaklikVALUE.Text = telemetri.sicaklik + " °C";
                labelPilGerilimiVALUE.Text = telemetri.pilGerilimi + " V";
                labelGps1LatGYVALUE.Text = telemetri.gps1Lat;
                labelGps1LongGYVALUE.Text = telemetri.gps1Long;
                labelGps1AltGYVALUE.Text = telemetri.gps1Alt + " m";
                labelPitchVALUE.Text = telemetri.pitch + " °";
                labelRollVALUE.Text = telemetri.roll + " °";
                labelYawVALUE.Text = telemetri.yaw + " °";
                labelTakimNoVALUE.Text = telemetri.takimNo;
                labelTinisHiziVALUE.Text = telemetri.tasiyiciInisHizi + " (m/s)";

                // label grafik ustundekiler
                labelGraphBasinc1.Text = "BASINÇ-1 (GÖREV YÜKÜ) : " + telemetri.basinc1 + "(Pa)";
                labelGraphBasinc2.Text = "BASINÇ-2 (TAŞIYICI) : " + telemetri.basinc2 + "(Pa)";
                labelGraphYukseklik1.Text = "YÜKSEKLİK-1 (GÖREV YÜKÜ) : " + telemetri.yukseklik1 + " m";
                labelGraphYukseklik2.Text = "YÜKSEKLİK-2 (TAŞIYICI) :" + telemetri.yukseklik2 + " m";
                labelGraphIrtifaFarki.Text = "İRTİFA FARKI : " + telemetri.irtifaFarki + " m";
                labelGraphGYinisHizi.Text = "GÖREV YÜKÜ İNİŞ HIZI : " + telemetri.inisHizi + " (m/s)";
                labelGraphSicaklik.Text = "SICAKLIK" + telemetri.sicaklik + " °C";
                labelGraphPilGerilimi.Text = "PİL GERİLİMİ" + telemetri.pilGerilimi + " V";

                string[] satir = {telemetri.paketNo,
                                telemetri.uyduStatu,
                                telemetri.hataKodu,
                                telemetri.gondermeSaati,
                                telemetri.basinc1,
                                telemetri.basinc2,
                                telemetri.yukseklik1,
                                telemetri.yukseklik2,
                                telemetri.irtifaFarki,
                                telemetri.inisHizi,
                                telemetri.sicaklik,
                                telemetri.pilGerilimi,
                                telemetri.gps1Lat,
                                telemetri.gps1Long,
                                telemetri.gps1Alt,
                                telemetri.pitch,
                                telemetri.roll,
                                telemetri.yaw,
                                telemetri.takimNo,
                                telemetri.tasiyiciInisHizi
            };

                var eklenenSatir = new ListViewItem(satir);

                if (!this.Text.Contains("?") || !this.Text.Contains("!"))
                {
                    if (telemetri.gondermeSaati != null)
                    {
                        listView1.Items.Add(eklenenSatir);
                        listView1.Items[listView1.Items.Count - 1].EnsureVisible();
                    }

                }


                if (telemetri.hataKodu.Length == 7)
                {
                    string boslukSil = telemetri.hataKodu.Trim();
                    Console.WriteLine("Hata kodu bosluksuz adet(" + boslukSil.Length + ")");
                    Console.WriteLine("Hata kodu bosluksuz (" + boslukSil + ")");
                    char[] _GelenHataKodunuDiziyeAyirma = boslukSil.ToCharArray();
                    telemetri.hataKodu1 = _GelenHataKodunuDiziyeAyirma[0];
                    telemetri.hataKodu2 = _GelenHataKodunuDiziyeAyirma[1];
                    telemetri.hataKodu3 = _GelenHataKodunuDiziyeAyirma[2];
                    telemetri.hataKodu4 = _GelenHataKodunuDiziyeAyirma[3];
                    telemetri.hataKodu5 = _GelenHataKodunuDiziyeAyirma[4];

                    Console.WriteLine("telemetri hatakodu1: " + telemetri.hataKodu1);
                    Console.WriteLine("telemetri hatakodu2: " + telemetri.hataKodu2);
                    Console.WriteLine("telemetri hatakodu3: " + telemetri.hataKodu3);
                    Console.WriteLine("telemetri hatakodu4: " + telemetri.hataKodu4);
                    Console.WriteLine("telemetri hatakodu5: " + telemetri.hataKodu5);

                    if (telemetri.hataKodu1 == '1') { buttonHKA1.ForeColor = Color.Red; buttonHKR1.BackColor = Color.Red; buttonHKR1.Text = "1"; }
                    if (telemetri.hataKodu1 == '0') { buttonHKA1.ForeColor = Color.Black; buttonHKR1.BackColor = Color.Green; buttonHKR1.Text = "0"; }

                    if (telemetri.hataKodu2 == '1') { buttonHKA2.ForeColor = Color.Red; buttonHKR2.BackColor = Color.Red; buttonHKR2.Text = "1"; }
                    if (telemetri.hataKodu2 == '0') { buttonHKA2.ForeColor = Color.Black; buttonHKR2.BackColor = Color.Green; buttonHKR2.Text = "0"; }

                    if (telemetri.hataKodu3 == '1') { buttonHKA3.ForeColor = Color.Red; buttonHKR3.BackColor = Color.Red; buttonHKR3.Text = "1"; }
                    if (telemetri.hataKodu3 == '0') { buttonHKA3.ForeColor = Color.Black; buttonHKR3.BackColor = Color.Green; buttonHKR3.Text = "0"; }

                    if (telemetri.hataKodu4 == '1') { buttonHKA4.ForeColor = Color.Red; buttonHKR4.BackColor = Color.Red; buttonHKR4.Text = "1"; }
                    if (telemetri.hataKodu4 == '0') { buttonHKA4.ForeColor = Color.Black; buttonHKR4.BackColor = Color.Green; buttonHKR4.Text = "0"; }

                    if (telemetri.hataKodu5 == '1') { buttonHKA5.ForeColor = Color.Red; buttonHKR5.BackColor = Color.Red; buttonHKR5.Text = "1"; }
                    if (telemetri.hataKodu5 == '0') { buttonHKA5.ForeColor = Color.Black; buttonHKR5.BackColor = Color.Green; buttonHKR5.Text = "0"; }

                }
                else
                {
                    Console.WriteLine("Hata kodu adet(" + telemetri.hataKodu.Length + ")");
                    Console.WriteLine("Hata kodu (" + telemetri.hataKodu + ")");
                }


            }


        }

        public void GrafikCizdir()
        {
            if (telemetri.gondermeSaati != null)
            {
                this.chartBasinc1.Series["basinc1"].Points.AddXY(telemetri.saat, telemetri.basinc1);
                this.chartBasinc2.Series["basinc2"].Points.AddXY(telemetri.saat, telemetri.basinc2);
                this.chartYukseklik1.Series["yukseklik1"].Points.AddXY(telemetri.saat, telemetri.yukseklik1);
                this.chartYukseklik2.Series["yukseklik2"].Points.AddXY(telemetri.saat, telemetri.yukseklik2);
                this.chartIrtifaFarki.Series["irtifaFarki"].Points.AddXY(telemetri.saat, telemetri.irtifaFarki);
                this.chartInisGizi.Series["inisHizi"].Points.AddXY(telemetri.saat, telemetri.inisHizi);
                this.chartSicaklik.Series["sicaklik"].Points.AddXY(telemetri.saat, telemetri.sicaklik);
                this.chartPilGerilimi.Series["pilGerilimi"].Points.AddXY(telemetri.saat, telemetri.pilGerilimi);
            }


        }

        private void buttonManuelAyril_Click(object sender, EventArgs e)
        {
            _serialPort.Write("A");
            Console.WriteLine("A");
        }

        private void buttonYer2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Yer2");
        }

        private void buttonBuzzer_Click(object sender, EventArgs e)
        {
            _serialPort.Write("B");
            Console.WriteLine("B");
        }

        private void buttonCsvTemizle_Click(object sender, EventArgs e)
        {
            if (File.Exists("TMUY2023_243868_TLM.csv"))
            {
                MessageBox.Show("Veriler silindi , listeleyebilirsiniz !");
                File.Delete("TMUY2023_243868_TLM.csv");


            }
            else { MessageBox.Show("Dosya mevcut değil."); }
            StringBuilder csvContent = new StringBuilder();

            string ilkSatir = "Paket No,Uydu Statu,Hata Kodu, Tarih, Saat, Basinc1, Basinc2, Yukseklik1, Yukseklik2, Irtifa Farki, InisHizi, Sicaklik, PilGerilimi," +
                 " GpsLatitude, GpsLongitude, GpsAltitude, Pitch, Roll, Yaw, Takim No, Tasiyici Inis Hizi";
            csvContent.AppendLine(ilkSatir);
            string csvPath = "TMUY2023_243868_TLM.csv";
            File.AppendAllText(csvPath, csvContent.ToString());
        }

        private void buttonCsvKaydet_Click(object sender, EventArgs e)
        {
            csvVeriKaydedilsinmi = true;
        }

        private void buttonCsvDurdur_Click(object sender, EventArgs e)
        {
            csvVeriKaydedilsinmi = false;
        }

        private void timerKamera_Tick(object sender, EventArgs e)
        {
            IVideoSource videoSource = videoSourcePlayer1.VideoSource;

            if (videoSource != null)
            {
                // get number of frames since the last timer tick
                int framesReceived = videoSource.FramesReceived;

                if (stopWatch == null)
                {
                    stopWatch = new System.Diagnostics.Stopwatch();
                    stopWatch.Start();
                }
                else
                {
                    stopWatch.Stop();

                    float fps = 1000.0f * framesReceived / stopWatch.ElapsedMilliseconds;


                    stopWatch.Reset();
                    stopWatch.Start();
                }
            }
        }

        private void buttonRecStart_Click(object sender, EventArgs e)
        {

        }

        private void buttonRecSave_Click(object sender, EventArgs e)
        {

        }

        private void buttonRecStart_Click_1(object sender, EventArgs e)
        {
            captureDevice = new VideoCaptureDeviceForm();
            buttonRecStop.Enabled = true;
            if (captureDevice.ShowDialog(this) == DialogResult.OK)
            {
                // create video source
                FinalVideo = captureDevice.VideoDevice;

                // open it
                OpenVideoSource(FinalVideo);
                FinalVideo.NewFrame += new NewFrameEventHandler(FinalVideo_NewFrame);
                FinalVideo.Start();
            }
        }

        private void buttonRecSave_Click_1(object sender, EventArgs e)
        {
            if (buttonRecStop.Text == "Kamerayı durdur")
            {
                saveAvi = new SaveFileDialog();
                saveAvi.Filter = "Avi Files (*.avi)|*.avi";
                if (saveAvi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    int h = captureDevice.VideoDevice.VideoResolution.FrameSize.Height;
                    int w = captureDevice.VideoDevice.VideoResolution.FrameSize.Width;
                    FileWriter.Open(saveAvi.FileName, w, h, 25, VideoCodec.Default, 5000000);
                    FileWriter.WriteVideoFrame(video);

                    buttonRecStop.Text = "Kaydı durdur";
                }
            }
        }

        private void buttonRecStop_Click(object sender, EventArgs e)
        {
            if (buttonRecStop.Text == "Kaydı durdur")
            {
                buttonRecStop.Text = "Kamerayı durdur";
                if (FinalVideo == null)
                { return; }
                if (FinalVideo.IsRunning)
                {
                    //this.FinalVideo.Stop();
                    FileWriter.Close();
                    //this.AVIwriter.Close();

                }
                MessageBox.Show("video kaydedildi");
            }
            else
            {
                this.FinalVideo.Stop();
                FileWriter.Close();
                //this.AVIwriter.Close();

            }
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // create video source
                FileVideoSource fileSource = new FileVideoSource(openFileDialog1.FileName);

                // open it
                OpenVideoSource(fileSource);
            }
        }
    }
}
