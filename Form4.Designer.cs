namespace _2023MUYGCS
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonFtpAl = new System.Windows.Forms.Button();
            this.labelFtpDurum = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textBoxDosyaAdi = new System.Windows.Forms.TextBox();
            this.textBoxftpurl = new System.Windows.Forms.TextBox();
            this.textBoxSunucuadi = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelAlmaDurumu = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // buttonFtpAl
            // 
            this.buttonFtpAl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFtpAl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonFtpAl.ForeColor = System.Drawing.Color.YellowGreen;
            this.buttonFtpAl.Location = new System.Drawing.Point(1747, 355);
            this.buttonFtpAl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonFtpAl.Name = "buttonFtpAl";
            this.buttonFtpAl.Size = new System.Drawing.Size(338, 154);
            this.buttonFtpAl.TabIndex = 77;
            this.buttonFtpAl.Text = "DOSYA ALMAYI BAŞLAT";
            this.buttonFtpAl.UseVisualStyleBackColor = true;
            this.buttonFtpAl.Click += new System.EventHandler(this.buttonFtpAl_Click);
            // 
            // labelFtpDurum
            // 
            this.labelFtpDurum.AutoSize = true;
            this.labelFtpDurum.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelFtpDurum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelFtpDurum.Location = new System.Drawing.Point(545, 613);
            this.labelFtpDurum.Name = "labelFtpDurum";
            this.labelFtpDurum.Size = new System.Drawing.Size(609, 46);
            this.labelFtpDurum.TabIndex = 78;
            this.labelFtpDurum.Text = "Sunucuyla Bağlantı Test edilmedi";
            // 
            // buttonConnect
            // 
            this.buttonConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonConnect.ForeColor = System.Drawing.Color.YellowGreen;
            this.buttonConnect.Location = new System.Drawing.Point(1747, 110);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(338, 154);
            this.buttonConnect.TabIndex = 79;
            this.buttonConnect.Text = "Sunucuya Bağlan";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // textBoxDosyaAdi
            // 
            this.textBoxDosyaAdi.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBoxDosyaAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxDosyaAdi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.textBoxDosyaAdi.Location = new System.Drawing.Point(434, 218);
            this.textBoxDosyaAdi.Name = "textBoxDosyaAdi";
            this.textBoxDosyaAdi.Size = new System.Drawing.Size(1102, 53);
            this.textBoxDosyaAdi.TabIndex = 80;
            this.textBoxDosyaAdi.Text = "videoplayback.mp4";
            this.textBoxDosyaAdi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxftpurl
            // 
            this.textBoxftpurl.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBoxftpurl.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxftpurl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.textBoxftpurl.Location = new System.Drawing.Point(434, 75);
            this.textBoxftpurl.Name = "textBoxftpurl";
            this.textBoxftpurl.Size = new System.Drawing.Size(1102, 53);
            this.textBoxftpurl.TabIndex = 81;
            this.textBoxftpurl.Text = "ftp://mt-sauron-da.guzelhosting.com/";
            this.textBoxftpurl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxSunucuadi
            // 
            this.textBoxSunucuadi.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBoxSunucuadi.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxSunucuadi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.textBoxSunucuadi.Location = new System.Drawing.Point(434, 355);
            this.textBoxSunucuadi.Name = "textBoxSunucuadi";
            this.textBoxSunucuadi.Size = new System.Drawing.Size(1102, 53);
            this.textBoxSunucuadi.TabIndex = 82;
            this.textBoxSunucuadi.Text = "yunusgu2";
            this.textBoxSunucuadi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBoxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.textBoxPassword.Location = new System.Drawing.Point(434, 469);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(1102, 53);
            this.textBoxPassword.TabIndex = 83;
            this.textBoxPassword.Text = "108484Yg.//";
            this.textBoxPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(102, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 46);
            this.label1.TabIndex = 84;
            this.label1.Text = "Sunucu Yolu:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label2.Location = new System.Drawing.Point(102, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 46);
            this.label2.TabIndex = 85;
            this.label2.Text = "Dosya Adi:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label3.Location = new System.Drawing.Point(102, 476);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 46);
            this.label3.TabIndex = 87;
            this.label3.Text = "Password:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label4.Location = new System.Drawing.Point(102, 340);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(214, 46);
            this.label4.TabIndex = 86;
            this.label4.Text = "Username:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label5.Location = new System.Drawing.Point(109, 613);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(388, 46);
            this.label5.TabIndex = 88;
            this.label5.Text = "Ftp Bağlantı durumu:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label6.Location = new System.Drawing.Point(109, 742);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(467, 46);
            this.label6.TabIndex = 89;
            this.label6.Text = "Ftp Dosya Alma Durumu:";
            // 
            // labelAlmaDurumu
            // 
            this.labelAlmaDurumu.AutoSize = true;
            this.labelAlmaDurumu.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelAlmaDurumu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelAlmaDurumu.Location = new System.Drawing.Point(597, 742);
            this.labelAlmaDurumu.Name = "labelAlmaDurumu";
            this.labelAlmaDurumu.Size = new System.Drawing.Size(0, 46);
            this.labelAlmaDurumu.TabIndex = 90;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(117, 820);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1445, 115);
            this.progressBar1.TabIndex = 91;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(2301, 994);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.labelAlmaDurumu);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxSunucuadi);
            this.Controls.Add(this.textBoxftpurl);
            this.Controls.Add(this.textBoxDosyaAdi);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.labelFtpDurum);
            this.Controls.Add(this.buttonFtpAl);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form4";
            this.Text = "YER İSTASYONU 2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form4_FormClosing);
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonFtpAl;
        private System.Windows.Forms.Label labelFtpDurum;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox textBoxDosyaAdi;
        private System.Windows.Forms.TextBox textBoxftpurl;
        private System.Windows.Forms.TextBox textBoxSunucuadi;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelAlmaDurumu;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}