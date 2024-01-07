using System;

using System.Windows.Forms; //na potrzeby funkcji OpenFileDialog
using System.IO; //na potrzeby funkcji wbudowanej StreamReader
using System.Windows.Forms.DataVisualization.Charting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq; //na potrzeby rysowania Chart 

namespace lab1_v2
{
    public class Klasa //klasa musi byc publiczna bo korzytsamy z niej w drugim formularzu
    {
        public double[,] tabDaneTurbiny = new double[3, 6];//paramtry charakterystyczne turbiny
        //wyskośc,moc znamionowa, vcutin,vPzn,vcutout
        public double[,] tabKrzywaMocy = new double[3, 30];//krzywa mocy z notry katalogowej

        public double[] tabWiatr = new double[650000];//tablica predkosci wiatru - do niej bedziemy zapisywac wartosc z pliku z pomiarami
        public double krokWiatr = 0; //zmienna w której zapiszemy sredmi krok pomiaru predkosci wiatru
        int liczbaProbekDzienWiatr = 0;
        public double[] tabHistogramWiatr = new double[30];//tablica 1d na potrzbye zapisania liczby pomiarow konkretnej predkosci wiatru
        public double predSrednia = 0;
        public Klasa()
        {
            JObject o1 = JObject.Parse(File.ReadAllText("../../e-44.json"));
            JObject o2 = JObject.Parse(File.ReadAllText("../../e-101.json"));
            JObject o3 = JObject.Parse(File.ReadAllText("../../e-33.json"));
            for (int i = 0; i < 5; i++) // zaciągnięcie danych z zewnętrznego pliku
            {
                tabDaneTurbiny[0, i] = (double)o1["daneTurbiny"][i];
                tabDaneTurbiny[1, i] = (double)o2["daneTurbiny"][i];
                tabDaneTurbiny[2, i] = (double)o3["daneTurbiny"][i];
            }
            for (int i = 0; i < 30; i++)
            {
                tabKrzywaMocy[0, i] = (double)o1["krzywaMocy"][i];
                tabKrzywaMocy[1, i] = (double)o2["krzywaMocy"][i];
                tabKrzywaMocy[2, i] = (double)o3["krzywaMocy"][i];
            }
        }

        public void otworzPlikWiatr(int liczbaDni)//tworzymy funkcje ktora pozwoli na otwrcie pliku z predkosciami wiatru
        {
            string nazwaPliku = "";

            OpenFileDialog openFile = new OpenFileDialog();//funkcja wbudowana służąca do otwarcia pliku z danymi predkosci wiatru
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)//jesli plik istnieje i uda sie go otworzyc to wchodzimy w if-a
            {
                nazwaPliku = openFile.FileName; //niewazne jaka nazwa pliku  teraz bedzie sie on nazwyał "nazwaPliku" - tak naprawdę to nie - w zmiennej nazwaPliku zapisujemy faktyczną nazwę pliku
                JObject o1 = JObject.Parse(File.ReadAllText(nazwaPliku));
                JArray srednie = (JArray)o1["WindSpeed"]["averages"];
                Array.Resize(ref tabWiatr, srednie.Count); //ewentualne zmniejszenie tablicy wiatr do wilkość licznik, aby na koncu nie było 0000000

                for (int i = 0; i < srednie.Count; i++)
                {
                    tabWiatr[i] = (double)srednie[i][1];
                }
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
                tabHistogramWiatr[i] /= tabHistogramWiatr.Length; //chcac uzyskac gestosc prawdopodobieństwa wystapienia poszczegolnych predkosci wiatru trzeba podzielic liczbe wystpien przez wyszystkie pomiary 
            }
            MessageBox.Show(String.Join(", ", tabHistogramWiatr), "utworzono histogram wiatr");
        }

        private double predWiatruWysokosc(double predWiatr, double wysokosc, double alfa)
        {
            return predWiatr * Math.Pow(wysokosc / 10, alfa); //wzór wynikajacy z pionowego profilu wiatru
        }

        public void rysujHistogram(Chart wykres, double[] tab, int nrSerii)
        {
            wykres.Series[nrSerii].Points.Clear(); //przed rysowaniem czyscimy wykres
            for (int i = 0; i < tab.Length; i++)
            {
                wykres.Series[nrSerii].Points.AddXY(i, tab[i]);//w petli nanosimy poszczegolne prawdopodobienastwa wystapienia  poszczegolnych predkosci
            }
        }
        public void rysujPorownanie(Chart wykres)
        {
            List<double> wyniki = new List<double>();
            for (int i = 0; i < tabDaneTurbiny.GetLength(0); i++)
            {
                Turbina t = new Turbina(" ", i);
                double sumaMocy = t.SumaEnergii(tabHistogramWiatr, tabKrzywaMocy);
                wyniki.Add(sumaMocy);
                int nrSerii = i + 6;
                wykres.Series[nrSerii].Points.AddXY(i, sumaMocy); //w petli nanosimy sumę mocy wygenerowanej przez każdą z turbin
            }
        }

        private double Weibull(double k, double c, double predW)
        {
            return (k / c) * Math.Pow((predW / c), k - 1) * Math.Exp(-Math.Pow((predW / c), k));
        }
        public void optymalnyWeibull(double[] wiatr)
        {
            List<(decimal wynik, double c, double k)> wyniki = new List<(decimal, double, double)>(wiatr.Length); // dwie najbardziej podobne wykresy mają najmniejszą powierzchnię między sobą   
            (decimal wynik, double c, double k) najlepszyWynik = (999999999, 0, 0);                               // w wynikach zapisujemy powierzchnię między wykresami oraz wartości c i k
            for (double c = 1.0; c < 10.0; c += .1)
            {

                for (double k = 1.0; k < 10.0; k += .1)
                {
                    double poprzedniaOdleglosc = 0;
                    decimal powierzchnia = 0;
                    for (int i = 0; i < wiatr.Length; i++)
                    {
                        double Wb = Weibull(k, c, i);
                        double wysokosc = tabHistogramWiatr[i];
                        double odleglosc = Wb - wysokosc;
                        bool skrzyzowane = ((poprzedniaOdleglosc < 0 && odleglosc > 0)  // Wykresy krzyżują się jeśli nastąpiła zmiana dodatniości różnicy między nimi
                                        || (poprzedniaOdleglosc > 0 && odleglosc < 0)); // Jeżeli wykresy nie są skrzyżowane powierzchnię między nimi tworzy trapez. 
                                                                                        // W innym wypadku powierzchnię tworzą 2 trójkąty leżące pomiędzy jego przekątnymi a podstawami.
                        poprzedniaOdleglosc = odleglosc;

                        double odlBW = Math.Abs(odleglosc), poprzedniaOdlBW = Math.Abs(poprzedniaOdleglosc);
                        // wartości bezwzględne odległości
                        double hTrapez = 1; // stały krok między wartościami
                        if (!skrzyzowane)
                        {
                            powierzchnia += (decimal)Math.Round((poprzedniaOdlBW + odlBW / 2 * hTrapez), 10, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            double hT1, hT2; // wysokości trójkątów
                            hT1 = hTrapez * odlBW / (odlBW + poprzedniaOdlBW); // podobieństwo trójkątów 
                                                                               // pomiędzy podstawami a przekątnymi trapezu
                                                                               // (kkk)

                            // hTrapez = hT1 + hT2

                            //    hT1                  odlBW              
                            //------------ = ---------------------------- 
                            //  hTrapez       poprzedniaOdlBW + odlBW     

                            //                            odlBW
                            // hT1 = hTrapez * ---------------------------
                            //                  poprzedniaOdlBW + odlBW

                            hT2 = hTrapez  * poprzedniaOdlBW / (odlBW + poprzedniaOdlBW);
                            powierzchnia += (decimal)Math.Round((odlBW * hT1 / 2), 10, MidpointRounding.AwayFromZero);
                            powierzchnia += (decimal)Math.Round((poprzedniaOdlBW * hT2 / 2), 10, MidpointRounding.AwayFromZero);
                        }
                    }
                    wyniki.Add((powierzchnia, c, k));
                }
            }

            foreach (var w in wyniki)
            {
                if (w.wynik < najlepszyWynik.wynik)
                    najlepszyWynik = w;
            }

            MessageBox.Show("Proponowane wartości zmiennych: k=" + najlepszyWynik.k.ToString() + " c=" + najlepszyWynik.c.ToString());
        }

        public void rysujWeibull(Chart wykres, double wspK, double wspC)
        {
            wykres.ChartAreas[0].AxisX.Minimum = 0;
            wykres.ChartAreas[0].AxisX.Maximum = 30;

            wykres.Series[1].Points.Clear();

            for (int i = 0; i < tabHistogramWiatr.Length; i++)
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
                // moc = -wspA * vCutIn + wspA * predkoscWiatru;
                // moc = wspA * (-1 * vCutIn) + wspA * predkoscWiatru;
                moc = wspA * (predkoscWiatru - vCutIn);
            }

            return moc;
        }

        public double mocTurbinySrednia(double vw, double[,] tabkrzywa) //metoda średniej
        {
            double moc;

            int vwDolna = (int)Math.Floor(vw);
            int vwGorna = (int)Math.Ceiling(vw);
            double minMoc = tabkrzywa[numerTurbiny, vwDolna];
            double maxMoc = tabkrzywa[numerTurbiny, vwGorna];
            moc = (minMoc + maxMoc) / 2;
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
            for (int i = 0; i < 29; i++)
            {
                double wartosc = mocTurbinySrednia(i + .5, tab);
                wykres.Series[3].Points.AddXY(i, wartosc);
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



        public void gestoscMocy(double[] tabHis, double[,] tabKrzywa)
        {
            for (int i = 0; i < tabGestoscMocy.Length; i++)
            {
                tabGestoscMocy[i] = tabHis[i] * mocTurbinySrednia(i, tabKrzywa);
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

        internal double SumaEnergii(double[] tabHis, double[,] tabKrzywa)
        {
            double suma = 0;
            for (int i = 0; i < tabHis.Length; i++)
            {
                suma += tabHis[i] * mocTurbinySrednia(i, tabKrzywa);
            }
            return suma;
        }
    }
}
