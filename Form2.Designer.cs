namespace _2023MUYGCS
{
    partial class Form2
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartYukseklik1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartYukseklik1)).BeginInit();
            this.SuspendLayout();
            // 
            // chartYukseklik1
            // 
            this.chartYukseklik1.BackColor = System.Drawing.Color.Black;
            this.chartYukseklik1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.Center;
            this.chartYukseklik1.BorderlineColor = System.Drawing.Color.IndianRed;
            chartArea1.Name = "ChartArea1";
            this.chartYukseklik1.ChartAreas.Add(chartArea1);
            this.chartYukseklik1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartYukseklik1.Location = new System.Drawing.Point(0, 0);
            this.chartYukseklik1.Name = "chartYukseklik1";
            this.chartYukseklik1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Name = "Series1";
            series1.YValuesPerPoint = 2;
            this.chartYukseklik1.Series.Add(series1);
            this.chartYukseklik1.Size = new System.Drawing.Size(1433, 949);
            this.chartYukseklik1.TabIndex = 7;
            this.chartYukseklik1.Text = "chart3";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1433, 949);
            this.Controls.Add(this.chartYukseklik1);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.chartYukseklik1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartYukseklik1;
    }
}