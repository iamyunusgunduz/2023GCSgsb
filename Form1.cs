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
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.IO;
using System.Net.NetworkInformation;
using System.Net;

namespace _2023MUYGCS
{

    public partial class Form1 : Form
    {
        String tryCatchDurumu = "";
        private System.Diagnostics.Stopwatch stopWatch = null;
        static TelemetriVerileriModel telemetri = new TelemetriVerileriModel();
        static bool _continue;
        public static bool ftpDurumu = false;
        static bool csvVeriKaydedilsinmi = false;
        public static string title;
        static SerialPort _serialPort;
        Thread readThread = new Thread(Read);
        public string ftpStatus = "%0";
        public string gonderilenKomut = "";
        public string ftpicinFileNamePath = "";
        public string ftpicinFileFullPath = "";
  
        static string ftpPingState = "";

        double x = 0, y = 0, z = 0;
        bool cx = false, cy = false, cz = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            porAdiGetir();
            gmap.DragButton = MouseButtons.Left;
            gmap.MapProvider = GMapProviders.GoogleMap;

            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            captureDevice = new VideoCaptureDeviceForm();

            

            backgroundWorker1.WorkerSupportsCancellation = true;

            GL.ClearColor(Color.FromArgb(110, 133, 221));//Color.FromArgb(143, 212, 150) Color.White
            timerXYZ.Interval = 10;
        }
      
        static void MyThreadFunction(CancellationToken token)
        {
 
            while (true)
            {
                // token'in Cancel çağrısı yapılırsa thread çalışmasını sonlandır
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Thread canceled");
                    return;
                }

                Console.WriteLine("Thread is running");
                string ipAddress = "192.168.4.3"; // ping atılacak IP adresi

                Ping pingSender = new Ping();

                try
                {
                    PingReply reply = pingSender.Send(ipAddress, 5000); // Timeout 5000 ms olarak ayarlandı

                    if (reply.Status == IPStatus.Success)
                    {
                        Console.WriteLine("+++++++++++++++++++++++++++++++++4Ping başarılı: " + reply.RoundtripTime + " ms");
                    
                        ftpPingState = "Ping: Basarılı";
                    }
                    else if (reply.Status == IPStatus.TimedOut)
                    {
                      
                        ftpPingState = "Ping: Timedout";
                    }
                   else  if (reply.Status == IPStatus.DestinationHostUnreachable)
                    {
                       
                        ftpPingState = "Ping: ip erisemedi";
                    }
                   else if (reply.Status == IPStatus.DestinationPortUnreachable)
                    {

                        ftpPingState = "Ping: ip baglanamadı";
                    }
                   else  if (reply.Status == IPStatus.DestinationNetworkUnreachable)
                    {

                        ftpPingState = "Ping: ağa erisemedi";
                    }
                    else
                    {
                        Console.WriteLine("--------------------------------------Ping başarısız: " + reply.Status);
                        // buttonFtpBaglantiTest.BackColor = Color.DarkRed;
                        //  buttonFtpBaglantiTest.Text = "Baglanti yok";
                        ftpPingState = "Ping: error";
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                Thread.Sleep(1000);
            }
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
                    buttonSerialPortBaglanti.ForeColor = Color.DarkRed;
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
                    string gelenTelemetriVerisiRemoveKucuktur = gelenTelemetriVerisiRemoveBuyuktur.Replace('<', ' ').Trim();
                    try
                    {
                        if (!gelenTelemetriVerisiRemoveKucuktur.Contains('?') && !gelenTelemetriVerisiRemoveKucuktur.Contains('#') && !gelenTelemetriVerisiRemoveKucuktur.Contains('~'))
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
                            telemetri.ftpGeldimi = gelenTelemetriVeriDizisi[21];

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
            Console.WriteLine("Debug:  FinalVideo_NewFrame girdi ");
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


            if (csvVeriKaydedilsinmi)
            {
                buttonCsvKaydet.BackColor = Color.DarkGreen;

            }
            else
            {
                buttonCsvKaydet.BackColor = Color.DarkRed;
            }
            
            Console.WriteLine("Thread durumu: " + readThread.ThreadState);
            this.Text = " Model uydu takımı Yer istasyonu :  " + title + "\t \t { " + gonderilenKomut + " } \t Debug:" + tryCatchDurumu + ftpPingState;
            gonderilenKomut = "";
            ftpPingState = "";
            Console.WriteLine(telemetri.ftpGeldimi);
            if (ftpStatus == "OK")
            {
                if (telemetri.ftpGeldimi.Trim() == "0")
                {

                    button30.ForeColor = Color.Red;

                }
                else
                {
                    button30.ForeColor = Color.Green;
                }

            }

            labelFtpStatus.Text = ftpStatus;
            if (_continue)
            {

                if (cx == false || cy == false || cz == false)
                {
                    cx = true; cy = true; cz = true;
                }
                else
                {
                    cx = false; cy = false; cz = false;
                    timerXYZ.Start();
                    // btnX.Text = "Uydu duruşunu göster ";
                }



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
                //    Console.WriteLine("Hata kodu bosluksuz adet(" + boslukSil.Length + ")");
                 //   Console.WriteLine("Hata kodu bosluksuz (" + boslukSil + ")");
                    char[] _GelenHataKodunuDiziyeAyirma = boslukSil.ToCharArray();
                    telemetri.hataKodu1 = _GelenHataKodunuDiziyeAyirma[0];
                    telemetri.hataKodu2 = _GelenHataKodunuDiziyeAyirma[1];
                    telemetri.hataKodu3 = _GelenHataKodunuDiziyeAyirma[2];
                    telemetri.hataKodu4 = _GelenHataKodunuDiziyeAyirma[3];
                    telemetri.hataKodu5 = _GelenHataKodunuDiziyeAyirma[4];

           //         Console.WriteLine("telemetri hatakodu1: " + telemetri.hataKodu1);
                //    Console.WriteLine("telemetri hatakodu2: " + telemetri.hataKodu2);
               //     Console.WriteLine("telemetri hatakodu3: " + telemetri.hataKodu3);
             //       Console.WriteLine("telemetri hatakodu4: " + telemetri.hataKodu4);
             //       Console.WriteLine("telemetri hatakodu5: " + telemetri.hataKodu5);

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

               /*
                * if (cx == false)
                    cx = true;
                else
                    cx = false;

                if (cy == false)
                    cy = true;
                else
                    cy = false;
                if (cz == false)
                    cz = true;
                else
                    cz = false;

                */

                timer1.Start();
                Zamanlayici.Start();



            
                try
                {
                    gmap.Position = new GMap.NET.PointLatLng(Convert.ToDouble(telemetri.gps1Lat.Replace('.', ',')), Convert.ToDouble(telemetri.gps1Long.Replace('.', ',')));
                    gmap.ShowCenter = true;
                    gmap.MinZoom = 0;
                    gmap.MaxZoom = 24;
                    gmap.Zoom = 18;
                }
                catch (Exception err)
                {

                    Console.WriteLine("Hata Harita " + err);
                }

                /*
                //  GorevYukuMarker
                GMapMarker GorevYukuMarker = new GMarkerGoogle(
                new PointLatLng(Convert.ToDouble(telemetri.gps1Lat), Convert.ToDouble(telemetri.gps1Long)),
                GMarkerGoogleType.red_small);
                markersGorevYuku.Markers.Add(GorevYukuMarker);
                gmap.Overlays.Add(markersGorevYuku);
                GorevYukuMarker.ToolTipText = "GorevYuku";
                GorevYukuMarker.ToolTipMode = MarkerTooltipMode.Always;
                GorevYukuMarker.ToolTip.Fill = Brushes.White;
                GorevYukuMarker.ToolTip.Foreground = Brushes.Red;
                GorevYukuMarker.ToolTip.Stroke = Pens.Black;
                GorevYukuMarker.ToolTip.TextPadding = new Size(9, 2);
                */
                if (gmap.Overlays.Count > 1)
                {
                    try
                    {

                        gmap.Overlays.Remove(markersGorevYuku);

                        gmap.Refresh();

                    }

                    catch (Exception err)
                    {

                        Console.WriteLine("hATA" + err);
                    }

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
            if (_continue)
            {
                _serialPort.Write("k");
                Console.WriteLine("Debug: k");
                gonderilenKomut = "k";
            }

        }

        private void buttonBuzzer_Click(object sender, EventArgs e)
        {
            if (_continue)
            {
                _serialPort.Write("b");
                Console.WriteLine("Debug: b");
                gonderilenKomut = "b";
            }
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






        private void Zamanlayici_Tick(object sender, EventArgs e)
        {
            try
            {

               // x = Convert.ToInt32(telemetri.roll);
               // y = Convert.ToInt32(telemetri.pitch);
               // z = Convert.ToInt32(telemetri.yaw);
               // glControl1.Invalidate();


            }
            catch
            {

            }
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
        }


        private void silindir(float step, float topla, float radius, float dikey1, float dikey2)
        {
            float eski_step = 0.1f;
            GL.Begin(BeginMode.Quads);
            while (step <= 360)
            {
                if (step < 45)
                    GL.Color3(Color.FromArgb(255, 0, 0));
                else if (step < 90)
                    GL.Color3(Color.FromArgb(0, 0, 0));
                else if (step < 135)
                    GL.Color3(Color.FromArgb(255, 0, 0));
                else if (step < 180)
                    GL.Color3(Color.FromArgb(0, 0, 0));
                else if (step < 225)
                    GL.Color3(Color.FromArgb(255, 0, 0));
                else if (step < 270)
                    GL.Color3(Color.FromArgb(0, 0, 0));
                else if (step < 315)
                    GL.Color3(Color.FromArgb(255, 0, 0));
                else if (step < 360)
                    GL.Color3(Color.FromArgb(0, 0, 0));


                float ciz1_x = (float)(radius * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey1, ciz1_y);

                float ciz2_x = (float)(radius * Math.Cos((step + 2) * Math.PI / 180F));
                float ciz2_y = (float)(radius * Math.Sin((step + 2) * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey1, ciz2_y);

                GL.Vertex3(ciz1_x, dikey2, ciz1_y);
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);
                step += topla;
            }
            GL.End();
            GL.Begin(BeginMode.Lines);
            step = eski_step;
            topla = step;
            while (step <= 180)// UST KAPAK
            {
                if (step < 45)
                    GL.Color3(Color.FromArgb(255, 1, 1));
                else if (step < 90)
                    GL.Color3(Color.FromArgb(0, 0, 0));
                else if (step < 135)
                    GL.Color3(Color.FromArgb(255, 1, 1));
                else if (step < 180)
                    GL.Color3(Color.FromArgb(0, 0, 0));
                else if (step < 225)
                    GL.Color3(Color.FromArgb(255, 1, 1));
                else if (step < 270)
                    GL.Color3(Color.FromArgb(0, 0, 0));
                else if (step < 315)
                    GL.Color3(Color.FromArgb(255, 1, 1));
                else if (step < 360)
                    GL.Color3(Color.FromArgb(0, 0, 0));


                float ciz1_x = (float)(radius * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey1, ciz1_y);

                float ciz2_x = (float)(radius * Math.Cos((step + 180) * Math.PI / 180F));
                float ciz2_y = (float)(radius * Math.Sin((step + 180) * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey1, ciz2_y);

                GL.Vertex3(ciz1_x, dikey1, ciz1_y);
                GL.Vertex3(ciz2_x, dikey1, ciz2_y);
                step += topla;
            }
            step = eski_step;
            topla = step;
            while (step <= 180)//ALT KAPAK
            {
                if (step < 45)
                    GL.Color3(Color.FromArgb(255, 1, 1));
                else if (step < 90)
                    GL.Color3(Color.FromArgb(0, 0, 0));
                else if (step < 135)
                    GL.Color3(Color.FromArgb(255, 1, 1));
                else if (step < 180)
                    GL.Color3(Color.FromArgb(0, 0, 0));
                else if (step < 225)
                    GL.Color3(Color.FromArgb(255, 1, 1));
                else if (step < 270)
                    GL.Color3(Color.FromArgb(0, 0, 0));
                else if (step < 315)
                    GL.Color3(Color.FromArgb(255, 1, 1));
                else if (step < 360)
                    GL.Color3(Color.FromArgb(0, 0, 0));

                float ciz1_x = (float)(radius * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey2, ciz1_y);

                float ciz2_x = (float)(radius * Math.Cos((step + 180) * Math.PI / 180F));
                float ciz2_y = (float)(radius * Math.Sin((step + 180) * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);

                GL.Vertex3(ciz1_x, dikey2, ciz1_y);
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);
                step += topla;
            }
            GL.End();
        }
        private void koni(float step, float topla, float radius1, float radius2, float dikey1, float dikey2)
        {
            float eski_step = 0.1f;
            GL.Begin(BeginMode.Lines);//Y EKSEN CIZIM DAİRENİN
            while (step <= 360)
            {
                if (step < 45)
                    GL.Color3(0.0, 0.0, 0.0);
                else if (step < 90)
                    GL.Color3(1.0, 0.0, 0.0);
                else if (step < 135)
                    GL.Color3(0.0, 0.0, 0.0);
                else if (step < 180)
                    GL.Color3(1.0, 0.0, 0.0);
                else if (step < 225)
                    GL.Color3(0.0, 0.0, 0.0);
                else if (step < 270)
                    GL.Color3(1.0, 0.0, 0.0);
                else if (step < 315)
                    GL.Color3(0.0, 0.0, 0.0);
                else if (step < 360)
                    GL.Color3(1.0, 0.0, 0.0);


                float ciz1_x = (float)(radius1 * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius1 * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey1, ciz1_y);

                float ciz2_x = (float)(radius2 * Math.Cos(step * Math.PI / 180F));
                float ciz2_y = (float)(radius2 * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);
                step += topla;
            }
            GL.End();

            GL.Begin(BeginMode.Lines);
            step = eski_step;
            topla = step;
            while (step <= 180)// UST KAPAK
            {
                if (step < 45)
                    GL.Color3(Color.FromArgb(0, 0, 0));
                else if (step < 90)
                    GL.Color3(Color.FromArgb(0, 0, 0));
                else if (step < 135)
                    GL.Color3(Color.FromArgb(0, 0, 0));
                else if (step < 180)
                    GL.Color3(Color.FromArgb(0, 0, 0));
                else if (step < 225)
                    GL.Color3(Color.FromArgb(0, 0, 0));
                else if (step < 270)
                    GL.Color3(Color.FromArgb(0, 0, 0));
                else if (step < 315)
                    GL.Color3(Color.FromArgb(0, 0, 0));
                else if (step < 360)
                    GL.Color3(Color.FromArgb(0, 0, 0));


                float ciz1_x = (float)(radius2 * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius2 * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey2, ciz1_y);

                float ciz2_x = (float)(radius2 * Math.Cos((step + 180) * Math.PI / 180F));
                float ciz2_y = (float)(radius2 * Math.Sin((step + 180) * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);

                GL.Vertex3(ciz1_x, dikey2, ciz1_y);
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);
                step += topla;
            }
            step = eski_step;
            topla = step;
            GL.End();
        }
        private void Pervane(float yukseklik, float uzunluk, float kalinlik, float egiklik)
        {
            float radius = 10, angle = 45.0f;
            GL.Begin(BeginMode.Quads);

            GL.Color3(Color.Black);
            GL.Vertex3(uzunluk, yukseklik, kalinlik);
            GL.Vertex3(uzunluk, yukseklik + egiklik, -kalinlik);
            GL.Vertex3(0.0, yukseklik + egiklik, -kalinlik);
            GL.Vertex3(0.0, yukseklik, kalinlik);

            GL.Color3(Color.Black);
            GL.Vertex3(-uzunluk, yukseklik + egiklik, kalinlik);
            GL.Vertex3(-uzunluk, yukseklik, -kalinlik);
            GL.Vertex3(0.0, yukseklik, -kalinlik);
            GL.Vertex3(0.0, yukseklik + egiklik, kalinlik);

            GL.Color3(Color.Black);
            GL.Vertex3(kalinlik, yukseklik, -uzunluk);
            GL.Vertex3(-kalinlik, yukseklik + egiklik, -uzunluk);
            GL.Vertex3(-kalinlik, yukseklik + egiklik, 0.0);//+
            GL.Vertex3(kalinlik, yukseklik, 0.0);//-

            GL.Color3(Color.Black);
            GL.Vertex3(kalinlik, yukseklik + egiklik, +uzunluk);
            GL.Vertex3(-kalinlik, yukseklik, +uzunluk);
            GL.Vertex3(-kalinlik, yukseklik, 0.0);
            GL.Vertex3(kalinlik, yukseklik + egiklik, 0.0);
            GL.End();

        }


        struct FtpSetting
        {
            public string Server { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string FileName { get; set; }
            public string FullName { get; set; }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
             

            string fileName = ((FtpSetting)e.Argument).FileName;
            string fullName = ((FtpSetting)e.Argument).FullName;
            string userName = ((FtpSetting)e.Argument).Username;
            string password = ((FtpSetting)e.Argument).Password;
            string server = ((FtpSetting)e.Argument).Server;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", server, fileName)));
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(userName, password);
          //  request.Timeout = 500000;
            try
            {
                Stream ftpStream = request.GetRequestStream();
                FileStream fs = File.OpenRead(fullName);
                byte[] buffer = new byte[1024];
                double total = (double)fs.Length;
                int byteRead = 0;
                double read = 0;
                do
                {
                    if (!backgroundWorker1.CancellationPending)
                    {
                        //Upload file & update  bar

                        byteRead = fs.Read(buffer, 0, 1024);
                        ftpStream.Write(buffer, 0, byteRead);
                        read += (double)byteRead;
                        double percentage = read / total * 100;
                        backgroundWorker1.ReportProgress((int)percentage);
                    }
                    else
                    {
                        Console.WriteLine("Debug: yiptaloldu");
                        e.Cancel = true;
                    }


                }
                while (byteRead != 0);
                fs.Close();
                ftpStream.Close();
            }
            catch (Exception err)
            {
                

                Console.WriteLine("Debug: ne oldi ftp yukleme "+err);

                ftpStatus = "Bağlanamadı";
                Console.WriteLine("Ftp Status" + ftpStatus);


                if (backgroundWorker1.WorkerSupportsCancellation == true)
                {
                    /* Eğer backgroundWorker1 için durdurulabilme özelliği de aktifse ki başta aktif ettim, butona
                     bastığımda backgroundWorker'in durdurulmasını istedim. Yanlış anlaşılmasın burada işlemi durdurmadım sadece
                     durdurulmasını istedim */
                    backgroundWorker1.CancelAsync();
                }
                BackgroundWorker worker = sender as BackgroundWorker;
                if (worker.CancellationPending == true)
                {
                    // Eğer yapılan işlemi durdumak için istek gönderildiyse, DoWork olayını durdur ve döngüden çık.
                    e.Cancel = true;

                }

                //if (backgroundWorker1.IsBusy != true)
                //{

                //}

                //if (lblStatus.Text == "Bağlanamadı")
                //{
                //    if (progressBar1.Value > 0 || progressBar1.Value < 100)
                //    {
                //        MessageBox.Show("Test");
                //    }
                //}


            }
        }
        FtpSetting _inputParameter;
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            ftpStatus = $"Yüklenen {e.ProgressPercentage} %";
            Console.WriteLine("Ftp Status" + ftpStatus);
            progressBar1.Value = e.ProgressPercentage;
            progressBar1.Update();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // İşlem tamamlandıktan sonra, worker örneğini yeniden başlatın
            if (!e.Cancelled)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }
        private void buttonRecStart_Click(object sender, EventArgs e)
        {
            captureDevice = new VideoCaptureDeviceForm();

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

        private void buttonRecSave_Click(object sender, EventArgs e)
        {
            if (buttonRecStop.Text == "Kamerayı durdur")
            {
                saveAvi = new SaveFileDialog();
                saveAvi.FileName = "MUY2023_243868_VIDEO.mp4";
                saveAvi.Filter = "MP4 Dosyaları (*.mp4)|*.mp4|Tüm Dosyalar (*.*)|*.*";

                if (saveAvi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    int h = captureDevice.VideoDevice.VideoResolution.FrameSize.Height;
                    int w = captureDevice.VideoDevice.VideoResolution.FrameSize.Width;
                    FileWriter.Open(saveAvi.FileName, w, h, 25, VideoCodec.Default, 5000000);
                    FileWriter.WriteVideoFrame(video);

                    buttonRecStop.Text = "Kaydı durdur";
                    buttonRecSave.ForeColor = Color.DarkGreen;
                }
            }
        }
        /*
        private void StartBackgroundWorker()
        {
            if (backgroundWorker1 == null)
            {
                backgroundWorker1 = new BackgroundWorker();
                backgroundWorker1.WorkerSupportsCancellation = true;
                backgroundWorker1.DoWork += backgroundWorker1_DoWork;
                backgroundWorker1.RunWorkerCompleted += Worker_RunWorkerCompleted;
            }

            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void StopBackgroundWorker()
        {
            if (backgroundWorker1 != null && backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
        }
        */
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
                buttonRecSave.ForeColor = Color.DarkRed;
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

        private void buttonKilitle_Click(object sender, EventArgs e)
        {
            if (_continue)
            {
                _serialPort.Write("s");
                Console.WriteLine("Debug: s");
                gonderilenKomut = "s";
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            // video atildiginda
            
                if (_continue)
                {
                    _serialPort.Write("v");
                    Console.WriteLine("Debug: v");
                    gonderilenKomut = "v";
                }
            
         
          
        }

        private void buttonGrafikTemizle_Click(object sender, EventArgs e)
        {
            foreach (var series in chartBasinc1.Series) series.Points.Clear();
            foreach (var series in chartBasinc2.Series) series.Points.Clear();
            foreach (var series in chartInisGizi.Series) series.Points.Clear();
            foreach (var series in chartIrtifaFarki.Series) series.Points.Clear();
            foreach (var series in chartPilGerilimi.Series) series.Points.Clear();
            foreach (var series in chartSicaklik.Series) series.Points.Clear();
            foreach (var series in chartYukseklik1.Series) series.Points.Clear();
            foreach (var series in chartYukseklik2.Series) series.Points.Clear();


        }

        private void button31_Click(object sender, EventArgs e)
        { 
            
            Form4 f4 = new Form4(); //this is the change, code for redirect  
            timer1.Stop();
           
            this.Hide();
            f4.Show();
           
          

        }

        private void buttonFtpBaglantiTest_Click(object sender, EventArgs e)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            Thread thread = new Thread(() => MyThreadFunction(token));
            thread.Start();

            // 5 saniye sonra thread'i iptal et
            Thread.Sleep(100);
            cts.Cancel();



        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ftpStatus != "Bağlanamadı")
            {
                ftpStatus = "OK";
                if (ftpStatus == "OK" && _continue)
                {

                    _serialPort.Write("v");
                    gonderilenKomut = "v";
                    Console.WriteLine("Debug: SerialWrite v");
                    Console.WriteLine("Ftp Status" + ftpStatus);

                }

            }
            else {

             //   backgroundWorker1.RunWorkerAsync(_inputParameter);
            }

            if (!e.Cancelled && ftpStatus != "OK")
            {
                backgroundWorker1.RunWorkerAsync();
            }

        }

        private void btnDosyaSec_Click(object sender, EventArgs e)
        {


            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = true, ValidateNames = true, Filter = "All files|*.*" })
            {

               
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fi = new FileInfo(ofd.FileName);

                    _inputParameter.Username = "huma";
                    _inputParameter.Password = "00000000";
                    _inputParameter.Server = "ftp://192.168.4.3:88";
                    /*
                  _inputParameter.Username = "yunusgu2";
                  _inputParameter.Password = "108484Yg.//";
                  _inputParameter.Server = "ftp://mt-sauron-da.guzelhosting.com:21";
                  */
                   
                    _inputParameter.FileName = fi.Name;
                    _inputParameter.FullName = fi.FullName;
                    ftpicinFileNamePath = fi.Name;
                    ftpicinFileFullPath = fi.FullName;
                    try
                    {
                        backgroundWorker1.RunWorkerAsync(_inputParameter);
                    }
                    catch (Exception err)
                    {

                        Console.WriteLine(err);
                    }
                  
                } 
            }
        }




        private void timerXYZ_Tick(object sender, EventArgs e)
        {
            /*
             if (cy == true)
             {
                 //roll
                 if (y < 360)
                     y = Convert.ToDouble(telemetri.roll);
                 else
                     y = 0;
                // lblX.Text = x.ToString();
             }
             if (cx == true)
             {
                 //pitch
                 if (x < 360)
                     x = Convert.ToDouble(telemetri.pitch);
                 else
                     x = 0;
                // lblY.Text = y.ToString();
             }
             if (cz == true)
             {
                 //yaw
                 if (z < 360)
                     z = Convert.ToDouble(telemetri.yaw);
                 else
                     z = 0;
               //  lblZ.Text = z.ToString();
             }
             glControl1.Invalidate();
            */

            try
            {
                y = Convert.ToDouble(telemetri.roll);

                //  y = 0;
               // lblX.Text = x.ToString();



                x = Convert.ToDouble(telemetri.pitch);

                //    x = 0;
              //  lblY.Text = y.ToString();



                z = Convert.ToDouble(telemetri.yaw);

                //      z = 0;
               // lblZ.Text = z.ToString();

                glControl1.Invalidate();
            }
            catch (Exception err)
            {

               // lblRefresh.Text = "" + err;
            }




        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {

            float step = 1.0f;
            float topla = step;
            float radius = 4.0f;
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(1.04f, 4 / 3, 1, 10000);
            Matrix4 lookat = Matrix4.LookAt(25, 0, 0, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.LoadMatrix(ref lookat);
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);


            GL.Rotate(x, 1.0, 0.0, 0.0);
            GL.Rotate(z, 0.0, 1.0, 0.0);
            GL.Rotate(y, 0.0, 0.0, 1.0);


            silindir(step, topla, radius, 3, -5);
            koni(0.01f, 0.01f, radius, 3.0f, 3, 5);//Ust koni
            koni(0.01f, 0.01f, radius, 2.0f, -5.0f, -10.0f);//Alt koni
            silindir(0.01f, topla, 0.07f, 9, 3);// rotor      

            silindir(0.01f, topla, 0.2f, 9, 9.3f);
            Pervane(9.0f, 7.0f, 0.3f, 0.3f);

            silindir(0.01f, topla, 0.2f, 7.3f, 7f);
            Pervane(7.0f, 7.0f, 0.3f, 0.3f);

            GL.Begin(BeginMode.Lines);

            GL.Color3(Color.FromArgb(250, 0, 0));
            GL.Vertex3(-1000, 0, 0);
            GL.Vertex3(1000, 0, 0);

            GL.Color3(Color.FromArgb(25, 150, 100));
            GL.Vertex3(0, 0, -1000);
            GL.Vertex3(0, 0, 1000);

            GL.Color3(Color.FromArgb(0, 0, 0));
            GL.Vertex3(0, 1000, 0);
            GL.Vertex3(0, -1000, 0);

            GL.End();

            glControl1.SwapBuffers();
        }
       GMapOverlay markersGorevYuku = new GMapOverlay("markersGorevYuku");

    }
   
}
