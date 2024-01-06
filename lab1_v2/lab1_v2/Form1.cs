using System;
using System.Linq;
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
            int i = comboBox1.SelectedIndex; 
            if (comboBox1.SelectedIndex == 1) liczbadni = 28;
            if (i == 3 || i == 5 || i == 8 || i == 10) liczbadni = 30;
            if (i == 0 || i == 2 || i == 4 || i == 6 || i == 7 || i == 9 || i == 11) liczbadni = 31;
            if (comboBox1.SelectedIndex == 12) liczbadni = 365;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            symulator.rysujWeibull(chart1, (double)NUD_wspK.Value, (double)NUD_wspC.Value);
        }
        private void optWeibullClick(object sender, EventArgs e)
        {
            symulator.optymalnyWeibull(symulator.tabHistogramWiatr);
        }
        private void porownanieMocyClick(object sender, EventArgs e)
        {
            symulator.rysujPorownanie(chart1); 
        }

        private void NUD_wspK_ValueChanged(object sender, EventArgs e)
        {
            symulator.rysujWeibull(chart1, (double)NUD_wspK.Value, (double)NUD_wspC.Value);
        }

        private void NUD_wspC_ValueChanged(object sender, EventArgs e)
        {
            symulator.rysujWeibull(chart1, (double)NUD_wspK.Value, (double)NUD_wspC.Value);
        }

        private void generuj3Modele(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Nie ustawiono turbiny");
                return;
            }
            turbina = new Turbina(comboBox2.Items[comboBox2.SelectedIndex].ToString(), comboBox2.SelectedIndex);
            turbina.wysTurbiny = symulator.tabDaneTurbiny[comboBox2.SelectedIndex, 0];

            MessageBox.Show(turbina.mocTurbinyInterpolacja(3.7, symulator.tabDaneTurbiny).ToString());
            MessageBox.Show(turbina.mocTurbinySrednia(3.7, symulator.tabKrzywaMocy).ToString());
            MessageBox.Show(turbina.mocTurbinyProporcja(3.7, symulator.tabKrzywaMocy).ToString());
        }

        private void rysuj3Modele(object sender, EventArgs e)
        {
            if (turbina == null)
            {
                MessageBox.Show("Nie ustawiono turbiny / nie wygenerowano modeli");
                return;
            }
            turbina.rysujKrzywaInterpolacja(chart1, symulator.tabDaneTurbiny);
            turbina.rysujKrzywaSrednia(chart1, symulator.tabKrzywaMocy);
            turbina.rysujKrzywaProporcja(chart1, symulator.tabKrzywaMocy);
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(turbina == null)
            {
                MessageBox.Show("nie ustawiono turbiny / nie wygenerowano modeli");
                return;
            }
            if(Enumerable.Aggregate(symulator.tabHistogramWiatr,(acc, x)=> acc+x) == 0)
            {
                MessageBox.Show("Histogram wiatru jest pusty!");
            }
            turbina.gestoscMocy(symulator.tabHistogramWiatr, symulator.tabKrzywaMocy);
            turbina.rysujGestoscMocy(chart1);
        }
    }
}
