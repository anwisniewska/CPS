using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS
{
    using System.Collections.Generic;

    using OxyPlot;

    public class LineChartViewModel
    {
        public double _A { get; set; }
        public double _t1 { get; set; }
        public double _d { get; set; }
        public double _T { get; set; }
        public double _kw { get; set; }
        public double _f { get; set; }

        public double _Srednia { get; set; }
        public double _SredniaBez { get; set; }
        public double _Skuteczna { get; set; }
        public double _Wariancja { get; set; }
        public double _MocSrednia { get; set; }

        public LineChartViewModel()
        {

        }

        public string Title { get; set; }

        public IList<OxyPlot.DataPoint> Points { get; set; }
    }
}
