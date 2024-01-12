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
        for (int i = 0; i < tabGestoscMocy.Length; i++)
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
        MessageBox.Show("krokczas ", krokczas.ToString());

        for (int i = 0; i < tabWiatr.Length; i++)
        {
            energiaCalkowita += mocTurbinyModel2(tabWiatr[i], tabKrzywa) * krokczas;
        }

        return energiaCalkowita;
    }
}
}
