using System;

using System.Windows.Forms; //na potrzeby funkcji OpenFileDialog
using System.IO; //na potrzeby funkcji wbudowanej StreamReader
using System.Windows.Forms.DataVisualization.Charting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json; //na potrzeby rysowania Chart 

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
            JObject o1 = JObject.Parse(File.ReadAllText("../../e-44.json"));
            JObject o2 = JObject.Parse(File.ReadAllText("../../e-101.json"));
            for (int i = 0; i<5; i++) // zaciągnięcie danych z zewnętrznego pliku
            {
                tabDaneTurbiny[0, i] = (double)o1["daneTurbiny"][i];
                tabDaneTurbiny[1, i] = (double)o2["daneTurbiny"][i];
            }
            for (int i = 0; i < 30; i++)
            {
                tabKrzywaMocy[0, i] = (double)o1["krzywaMocy"][i];
                tabKrzywaMocy[1, i] = (double)o2["krzywaMocy"][i];
            }
        }

        public void otworzPlikWiatr(int liczbaDni)//tworzymy funkcje ktora pozwoli na otwrcie pliku z predkosciami wiatru
        {
            double suma = 0;//zmienna pomocnicza
            string nazwaPliku = "";
            OpenFileDialog openFile = new OpenFileDialog();//funkcja wbudowana służąca do otwarcia pliku z danymi predkosci wiatru
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)//jesli plik istnieje i uda sie go otworzyc to wchodzimy w if-a
            {
                nazwaPliku = openFile.FileName; //niewazne jaka nazwa pliku  teraz bedzie sie on nazwyał "nazwaPliku"
                StreamReader plik = new StreamReader(nazwaPliku);//korzystajac z funkcji wbudowanej czytamy znaki
                int licznik = 0;
                while (!plik.EndOfStream)//powtarzamy petle tak długo az odczytamy wszystkie znaki
                {
                    if (tabWiatr.Length <= licznik) Array.Resize(ref tabWiatr, tabWiatr.Length + 1);
                    //ziwekszamy tablice tak długo jak są jeszcze pomiary w pliku
                    tabWiatr[licznik] = Convert.ToDouble(plik.ReadLine());//odczytane znaki konwertuejmy i zapisujemy do tablicy
                    suma += tabWiatr[licznik];//wszytkie wartosci predkosci wiatru dodajemy do siebie , zeby pozniej wyznaczyc średnią prędkość wiatru
                    licznik++;
                }
                predSrednia = suma / licznik;//wyznacznie predkosci sredniej wiatru
                Array.Resize(ref tabWiatr, licznik); //ewentualne zmniejszenie tablicy wiatr do wilkość licznik, aby na koncu nie było 0000000
                krokWiatr = (double)(365 * 24) / licznik;//wyzbnacznire sredniego kroku predkosci wiatru
                liczbaProbekDzienWiatr = (int)(licznik / liczbaDni);//liczba probek jak została wygenerowana w ciagu dnia
                plik.Close();//zaykamy plik na ktorym pracowalismy
            }
            utworzHistogramWiatr();
        }
        private void utworzHistogramWiatr()
        {
            decimal vPrzelicz = 0; //zmienna pomocnicza do przeliczania wartości v waitru

            for (int i = 0; i < tabHistogramWiatr.Length; i++)
            {
                vPrzelicz = (decimal)predWiatruWysokosc(tabWiatr[i], 135, 0.2); //wywowłanie funkcji uwzgledniajacej pionowy profil waitru
                int index = (int)Math.Floor(vPrzelicz);
                tabHistogramWiatr[index] += 1;
            }

            for (int i = 0; i < tabHistogramWiatr.Length; i++)
            {
                tabHistogramWiatr[i] /= tabWiatr.Length; //chcac uzyskac gestosc prawodpowobieniastrqwa wystapienia poszczegolnych predkosci wiatru trzeba podzielic liczbe wystpien przez wyszystkie pomiary 
            }
            MessageBox.Show(String.Join(", ", tabHistogramWiatr, "utworzono histogram wiatr"));

        }

        private double predWiatruWysokosc(double predWiatr, double wysokosc, double alfa)
        {
            return predWiatr * Math.Pow((wysokosc / 10), alfa); //wzór wynikajacy z pionowego profilu wiatru
        }

        public void rysujHistogram(Chart wykres, double[] tab, int nrSerii)
        {
            wykres.Series[nrSerii].Points.Clear();//przed rysowaniem czyscimy wykres
            for (int i = 0; i < tab.Length; i++)
            {
                wykres.Series[nrSerii].Points.AddXY(i, tab[i] * 3000 );//w petli nanosimy poszczegolne prawdopodobienastwa wystapienia  poszczegolnych predkosci
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
        public double[] tabGestoscMocy = new double[30];
        private int numerTurbiny;


        public Turbina(string nazwa, int nrTurbiny)
        {
            numerTurbiny = nrTurbiny;
            nazwaTurbiny = nazwa;
        }

        public double mocTurbinyInterpolacja(double predkoscWiatru, double[,] tab) //interpolacja liniowa gdy rosnie moc i trzeba okreslać moc na bieżąco lub spada
        {
            mocZn = tab[numerTurbiny, 1];
            vCutIn = tab[numerTurbiny, 2];
            vPZn = tab[numerTurbiny, 3];
            vCutOut = tab[numerTurbiny, 4];
            double moc = 0;

            if (predkoscWiatru <= vCutIn) moc = 0;
            if ((predkoscWiatru >= vPZn) && (predkoscWiatru <= vCutOut)) moc = mocZn;
            if (predkoscWiatru > vCutOut) moc = 0;

            if ((predkoscWiatru > vCutIn) && (predkoscWiatru < vPZn))
            {
                double wspA = mocZn / (vPZn - vCutIn);
                moc = -wspA * vCutIn + wspA * predkoscWiatru;
            }

            return moc;
        }

        public double mocTurbinySrednia(double vw, double[,] tabkrzywa) //metoda średniej
        {
            double moc = 0;

            int vwDolna = (int)Math.Floor(vw);
            int vwGorna = (int)Math.Ceiling(vw);
            moc = (tabkrzywa[numerTurbiny, vwDolna] + tabkrzywa[numerTurbiny, vwGorna]) / 2;
            return moc;
        }

        public double mocTurbinyProporcja(double vw, double[,] tabKrzywa) // metdoa proporcji
        {
            double moc = 0;
            int vwDolna = (int)Math.Floor(vw);

            if (vwDolna > 0) moc = (vw / vwDolna) * tabKrzywa[numerTurbiny, vwDolna];
            else moc = 0;
            return moc;
        }


        public void rysujKrzywaInterpolacja(Chart wykres, double[,] tab)
        {
            wykres.ChartAreas[1].AxisX.Minimum = 0;
            wykres.ChartAreas[1].AxisX.Maximum = 30;

            wykres.Series[2].Points.Clear();
            for (int i = 0; i < 30; i++)
            {
                wykres.Series[2].Points.AddXY(i, mocTurbinyInterpolacja(i, tab));
            }
        }


        public void rysujKrzywaSrednia(Chart wykres, double[,] tab)
        {
            wykres.ChartAreas[1].AxisX.Minimum = 0;
            wykres.ChartAreas[1].AxisX.Maximum = 30;

            wykres.Series[3].Points.Clear();
            for (int i = 0; i < 30; i++)
            {
                wykres.Series[3].Points.AddXY(i, mocTurbinySrednia(i, tab));
            }
        }

        public void rysujKrzywaProporcja(Chart wykres, double[,] tab)
        {
            wykres.ChartAreas[1].AxisX.Minimum = 0;
            wykres.ChartAreas[1].AxisX.Maximum = 30;

            wykres.Series[4].Points.Clear();
            for (int i = 0; i < 30; i++)
            {
                wykres.Series[4].Points.AddXY(i, mocTurbinyProporcja(i, tab));
            }
        }







        public double energiaGenerowana(double[] tabWiatr, double[,] tabKrzywa, double krokczas)
        {
        double energiaCalkowita = 0;
            MessageBox.Show("krokczas ", krokczas.ToString());

            for (int i=0; i<tabWiatr.Length; i++)
            {
                energiaCalkowita += mocTurbinySrednia(tabWiatr[i], tabKrzywa) * krokczas;
            }

        return energiaCalkowita;
        }
    public void gestoscMocy(double[] tabHis, double[,] tabKrzywa)
    {
        double predkosc = 0;
        for (int i = 0; i < tabGestoscMocy.Length; i++)
        {
            tabGestoscMocy[i] = tabHis[i] * mocTurbinySrednia(predkosc, tabKrzywa);
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
}
}
