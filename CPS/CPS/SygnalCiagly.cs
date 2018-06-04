using System;
using System.Numerics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using System.Diagnostics;

namespace CPS
{
    [Serializable]
    class SygnalCiagly
    {
        public double _A { get; set; }
        public double _t1 { get; set; }
        public double _d { get; set; }
        public double _T { get; set; }
        public double _kw { get; set; }
        public double _f { get; set; }
        public double _ns { get; set; }
        public int _his { get; set; }
        [NonSerialized]
        public IList<OxyPlot.DataPoint> Points = new List<OxyPlot.DataPoint>();
        public ICollection<Point> TimeAndAmplitude = new Collection<Point>();
        [NonSerialized]
        public IList<OxyPlot.DataPoint> PointsDys = new List<OxyPlot.DataPoint>();
        public ICollection<Point> TimeAndAmplitudeDys = new Collection<Point>();
        [NonSerialized]
        public IList<OxyPlot.DataPoint> PointsOdt = new List<OxyPlot.DataPoint>();
        [NonSerialized]
        public IList<OxyPlot.DataPoint> PointsDysKwan = new List<OxyPlot.DataPoint>();
        [NonSerialized]
        public IList<OxyPlot.DataPoint> Filtr = new List<OxyPlot.DataPoint>();
        [NonSerialized]
        public IList<OxyPlot.DataPoint> SygFiltrowany = new List<OxyPlot.DataPoint>();
        [NonSerialized]
        public IList<OxyPlot.DataPoint> Opozniony = new List<OxyPlot.DataPoint>();
        [NonSerialized]
        public IList<OxyPlot.DataPoint> Radar = new List<OxyPlot.DataPoint>();
        [NonSerialized]
        public IList<OxyPlot.DataPoint> Re = new List<OxyPlot.DataPoint>();
        [NonSerialized]
        public IList<OxyPlot.DataPoint> Im = new List<OxyPlot.DataPoint>();
        public double _Srednia { get; set; }
        public double _SredniaBez { get; set; }
        public double _Skuteczna { get; set; }
        public double _Wariancja { get; set; }
        public double _MocSrednia { get; set; }
        public double _MSE { get; set; }
        public double _SNR { get; set; }
        public double _PSNR { get; set; }
        public double _MD { get; set; }
        public double _SredniaDys { get; set; }
        public double _SredniaBezDys { get; set; }
        public double _SkutecznaDys { get; set; }
        public double _WariancjaDys { get; set; }
        public double _MocSredniaDys { get; set; }
        public double _MSEDys { get; set; }
        public double _SNRDys { get; set; }
        public double _PSNRDys { get; set; }
        public double _MDDys { get; set; }
        public double _M { get; set; }
        public double _K { get; set; }
        public double _N { get; set; }
        public double _Opoznienie { get; set; }
        public double _Odleglosc { get; set; }
        public double _CzasOpoznienia { get; set; }

        public Stopwatch zegarek;


        public SygnalCiagly(double A, double t1, double d, double T, double kw, double f, double ns, int his, int N, int K, int M, int op)
        {
            this._A = A;
            this._t1 = t1;
            this._d = d;
            this._T = T;
            this._kw = kw;
            this._f = f;
            this._ns = ns;
            this._his = his;
            this._M = M;
            this._K = K;
            this._N = N;
            this._Opoznienie = op;
            zegarek = new Stopwatch();

            // co by okres sie dziwnie nie konczyl
            if (_T != 0)
            {
                int calosci = (int)(_d / _T);
                _d = _T * calosci;
            }

            _Odleglosc = 0;
            _CzasOpoznienia = 0;

        }

        public void SzumJednostajny()
        {
            Random rand = new Random();
            for (double i = _t1; i <= _t1 + _d; i += 1 / (_f * 10))
            {
                Points.Add(new OxyPlot.DataPoint(Math.Round(i, 2), Math.Round(Math.Sqrt(12.0 * _A) * (((rand.Next() % 101) - 50.0) / 100.0) + 0, 2)));
            }
        }

        public void SzumGaussowski()
        {
            Random rand = new Random();
            for (double i = _t1; i <= _t1 + _d; i += 1 / (_f * 10))
            {
                int n = 10;
                double x = 0.0;
                for (int j = 0; j < n; j++)
                {
                    x += Math.Sqrt(12.0 * 1) * (((rand.Next() % 101) - 50.0) / 100.0) + 0;
                }
                Points.Add(new OxyPlot.DataPoint(Math.Round(i, 2), Math.Round(x * Math.Sqrt(_A / (double)n) + 0, 2)));
            }
        }

        public void SygnalSinusoidalny()
        {
            for (double i = _t1; i <= _t1 + _d; i += 1 / (_f * 10))
            {
                Points.Add(new OxyPlot.DataPoint(Math.Round(i, 2), Math.Round(_A * Math.Sin(((2 * Math.PI) / _T) * (i - _t1)), 2)));
            }
            if (_Opoznienie > 0)
            {
                double op = _Opoznienie / (_f * 10);
                for (double i = _t1 + op; i <= _t1 + _d + op; i += 1 / (_f * 10))
                {
                    Opozniony.Add(new OxyPlot.DataPoint(Math.Round(i - op, 2), Math.Round(_A * Math.Sin(((2 * Math.PI) / _T) * (i - _t1)), 2)));
                }
            }
        }

        public void HardCodedExample()
        {
            double i = 0;
            double n = 0;
            do
            {
                Points.Add(new OxyPlot.DataPoint(n, 2.0 * Math.Sin(Math.PI * i + Math.PI / 2) + 5.0 * Math.Sin(((2 * Math.PI) / 0.5) * i + Math.PI / 2.0)));
                i = i + (1.0 / 16.0);
                n++;
            } while (n < Math.Pow(2, _d));
            //while (i < 5);
        }

        public void SygnalSinusoidalnyWyprostowanyJednopolowkowo()
        {
            for (double i = _t1; i <= _t1 + _d; i += 1 / (_f * 10))
            {
                Points.Add(new OxyPlot.DataPoint(Math.Round(i, 2), Math.Round(0.5 * _A * (Math.Sin(((2 * Math.PI) / _T) * (i - _t1)) + Math.Abs(Math.Sin(((2 * Math.PI) / _T) * (i - _t1)))), 2)));
            }

            if (_Opoznienie > 0)
            {
                double op = _Opoznienie / (_f * 10);
                for (double i = _t1 + op; i <= _t1 + _d + op; i += 1 / (_f * 10))
                {
                    Opozniony.Add(new OxyPlot.DataPoint(Math.Round(i - op, 2), Math.Round(0.5 * _A * (Math.Sin(((2 * Math.PI) / _T) * (i - _t1)) + Math.Abs(Math.Sin(((2 * Math.PI) / _T) * (i - _t1)))), 2)));
                }
            }
        }

        public void SygnalSinusoidalnyWyprostowanyDwupolowkowo()
        {
            for (double i = _t1; i <= _t1 + _d; i += 1 / (_f * 10))
            {
                Points.Add(new OxyPlot.DataPoint(Math.Round(i, 2), Math.Round(_A * Math.Abs(Math.Sin(((2 * Math.PI) / _T) * (i - _t1))), 2)));
            }

            if (_Opoznienie > 0)
            {
                double op = _Opoznienie / (_f * 10);
                for (double i = _t1 + op; i <= _t1 + _d + op; i += 1 / (_f * 10))
                {
                    Opozniony.Add(new OxyPlot.DataPoint(Math.Round(i - op, 2), Math.Round(_A * Math.Abs(Math.Sin(((2 * Math.PI) / _T) * (i - _t1))), 2)));
                }
            }
        }

        public void Skok()
        {
            for (double i = _t1; i <= _t1 + _d; i += 1 / (_f * 10))
            {
                if (i < _ns)
                    Points.Add(new OxyPlot.DataPoint(Math.Round(i, 2), Math.Round(0.0, 2)));
                if (i == _ns)
                    Points.Add(new OxyPlot.DataPoint(Math.Round(i, 2), Math.Round(0.5 * _A, 2)));
                if (i > _ns)
                    Points.Add(new OxyPlot.DataPoint(Math.Round(i, 2), Math.Round(_A, 2)));
            }
        }

        public void SygnalProstokatny()
        {
            {
                int ktory = 1;
                double wypelnienie = _T * _kw;
                for (double i = _t1; i <= _t1 + _d; i += 1 / (_f * 10))
                {
                    double koniecOkresu = _t1 + (ktory * _T);
                    if (i < koniecOkresu - (_T - wypelnienie))
                    {
                        Points.Add(new OxyPlot.DataPoint(Math.Round(i, 2), Math.Round(_A, 2)));
                    }
                    if (i >= koniecOkresu - (_T - wypelnienie))
                    {
                        Points.Add(new OxyPlot.DataPoint(Math.Round(i, 2), Math.Round(0.0, 2)));
                    }
                    if (i >= koniecOkresu)
                        ktory++;
                }
            }

            if (_Opoznienie > 0)
            {
                double op = _Opoznienie / (_f * 10);
                int ktory = 1;
                double wypelnienie = _T * _kw;
                for (double i = _t1 + op; i <= _t1 + _d + op; i += 1 / (_f * 10))
                {
                    double koniecOkresu = _t1 + (ktory * _T);
                    if (i < koniecOkresu - (_T - wypelnienie))
                    {
                        Opozniony.Add(new OxyPlot.DataPoint(Math.Round(i - op, 2), Math.Round(_A, 2)));
                    }
                    if (i >= koniecOkresu - (_T - wypelnienie))
                    {
                        Opozniony.Add(new OxyPlot.DataPoint(Math.Round(i - op, 2), Math.Round(0.0, 2)));
                    }
                    if (i >= koniecOkresu)
                        ktory++;
                }
            }

        }

        public void SygnalProstokatnySymetryczny()
        {
            {
                int ktory = 1;
                double wypelnienie = _T * _kw;
                for (double i = _t1; i <= _t1 + _d; i += 1 / (_f * 10))
                {
                    double koniecOkresu = _t1 + (ktory * _T);
                    if (i < koniecOkresu - (_T - wypelnienie))
                    {
                        Points.Add(new OxyPlot.DataPoint(Math.Round(i, 2), Math.Round(_A, 2)));
                    }
                    if (i >= koniecOkresu - (_T - wypelnienie))
                    {
                        Points.Add(new OxyPlot.DataPoint(Math.Round(i, 2), Math.Round(-_A, 2)));
                    }
                    if (i >= koniecOkresu)
                        ktory++;
                }
            }
            if (_Opoznienie > 0)
            {
                double op = _Opoznienie / (_f * 10);
                int ktory = 1;
                double wypelnienie = _T * _kw;
                for (double i = _t1 + op; i <= _t1 + _d + op; i += 1 / (_f * 10))
                {
                    double koniecOkresu = _t1 + (ktory * _T);
                    if (i < koniecOkresu - (_T - wypelnienie))
                    {
                        Opozniony.Add(new OxyPlot.DataPoint(Math.Round(i - op, 2), Math.Round(_A, 2)));
                    }
                    if (i >= koniecOkresu - (_T - wypelnienie))
                    {
                        Opozniony.Add(new OxyPlot.DataPoint(Math.Round(i - op, 2), Math.Round(-_A, 2)));
                    }
                    if (i >= koniecOkresu)
                        ktory++;
                }
            }
        }

        public void SygnalTrojkatny()
        {
            {
                int ktory = 1;
                double wypelnienie = _T * _kw;
                for (double i = _t1; i <= _t1 + _d; i += 1 / (_f * 10))
                {
                    double poczatekOkresu = _t1 + ((ktory - 1) * _T);
                    double koniecOkresu = _t1 + (ktory * _T);
                    double szczyt = koniecOkresu - (_T - wypelnienie);
                    if (i < szczyt) // zbocze rosnace
                    {
                        double wartosc = (((_A) / (szczyt - poczatekOkresu)) * i) + ((-_A * poczatekOkresu) / (szczyt - poczatekOkresu));
                        Points.Add(new OxyPlot.DataPoint(Math.Round(i, 2), Math.Round(wartosc, 2)));
                    }
                    if (i >= szczyt) // zbocze malejace
                    {
                        double wartosc = (((-_A) / (koniecOkresu - szczyt)) * i) + ((_A * koniecOkresu) / (koniecOkresu - szczyt));
                        Points.Add(new OxyPlot.DataPoint(Math.Round(i, 2), Math.Round(wartosc, 2)));
                    }
                    if (i >= koniecOkresu)
                        ktory++;
                }
            }
            if (_Opoznienie > 0)
            {
                double op = _Opoznienie / (_f * 10);
                int ktory = 1;
                double wypelnienie = _T * _kw;
                for (double i = _t1 + op; i <= _t1 + _d + op; i += 1 / (_f * 10))
                {
                    double poczatekOkresu = _t1 + ((ktory - 1) * _T);
                    double koniecOkresu = _t1 + (ktory * _T);
                    double szczyt = koniecOkresu - (_T - wypelnienie);
                    if (i < szczyt) // zbocze rosnace
                    {
                        double wartosc = (((_A) / (szczyt - poczatekOkresu)) * i) + ((-_A * poczatekOkresu) / (szczyt - poczatekOkresu));
                        Opozniony.Add(new OxyPlot.DataPoint(Math.Round(i - op, 2), Math.Round(wartosc, 2)));
                    }
                    if (i >= szczyt) // zbocze malejace
                    {
                        double wartosc = (((-_A) / (koniecOkresu - szczyt)) * i) + ((_A * koniecOkresu) / (koniecOkresu - szczyt));
                        Opozniony.Add(new OxyPlot.DataPoint(Math.Round(i - op, 2), Math.Round(wartosc, 2)));
                    }
                    if (i >= koniecOkresu)
                        ktory++;
                }

            }

        }

        public void CalculateInfo()
        {
            // srednia
            double mnoznik = (1) / (Points.Last().X - Points.First().X + 1);
            double suma = 0;
            foreach (var i in Points)
            {
                suma = suma + i.Y;
            }
            _Srednia = mnoznik + suma;
            // srednia bezwzgledna
            suma = 0;
            foreach (var i in Points)
            {
                suma = suma + Math.Abs(i.Y);
            }
            _SredniaBez = mnoznik + suma;
            // moc srednia
            suma = 0;
            foreach (var i in Points)
            {
                suma = suma + Math.Pow(i.Y, 2);
            }
            _MocSrednia = mnoznik + suma;
            // wariancja
            suma = 0;
            foreach (var i in Points)
            {
                suma = suma + Math.Pow((i.Y - _Srednia), 2);
            }
            _Wariancja = mnoznik + suma;
            // wartosc skuteczna
            _Skuteczna = Math.Sqrt(_MocSrednia);


        }

        public void CalculateInfoDys()
        {
            // srednia
            double mnoznik = (1) / (PointsDys.Last().X - PointsDys.First().X + 1);
            double suma = 0;
            foreach (var i in PointsDys)
            {
                suma = suma + i.Y;
            }
            _SredniaDys = mnoznik + suma;
            // srednia bezwzgledna
            suma = 0;
            foreach (var i in PointsDys)
            {
                suma = suma + Math.Abs(i.Y);
            }
            _SredniaBezDys = mnoznik + suma;
            // moc srednia
            suma = 0;
            foreach (var i in PointsDys)
            {
                suma = suma + Math.Pow(i.Y, 2);
            }
            _MocSredniaDys = mnoznik + suma;
            // wariancja
            suma = 0;
            foreach (var i in PointsDys)
            {
                suma = suma + Math.Pow((i.Y - _Srednia), 2);
            }
            _WariancjaDys = mnoznik + suma;
            // wartosc skuteczna
            _SkutecznaDys = Math.Sqrt(_MocSrednia);


        }

        public void FromPointsToTimeAndAmplitude()
        {
            foreach (var point in Points)
            {
                TimeAndAmplitude.Add(new Point(point.X, point.Y));
            }
        }

        public void FromTimeAndAmplitudeToPoints()
        {
            Points = new List<OxyPlot.DataPoint>();
            foreach (var point in TimeAndAmplitude)
            {
                Points.Add(new DataPoint(point.X, point.Y));
            }
        }

        public void FromPointsToTimeAndAmplitudeDys()
        {
            foreach (var point in PointsDys)
            {
                TimeAndAmplitudeDys.Add(new Point(point.X, point.Y));
            }
        }

        public void FromTimeAndAmplitudeToPointsDys()
        {
            PointsDys = new List<OxyPlot.DataPoint>();
            foreach (var point in TimeAndAmplitudeDys)
            {
                PointsDys.Add(new DataPoint(point.X, point.Y));
            }
        }

        public void Dyskryminacja()
        {

            double iter = 0;
            foreach (var point in Points)
            {

                if (point.X == iter)
                {
                    PointsDys.Add(new DataPoint(point.X, point.Y));
                    iter += 1 / (_f);      /// pomnoz na poczatku przez 10 a to zmien na co ktoras probke bo sie posiekam
                    iter = Math.Round(iter, 2);
                }
            }
        }

        public void KwantyzacjaZObcieciem()
        {
            PointsDysKwan = new List<OxyPlot.DataPoint>();
            foreach (var point in PointsDys)
            {
                PointsDysKwan.Add(new DataPoint(point.X, (int)point.Y));
            }
        }

        public void KwantyzacjaZZaokragleniem()
        {
            PointsDysKwan = new List<OxyPlot.DataPoint>();
            foreach (var point in PointsDys)
            {
                if ((point.Y - (int)point.Y) < 0.5)
                    PointsDysKwan.Add(new DataPoint(point.X, (int)point.Y));
                else
                    PointsDysKwan.Add(new DataPoint(point.X, ((int)point.Y) + 1));
            }
        }

        public void ZerowyRzad()
        {
            IList<OxyPlot.DataPoint> PointsDysOdtCiag = new List<OxyPlot.DataPoint>();
            int ile = (Points.Count) / (PointsDys.Count);
            int iter = 0;
            int ileJuzDodane = 0;
            foreach (var point in Points)
            {
                PointsDysOdtCiag.Add(new DataPoint(point.X, PointsDys.ElementAt(iter).Y));
                ileJuzDodane++;
                if (ileJuzDodane == 10)
                {
                    iter++;
                    ileJuzDodane = 0;
                }

            }
            PointsOdt = PointsDysOdtCiag;
        }

        public void PierwszyRzad()
        {
            IList<OxyPlot.DataPoint> PointsDysOdtCiag = new List<OxyPlot.DataPoint>();
            int ile = (Points.Count - 1) / (PointsDys.Count - 1);
            int iter = 0;
            int ileJuzDodane = 0;
            foreach (var point in Points)
            {
                if (iter == PointsDys.Count - 1)
                {
                    PointsDysOdtCiag.Add(new DataPoint(point.X, PointsDys.ElementAt(iter).Y));
                }
                else
                {
                    double xa = PointsDys.ElementAt(iter).X;
                    double xb = PointsDys.ElementAt(iter + 1).X;
                    double ya = PointsDys.ElementAt(iter).Y;
                    double yb = PointsDys.ElementAt(iter + 1).Y;
                    PointsDysOdtCiag.Add(new DataPoint(point.X, (((yb - ya) / (xb - xa)) * point.X) + (((ya * xb) - (yb * xa)) / (xb - xa))));
                }

                ileJuzDodane++;
                if (ileJuzDodane == 10)
                {
                    iter++;
                    ileJuzDodane = 0;
                }

            }
            PointsOdt = PointsDysOdtCiag;
        }

        public void CalculateErrors()
        {
            //blad sredniokwadratowy mse
            double roznica = 0;
            double suma = 0;
            for (int i = 0; i < Points.Count; i++)
            {
                roznica = PointsOdt.ElementAt(i).Y - Points.ElementAt(i).Y;
                roznica = roznica * roznica;
                suma = suma + roznica;
            }
            _MSE = (1 / (double)Points.Count) * suma;
            //stosune sygnal - szum snr
            double x2 = 0;
            double sumaKwadratow = 0;
            foreach (var point in PointsOdt)
            {
                x2 = point.Y * point.Y;
                sumaKwadratow = sumaKwadratow + x2;
            }
            _SNR = 10 * Math.Log10(sumaKwadratow / suma);
            //szczytowy stosunek sygnal - szum psnr
            double max = PointsOdt.ElementAt(0).Y;
            foreach (var point in PointsOdt)
            {
                if (point.Y > max)
                    max = point.Y;
            }
            _PSNR = 10 * Math.Log10(max / _MSE);
            //maksymalna roznica md
            double roznicaMd = Math.Abs(PointsOdt.ElementAt(0).Y - Points.ElementAt(0).Y);
            for (int i = 0; i < PointsOdt.Count; i++)
            {
                double nowaRoznicaMd = Math.Abs(PointsOdt.ElementAt(i).Y - Points.ElementAt(i).Y);
                if (nowaRoznicaMd > roznicaMd)
                    roznicaMd = nowaRoznicaMd;
            }
            _MD = roznicaMd;
        }

        public void CalculateErrorsDys()
        {
            //blad sredniokwadratowy mse
            double roznica = 0;
            double suma = 0;
            for (int i = 0; i < PointsDys.Count; i++)
            {
                roznica = PointsDysKwan.ElementAt(i).Y - PointsDys.ElementAt(i).Y;
                roznica = roznica * roznica;
                suma = suma + roznica;
            }
            _MSEDys = (1 / (double)PointsDys.Count) * suma;
            //stosune sygnal - szum snr
            double x2 = 0;
            double sumaKwadratow = 0;
            foreach (var point in PointsDysKwan)
            {
                x2 = point.Y * point.Y;
                sumaKwadratow = sumaKwadratow + x2;
            }
            _SNRDys = 10 * Math.Log10(sumaKwadratow / suma);
            //szczytowy stosunek sygnal - szum psnr
            double max = PointsDysKwan.ElementAt(0).Y;
            foreach (var point in PointsDysKwan)
            {
                if (point.Y > max)
                    max = point.Y;
            }
            _PSNRDys = 10 * Math.Log10(max / _MSE);
            //maksymalna roznica md
            double roznicaMd = Math.Abs(PointsDysKwan.ElementAt(0).Y - PointsDys.ElementAt(0).Y);
            for (int j = 0; j < PointsDysKwan.Count; j++)
            {
                double nowaRoznicaMd = Math.Abs(PointsDysKwan.ElementAt(j).Y - PointsDys.ElementAt(j).Y);
                if (nowaRoznicaMd > roznicaMd)
                    roznicaMd = nowaRoznicaMd;
            }
            _MDDys = roznicaMd;
        }

        public void StworzFiltr()
        {
            Filtr = new List<OxyPlot.DataPoint>();
            for (int n = 0; n <= _M - 1; n++)
            {
                double cos = 0;
                if (n == (_M - 1) / 2)
                    cos = 2 / _K;
                else
                {
                    double gora = Math.Sin((2 * Math.PI * (n - (_M - 1) / 2) / _K));
                    double dol = Math.PI * (n - (_M - 1) / 2);
                    cos = gora / dol;
                }
                Filtr.Add(new DataPoint(n, cos));
            }
        }

        public void OHamming()
        {
            IList<OxyPlot.DataPoint> nowyFiltr = new List<OxyPlot.DataPoint>();
            for (int n = 0; n <= _M - 1; n++)
            {
                double cos = 0.53836 - (0.46164 * Math.Cos((2 * Math.PI * n) / _M));
                nowyFiltr.Add(new DataPoint(n, Filtr.ElementAt(n).Y * cos));
            }
            Filtr = nowyFiltr;
        }
        public void OHanning()
        {
            IList<OxyPlot.DataPoint> nowyFiltr = new List<OxyPlot.DataPoint>();
            for (int n = 0; n <= _M - 1; n++)
            {
                double cos = 0.5 - (0.5 * Math.Cos((2 * Math.PI * n) / _M));
                nowyFiltr.Add(new DataPoint(n, Filtr.ElementAt(n).Y * cos));
            }
            Filtr = nowyFiltr;
        }
        public void OBlackman()
        {
            IList<OxyPlot.DataPoint> nowyFiltr = new List<OxyPlot.DataPoint>();
            for (int n = 0; n <= _M - 1; n++)
            {
                double cos = 0.42 - (0.5 * Math.Cos((2 * Math.PI * n) / _M)) + (0.8 * Math.Cos((4 * Math.PI * n) / _M));
                nowyFiltr.Add(new DataPoint(n, Filtr.ElementAt(n).Y * cos));
            }
            Filtr = nowyFiltr;
        }

        public void FiltrSrodkowo()
        {
            IList<OxyPlot.DataPoint> nowyFiltr = new List<OxyPlot.DataPoint>();
            for (int n = 0; n <= _M - 1; n++)
            {
                double cos = 2 * Math.Sin((Math.PI * n) / 2);
                nowyFiltr.Add(new DataPoint(n, Filtr.ElementAt(n).Y * cos));
            }
            Filtr = nowyFiltr;
        }
        public void FiltrGorno()
        {
            IList<OxyPlot.DataPoint> nowyFiltr = new List<OxyPlot.DataPoint>();
            for (int n = 0; n <= _M - 1; n++)
            {
                double cos = Math.Pow((-1), n);
                nowyFiltr.Add(new DataPoint(n, Filtr.ElementAt(n).Y * cos));
            }
            Filtr = nowyFiltr;
        }

        public void Filtracja(string okno, string typFiltru)
        {
            StworzFiltr();
            if (okno == "Okno Hamminga") OHamming();
            if (okno == "Okno Hanninga") OHanning();
            if (okno == "Okno Blackmana") OBlackman();
            if (typFiltru == "Filtr środkowoprzepustowy") FiltrSrodkowo();
            if (typFiltru == "Filtr górnoprzepustowy") FiltrGorno();

            SygFiltrowany = new List<OxyPlot.DataPoint>();
            int M = Filtr.Count();
            int N = Points.Count();
            for (int i = 0; i <= M + N - 1; i++)
            {
                double splot = 0;
                for (int j = 0; j <= M - 1; j++)
                {
                    if ((i - j) < 0) break;
                    if ((i - j) < N)
                        splot += Filtr.ElementAt(j).Y * Points.ElementAt(i - j).Y;
                }

                SygFiltrowany.Add(new DataPoint(i, splot));
            }
        }

        public void Radaruj()
        {
            //korelacja
            int M = Opozniony.Count();
            int N = Points.Count();
            for (int i = (M - 1) * (-1); i <= N - 1; i++)
            {
                double korelacja = 0;
                for (int j = 0; j <= M - 1; j++)
                {
                    if ((i + j) >= N) break;
                    if ((i + j) >= 0)
                        korelacja += Opozniony.ElementAt(j).Y * Points.ElementAt(i + j).Y;
                }

                Radar.Add(new DataPoint(i, korelacja));
            }

            //szukanie probki maksymalnej
            double x = 0;
            double y = 0;
            foreach (var point in Radar)
            {
                if (point.X == 0)
                {
                    x = Radar.ElementAt(0).X;
                    y = Radar.ElementAt(0).Y;
                }
                if (point.X > 0)
                {
                    if (point.Y > y)
                    {
                        x = point.X;
                        y = point.Y;
                    }
                }
            }

            //delta czasu
            double sekunda = 1 / (_f * 10);
            _CzasOpoznienia = x * sekunda;

            //droga w ta i z powrotem
            double predkoscSwiatla = 299792458; // m/s (V)
            double drogaWDwieStrony = predkoscSwiatla * _CzasOpoznienia; // S = Vt

            //chwilowa odleglosc od czujnika
            _Odleglosc = drogaWDwieStrony / 2;
        }

        public void DFT()
        {
            //double N = Math.Pow(2, _d);
            int N = Points.Count();
            if (!((N & (N - 1)) == 0))
            {
                int res = 0;
                for (int i = N; i >= 1; i--)
                {
                    if ((i & (i - 1)) == 0)
                    {
                        res = i;
                        break;
                    }
                }
                N = res;
            }
            zegarek.Start();
            for (int m = 0; m < N; m++)
            {
                double sumaRe = 0;
                double sumaIm = 0;
                for (int n = 0; n < N; n++)
                {
                    sumaRe += Points.ElementAt(n).Y * Math.Cos((2.0 * Math.PI * m * n) / N);
                    sumaIm += Points.ElementAt(n).Y * (-Math.Sin((2.0 * Math.PI * m * n) / N));
                }
                Re.Add(new DataPoint(m, sumaRe));
                Im.Add(new DataPoint(m, sumaIm));
            }
            zegarek.Stop();
        }

        //public void FFT()
        //{
        //    //double N = Math.Pow(2, _d);
        //    double N = Points.Count();

        //    if (N % 2 == 0)
        //        for (int m = 0; m < N; m++)
        //        {
        //            double sumaRe1 = 0;
        //            double sumaIm1 = 0;
        //            double sumaRe2 = 0;
        //            double sumaIm2 = 0;
        //            for (int n = 0; n < N / 2; n++)
        //            {
        //                double cos = Math.Cos((2.0 * Math.PI * m * n) / (N / 2));
        //                double sin = (-Math.Sin((2.0 * Math.PI * m * n) / (N / 2)));
        //                sumaRe1 += Points.ElementAt(2 * n).Y * cos;
        //                sumaIm1 += Points.ElementAt(2 * n).Y * sin;
        //                sumaRe2 += Points.ElementAt(2 * n + 1).Y * cos;
        //                sumaIm2 += Points.ElementAt(2 * n + 1).Y * sin;
        //            }
        //            Re.Add(new DataPoint(m, (sumaRe1 + Math.Cos((2.0 * Math.PI * m) / N) * sumaRe2)));
        //            Im.Add(new DataPoint(m, (sumaIm1 - Math.Cos((2.0 * Math.PI * m) / N) * sumaIm2)));
        //        }
        //}


        //void Separate(Complex[] X, int N, int n)
        //{
        //    Complex[] temp = new Complex[N / 2];
        //    for (int i = 0; i < N / 2; i++)
        //        temp[i] = X[(i) * 2 + 1 +n];

        //    for (int i = 0; i < N / 2; i++)
        //        X[(i) + n] = X[(i) * 2 + n];

        //    for (int i = 0; i < N / 2; i++)
        //        X[(i) + N / 2 +n] = temp[i];

        //}

        //void Fft2(Complex[] X, int N, int n)
        //{
        //    if (N < 2)
        //    {
        //    }
        //    else
        //    {
        //        Separate(X, N, n);
        //        Fft2(X, N / 2, n);
        //        Fft2(X, N / 2, N / 2);

        //        int k = n;
        //        do
        //        {
        //            Complex e = X[k];
        //            Complex o = X[k + N / 2];

        //            Complex w = new Complex(Math.Cos(2.0 * Math.PI * (k - n) / N), -Math.Sin(2.0 * Math.PI * (k - n) / N));
        //            X[k] = e + w * o;
        //            X[k + N / 2] = e - w * o;

        //            k++;
        //        } while (k < N / 2 + n);
        //    }
        //}

        //public void FFT()
        //{
        //    int N = Points.Count();
        //    if (N > 0 && ((N & (N - 1)) == 0))
        //    {
        //        Complex[] X = new Complex[N];
        //        for (int i = 0; i < N; i++)
        //        {
        //            X[i] = new Complex(Points.ElementAt(i).Y, 0);
        //        }

        //        Fft2(X, N, 0);

        //        for (int i = 0; i < N; i++)
        //        {
        //            Re.Add(new DataPoint(i, X[i].Real));
        //            Im.Add(new DataPoint(i, X[i].Imaginary));
        //        }
        //    }
        //}

        public void FFT()
        {
            int N = Points.Count();
            if (!((N & (N - 1)) == 0))
            {
                int res = 0;
                for (int i = N; i >= 1; i--)
                {
                    if ((i & (i - 1)) == 0)
                    {
                        res = i;
                        break;
                    }
                }
                N = res;
            }
            if (N > 0 && ((N & (N - 1)) == 0))
            {
                Double[] X = new Double[N];
                for (int i = 0; i < N; i++)
                {
                    X[i] = Points.ElementAt(i).Y;
                }

                zegarek.Start();
                Double[] syg = new Double[N];
                syg = ZmienKolejnosc(X);

                var SYGNAL = new List<Complex>();
                for (int i = 0; i < N; i++)
                {
                    SYGNAL.Add(new Complex(syg[i], 0));
                }

                for (int i = 2; i <= N; i = i * 2)
                {
                    SYGNAL = Fft2(SYGNAL, i);
                }

                for (int i = 0; i < N; i++)
                {
                    Re.Add(new DataPoint(i, SYGNAL[i].Real));
                    Im.Add(new DataPoint(i, SYGNAL[i].Imaginary));
                }
                zegarek.Stop();
            }

        }

        public List<Complex> W(int N)
        {
            List<Complex> w = new List<Complex>();
            for (int k = 0; k < N / 2; k++)
            {
                w.Add(new Complex(Math.Cos(2.0 * Math.PI * k / N), -Math.Sin(2.0 * Math.PI * k / N)));
            }
            for (int k = 0; k < N / 2; k++)
            {
                w.Add(w[k] * (-1.0));
            }
            return w;
        }

        public Complex Motylek(Complex x1, Complex x2, Complex w, double znak = 1.0)
        {
            return x1 + x2 * w * znak;
        }

        public List<Complex> Fft2(List<Complex> sygnal, int N)
        {
            var d = new List<Complex>();
            var w = W(N);
            for (int i = 0; i < sygnal.Count / N; i++)
            {
                for (int j = 0; j < N / 2; j++)
                {
                    var value = Motylek(sygnal[N * i + j], sygnal[N * i + j + N / 2], w[j]);
                    d.Add(value);
                }
                for (int j = 0; j < N / 2; j++)
                {
                    var value = Motylek(sygnal[N * i + j], sygnal[N * i + j + N / 2], w[j], -1.0);
                    d.Add(value);
                }
            }
            return d;
        }

        public double[] ZmienKolejnosc(double[] sygnal)
        {
            double[][] wynik = new double[2][];
            if (sygnal.Length / 2 >= 2)
            {
                double[][] zmiana = new double[2][];
                for (int i = 0; i < zmiana.Length; i++)
                {
                    zmiana[i] = new double[sygnal.Length / 2];
                }
                for (int j = 0; j < sygnal.Length; j++)
                {
                    if (j % 2 == 0)
                    {
                        zmiana[0][j / 2] = sygnal[j];
                    }
                    else
                    {
                        zmiana[1][(j - 1) / 2] = sygnal[j];
                    }
                }
                wynik[0] = ZmienKolejnosc(zmiana[0]);
                wynik[1] = ZmienKolejnosc(zmiana[1]);
            }
            else
            {
                return sygnal;
            }
            double[] koniec = new double[wynik[0].Length + wynik[1].Length];
            Array.Copy(wynik[0], koniec, wynik[0].Length);
            Array.Copy(wynik[1], 0, koniec, wynik[0].Length, wynik[1].Length);
            return koniec;
        }

        public void DCTII()
        {
            
            int N = Points.Count();
            if (!((N & (N - 1)) == 0))
            {
                int res = 0;
                for (int i = N; i >= 1; i--)
                {
                    if ((i & (i - 1)) == 0)
                    {
                        res = i;
                        break;
                    }
                }
                N = res;
            }
            zegarek.Start();
            double cm = Math.Sqrt(2 / N);

            for (int m = 0; m < N; m++)
            {
                if(m == 0) cm = Math.Sqrt(1 / N);
                double sumaRe = 0;
                for (int n = 0; n < N; n++)
                {
                    sumaRe += Points.ElementAt(n).Y * Math.Cos((Math.PI * m * (2.0*n +1)) / (2*N));
                }
                Re.Add(new DataPoint(m, cm*sumaRe));
            }
            zegarek.Stop();
        }

        public void FCTII()
        {
            
            int N = Points.Count();
            if (!((N & (N - 1)) == 0))
            {
                int res = 0;
                for (int i = N; i >= 1; i--)
                {
                    if ((i & (i - 1)) == 0)
                    {
                        res = i;
                        break;
                    }
                }
                N = res;
            }
            zegarek.Start();
            if (N % 2 == 0)
            {
                ICollection<Double> nowaKolejnosc = new Collection<Double>();
                for (int i = 0; i < N / 2; i++)
                {
                    nowaKolejnosc.Add(Points.ElementAt(2 * i).Y);
                }
                for (int i = (int)((N / 2) - 1); i >= 0; i--)
                {
                    nowaKolejnosc.Add(Points.ElementAt(2 * i+1).Y);
                }

                double cm = Math.Sqrt(2 / N);

                for (int m = 0; m < N; m++)
                {
                    if (m == 0) cm = Math.Sqrt(1 / N);
                    double sumaRe = 0;
                    for (int n = 0; n < N; n++)
                    {
                        sumaRe += nowaKolejnosc.ElementAt(n) * Math.Cos((Math.PI * m * 2.0 * n) / N);
                    }
                    Re.Add(new DataPoint(m, cm*Math.Cos((Math.PI * m) / (2*N)) *sumaRe));
                }
            }
            zegarek.Stop();

        }

        public LineChartViewModel MakeChart(string title, string okno, string typFiltru, string trans)
        {
            Filtracja(okno, typFiltru);
            if (_Opoznienie > 0) Radaruj();
            IList<OxyPlot.DataPoint> PointsCopy = new List<OxyPlot.DataPoint>();
            if (_K != 0)
            {
                PointsCopy = Points;
                Points = SygFiltrowany;
            }
            if (trans == "DFT") DFT();
            if (trans == "FFT") FFT();
            if (trans == "DCTII") DCTII();
            if (trans == "FCTII") FCTII();
            if(_K !=0)
            {
                Points = PointsCopy;
            }
            LineChartViewModel vm = new LineChartViewModel();
            vm.Title = title;
            vm.Points = Points;
            vm.PointsDys = PointsDys;
            vm.PointsOdt = PointsOdt;
            vm.PointsDysKwan = PointsDysKwan;
            vm.Filtr = Filtr;
            vm.SygFiltrowany = SygFiltrowany;
            vm.Opozniony = Opozniony;
            vm.Radar = Radar;
            vm.Re = Re;
            vm.Im = Im;
            vm.TitleFiltr = "Filtr + " + okno + " + " + typFiltru;
            vm._A = _A;
            vm._t1 = _t1;
            vm._T = _T;
            vm._d = _d;
            vm._kw = _kw;
            vm._f = _f;
            vm._CzasFourier = ((double)zegarek.ElapsedTicks / (double)Stopwatch.Frequency) *1000.0;

            vm._Srednia = Math.Round(_Srednia, 2);
            vm._SredniaBez = Math.Round(_SredniaBez, 2);
            vm._MocSrednia = Math.Round(_MocSrednia, 2);
            vm._Wariancja = Math.Round(_Wariancja, 2);
            vm._Skuteczna = Math.Round(_Skuteczna, 2);

            vm._MSE = Math.Round(_MSE, 2);
            vm._SNR = Math.Round(_SNR, 2);
            vm._PSNR = Math.Round(_PSNR, 2);
            vm._MD = Math.Round(_MD, 2);

            vm._SredniaDys = Math.Round(_SredniaDys, 2);
            vm._SredniaBezDys = Math.Round(_SredniaBezDys, 2);
            vm._MocSredniaDys = Math.Round(_MocSredniaDys, 2);
            vm._WariancjaDys = Math.Round(_WariancjaDys, 2);
            vm._SkutecznaDys = Math.Round(_SkutecznaDys, 2);

            vm._MSEDys = Math.Round(_MSEDys, 2);
            vm._SNRDys = Math.Round(_SNRDys, 2);
            vm._PSNRDys = Math.Round(_PSNRDys, 2);
            vm._MDDys = Math.Round(_MDDys, 2);

            vm._Odleglosc = _Odleglosc;
            vm._Opoznienie = _Opoznienie;
            vm._CzasOpoznienia = _CzasOpoznienia;
            return vm;
        }

        public HistogramViewModel MakeHistogram()
        {
            HistogramViewModel h = new HistogramViewModel();
            Collection<Item> Items = new Collection<Item>();
            double maxA = 0;
            double minA = 0;
            foreach(var point in Points)
            {
                if (maxA < point.Y) maxA = point.Y;
                if (minA > point.Y) minA = point.Y;
            }
            double diff = maxA - minA;
            double blok = diff / _his;
            for(int i = 0; i < _his; i++)
            {
                int ile = 0;
                double min = minA + (i * blok);
                double max = minA + ((i+1) * blok);
                foreach (var point in Points)
                {
                
                    if (point.Y >= min && point.Y <= max)
                    {
                        ile++;
                    }
                }
                Items.Add(new Item { Label = Math.Round(min,2).ToString() + " / " +Math.Round(max,2).ToString(), Value = ile });
            }
            h.Items = Items;
            h.Make();
            return h;
        }

    }
}
