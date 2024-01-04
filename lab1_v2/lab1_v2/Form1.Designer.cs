using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace lab1_v2
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();

            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.manuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otworzPlikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.NUD_wspC = new System.Windows.Forms.NumericUpDown();
            this.NUD_wspK = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonOptymalnyWeibull = new System.Windows.Forms.Button();
            this.buttonPorownanie = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_wspC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_wspK)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1010, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // manuToolStripMenuItem
            // 
            this.manuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.otworzPlikToolStripMenuItem});
            this.manuToolStripMenuItem.Name = "manuToolStripMenuItem";
            this.manuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.manuToolStripMenuItem.Text = "Menu";
            // 
            // otworzPlikToolStripMenuItem
            // 
            this.otworzPlikToolStripMenuItem.Name = "otworzPlikToolStripMenuItem";
            this.otworzPlikToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.otworzPlikToolStripMenuItem.Text = "Otworz plik";
            this.otworzPlikToolStripMenuItem.Click += new System.EventHandler(this.otworzPlikToolStripMenuItem_Click);
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chartArea2.Name = "ChartArea2";
            chartArea3.Name = "ChartArea3";
            chartArea4.Name = "ChartArea4";

            chartArea1.AxisX.Title = "Prędkość wiatru";
            chartArea1.AxisY.Title = "Gęstość prawdopodobieństwa";



            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.ChartAreas.Add(chartArea3);
            this.chart1.ChartAreas.Add(chartArea4);

            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(290, 40);
            this.chart1.Name = "chart1";

            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Gęstość prawdopodobieństwa wiatru o danej prędkości";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Rozkład Weibulla";

            series3.ChartArea = "ChartArea2";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Moc turbiny - model 1";

            series4.ChartArea = "ChartArea2";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "Moc turbiny - model 2";

            series5.ChartArea = "ChartArea2";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Legend = "Legend1";
            series5.Name = "Moc turbiny - model 3";

            series6.ChartArea = "ChartArea3";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Legend = "Legend1";
            series6.Name = "Gęstość mocy";

            series7.ChartArea = "ChartArea4";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            series7.Legend = "Legend1";
            series7.Name = "Moc wygenerowana - turbina 1";

            series8.ChartArea = "ChartArea4";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            series8.Legend = "Legend1";
            series8.Name = "Moc wygenerowana - turbina 2";

            series9.ChartArea = "ChartArea4";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            series9.Legend = "Legend1";
            series9.Name = "Moc wygenerowana - turbina 3";

            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Series.Add(series5);
            this.chart1.Series.Add(series6);
            this.chart1.Series.Add(series7);
            this.chart1.Series.Add(series8);
            this.chart1.Series.Add(series9);
            this.chart1.Size = new System.Drawing.Size(728, 284);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "styczeń",
            "luty",
            "marzec",
            "kwiecień",
            "maj",
            "czerwiec",
            "lipiec",
            "sierpień",
            "wrzesień",
            "październik",
            "listopad",
            "grudzień",
            "cały rok"});
            this.comboBox1.Location = new System.Drawing.Point(12, 27);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(101, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.buttonOptymalnyWeibull);
            this.groupBox1.Controls.Add(this.buttonPorownanie);
            this.groupBox1.Controls.Add(this.NUD_wspC);
            this.groupBox1.Controls.Add(this.NUD_wspK);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 177);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rozkład Weibulla";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 25);
            this.button1.TabIndex = 4;
            this.button1.Text = "Rozkład Weibulla";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            ///
            /// optymalny Weibull
            ///
            this.buttonOptymalnyWeibull.Location = new System.Drawing.Point(9, 123);
            this.buttonOptymalnyWeibull.Name = "buttonOptymalnyWeibull";
            this.buttonOptymalnyWeibull.Size = new System.Drawing.Size(170, 25);
            this.buttonOptymalnyWeibull.TabIndex = 4;
            this.buttonOptymalnyWeibull.Text = "Optymalny rozkład Weibulla";
            this.buttonOptymalnyWeibull.UseVisualStyleBackColor = true;
            this.buttonOptymalnyWeibull.Click += new System.EventHandler(this.optWeibullClick);
            ///
            /// porównanie turbin
            ///
            this.buttonPorownanie.Location = new System.Drawing.Point(9, 153);
            this.buttonPorownanie.Name = "buttonPorownanie";
            this.buttonPorownanie.Size = new System.Drawing.Size(170, 25);
            this.buttonPorownanie.TabIndex = 4;
            this.buttonPorownanie.Text = "Porównaj wygenerowaną moc";
            this.buttonPorownanie.UseVisualStyleBackColor = true;
            this.buttonPorownanie.Click += new System.EventHandler(this.porownanieMocyClick);
            // 
            // NUD_wspC
            // 
            this.NUD_wspC.DecimalPlaces = 1;
            this.NUD_wspC.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NUD_wspC.Location = new System.Drawing.Point(103, 59);
            this.NUD_wspC.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NUD_wspC.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_wspC.Name = "NUD_wspC";
            this.NUD_wspC.Size = new System.Drawing.Size(77, 20);
            this.NUD_wspC.TabIndex = 3;
            this.NUD_wspC.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_wspC.ValueChanged += new System.EventHandler(this.NUD_wspC_ValueChanged);
            // 
            // NUD_wspK
            // 
            this.NUD_wspK.DecimalPlaces = 1;
            this.NUD_wspK.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NUD_wspK.Location = new System.Drawing.Point(103, 28);
            this.NUD_wspK.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NUD_wspK.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_wspK.Name = "NUD_wspK";
            this.NUD_wspK.Size = new System.Drawing.Size(77, 20);
            this.NUD_wspK.TabIndex = 2;
            this.NUD_wspK.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_wspK.ValueChanged += new System.EventHandler(this.NUD_wspK_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Współczynnik c=";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Współczynnik k=";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "E-44",
            "E-101",
            "E-33"});
            this.comboBox2.Location = new System.Drawing.Point(45, 245);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(129, 21);
            this.comboBox2.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(45, 272);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Tworzenie 3 modeli";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.generuj3Modele);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(45, 301);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(129, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Rysuj model 1";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.rysuj3Modele);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(45, 330);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(129, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "Gęstośc mocy";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 450);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_wspC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_wspK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ToolStripMenuItem manuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otworzPlikToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NUD_wspC;
        private System.Windows.Forms.NumericUpDown NUD_wspK;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button buttonOptymalnyWeibull;
        private System.Windows.Forms.Button buttonPorownanie;

    }
}

