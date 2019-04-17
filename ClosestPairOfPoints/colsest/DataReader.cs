using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosestPairOfPoints
{
    class DataReader
    {
        public static Point[] ReadFile(String fileName)
        {
            List<string> listA = new List<string>();
            List<string> listB = new List<string>();

            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    listA.Add(values[0]);
                    listB.Add(values[1]);
                }
            }

            Point[] arrayOfPoint = new Point[listA.Count];
            for (int i = 0; i < listA.Count; i++)
            {
                arrayOfPoint[i] = new Point(Int32.Parse(listA.ElementAt(i)), Int32.Parse(listB.ElementAt(i)));
            }

            return arrayOfPoint;
        }
    }
}
