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

        public double _Opoznienie { get; set; }

        public LineChartViewModel()
        {

        }

        public string Title { get; set; }
        public string TitleFiltr { get; set; }

        public IList<OxyPlot.DataPoint> Points { get; set; }
        public IList<OxyPlot.DataPoint> PointsDys { get; set; }
        public IList<OxyPlot.DataPoint> PointsOdt { get; set; }
        public IList<OxyPlot.DataPoint> PointsDysKwan { get; set; }
        public IList<OxyPlot.DataPoint> Filtr { get; set; }
        public IList<OxyPlot.DataPoint> SygFiltrowany { get; set; }
        public IList<OxyPlot.DataPoint> Opozniony { get; set; }
        public IList<OxyPlot.DataPoint> Radar { get; set; }
    }
}
