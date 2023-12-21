using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1_v2
{
    public partial class Form1 : Form
    {
        public Klasa symulator; //tworzenie konstruktora
        public Turbina turbina;
        private int liczbadni = 0; //zmienna pomocnicza

        public Form1()
        {
            InitializeComponent();
            symulator = new Klasa();//uruchomienie konstruktora
            
            liczbadni = 31;
            comboBox1.SelectedIndex = 0; //domyslnie ustawionty styczeń
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 30; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void otworzPlikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            symulator.otworzPlikWiatr(liczbadni);//wywołanie funkcji otwierajacej plik z probkami wiatru
            symulator.rysujHistogram(chart1, symulator.tabHistogramWiatr, 0);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) liczbadni = 31;
            if (comboBox1.SelectedIndex == 1) liczbadni = 28;
            if (comboBox1.SelectedIndex == 2) liczbadni = 31;
            if (comboBox1.SelectedIndex == 3) liczbadni = 30;
            if (comboBox1.SelectedIndex == 4) liczbadni = 31;
            if (comboBox1.SelectedIndex == 5) liczbadni = 30;
            if (comboBox1.SelectedIndex == 6) liczbadni = 31;
            if (comboBox1.SelectedIndex == 7) liczbadni = 31;
            if (comboBox1.SelectedIndex == 8) liczbadni = 30;
            if (comboBox1.SelectedIndex == 9) liczbadni = 31;
            if (comboBox1.SelectedIndex == 10) liczbadni = 30;
            if (comboBox1.SelectedIndex == 11) liczbadni = 31;
            if (comboBox1.SelectedIndex == 12) liczbadni = 365;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            symulator.rysujWeibull(chart1, (double)NUD_wspK.Value, (double)NUD_wspC.Value);
        }

        private void NUD_wspK_ValueChanged(object sender, EventArgs e)
        {
            symulator.rysujWeibull(chart1, (double)NUD_wspK.Value, (double)NUD_wspC.Value);

        }

        private void NUD_wspC_ValueChanged(object sender, EventArgs e)
        {
            symulator.rysujWeibull(chart1, (double)NUD_wspK.Value, (double)NUD_wspC.Value);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            turbina = new Turbina(comboBox2.Items[comboBox2.SelectedIndex].ToString(), comboBox2.SelectedIndex);
            turbina.wysTurbiny = symulator.tabDaneTurbiny[comboBox2.SelectedIndex, 0];

            MessageBox.Show(turbina.mocTurbinyModel1(3.7, symulator.tabDaneTurbiny).ToString());
            MessageBox.Show(turbina.mocTurbinyModel2(3.7, symulator.tabKrzywaMocy).ToString());
            MessageBox.Show(turbina.mocTurbinyModel3(3.7, symulator.tabKrzywaMocy).ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            turbina.rysujKrzywaModel1(chart1, symulator.tabDaneTurbiny);
            turbina.rysujKrzywaModel2(chart1, symulator.tabKrzywaMocy);
            turbina.rysujKrzywaModel3(chart1, symulator.tabKrzywaMocy);




        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            turbina.gestoscMocy(symulator.tabHistogramWiatr, symulator.tabKrzywaMocy);
            turbina.rysujGestoscMocy(chart1);
            MessageBox.Show(turbina.energiaGenerowana(symulator.tabWiatr, symulator.tabKrzywaMocy, symulator.krokWiatr).ToString());
            
        }
    }
}
