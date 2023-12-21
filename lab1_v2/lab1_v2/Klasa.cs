using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; //na potrzeby funkcji OpenFileDialog
using System.IO; //na potrzeby funkcji wbudowanej StreamReader
using System.Windows.Forms.DataVisualization.Charting; //na potrzeby rysowania Chart 

namespace lab1_v2
{
    public class Klasa //klasa musi byc publiczna bo korzytsamy z niej w drugim formularzu
    {
        public double[,] tabDaneTurbiny = new double[2, 6];//paramtry charakterystyczne turbiny
        //wyskośc,moc znamionowa, vcutin,vPzn,vcutout
        public double[,] tabKrzywaMocy = new double[2, 30];//krzywa mocy z notry katalogowej

        public double[] tabWiatr = new double[650000];//tablica predkosci wiatru - do niej bedziemy zapisywac wartosc z pliku z pomiarami
        public double krokWiatr = 0; //zmienna w której zapiszemy sredmi krok pomiaru predkosci wiatru
        int liczbaProbekDzienWiatr = 0;
        public double[] tabHistogramWiatr = new double[30];//tablica 1d na potrzbye zapisania liczby pomiarow konkretnej predkosci wiatru
        public double predSrednia = 0;

        public Klasa()
        {
            //turbina ENORCON E-44
            tabDaneTurbiny[0, 0] = 55; //wysokosc masztu
            tabDaneTurbiny[0, 1] = 910; //moc znamionowa turbiny
            tabDaneTurbiny[0, 2] = 2;//vCutIn
            tabDaneTurbiny[0, 3] = 17;//vPzn
            tabDaneTurbiny[0, 4] = 25;//vCutOut

            //turbina ENORCON E-101
            tabDaneTurbiny[1, 0] = 135; //wysokosc masztu
            tabDaneTurbiny[1, 1] = 3050; //moc znamionowa turbiny
            tabDaneTurbiny[1, 2] = 2;//vCutIn
            tabDaneTurbiny[1, 3] = 13;//vPzn
            tabDaneTurbiny[1, 4] = 25;//vCutOut

            //[0]-> ENERCON E-44 (900kW)
            tabKrzywaMocy[0, 0] = 0;
            tabKrzywaMocy[0, 1] = 0;
            tabKrzywaMocy[0, 2] = 1.4;
            tabKrzywaMocy[0, 3] = 8;
            tabKrzywaMocy[0, 4] = 24.5;
            tabKrzywaMocy[0, 5] = 53;
            tabKrzywaMocy[0, 6] = 96;
            tabKrzywaMocy[0, 7] = 156;
            tabKrzywaMocy[0, 8] = 238;
            tabKrzywaMocy[0, 9] = 340;
            tabKrzywaMocy[0, 10] = 466;
            tabKrzywaMocy[0, 11] = 600;
            tabKrzywaMocy[0, 12] = 710;
            tabKrzywaMocy[0, 13] = 790;
            tabKrzywaMocy[0, 14] = 850;
            tabKrzywaMocy[0, 15] = 880;
            tabKrzywaMocy[0, 16] = 905;
            tabKrzywaMocy[0, 17] = 910;
            tabKrzywaMocy[0, 18] = 910;
            tabKrzywaMocy[0, 19] = 910;
            tabKrzywaMocy[0, 20] = 910;
            tabKrzywaMocy[0, 21] = 910;
            tabKrzywaMocy[0, 22] = 910;
            tabKrzywaMocy[0, 23] = 910;
            tabKrzywaMocy[0, 24] = 910;
            tabKrzywaMocy[0, 25] = 910;
            tabKrzywaMocy[0, 26] = 0;
            tabKrzywaMocy[0, 27] = 0;
            tabKrzywaMocy[0, 28] = 0;
            tabKrzywaMocy[0, 29] = 0;

            //[1]-> ENERCON E-101 (3000kW)
            tabKrzywaMocy[1, 0] = 0;
            tabKrzywaMocy[1, 1] = 0;
            tabKrzywaMocy[1, 2] = 3;
            tabKrzywaMocy[1, 3] = 37;
            tabKrzywaMocy[1, 4] = 118;
            tabKrzywaMocy[1, 5] = 258;
            tabKrzywaMocy[1, 6] = 479;
            tabKrzywaMocy[1, 7] = 790;
            tabKrzywaMocy[1, 8] = 1200;
            tabKrzywaMocy[1, 9] = 1710;
            tabKrzywaMocy[1, 10] = 2340;
            tabKrzywaMocy[1, 11] = 2876;
            tabKrzywaMocy[1, 12] = 3034;
            tabKrzywaMocy[1, 13] = 3050;
            tabKrzywaMocy[1, 14] = 3050;
            tabKrzywaMocy[1, 15] = 3050;
            tabKrzywaMocy[1, 16] = 3050;
            tabKrzywaMocy[1, 17] = 3050;
            tabKrzywaMocy[1, 18] = 3050;
            tabKrzywaMocy[1, 19] = 3050;
            tabKrzywaMocy[1, 20] = 3050;
            tabKrzywaMocy[1, 21] = 3050;
            tabKrzywaMocy[1, 22] = 3050;
            tabKrzywaMocy[1, 23] = 3050;
            tabKrzywaMocy[1, 24] = 3050;
            tabKrzywaMocy[1, 25] = 3050;
            tabKrzywaMocy[1, 26] = 0;
            tabKrzywaMocy[1, 27] = 0;
            tabKrzywaMocy[1, 28] = 0;
            tabKrzywaMocy[1, 29] = 0;

        }

        public void otworzPlikWiatr(int liczbaDni)//tworzymy funkcje ktora pozwoli na otwrcie pliku z predkosciami wiatru
        {
            double suma = 0;//zmienna pomocnicza
            string nazwaPliku = "";
            OpenFileDialog openFile = new OpenFileDialog();//funkcja wbudowana służąca do otwarcia pliku z danymi predkosci wiatru
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)//jesli plik istnieje i uda sie go otworzyc to wchodzimy w if-a
            {
                nazwaPliku = openFile.FileName; //niewazne jaka nazwa pliku  teraz bedzie sie on nazwyał "nazwaPliku"
                StreamReader plik = new StreamReader(nazwaPliku);//korzystajac z fucbnki wbudowanej czytamy znaki
                int licznik = 0;
                while (!plik.EndOfStream)//powtarzamy petle tak długo az odczytamy wszystkie znaki
                {
                    if (tabWiatr.Length <= licznik) Array.Resize(ref tabWiatr, tabWiatr.Length + 1);
                    //ziwekszamy tablice tak długo jak są jeszcze pomiary w pliku
                    tabWiatr[licznik] = Convert.ToDouble(plik.ReadLine());//odczytane znaki konwertuejmy i zapisujemy do tablicy
                    suma += tabWiatr[licznik];//wszytkie wartosci predkosci wiatru dodajemy do siebie , zeby pozniej wyznaczyc średni Vw
                    licznik++;
                }
                predSrednia = suma / licznik;//wyznacznie predkosci sredniej wiatru
                Array.Resize(ref tabWiatr, licznik); //ewentualne zmniejszenie tablicy wiatr do wilkość licznik, aby na koncu nie było 0000000
                krokWiatr = (double)(365 * 24) / licznik;//wyzbnacznire sredniego kroku predkosci wiatru
                liczbaProbekDzienWiatr = (int)(licznik / liczbaDni);//liczba probek jak została wygenerowana w coiagu dnia
                plik.Close();//zaykamy plik na ktorym pracowalismy

            }
            utworzHistogramWiatr();
        }
        private void utworzHistogramWiatr()
        {
            void inkrementujTablice(double wartoscPrzelicznika, int indexTablicy)
            {
                if(wartoscPrzelicznika >= indexTablicy && wartoscPrzelicznika < indexTablicy + 1)
                {
                    tabHistogramWiatr[indexTablicy] += 1;
                }
            }
            double suma = 0;
            double vPrzelicz = 0; //zmienna pomocnicza do przeliczania wartości v waitru

            for (int i = 0; i < tabWiatr.Length; i++)
            {
                vPrzelicz = predWiatruWysokosc(tabWiatr[i], 135, 0.2); //wywowłanie funkcji uwzgledniajacej pionowy rpofil waitru
                for (int j = 0; j< 30; j++)
                {
                    inkrementujTablice(vPrzelicz, j);
                }
            }

            for (int i = 0; i < tabHistogramWiatr.Length; i++)
            {
                tabHistogramWiatr[i] /= tabWiatr.Length; //chcac uzyskac gestosc prawodpowobieniastrqwa wystapienia poszczegolnych predkosci wiatru trzeba podzielic liczbe wystpien przez wyszystkie pomiary 
            }
        }

        private double predWiatruWysokosc(double predWiatr, double wysokosc, double alfa)
        {
            return predWiatr * Math.Pow((wysokosc / 10), alfa);//wzór wynikajacy z pionowego profilu wiatru
        }

        public void rysujHistogram(Chart wykres, double[] tab, int nrSerii)
        {
            wykres.Series[nrSerii].Points.Clear();//przed rysowaniem czyscimy wykres
            for (int i = 0; i < tab.Length; i++)
            {
                wykres.Series[nrSerii].Points.AddXY(i, tab[i]);//w petli nanosimy poszczegolne prawdopodobienastwa wystapienia  poszczegolnych predkosci
            }
        }

        private double Weibull(double k, double c, double predW)
        {
            return (k / c) * Math.Pow((predW / c), k - 1) * Math.Exp(-Math.Pow((predW / c), k));
        }

        public void rysujWeibull(Chart wykres, double wspK, double wspC)
        {
            wykres.ChartAreas[0].AxisX.Minimum = 0;
            wykres.ChartAreas[0].AxisX.Maximum = 30;

            wykres.Series[1].Points.Clear();

            for (int i = 0; i < 31; i++)
            {
                wykres.Series[1].Points.AddXY(i, Weibull(wspK, wspC, i));
            }
        }
    }

    public class Turbina
    {
        public string nazwaTurbiny = "";
        public double wysTurbiny = 0;
        public double mocZn = 0;
        public double vCutIn = 0;
        public double vPZn = 0;
        public double vCutOut;

        private int numerTurbiny;

        public double[] tabGestoscMocy = new double[30];

        public Turbina(string nazwa, int nrTurbiny)
        {
            numerTurbiny = nrTurbiny;
            nazwaTurbiny = nazwa;
        }

        public double mocTurbinyModel1(double vw, double[,] tab) //interpolacja liniowa gdy rosnie moc i trzeba okreslać moc na bieżąco lub spada
        {
            mocZn = tab[numerTurbiny, 1];
            vCutIn = tab[numerTurbiny, 2];
            vPZn = tab[numerTurbiny, 3];
            vCutOut = tab[numerTurbiny, 4];
            double moc = 0;

            if (vw <= vCutIn) moc = 0;
            if ((vw >= vPZn) && (vw <= vCutOut)) moc = mocZn;
            if (vw > vCutOut) moc = 0;

            if ((vw > vCutIn) && (vw < vPZn))
            {
                double wspA = mocZn / (vPZn - vCutIn);
                moc = -wspA * vCutIn + wspA * vw;
            }

            return moc;
        }

        public double mocTurbinyModel2(double vw, double[,] tabkrzywa) //metoda średniej
        {
            double moc = 0;

            int vwDolna = (int)Math.Floor(vw);
            int vwGorna = (int)Math.Ceiling(vw);
            moc = (tabkrzywa[numerTurbiny, vwDolna] + tabkrzywa[numerTurbiny, vwGorna]) / 2;
            return moc;
        }

        public double mocTurbinyModel3(double vw, double[,] tabKrzywa) // metdoa proporcji
        {
            double moc = 0;
            int vwDolna = (int)Math.Floor(vw);

            if (vwDolna > 0) moc = (vw / vwDolna) * tabKrzywa[numerTurbiny, vwDolna];
            else moc = 0;
            return moc;
        }


        public void rysujKrzywaModel1(Chart wykres, double[,] tab)
        {
            wykres.ChartAreas[1].AxisX.Minimum = 0;
            wykres.ChartAreas[1].AxisX.Maximum = 30;

            wykres.Series[2].Points.Clear();
            for (int i = 0; i < 30; i++)
            {
                wykres.Series[2].Points.AddXY(i, mocTurbinyModel1(i, tab));
            }
        }


        public void rysujKrzywaModel2(Chart wykres, double[,] tab)
        {
            wykres.ChartAreas[1].AxisX.Minimum = 0;
            wykres.ChartAreas[1].AxisX.Maximum = 30;

            wykres.Series[3].Points.Clear();
            for (int i = 0; i < 30; i++)
            {
                wykres.Series[3].Points.AddXY(i, mocTurbinyModel2(i, tab));
            }
        }

        public void rysujKrzywaModel3(Chart wykres, double[,] tab)
        {
            wykres.ChartAreas[1].AxisX.Minimum = 0;
            wykres.ChartAreas[1].AxisX.Maximum = 30;

            wykres.Series[4].Points.Clear();
            for (int i = 0; i < 30; i++)
            {
                wykres.Series[4].Points.AddXY(i, mocTurbinyModel3(i, tab));
            }
        }


        public void gestoscMocy(double[] tabHis, double[,] tabKrzywa)
        {
            double predkosc = 0;
            for (int i = 0; i < tabGestoscMocy.Length - 1; i++)
            {
                tabGestoscMocy[i] = tabHis[i] * mocTurbinyModel2(predkosc, tabKrzywa);
                predkosc += 1;
            }
        }

        public void rysujGestoscMocy(Chart wykres)
        {
            wykres.ChartAreas[2].AxisX.Minimum = 0;
            wykres.ChartAreas[2].AxisX.Maximum = 30;
            wykres.Series[5].Points.Clear();
            for (int i = 0; i < 30; i++)
            {
                wykres.Series[5].Points.AddXY(i, tabGestoscMocy[i]);
            }
        }


        public double energiaGenerowana(double[] tabWiatr, double[,] tabKrzywa, double krokczas)
        {
        double energiaCalkowita = 0;
        for (int i=0; i<tabWiatr.Length; i++)
            {
                energiaCalkowita += mocTurbinyModel2(tabWiatr[i], tabKrzywa) * krokczas;
            }

        return energiaCalkowita;
        }
    }
}
