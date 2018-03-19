using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;

namespace CPS
{
    [Serializable]
    class SygnalDyskretny
    {
        public double _A { get; set; }
        public double _t1 { get; set; }
        public double _d { get; set; }
        public double _ns { get; set; }
        public double _f { get; set; }
        public double _p { get; set; }
        public int _his { get; set; }
        [NonSerialized]
        public IList<OxyPlot.DataPoint> Points = new List<OxyPlot.DataPoint>();
        public ICollection<Point> TimeAndAmplitude = new Collection<Point>();
        public double _Srednia { get; set; }
        public double _SredniaBez { get; set; }
        public double _Skuteczna { get; set; }
        public double _Wariancja { get; set; }
        public double _MocSrednia { get; set; }

        public SygnalDyskretny(double A, double t1, double d, double ns, double f, double p, int his)
        {
            this._A = A;
            this._t1 = t1;
            this._d = d;
            this._ns = ns;
            this._f = f;
            this._p = p;
            this._his = his;
        }

        public void ImpulsJednostkowy()
        {
            int iter = 0;
            for(double i=_t1; i<=_t1+_d; i+=1/_f)
            {
                double wart;
                if (iter == _ns) wart = _A;
                else wart = 0;
                Points.Add(new OxyPlot.DataPoint(Math.Round(i, 2), Math.Round(wart, 2)));
                iter++;
            }
        }

        public void SzumImpulsowy()
        {
            double wart = 0;
            double rand = 0;
            Random r = new Random();
            for (double i = _t1; i <= _t1 + _d; i += 1/_f)
            {
                rand = r.NextDouble();
                if (rand < _p) wart = _A;
                else wart = 0;
                Points.Add(new OxyPlot.DataPoint(Math.Round(i, 2), Math.Round(wart, 2)));
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

        public PointChartViewModel MakeChart(string title)
        {
            PointChartViewModel vm = new PointChartViewModel();
            vm.Title = title;
            vm.Points = Points;
            vm._A = _A;
            vm._t1 = _t1;
            vm._ns = _ns;
            vm._d = _d;
            vm._f = _f;
            vm._p = _p;
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
            foreach (var point in Points)
            {
                if (maxA < point.Y) maxA = point.Y;
                if (minA > point.Y) minA = point.Y;
            }
            double diff = maxA - minA;
            double blok = diff / _his;
            for (int i = 0; i < _his; i++)
            {
                int ile = 0;
                double min = minA + (i * blok);
                double max = minA + ((i + 1) * blok);
                foreach (var point in Points)
                {

                    if (point.Y >= min && point.Y <= max)
                    {
                        ile++;
                    }
                }
                Items.Add(new Item { Label = Math.Round(min, 2).ToString() + " / " + Math.Round(max, 2).ToString(), Value = ile });
            }
            h.Items = Items;
            h.Make();
            return h;
        }
    }
}
