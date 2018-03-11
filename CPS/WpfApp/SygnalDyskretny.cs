using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;

namespace CPS
{
    class SygnalDyskretny
    {
        public double _A { get; set; }
        public double _t1 { get; set; }
        public double _d { get; set; }
        public double _n1 { get; set; }
        public double _ns { get; set; }
        public double _f { get; set; }
        public double _p { get; set; }
        public IList<DataPoint> Points = new List<DataPoint>();
        public double _Srednia { get; set; }
        public double _SredniaBez { get; set; }
        public double _Skuteczna { get; set; }
        public double _Wariancja { get; set; }
        public double _MocSrednia { get; set; }

        public SygnalDyskretny(double A, double t1, double d, double n1, double ns, double f, double p)
        {
            this._A = A;
            this._t1 = t1;
            this._d = d;
            this._n1 = n1;
            this._ns = ns;
            this._f = f;
            this._p = p;
        }

        public void ImpulsJednostkowy()
        {
            int iter = 0;
            for(double i=_n1; i<=_n1+_d; i+=1/_f)
            {
                double wart;
                if (iter == _ns) wart = _A;
                else wart = 0;
                Points.Add(new DataPoint(Math.Round(i, 2), Math.Round(wart, 2)));
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
                Points.Add(new DataPoint(Math.Round(i, 2), Math.Round(wart, 2)));
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

        public PointChartViewModel MakeChart(string title)
        {
            PointChartViewModel vm = new PointChartViewModel();
            vm.Title = title;
            vm.Points = Points;
            vm._A = _A;
            vm._t1 = _t1;
            vm._n1 = _n1;
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
    }
}
