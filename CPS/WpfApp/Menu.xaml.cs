using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CPS
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {

        public double _A { get; set; }
        public double _t1 { get; set; }
        public double _d { get; set; }
        public double _T { get; set; }
        public double _kw { get; set; }
        public double _n1 { get; set; }
        public double _ns { get; set; }
        public double _f { get; set; }
        public double _p { get; set; }

        public Menu()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click_szumjedno(object sender, RoutedEventArgs e)
        { 
        }

        private void Button_Click_sygsin(object sender, RoutedEventArgs e)
        {
            SygnalCiagly ss = new SygnalCiagly(Convert.ToDouble(_A), Convert.ToDouble(_t1), Convert.ToDouble(_d), Convert.ToDouble(_T), Convert.ToDouble(_kw), Convert.ToDouble(_f));
            LineChart lc = new LineChart();
            ss.SygnalSinusoidalny();
            ss.CalculateInfo();
            lc.DataContext = ss.MakeChart("Sygnał sinusoidalny");
            lc.Show();
        }

        private void Button_Click_sygsinjed(object sender, RoutedEventArgs e)
        {
            SygnalCiagly sswj = new SygnalCiagly(Convert.ToDouble(_A), Convert.ToDouble(_t1), Convert.ToDouble(_d), Convert.ToDouble(_T), Convert.ToDouble(_kw), Convert.ToDouble(_f));
            LineChart lc = new LineChart();
            sswj.SygnalSinusoidalnyWyprostowanyJednopolowkowo();
            sswj.CalculateInfo();
            lc.DataContext = sswj.MakeChart("Sygnał sinusoidalny wyprostowany jednopołówkowo");
            lc.Show();
        }

        private void Button_Click_sygsindwu(object sender, RoutedEventArgs e)
        {
            SygnalCiagly sswj = new SygnalCiagly(Convert.ToDouble(_A), Convert.ToDouble(_t1), Convert.ToDouble(_d), Convert.ToDouble(_T), Convert.ToDouble(_kw), Convert.ToDouble(_f));
            LineChart lc = new LineChart();
            sswj.SygnalSinusoidalnyWyprostowanyDwupolowkowo();
            sswj.CalculateInfo();
            lc.DataContext = sswj.MakeChart("Sygnał sinusoidalny wyprostowany dwupołówkowo");
            lc.Show();
        }

        private void Button_Click_imp(object sender, RoutedEventArgs e)
        {
            SygnalDyskretny sswj = new SygnalDyskretny(Convert.ToDouble(_A), Convert.ToDouble(_t1), Convert.ToDouble(_d), Convert.ToDouble(_n1), Convert.ToDouble(_ns), Convert.ToDouble(_f), Convert.ToDouble(_p));
            PointChart pc = new PointChart();
            sswj.ImpulsJednostkowy();
            sswj.CalculateInfo();
            pc.DataContext = sswj.MakeChart("Impuls jednostkowy");
            pc.Show();
        }

        private void Button_Click_szumimp(object sender, RoutedEventArgs e)
        {
            SygnalDyskretny sswj = new SygnalDyskretny(Convert.ToDouble(_A), Convert.ToDouble(_t1), Convert.ToDouble(_d), Convert.ToDouble(_n1), Convert.ToDouble(_ns), Convert.ToDouble(_f), Convert.ToDouble(_p));
            PointChart pc = new PointChart();
            sswj.SzumImpulsowy();
            sswj.CalculateInfo();
            pc.DataContext = sswj.MakeChart("Szum impulsowy");
            pc.Show();
        }
    }
}
