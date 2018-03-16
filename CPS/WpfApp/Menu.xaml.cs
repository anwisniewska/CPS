using Microsoft.Win32;
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
        public int _his { get; set; }
        private SygnalCiagly _sygX = null;
        private SygnalCiagly _sygY = null;

        public Menu()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click_szumjedno(object sender, RoutedEventArgs e)
        {
            SygnalCiagly ss = new SygnalCiagly(Convert.ToDouble(_A), Convert.ToDouble(_t1), Convert.ToDouble(_d), Convert.ToDouble(_T), Convert.ToDouble(_kw), Convert.ToDouble(_f), Convert.ToDouble(_ns), Convert.ToInt32(_his));
            LineChart lc = new LineChart();
            ss.SzumJednostajny();
            ss.CalculateInfo();
            lc.DataContext = ss.MakeChart("Szum jednostajny");
            lc.Show();

            if(_his > 0)
            {
                Histogram his = new Histogram();
                his.DataContext = ss.MakeHistogram();
                his.Show();
            }

            if (checkboxzapisz.IsChecked.GetValueOrDefault() == true)
            {
                WriteRead.WriteToFile(ss, "Szum jednostajny");
            }
        }

        private void Button_Click_szumgaus(object sender, RoutedEventArgs e)
        {
            SygnalCiagly ss = new SygnalCiagly(Convert.ToDouble(_A), Convert.ToDouble(_t1), Convert.ToDouble(_d), Convert.ToDouble(_T), Convert.ToDouble(_kw), Convert.ToDouble(_f), Convert.ToDouble(_ns), Convert.ToInt32(_his));
            LineChart lc = new LineChart();
            ss.SzumGaussowski();
            ss.CalculateInfo();
            lc.DataContext = ss.MakeChart("Szum gaussowski");
            lc.Show();

            if (_his > 0)
            {
                Histogram his = new Histogram();
                his.DataContext = ss.MakeHistogram();
                his.Show();
            }

            if (checkboxzapisz.IsChecked.GetValueOrDefault() == true)
            {
                WriteRead.WriteToFile(ss, "Szum gaussowski");
            }
        }

        private void Button_Click_sygsin(object sender, RoutedEventArgs e)
        {
            SygnalCiagly ss = new SygnalCiagly(Convert.ToDouble(_A), Convert.ToDouble(_t1), Convert.ToDouble(_d), Convert.ToDouble(_T), Convert.ToDouble(_kw), Convert.ToDouble(_f), Convert.ToDouble(_ns), Convert.ToInt32(_his));
            LineChart lc = new LineChart();
            ss.SygnalSinusoidalny();
            ss.CalculateInfo();
            lc.DataContext = ss.MakeChart("Sygnał sinusoidalny");
            lc.Show();

            if (_his > 0)
            {
                Histogram his = new Histogram();
                his.DataContext = ss.MakeHistogram();
                his.Show();
            }

            if (checkboxzapisz.IsChecked.GetValueOrDefault() == true)
            {
                WriteRead.WriteToFile(ss, "Sygnał sinusoidalny");
            }
        }

        private void Button_Click_sygsinjed(object sender, RoutedEventArgs e)
        {
            SygnalCiagly sswj = new SygnalCiagly(Convert.ToDouble(_A), Convert.ToDouble(_t1), Convert.ToDouble(_d), Convert.ToDouble(_T), Convert.ToDouble(_kw), Convert.ToDouble(_f), Convert.ToDouble(_ns), Convert.ToInt32(_his));
            LineChart lc = new LineChart();
            sswj.SygnalSinusoidalnyWyprostowanyJednopolowkowo();
            sswj.CalculateInfo();
            lc.DataContext = sswj.MakeChart("Sygnał sinusoidalny wyprostowany jednopołówkowo");
            lc.Show();

            if (_his > 0)
            {
                Histogram his = new Histogram();
                his.DataContext = sswj.MakeHistogram();
                his.Show();
            }

            if (checkboxzapisz.IsChecked.GetValueOrDefault() == true)
            {
                WriteRead.WriteToFile(sswj, "Sygnał sinusoidalny wyprostowany jednopołówkowo");
            }
        }

        private void Button_Click_sygsindwu(object sender, RoutedEventArgs e)
        {
            SygnalCiagly sswj = new SygnalCiagly(Convert.ToDouble(_A), Convert.ToDouble(_t1), Convert.ToDouble(_d), Convert.ToDouble(_T), Convert.ToDouble(_kw), Convert.ToDouble(_f), Convert.ToDouble(_ns), Convert.ToInt32(_his));
            LineChart lc = new LineChart();
            sswj.SygnalSinusoidalnyWyprostowanyDwupolowkowo();
            sswj.CalculateInfo();
            lc.DataContext = sswj.MakeChart("Sygnał sinusoidalny wyprostowany dwupołówkowo");
            lc.Show();

            if (_his > 0)
            {
                Histogram his = new Histogram();
                his.DataContext = sswj.MakeHistogram();
                his.Show();
            };

            if (checkboxzapisz.IsChecked.GetValueOrDefault() == true)
            {
                WriteRead.WriteToFile(sswj, "Sygnał sinusoidalny wyprostowany dwupołówkowo");
            }
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

        private void Button_Click_skok(object sender, RoutedEventArgs e)
        {
            SygnalCiagly sswj = new SygnalCiagly(Convert.ToDouble(_A), Convert.ToDouble(_t1), Convert.ToDouble(_d), Convert.ToDouble(_T), Convert.ToDouble(_kw), Convert.ToDouble(_f), Convert.ToDouble(_ns), Convert.ToInt32(_his));
            LineChart lc = new LineChart();
            sswj.Skok();
            sswj.CalculateInfo();
            lc.DataContext = sswj.MakeChart("Skok jednostkowy");
            lc.Show();

            if (_his > 0)
            {
                Histogram his = new Histogram();
                his.DataContext = sswj.MakeHistogram();
                his.Show();
            }

            if (checkboxzapisz.IsChecked.GetValueOrDefault() == true)
            {
                WriteRead.WriteToFile(sswj, "Skok jednostkowy");
            }
        }

        private void Button_Click_sygpro(object sender, RoutedEventArgs e)
        {
            SygnalCiagly sswj = new SygnalCiagly(Convert.ToDouble(_A), Convert.ToDouble(_t1), Convert.ToDouble(_d), Convert.ToDouble(_T), Convert.ToDouble(_kw), Convert.ToDouble(_f), Convert.ToDouble(_ns), Convert.ToInt32(_his));
            LineChart lc = new LineChart();
            sswj.SygnalProstokatny();
            sswj.CalculateInfo();
            lc.DataContext = sswj.MakeChart("Sygnał prostokątny");
            lc.Show();

            if (_his > 0)
            {
                Histogram his = new Histogram();
                his.DataContext = sswj.MakeHistogram();
                his.Show();
            }

            if (checkboxzapisz.IsChecked.GetValueOrDefault() == true)
            {
                WriteRead.WriteToFile(sswj, "Sygnał prostokątny");
            }
        }

        private void Button_Click_sygprosym(object sender, RoutedEventArgs e)
        {
            SygnalCiagly sswj = new SygnalCiagly(Convert.ToDouble(_A), Convert.ToDouble(_t1), Convert.ToDouble(_d), Convert.ToDouble(_T), Convert.ToDouble(_kw), Convert.ToDouble(_f), Convert.ToDouble(_ns), Convert.ToInt32(_his));
            LineChart lc = new LineChart();
            sswj.SygnalProstokatnySymetryczny();
            sswj.CalculateInfo();
            lc.DataContext = sswj.MakeChart("Sygnał prostokątny symetryczny");
            lc.Show();

            if (_his > 0)
            {
                Histogram his = new Histogram();
                his.DataContext = sswj.MakeHistogram();
                his.Show();
            }

            if (checkboxzapisz.IsChecked.GetValueOrDefault() == true)
            {
                WriteRead.WriteToFile(sswj, "Sygnał prostokątny symetryczny");
            }
        }

        private void Button_Click_sygtroj(object sender, RoutedEventArgs e)
        {
            SygnalCiagly sswj = new SygnalCiagly(Convert.ToDouble(_A), Convert.ToDouble(_t1), Convert.ToDouble(_d), Convert.ToDouble(_T), Convert.ToDouble(_kw), Convert.ToDouble(_f), Convert.ToDouble(_ns), Convert.ToInt32(_his));
            LineChart lc = new LineChart();
            sswj.SygnalTrojkatny();
            sswj.CalculateInfo();
            lc.DataContext = sswj.MakeChart("Sygnał trójkątny");
            lc.Show();

            if (_his > 0)
            {
                Histogram his = new Histogram();
                his.DataContext = sswj.MakeHistogram();
                his.Show();
            }

            if(checkboxzapisz.IsChecked.GetValueOrDefault() == true)
            {
                WriteRead.WriteToFile(sswj, "Sygnał trójkątny");
            }
        }

        private void Button_Click_odtw(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog.Filter = "Binary|*.bin";
            if (openFileDialog.ShowDialog() == true)
            {
                SygnalCiagly s = WriteRead.ReadFromFile(openFileDialog.FileName);
                LineChart lc = new LineChart();
                string nazwa = openFileDialog.FileName;
                nazwa = nazwa.Substring(nazwa.IndexOf(".") + 1);
                nazwa = nazwa.Substring(0, nazwa.IndexOf("-"));
                lc.DataContext = s.MakeChart(nazwa);
                lc.Show();
            }
        }

        private void Button_Click_X(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog.Filter = "Binary|*.bin";
            if (openFileDialog.ShowDialog() == true)
            {
                _sygX = WriteRead.ReadFromFile(openFileDialog.FileName);
            }
        }

        private void Button_Click_Y(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog.Filter = "Binary|*.bin";
            if (openFileDialog.ShowDialog() == true)
            {
                _sygY = WriteRead.ReadFromFile(openFileDialog.FileName);
            }
        }

        private void Button_Click_wynik(object sender, RoutedEventArgs e)
        {
            if(_sygX == null || _sygY == null)
            {
                MessageBox.Show("Nie wybrano sygnałów", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (_sygX._t1 != _sygY._t1 || _sygX._d != _sygY._d || _sygX._f != _sygY._f)
                {
                    MessageBox.Show("Nie można wykonać operacji na tych dwóch sygnałach", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Information);
                    _sygX = null;
                    _sygY = null;
                }
                else
                {
                    if (operation.Text == "+")
                    {
                        SygnalCiagly wynik = Operations.Add(_sygX, _sygY);
                        LineChart lc = new LineChart();
                        lc.DataContext = wynik.MakeChart("Sygnał wynikowy");
                        lc.Show();
                        WriteRead.WriteToFile(wynik, "Sygnał wynikowy");
                    }
                    if (operation.Text == "-")
                    {
                        SygnalCiagly wynik = Operations.Subtract(_sygX, _sygY);
                        LineChart lc = new LineChart();
                        lc.DataContext = wynik.MakeChart("Sygnał wynikowy");
                        lc.Show();
                        WriteRead.WriteToFile(wynik, "Sygnał wynikowy");
                    }
                    if (operation.Text == "*")
                    {
                        SygnalCiagly wynik = Operations.Muliply(_sygX, _sygY);
                        LineChart lc = new LineChart();
                        lc.DataContext = wynik.MakeChart("Sygnał wynikowy");
                        lc.Show();
                        WriteRead.WriteToFile(wynik, "Sygnał wynikowy");
                    }
                    if (operation.Text == "/")
                    {
                        SygnalCiagly wynik = Operations.Divide(_sygX, _sygY);
                        LineChart lc = new LineChart();
                        lc.DataContext = wynik.MakeChart("Sygnał wynikowy");
                        lc.Show();
                        WriteRead.WriteToFile(wynik, "Sygnał wynikowy");
                    }
                }
                
            }
        }
    }
}

