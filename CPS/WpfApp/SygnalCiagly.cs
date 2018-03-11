using System;
using System.Collections.Generic;
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
        public IList<DataPoint> Points = new List<DataPoint>();
        public double _Srednia { get; set; }
        public double _SredniaBez { get; set; }
        public double _Skuteczna { get; set; }
        public double _Wariancja { get; set; }
        public double _MocSrednia { get; set; }

        public SygnalCiagly(double A, double t1, double d, double T, double kw, double f)
        {
            this._A = A;
            this._t1 = t1;
            this._d = d;
            this._T = T;
            this._kw = kw;
            this._f = f;
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
    }
}
