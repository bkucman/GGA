using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosestPairOfPoints
{
    class ClosestPointsT
    {
        public Tuple<double, Point[]> Prepare(Point[] points)
        {
            Array.Sort(points); // to sortowanie działa tak że sprawdza który x mniejszy jeśli równe to porównuje y

            //Array.Sort(points, CompareY); // nie wiem czy potrzebne  sortowanie najpierw po y, jak równe to po x

           
            foreach (Point elem in points)
            {
                Console.WriteLine("P :"+ elem);
            }

            return PairClosestPoints(points);
        }

        private Tuple<double, Point[]> CheckShortestDistancePoints(Point[] points)
        {
            if (points.Length == 2)
                return Tuple.Create(DistanceSegment(points[0], points[1]), new Point[] { points[0], points[1] });

            var distance01 = DistanceSegment(points[0], points[1]);
            var distance12 = DistanceSegment(points[1], points[2]);
            var distance02 = DistanceSegment(points[0], points[2]);

            if (distance01 < distance12)
            {
                if (distance01 < distance02)
                {
                    return Tuple.Create(distance01, new Point[] { points[0], points[1] });
                }
                else
                {
                    return Tuple.Create(distance02, new Point[] { points[0], points[2] });
                }
            }
            else
            {
                if (distance12 < distance02)
                {
                    return Tuple.Create(distance12, new Point[] { points[1], points[2] });
                }
                else
                {
                    return Tuple.Create(distance02, new Point[] { points[0], points[2] });
                }
            }
        }
        private Tuple<double, Point[]> Min(Tuple<double, Point[]> a, Tuple<double, Point[]> b)
        {
            
            return a.Item1 < b.Item1 ? a : b;
        }
        private Tuple<double, Point[]> PairClosestPoints(Point[] points)
        {
            if (points.Length <= 3)
            {
                return CheckShortestDistancePoints(points);
            }

            int divisionAt = points.Length / 2;
            var divisonPoint = points[divisionAt];
            var leftArray = new ArraySegment<Point>(points, 0, divisionAt).ToArray();
            var rightArray = new ArraySegment<Point>(points, divisionAt, (points.Length - divisionAt)).ToArray();

            var divisionLeftSide = PairClosestPoints(new ArraySegment<Point>(points, 0, divisionAt).ToArray());
            var divisionRightSide = PairClosestPoints(new ArraySegment<Point>(points, divisionAt, (points.Length - divisionAt)).ToArray());

            //Console.WriteLine(divisionLeftSide.Item1+" "+ divisionLeftSide.Item2[0] + " " + divisionLeftSide.Item2[1]+ " " + divisionRightSide.Item1);
            var twoDivisionClosest = Min(divisionLeftSide, divisionRightSide);

            //Console.WriteLine(twoDivisionClosest.Item1);
            //Array.Sort(points, CompareY);
            List<Point> listPointsFromStrip = new List<Point>();

            //closes points in strip only smaller than twoDivisonNearest
            //for (int i = 0; i < points.Length; i++)
            //{
            //    //if (DistanceSegment(points[i], divisonPoint) < twoDivisionClosest.Item1)
            //    if (Math.Abs(points[i].GetX() - divisonPoint.GetX()) < twoDivisionClosest.Item1)
            //    {
            //        listPointsFromStrip.Add(points[i]);
            //    }
            //}
            for (int i = divisionAt - 1; i >= 0; i--)
            {
                //if (DistanceSegment(points[i], divisonPoint) < twoDivisionClosest.Item1)
                if (points[i].GetX() - divisonPoint.GetX() < twoDivisionClosest.Item1)
                {
                    listPointsFromStrip.Add(points[i]);
                }
                else
                {
                    i = -1;
                }
            }
            for (int i = divisionAt; i < points.Length; i++)
            {
                //if (DistanceSegment(points[i], divisonPoint) < twoDivisionClosest.Item1)
                if (points[i].GetX() - divisonPoint.GetX() < twoDivisionClosest.Item1)
                {
                    listPointsFromStrip.Add(points[i]);
                }
                else
                {
                    i = points.Length;
                }
            }

            return Min(twoDivisionClosest, StripClosest(listPointsFromStrip.ToArray(), twoDivisionClosest));
        }
        public int CompareY(Point a, Point b)
        {
            return a.CompareTo(b, 1);
        }
        private Tuple<double, Point[]> StripClosest(Point[] points, Tuple<double, Point[]> minDistance)
        {

            //for (int i = 0; i < points.Length; i++)
            //    for (int j = i + 1; j < points.Length && (points[j].GetY() - points[i].GetY()) < minDistance.Item1; j++)
            //        if (DistanceSegment(points[i], points[j]) <= minDistance.Item1)
            //            minDistance = Tuple.Create(DistanceSegment(points[i], points[j]), new Point[] { points[i], points[j] });
            // pętla dla lewej częśći
            int k = 0;
            int z = 0;
            //Console.WriteLine("TabL " + points.Length);
            for (int i = points.Length / 2 - 1; i >= 0; i--, k++)
            {
                for (int j = i + 1; j < points.Length; j++, z++)
                {
                    if (points[i].GetY() - points[j].GetY() >= minDistance.Item1)
                        break;
                    if (DistanceSegment(points[i], points[j]) <= minDistance.Item1)
                        minDistance = Tuple.Create(DistanceSegment(points[i], points[j]), new Point[] { points[i], points[j] });
                }
                //Console.WriteLine(z);
                z = 0;
            }

            z = 0;
            //Console.WriteLine("");
            // pętla dla prawej części
            for (int i = points.Length / 2; i < points.Length; i++, ++z)
            {
                for (int j = i + 1; j < points.Length; j++, ++z)
                {
                    if (points[i].GetY() - points[j].GetY() >= minDistance.Item1)
                        break;
                    if (DistanceSegment(points[i], points[j]) <= minDistance.Item1)
                        minDistance = Tuple.Create(DistanceSegment(points[i], points[j]), new Point[] { points[i], points[j] });
                }
                //Console.WriteLine(z);
            }
            //z = 0;
            //for (int i = points.Length / 2 - 1; i >= 0; i--)
            //{
            //    for (int j = i + 1; j < points.Length && Math.Abs(points[j].GetY() - points[i].GetY()) < minDistance.Item1; j++, ++z)
            //    {
            //        if (DistanceSegment(points[i], points[j]) <= minDistance.Item1)
            //            minDistance = Tuple.Create(DistanceSegment(points[i], points[j]), new Point[] { points[i], points[j] });
            //    }
            //    Console.WriteLine(z);
            //}
            //z = 0;
            //Console.WriteLine("");
            //// pętla dla prawej części
            //for (int i = points.Length / 2; i < points.Length; i++)
            //{
            //    for (int j = i + 1; j < points.Length && Math.Abs(points[j].GetY() - points[i].GetY()) < minDistance.Item1; j++, ++z)
            //    {
            //        if (DistanceSegment(points[i], points[j]) <= minDistance.Item1)
            //            minDistance = Tuple.Create(DistanceSegment(points[i], points[j]), new Point[] { points[i], points[j] });
            //    }
            //    Console.WriteLine(z);
            //}
            //z = 0;
            //Console.WriteLine("");


            return minDistance;
        }
        private double DistanceSegment(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.GetX() - b.GetX(), 2) + Math.Pow(a.GetY() - b.GetY(), 2));
        }
    }
}
//If the size is fewer than 16 elements, it uses an insertion sort algorithm.
//If the size exceeds 2 * log^N, where N is the range of the input array, it uses a Heap Sort algorithm.
//Otherwise, it uses a Quicksort algorithm
