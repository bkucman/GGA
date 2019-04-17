using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvexHull
{
    public class DataReader
    {
        public static List<Point> ReadFile(String fileName)
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
            List<Point> arrayOfPoints = new List<Point>();
            for (int i = 0; i < listA.Count; i++)
            {
                //arrayOfPoint[i] = new Point(Int32.Parse(listA.ElementAt(i)), Int32.Parse(listB.ElementAt(i)));
                arrayOfPoints.Add(new Point(Int32.Parse(listA.ElementAt(i)), Int32.Parse(listB.ElementAt(i))));

            }

            return arrayOfPoints;
        }
        public static void WriteFile(Stack<Point> stack)
        {
            string path = "..\\..\\..\\files\\polygon.csv";
            using (var w = new StreamWriter(path))
            {
                foreach (Point p in stack)
                {
                    var first = p.GetX(); ;
                    var second = p.GetY();
                    var line = string.Format("{0};{1}", first, second);
                    w.WriteLine(line);
                    w.Flush();
                }
            }
        }
    }
}
