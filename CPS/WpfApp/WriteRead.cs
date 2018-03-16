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
        public static void WriteToFile(SygnalCiagly syg, string name)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("." + name + "- t1-" + syg._t1 + " d-" + syg._d + " f-" + syg._f, FileMode.Create, FileAccess.Write, FileShare.None);
            syg.FromPointsToTimeAndAmplitude();
            formatter.Serialize(stream, syg);
            stream.Close();
        }

        public static SygnalCiagly ReadFromFile(string name)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(name, FileMode.Open, FileAccess.Read, FileShare.Read);
            SygnalCiagly obj = (SygnalCiagly)formatter.Deserialize(stream);
            obj.FromTimeAndAmplitudeToPoints();
            stream.Close();
            return obj;
        }
    }
}
