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
            SygnalCiagly wynik = new SygnalCiagly(0,0,0,0,0,0,0,0);
            wynik.TimeAndAmplitude = newPoints;
            wynik.FromTimeAndAmplitudeToPoints();
            wynik.CalculateInfo();
            return wynik;
        }
    }
}
