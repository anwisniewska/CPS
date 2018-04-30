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
            for (int i = 0; i < x.TimeAndAmplitudeDys.Count(); i++)
            {
                newPoints2.Add(new Point(x.TimeAndAmplitudeDys.ElementAt(i).X, x.TimeAndAmplitudeDys.ElementAt(i).Y + y.TimeAndAmplitudeDys.ElementAt(i).Y));
            }
            SygnalCiagly wynik = new SygnalCiagly(0,0,0,0,0,0,0,0);
            wynik.TimeAndAmplitude = newPoints;
            wynik.FromTimeAndAmplitudeToPoints();
            wynik.TimeAndAmplitudeDys = newPoints2;
            wynik.FromTimeAndAmplitudeToPointsDys();
            
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
            for (int i = 0; i < x.TimeAndAmplitudeDys.Count(); i++)
            {
                newPoints2.Add(new Point(x.TimeAndAmplitudeDys.ElementAt(i).X, x.TimeAndAmplitudeDys.ElementAt(i).Y - y.TimeAndAmplitudeDys.ElementAt(i).Y));
            }
            SygnalCiagly wynik = new SygnalCiagly(0, 0, 0, 0, 0, 0, 0, 0);
            wynik.TimeAndAmplitude = newPoints;
            wynik.FromTimeAndAmplitudeToPoints();
            wynik.TimeAndAmplitudeDys = newPoints2;
            wynik.FromTimeAndAmplitudeToPointsDys();

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
            for (int i = 0; i < x.TimeAndAmplitudeDys.Count(); i++)
            {
                newPoints2.Add(new Point(x.TimeAndAmplitudeDys.ElementAt(i).X, x.TimeAndAmplitudeDys.ElementAt(i).Y * y.TimeAndAmplitudeDys.ElementAt(i).Y));
            }
            SygnalCiagly wynik = new SygnalCiagly(0, 0, 0, 0, 0, 0, 0, 0);
            wynik.TimeAndAmplitude = newPoints;
            wynik.FromTimeAndAmplitudeToPoints();
            wynik.TimeAndAmplitudeDys = newPoints2;
            wynik.FromTimeAndAmplitudeToPointsDys();

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
            for (int i = 0; i < x.TimeAndAmplitudeDys.Count(); i++)
            {
                double iloraz;
                if (y.TimeAndAmplitudeDys.ElementAt(i).Y == 0) iloraz = 0;
                else iloraz = x.TimeAndAmplitudeDys.ElementAt(i).Y / y.TimeAndAmplitudeDys.ElementAt(i).Y;
                newPoints2.Add(new Point(x.TimeAndAmplitudeDys.ElementAt(i).X, iloraz));
            }
            SygnalCiagly wynik = new SygnalCiagly(0, 0, 0, 0, 0, 0, 0, 0);
            wynik.TimeAndAmplitude = newPoints;
            wynik.FromTimeAndAmplitudeToPoints();
            wynik.TimeAndAmplitudeDys = newPoints2;
            wynik.FromTimeAndAmplitudeToPointsDys();

            return wynik;
        }

        public static SygnalCiagly Splot(SygnalCiagly x, SygnalCiagly y)
        {
            ICollection<Point> newPoints = new Collection<Point>();
            int M = x.TimeAndAmplitude.Count();
            int N = y.TimeAndAmplitude.Count();
            for (int i = 0; i <= M+N-1; i++)
            {
                double splot = 0;
                for(int j = 0; j<=M-1; j++)
                {
                    if((i-j)<0) break;
                    if ((i - j) < N)
                        splot += x.TimeAndAmplitude.ElementAt(j).Y * y.TimeAndAmplitude.ElementAt(i - j).Y;
                }

                newPoints.Add(new Point(i, splot));
            }


            ICollection<Point> newPoints2 = new Collection<Point>();
            int M2 = x.TimeAndAmplitudeDys.Count();
            int N2 = y.TimeAndAmplitudeDys.Count();
            for (int i = 0; i <= M2 + N2 - 1; i++)
            {
                double splot = 0;
                for (int j = 0; j <= M2 - 1; j++)
                {
                    if ((i - j) < 0) break;
                    if ((i - j) < N2)
                        splot += x.TimeAndAmplitudeDys.ElementAt(j).Y * y.TimeAndAmplitudeDys.ElementAt(i - j).Y;
                }

                newPoints2.Add(new Point(i, splot));
            }


            SygnalCiagly wynik = new SygnalCiagly(0, 0, 0, 0, 0, 0, 0, 0);
            wynik.TimeAndAmplitude = newPoints;
            wynik.FromTimeAndAmplitudeToPoints();
            wynik.TimeAndAmplitudeDys = newPoints2;
            wynik.FromTimeAndAmplitudeToPointsDys();

            return wynik;
        }

        public static SygnalCiagly KorelacjaBezpo(SygnalCiagly x, SygnalCiagly y)
        {
            ICollection<Point> newPoints = new Collection<Point>();
            int M = x.TimeAndAmplitude.Count();
            int N = y.TimeAndAmplitude.Count();
            for (int i = (M-1)*(-1); i <= N - 1; i++)
            {
                double korelacja = 0;
                for (int j = 0; j <= M - 1; j++)
                {
                    if ((i + j) >= N) break;
                    if ((i + j) >= 0)
                        korelacja += x.TimeAndAmplitude.ElementAt(j).Y * y.TimeAndAmplitude.ElementAt(i + j).Y;
                }

                newPoints.Add(new Point(i, korelacja));
            }


            ICollection<Point> newPoints2 = new Collection<Point>();
            int M2 = x.TimeAndAmplitudeDys.Count();
            int N2 = y.TimeAndAmplitudeDys.Count();
            for (int i = (M2-1)*(-1); i <= N2 - 1; i++)
            {
                double korelacja = 0;
                for (int j = 0; j <= M2 - 1; j++)
                {
                    if ((i + j) >= N2) break;
                    if ((i + j) >= 0)
                        korelacja += x.TimeAndAmplitudeDys.ElementAt(j).Y * y.TimeAndAmplitudeDys.ElementAt(i + j).Y;
                }

                newPoints2.Add(new Point(i, korelacja));
            }


            SygnalCiagly wynik = new SygnalCiagly(0, 0, 0, 0, 0, 0, 0, 0);
            wynik.TimeAndAmplitude = newPoints;
            wynik.FromTimeAndAmplitudeToPoints();
            wynik.TimeAndAmplitudeDys = newPoints2;
            wynik.FromTimeAndAmplitudeToPointsDys();

            return wynik;
        }
    }
}
