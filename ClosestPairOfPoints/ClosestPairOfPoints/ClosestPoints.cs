using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosestPairOfPoints
{
    class ClosestPoints
    {
        public double Prepare(Point[] Points)
        {
            Array.Sort(Points);
            Console.WriteLine("hej");
            foreach (Point elem in Points)
            {
                Console.WriteLine("P :" + elem);
            }
            return PairClosestPoints(Points);
        }

        private double CheckShortestDistancePoints(Point[] points)
        {
            if (points.Length == 2)
                return DistanceSegment(points[0], points[1]);

            var distance01 = DistanceSegment(points[0], points[1]);
            var distance12 = DistanceSegment(points[1], points[2]);
            var distance02 = DistanceSegment(points[0], points[2]);

            if (distance01 < distance12)
                return distance01 < distance02 ? distance01 : distance02;
            else
                return distance12 < distance02 ? distance12 : distance02;
        }

        private double PairClosestPoints(Point[] points)
        {
            if (points.Length <= 3)
            {
                return CheckShortestDistancePoints(points);
            }

            var divisionAt = points.Length / 2;
            var divisonPoint = points[divisionAt];

            var divisionLeftSide = PairClosestPoints(new ArraySegment<Point>(points, 0, divisionAt).ToArray());
            var divisionRightSide = PairClosestPoints(new ArraySegment<Point>(points, divisionAt, (points.Length - divisionAt)).ToArray());

            //double divisionLeftSide = PairClosestPoints(new ArraySegment<Point>(points, 0, divisionAt).ToArray());
            //double divisionRightSide = PairClosestPoints(new ArraySegment<Point>(points, divisionAt, (points.Length - divisionAt)).ToArray());
            Console.WriteLine(divisionLeftSide + " " + divisionRightSide);
            double twoDivisionClosest = Math.Min(divisionLeftSide, divisionRightSide);

            List<Point> listPointsFromStrip = new List<Point>();
            // closes points in strip only smaller than twoDivisonClosest
            for (int i = 0; i < points.Length; i++)
            {
                if (DistanceSegment(points[i], divisonPoint) < twoDivisionClosest)
                    if (DistanceSegment(points[i], divisonPoint) < twoDivisionClosest)
                {
                    listPointsFromStrip.Add(points[i]);
                }
            }

            return Math.Min(twoDivisionClosest, StripClosest(listPointsFromStrip.ToArray(), twoDivisionClosest));
        }

        private double StripClosest(Point[] Points, double minDistance)
        {
            for (int i = 0; i < Points.Length; ++i)
                for (int j = i + 1; j < Points.Length && (Points[j].GetY() - Points[i].GetY()) < minDistance; ++j)
                    if (DistanceSegment(Points[i], Points[j]) < minDistance)
                        minDistance = DistanceSegment(Points[i], Points[j]);

            return minDistance;
        }
        private double DistanceSegment(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.GetX() - b.GetX(), 2) + Math.Pow(a.GetY() - b.GetY(), 2));
        }
    }
}
