using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Twart.Visibility = Visibility.Hidden;
            kwwart.Visibility = Visibility.Hidden;
            nswart.Visibility = Visibility.Hidden;
            pwart.Visibility = Visibility.Hidden;
            Tnazwa.Visibility = Visibility.Hidden;
            kwnazwa.Visibility = Visibility.Hidden;
            nsnazwa.Visibility = Visibility.Hidden;
            pnazwa.Visibility = Visibility.Hidden;
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

        private void Button_Click_stworz(object sender, RoutedEventArgs e)
        {
            SygnalCiagly sc = null;
            SygnalDyskretny sd = null;
            if (signal.Text == "Szum impulsowy" || signal.Text == "Impuls jednostkowy")
                sd = new SygnalDyskretny(Convert.ToDouble(_A), Convert.ToDouble(_t1), Convert.ToDouble(_d), Convert.ToDouble(_ns), Convert.ToDouble(_f), Convert.ToDouble(_p), Convert.ToInt32(_his));
            else
                sc = new SygnalCiagly(Convert.ToDouble(_A), Convert.ToDouble(_t1), Convert.ToDouble(_d), Convert.ToDouble(_T), Convert.ToDouble(_kw), Convert.ToDouble(_f), Convert.ToDouble(_ns), Convert.ToInt32(_his));

            LineChart lc = new LineChart();

            if (signal.Text == "Szum o rozkładzie jednostajnym") sc.SzumJednostajny();
            if (signal.Text == "Szum gaussowski") sc.SzumGaussowski();
            if (signal.Text == "Sygnał sinusoidalny") sc.SygnalSinusoidalny();
            if (signal.Text == "Sygnał sinusoidalny wyprostowany jednopołówkowo") sc.SygnalSinusoidalnyWyprostowanyJednopolowkowo();
            if (signal.Text == "Sygnał sinusoidalny wyprostowany dwupołówkowo") sc.SygnalSinusoidalnyWyprostowanyDwupolowkowo();
            if (signal.Text == "Sygnał prostokątny") sc.SygnalProstokatny();
            if (signal.Text == "Sygnał prostokątny symetryczny") sc.SygnalProstokatnySymetryczny();
            if (signal.Text == "Sygnał trójkątny") sc.SygnalTrojkatny();
            if (signal.Text == "Skok jednostkowy") sc.Skok();
            if (signal.Text == "Impuls jednostkowy") sd.ImpulsJednostkowy();
            if (signal.Text == "Szum impulsowy") sd.SzumImpulsowy();

            if (sc != null)
            {
                sc.CalculateInfo();
                lc.DataContext = sc.MakeChart(signal.Text);
                lc.Show();

                if (checkboxzapisz.IsChecked.GetValueOrDefault() == true)
                {
                    WriteRead.WriteToFile(sc, signal.Text);
                }
            }
            if (sd != null)
            {
                sd.CalculateInfo();
                lc.DataContext = sd.MakeChart(signal.Text);
                lc.Show();
            }

            if (_his > 0)
            {
                Histogram his = new Histogram();
                if (sc != null)
                    his.DataContext = sc.MakeHistogram();
                if (sd != null)
                    his.DataContext = sd.MakeHistogram();
                his.Show();
            }

        }

        private void signal_DropDownClosed(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            if (signal.Text == "Szum o rozkładzie jednostajnym" ||
                signal.Text == "Szum gaussowski")
            {
                Twart.Visibility = Visibility.Hidden;
                kwwart.Visibility = Visibility.Hidden;
                nswart.Visibility = Visibility.Hidden;
                pwart.Visibility = Visibility.Hidden;
                Tnazwa.Visibility = Visibility.Hidden;
                kwnazwa.Visibility = Visibility.Hidden;
                nsnazwa.Visibility = Visibility.Hidden;
                pnazwa.Visibility = Visibility.Hidden;
            }
            if (signal.Text == "Sygnał sinusoidalny" ||
                signal.Text == "Sygnał sinusoidalny wyprostowany jednopołówkowo" ||
                signal.Text == "Sygnał sinusoidalny wyprostowany dwupołówkowo")
            {
                Twart.Visibility = Visibility.Visible;
                kwwart.Visibility = Visibility.Hidden;
                nswart.Visibility = Visibility.Hidden;
                pwart.Visibility = Visibility.Hidden;
                Tnazwa.Visibility = Visibility.Visible;
                kwnazwa.Visibility = Visibility.Hidden;
                nsnazwa.Visibility = Visibility.Hidden;
                pnazwa.Visibility = Visibility.Hidden;
            }
            if (signal.Text == "Sygnał prostokątny" ||
                signal.Text == "Sygnał prostokątny symetryczny" ||
                signal.Text == "Sygnał trójkątny")
            {
                Twart.Visibility = Visibility.Visible;
                kwwart.Visibility = Visibility.Visible;
                nswart.Visibility = Visibility.Hidden;
                pwart.Visibility = Visibility.Hidden;
                Tnazwa.Visibility = Visibility.Visible;
                kwnazwa.Visibility = Visibility.Visible;
                nsnazwa.Visibility = Visibility.Hidden;
                pnazwa.Visibility = Visibility.Hidden;
            }
            if (signal.Text == "Skok jednostkowy" ||
                signal.Text == "Impuls jednostkowy")
            {
                Twart.Visibility = Visibility.Hidden;
                kwwart.Visibility = Visibility.Hidden;
                nswart.Visibility = Visibility.Visible;
                pwart.Visibility = Visibility.Hidden;
                Tnazwa.Visibility = Visibility.Hidden;
                kwnazwa.Visibility = Visibility.Hidden;
                nsnazwa.Visibility = Visibility.Visible;
                pnazwa.Visibility = Visibility.Hidden;
            }
            if (signal.Text == "Szum impulsowy")
            {
                Twart.Visibility = Visibility.Hidden;
                kwwart.Visibility = Visibility.Hidden;
                nswart.Visibility = Visibility.Hidden;
                pwart.Visibility = Visibility.Visible;
                Tnazwa.Visibility = Visibility.Hidden;
                kwnazwa.Visibility = Visibility.Hidden;
                nsnazwa.Visibility = Visibility.Hidden;
                pnazwa.Visibility = Visibility.Visible;
            }
        }
    }
}

