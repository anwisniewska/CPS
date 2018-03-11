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
            // Create some data
            this.Items = new Collection<Item>
                            {
                                new Item {Label = "Apples", Value1 = 37},
                                new Item {Label = "Pears", Value1 = 7},
                                new Item {Label = "Bananas", Value1 = 23}
                            };

            // Create the plot model
            var tmp = new PlotModel { Title = "Column series" };

            // Add the axes, note that MinimumPadding and AbsoluteMinimum should be set on the value axis.
            tmp.Axes.Add(new CategoryAxis { ItemsSource = this.Items, LabelField = "Label" });
            tmp.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, AbsoluteMinimum = 0 });

            // Add the series, note that the BarSeries are using the same ItemsSource as the CategoryAxis.
            tmp.Series.Add(new ColumnSeries { ItemsSource = this.Items, ValueField = "Value1" });

            this.Model1 = tmp;
        }

        public Collection<Item> Items { get; set; }

        public PlotModel Model1 { get; set; }
    }


    public class Item
    {
        public string Label { get; set; }
        public double Value1 { get; set; }
    }
}
