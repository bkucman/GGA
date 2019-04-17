using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosestPairOfPoints
{
    class Program
    {
        static void Main(string[] args)
        {
            //String fileName = "..\\..\\..\\files\\p2.csv";
            //String fileName = "..\\..\\..\\files\\dist.csv";
            //String fileName = "..\\..\\..\\files\\solved.csv";
            //String fileName = "..\\..\\..\\files\\przyklad2.csv";
            String fileName = "..\\..\\..\\files\\b.csv";

            var arrayOfPoints = DataReader.ReadFile(fileName);

            foreach (Point elem in arrayOfPoints)
            {
                Console.WriteLine(elem);
            }

            //var closestPoints = new ClosestPoints();
            //Console.WriteLine("Distance = " + closestPoints.Prepare(arrayOfPoints));

            Console.WriteLine();

            var closestPointsT = new ClosestPointsT();
            var result = closestPointsT.Prepare(arrayOfPoints);
            Console.WriteLine("Distance = " + result.Item1 + " Between: " + result.Item2[0] + "and " + result.Item2[1]);

            //var closestPointsT2 = new ClosestPointT2();
            //var result2 = closestPointsT2.Prepare(arrayOfPoints);
            //Console.WriteLine("Distance = " + result.Item1 + " Between: " + result.Item2[0] + "and " + result.Item2[1]);


            Console.ReadKey();
        }
    }
}
