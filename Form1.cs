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

namespace _2023MUYGCS
{

    public partial class Form1 : Form
    {


       static TelemetriVerileriModel telemetri = new TelemetriVerileriModel();
        static bool _continue;
        static SerialPort _serialPort;
        Thread readThread = new Thread(Read);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            porAdiGetir();
            




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
            comboBoxPortName.SelectedIndex = 0;
           
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
                   
                    if(readThread.ThreadState == ThreadState.Suspended)
                    {
                        readThread.Resume();
                    }
                    else
                    {
                        readThread.Start();
                    }
                   
                    buttonSerialPortBaglanti.ForeColor = Color.Red;
                    buttonSerialPortBaglanti.Text = "Baglantiyi kes";
                    timer1.Start();
                }
                catch (Exception err)
                {

                    Console.WriteLine("hata"+ err);
                }
            }
            else
            {
                try
                {
                    _serialPort.Close();
                    _continue = false;
                    readThread.Suspend();
               
                    buttonSerialPortBaglanti.ForeColor = Color.Green;
                    buttonSerialPortBaglanti.Text = "Bağlan";
                    timer1.Stop();
                }
                catch (Exception err)
                {

                    Console.WriteLine("Hata "+ err);
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
                    Console.WriteLine(gelenTelemetriVerisi);
                  string gelenTelemetriVerisiRemoveBuyuktur = gelenTelemetriVerisi.Replace('>',' ');
                    string gelenTelemetriVerisiRemoveKucuktur = gelenTelemetriVerisiRemoveBuyuktur.Replace('<', ' ');
                    if (!gelenTelemetriVerisiRemoveKucuktur.Contains('?') || !gelenTelemetriVerisiRemoveKucuktur.Contains('#'))
                    {
                        string[] gelenTelemetriVeriDizisi = gelenTelemetriVerisiRemoveKucuktur.Split(',');

                        telemetri.paketNo = gelenTelemetriVeriDizisi[0];
                        telemetri.uyduStatu = gelenTelemetriVeriDizisi[1];
                        telemetri.hataKodu = gelenTelemetriVeriDizisi[2];
                        telemetri.gondermeSaati = gelenTelemetriVeriDizisi[3] +", "+ gelenTelemetriVeriDizisi[4];
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
                    }
                   

                }
                catch (Exception err) {
                    Console.WriteLine("Hata "+err);
                }
                
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
             
                _serialPort.Close();
                _continue = false;
                readThread.Join();
            timer1.Stop();
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // sagdaki labeller
            labelPaketNoVALUE.Text = telemetri.paketNo;
         labelUyduStatusuVALUE.Text = telemetri.uyduStatu;
          labelHataKoduVALUE.Text = telemetri.hataKodu ;
          labelGondermeSaatiVALUE.Text = telemetri.gondermeSaati ;
          labelBasinc1GYVALUE.Text = telemetri.basinc1 + " (Pa)";
         labelBasinc2TVALUE.Text  = telemetri.basinc2 + " (Pa)";
            labelYukseklik1GYVALUE.Text   = telemetri.yukseklik1 + " m";
            labelYukseklik2TVALUE.Text= telemetri.yukseklik2 + " m";
            labelIrtifaFarkiVALUE.Text = telemetri.irtifaFarki + " m";
            labelGYinisHiziVALUE.Text = telemetri.inisHizi + " (m/s)";
            labelSicaklikVALUE.Text = telemetri.sicaklik + " °C";
            labelPilGerilimiVALUE.Text = telemetri.pilGerilimi + " V";
            labelGps1LatGYVALUE.Text =  telemetri.gps1Lat ;
            labelGps1LongGYVALUE.Text  = telemetri.gps1Long ;
        labelGps1AltGYVALUE.Text  =  telemetri.gps1Alt + " m";
            labelPitchVALUE.Text  = telemetri.pitch + " °";
            labelRollVALUE.Text  = telemetri.roll + " °";
            labelYawVALUE.Text =  telemetri.yaw + " °";
            labelTakimNoVALUE.Text  = telemetri.takimNo ;
         labelTinisHiziVALUE.Text  = telemetri.tasiyiciInisHizi + " (m/s)";

            // label grafik ustundekiler
            labelGraphBasinc1.Text = "BASINÇ-1 (GÖREV YÜKÜ) : "+ telemetri.basinc1 + "(Pa)";
            labelGraphBasinc2.Text = "BASINÇ-2 (TAŞIYICI) : " + telemetri.basinc2 + "(Pa)";
            labelGraphYukseklik1.Text = "YÜKSEKLİK-1 (GÖREV YÜKÜ) : "+ telemetri.yukseklik1 + " m";
            labelGraphYukseklik2.Text = "YÜKSEKLİK-2 (TAŞIYICI) :" + telemetri.yukseklik2 + " m";
            labelGraphIrtifaFarki.Text = "İRTİFA FARKI : " + telemetri.irtifaFarki + " m";
            labelGraphGYinisHizi.Text = "GÖREV YÜKÜ İNİŞ HIZI : " + telemetri.inisHizi + " (m/s)";
            labelGraphSicaklik.Text = "SICAKLIK" + telemetri.sicaklik + " °C";
            labelGraphPilGerilimi.Text = "PİL GERİLİMİ" + telemetri.pilGerilimi + " V";
        }
    }
}
