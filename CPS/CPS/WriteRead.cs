using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CPS
{
    static class WriteRead
    {
        static int i = 0;
        public static void WriteToFile(SygnalCiagly syg, string name)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(i + "." + name + "- t1-" + syg._t1 + " d-" + syg._d + " f-" + syg._f + ".bin", FileMode.Create, FileAccess.Write, FileShare.None);
            syg.FromPointsToTimeAndAmplitude(syg.Points, syg.TimeAndAmplitude);
            formatter.Serialize(stream, syg);
            stream.Close();
            i++;
        }

        public static SygnalCiagly ReadFromFile(string name)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(name, FileMode.Open, FileAccess.Read, FileShare.Read);
            SygnalCiagly obj = (SygnalCiagly)formatter.Deserialize(stream);
            obj.FromTimeAndAmplitudeToPoints(obj.Points, obj.TimeAndAmplitude);
            stream.Close();
            return obj;
        }

        public static void WriteToFile(SygnalDyskretny syg, string name)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(i + "." + name + "- t1-" + syg._t1 + " d-" + syg._d + " f-" + syg._f + ".bin", FileMode.Create, FileAccess.Write, FileShare.None);
            syg.FromPointsToTimeAndAmplitude();
            formatter.Serialize(stream, syg);
            stream.Close();
            i++;
        }

        public static SygnalDyskretny ReadFromFileDys(string name)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(name, FileMode.Open, FileAccess.Read, FileShare.Read);
            SygnalDyskretny obj = (SygnalDyskretny)formatter.Deserialize(stream);
            obj.FromTimeAndAmplitudeToPoints();
            stream.Close();
            return obj;
        }
    }
}
