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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button28 = new System.Windows.Forms.Button();
            this.btnDosyaSec = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.MediumPurple;
            this.progressBar1.Location = new System.Drawing.Point(148, 427);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(798, 49);
            this.progressBar1.TabIndex = 74;
            this.progressBar1.Value = 20;
            // 
            // button28
            // 
            this.button28.AutoSize = true;
            this.button28.BackColor = System.Drawing.Color.Black;
            this.button28.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button28.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button28.Location = new System.Drawing.Point(159, 242);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(219, 49);
            this.button28.TabIndex = 80;
            this.button28.Text = "COM1";
            this.button28.UseVisualStyleBackColor = false;
            // 
            // btnDosyaSec
            // 
            this.btnDosyaSec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDosyaSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDosyaSec.ForeColor = System.Drawing.Color.Green;
            this.btnDosyaSec.Location = new System.Drawing.Point(438, 191);
            this.btnDosyaSec.Name = "btnDosyaSec";
            this.btnDosyaSec.Size = new System.Drawing.Size(219, 46);
            this.btnDosyaSec.TabIndex = 75;
            this.btnDosyaSec.Text = "UYDUYA BAĞLAN";
            this.btnDosyaSec.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.ForeColor = System.Drawing.Color.YellowGreen;
            this.button2.Location = new System.Drawing.Point(721, 269);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(225, 101);
            this.button2.TabIndex = 76;
            this.button2.Text = "DOSYA ALMAYI DURDUR";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button7.ForeColor = System.Drawing.Color.YellowGreen;
            this.button7.Location = new System.Drawing.Point(721, 163);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(225, 100);
            this.button7.TabIndex = 77;
            this.button7.Text = "DOSYA ALMAYI BAŞLAT";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.ForeColor = System.Drawing.Color.Green;
            this.button1.Location = new System.Drawing.Point(432, 296);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(225, 46);
            this.button1.TabIndex = 81;
            this.button1.Text = "BAĞLANTIYI KES";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1092, 646);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button28);
            this.Controls.Add(this.btnDosyaSec);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button7);
            this.Name = "Form4";
            this.Text = "YER İSTASYONU 2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button28;
        private System.Windows.Forms.Button btnDosyaSec;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button1;
    }
}