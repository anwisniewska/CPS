using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS
{
    using System.Collections.Generic;

    using OxyPlot;

    public class PointChartViewModel
    {
        public double _A { get; set; }
        public double _t1 { get; set; }
        public double _d { get; set; }
        public double _n1 { get; set; }
        public double _ns { get; set; }
        public double _f { get; set; }
        public double _p { get; set; }
        public double _Srednia { get; set; }
        public double _SredniaBez { get; set; }
        public double _Skuteczna { get; set; }
        public double _Wariancja { get; set; }
        public double _MocSrednia { get; set; }

        public PointChartViewModel()
        {

        }

        public string Title { get; set; }

        public IList<DataPoint> Points { get; set; }
    }
}
