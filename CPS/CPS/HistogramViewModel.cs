using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.ObjectModel;

namespace CPS
{
    class HistogramViewModel
    {
        public HistogramViewModel()
        {

        }

        public void Make()
        {
            var tmp = new PlotModel { Title = "Histogram" };

            tmp.Axes.Add(new CategoryAxis { ItemsSource = this.Items, LabelField = "Label" });
            tmp.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, AbsoluteMinimum = 0 });

            tmp.Series.Add(new ColumnSeries { ItemsSource = this.Items, ValueField = "Value" });

            this.Model = tmp;
        }

        public Collection<Item> Items { get; set; }

        public PlotModel Model { get; set; }

    }


    public class Item
    {
        public string Label { get; set; }
        public double Value { get; set; }
    }
}
