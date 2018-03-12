using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;

namespace CPS
{
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
        public IList<DataPoint> Points = new List<DataPoint>();
        public double _Srednia { get; set; }
        public double _SredniaBez { get; set; }
        public double _Skuteczna { get; set; }
        public double _Wariancja { get; set; }
        public double _MocSrednia { get; set; }

        public SygnalCiagly(double A, double t1, double d, double T, double kw, double f, double ns, int his)
        {
            this._A = A;
            this._t1 = t1;
            this._d = d;
            this._T = T;
            this._kw = kw;
            this._f = f;
            this._ns = ns;
            this._his = his;

            // co by okres sie dziwnie nie konczyl
            if (_T != 0)
            {
                int calosci = (int)(_d / _T);
                _d = _T * calosci;
            }
            
        }
      
        public void SzumJednostajny()
        {
            Random rand = new Random();
            for (double i = _t1; i <= _t1 + _d; i += 1 / _f)
            {
                Points.Add(new DataPoint(Math.Round(i, 2), Math.Round(Math.Sqrt(12.0 * _A) * (((rand.Next() % 101) - 50.0) / 100.0) + 0, 2)));
            }
        }

        public void SzumGaussowski()
        {
            Random rand = new Random();
            for (double i = _t1; i <= _t1 + _d; i += 1 / _f)
            {
                int n = 10;
                double x = 0.0;
                for (int j = 0; j < n; j++)
                {
                    x += Math.Sqrt(12.0 * 1) * (((rand.Next() % 101) - 50.0) / 100.0) + 0;
                }
                Points.Add(new DataPoint(Math.Round(i, 2), Math.Round(x * Math.Sqrt(_A / (double)n) + 0, 2)));
            }
        }

        public void SygnalSinusoidalny()
        {
            for (double i=_t1; i<=_t1+_d; i+=1/_f)
            {
                Points.Add(new DataPoint(Math.Round(i, 2), Math.Round(_A * Math.Sin(((2 * Math.PI) / _T) * (i - _t1)), 2)));
            }
        }

        public void SygnalSinusoidalnyWyprostowanyJednopolowkowo()
        {
            for (double i = _t1; i <= _t1 + _d; i += 1/_f)
            {
                Points.Add(new DataPoint(Math.Round(i, 2), Math.Round(0.5*_A * (Math.Sin(((2 * Math.PI) / _T) * (i - _t1)) + Math.Abs(Math.Sin(((2 * Math.PI) / _T) * (i - _t1)))), 2)));
            }
        }

        public void SygnalSinusoidalnyWyprostowanyDwupolowkowo()
        {
            for (double i = _t1; i <= _t1 + _d; i += 1/_f)
            {
                Points.Add(new DataPoint(Math.Round(i, 2), Math.Round(_A * Math.Abs(Math.Sin(((2 * Math.PI) / _T) * (i - _t1))), 2)));
            }
        }

        public void Skok()
        {
            for (double i = _t1; i <= _t1 + _d; i += 1 / _f)
            {
                if(i < _ns)
                    Points.Add(new DataPoint(Math.Round(i, 2), Math.Round(0.0, 2)));
                if (i == _ns)
                    Points.Add(new DataPoint(Math.Round(i, 2), Math.Round(0.5*_A, 2)));
                if (i > _ns)
                    Points.Add(new DataPoint(Math.Round(i, 2), Math.Round(_A, 2)));
            }
        }

        public void SygnalProstokatny()
        {
            int iloscOkresow = (int)(_d / _T);
            int ktory = 1;
            double wypelnienie = _T * _kw;
            for (double i = _t1; i <= _t1 + _d; i += 1 / _f)
            {
                double koniecOkresu = _t1 + (ktory * _T);
                if (i < koniecOkresu - (_T - wypelnienie))
                {
                    Points.Add(new DataPoint(Math.Round(i, 2), Math.Round(_A, 2)));
                }
                if (i >= koniecOkresu - (_T - wypelnienie))
                {
                    Points.Add(new DataPoint(Math.Round(i, 2), Math.Round(0.0, 2)));
                }
                if (i == koniecOkresu)
                    ktory++;
            }
        }

        public void SygnalProstokatnySymetryczny()
        {
            int iloscOkresow = (int)(_d / _T);
            int ktory = 1;
            double wypelnienie = _T * _kw;
            for (double i = _t1; i <= _t1 + _d; i += 1 / _f)
            {
                double koniecOkresu = _t1 + (ktory * _T);
                if (i < koniecOkresu - (_T - wypelnienie))
                {
                    Points.Add(new DataPoint(Math.Round(i, 2), Math.Round(_A, 2)));
                }
                if (i >= koniecOkresu - (_T - wypelnienie))
                {
                    Points.Add(new DataPoint(Math.Round(i, 2), Math.Round(_A, 2)));
                }
                if (i == koniecOkresu)
                    ktory++;
            }
        }

        public void CalculateInfo()
        {
            // srednia
            double mnoznik = (1) / (Points.Last().X - Points.First().X + 1);
            double suma = 0;
            foreach(var i in Points)
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

        public LineChartViewModel MakeChart(string title)
        {
            LineChartViewModel vm = new LineChartViewModel();
            vm.Title = title;
            vm.Points = Points;
            vm._A = _A;
            vm._t1 = _t1;
            vm._T = _T;
            vm._d = _d;
            vm._kw = _kw;
            vm._f = _f;
            vm._Srednia = Math.Round(_Srednia, 2);
            vm._SredniaBez = Math.Round(_SredniaBez, 2);
            vm._MocSrednia = Math.Round(_MocSrednia, 2);
            vm._Wariancja = Math.Round(_Wariancja, 2);
            vm._Skuteczna = Math.Round(_Skuteczna, 2);
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
                
                    if (point.Y >= min && point.Y < max)
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
