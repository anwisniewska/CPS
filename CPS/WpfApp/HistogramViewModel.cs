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
            //this.Items = new Collection<Item>
            //                {
            //                    new Item {Label = "Apples", Value = 37},
            //                    new Item {Label = "Pears", Value = 7},
            //                    new Item {Label = "Bananas", Value = 23}
            //                };
        }

        public void Make()
        {
            // Create the plot model
            var tmp = new PlotModel { Title = "Histogram" };

            // Add the axes, note that MinimumPadding and AbsoluteMinimum should be set on the value axis.
            tmp.Axes.Add(new CategoryAxis { ItemsSource = this.Items, LabelField = "Label" });
            tmp.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, AbsoluteMinimum = 0 });

            // Add the series, note that the BarSeries are using the same ItemsSource as the CategoryAxis.
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
