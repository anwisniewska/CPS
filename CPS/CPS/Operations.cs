using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS
{
    static class Operations
    {
        public static SygnalCiagly Add(SygnalCiagly x, SygnalCiagly y)
        {
            ICollection<Point> newPoints = new Collection<Point>();
            for(int i = 0; i<x.TimeAndAmplitude.Count(); i++)
            {
                newPoints.Add(new Point(x.TimeAndAmplitude.ElementAt(i).X, x.TimeAndAmplitude.ElementAt(i).Y + y.TimeAndAmplitude.ElementAt(i).Y));
            }
            ICollection<Point> newPoints2 = new Collection<Point>();
            for (int i = 0; i < x.TimeAndAmplitude2.Count(); i++)
            {
                newPoints2.Add(new Point(x.TimeAndAmplitude2.ElementAt(i).X, x.TimeAndAmplitude2.ElementAt(i).Y + y.TimeAndAmplitude2.ElementAt(i).Y));
            }
            SygnalCiagly wynik = new SygnalCiagly(0,0,0,0,0,0,0,0);
            wynik.TimeAndAmplitude = newPoints;
            wynik.TimeAndAmplitude2 = newPoints2;
            wynik.FromTimeAndAmplitudeToPoints(wynik.Points, wynik.TimeAndAmplitude);
            wynik.FromTimeAndAmplitudeToPoints(wynik.Points2, wynik.TimeAndAmplitude2);
            wynik.CalculateInfo();
            return wynik;
        }

        public static SygnalCiagly Subtract(SygnalCiagly x, SygnalCiagly y)
        {
            ICollection<Point> newPoints = new Collection<Point>();
            for (int i = 0; i < x.TimeAndAmplitude.Count(); i++)
            {
                newPoints.Add(new Point(x.TimeAndAmplitude.ElementAt(i).X, x.TimeAndAmplitude.ElementAt(i).Y - y.TimeAndAmplitude.ElementAt(i).Y));
            }
            ICollection<Point> newPoints2 = new Collection<Point>();
            for (int i = 0; i < x.TimeAndAmplitude2.Count(); i++)
            {
                newPoints2.Add(new Point(x.TimeAndAmplitude2.ElementAt(i).X, x.TimeAndAmplitude2.ElementAt(i).Y - y.TimeAndAmplitude2.ElementAt(i).Y));
            }
            SygnalCiagly wynik = new SygnalCiagly(0, 0, 0, 0, 0, 0, 0, 0);
            wynik.TimeAndAmplitude = newPoints;
            wynik.FromTimeAndAmplitudeToPoints(wynik.Points, wynik.TimeAndAmplitude);
            wynik.TimeAndAmplitude2 = newPoints2;
            wynik.FromTimeAndAmplitudeToPoints(wynik.Points2, wynik.TimeAndAmplitude2);
            wynik.CalculateInfo();
            wynik.CalculateInfo();
            return wynik;
        }

        public static SygnalCiagly Muliply(SygnalCiagly x, SygnalCiagly y)
        {
            ICollection<Point> newPoints = new Collection<Point>();
            for (int i = 0; i < x.TimeAndAmplitude.Count(); i++)
            {
                newPoints.Add(new Point(x.TimeAndAmplitude.ElementAt(i).X, x.TimeAndAmplitude.ElementAt(i).Y * y.TimeAndAmplitude.ElementAt(i).Y));
            }
            ICollection<Point> newPoints2 = new Collection<Point>();
            for (int i = 0; i < x.TimeAndAmplitude2.Count(); i++)
            {
                newPoints2.Add(new Point(x.TimeAndAmplitude2.ElementAt(i).X, x.TimeAndAmplitude2.ElementAt(i).Y * y.TimeAndAmplitude2.ElementAt(i).Y));
            }
            SygnalCiagly wynik = new SygnalCiagly(0, 0, 0, 0, 0, 0, 0, 0);
            wynik.TimeAndAmplitude = newPoints;
            wynik.FromTimeAndAmplitudeToPoints(wynik.Points, wynik.TimeAndAmplitude);
            wynik.TimeAndAmplitude2 = newPoints2;
            wynik.FromTimeAndAmplitudeToPoints(wynik.Points2, wynik.TimeAndAmplitude2);
            wynik.CalculateInfo();
            return wynik;
        }

        public static SygnalCiagly Divide(SygnalCiagly x, SygnalCiagly y)
        {
            ICollection<Point> newPoints = new Collection<Point>();
            for (int i = 0; i < x.TimeAndAmplitude.Count(); i++)
            {
                double iloraz;
                if (y.TimeAndAmplitude.ElementAt(i).Y == 0) iloraz = 0;
                else iloraz = x.TimeAndAmplitude.ElementAt(i).Y / y.TimeAndAmplitude.ElementAt(i).Y;
                newPoints.Add(new Point(x.TimeAndAmplitude.ElementAt(i).X, iloraz));
            }
            ICollection<Point> newPoints2 = new Collection<Point>();
            for (int i = 0; i < x.TimeAndAmplitude2.Count(); i++)
            {
                double iloraz;
                if (y.TimeAndAmplitude2.ElementAt(i).Y == 0) iloraz = 0;
                else iloraz = x.TimeAndAmplitude2.ElementAt(i).Y / y.TimeAndAmplitude2.ElementAt(i).Y;
                newPoints2.Add(new Point(x.TimeAndAmplitude2.ElementAt(i).X, iloraz));
            }
            SygnalCiagly wynik = new SygnalCiagly(0, 0, 0, 0, 0, 0, 0, 0);
            wynik.TimeAndAmplitude = newPoints;
            wynik.FromTimeAndAmplitudeToPoints(wynik.Points, wynik.TimeAndAmplitude);
            wynik.TimeAndAmplitude2 = newPoints2;
            wynik.FromTimeAndAmplitudeToPoints(wynik.Points2, wynik.TimeAndAmplitude2);
            wynik.CalculateInfo();
            return wynik;
        }
    }
}
