using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosestPairOfPoints
{
    class ClosestPointT2
    {
        public Tuple<double, Point[]> Prepare(Point[] pointsX)
        {
            var pointsY = (Point[])pointsX.Clone();

            Array.Sort(pointsX);
            Array.Sort(pointsY, CompareY);

            //Console.WriteLine("y");
            //foreach (Point elem in pointsY)
            //{
            //    Console.WriteLine(elem);
            //}
            //Console.WriteLine("x");
            //foreach (Point elem in points)
            //{
            //    Console.WriteLine(elem);
            //}

            return PairClosestPoints(pointsX, pointsY, pointsY.Length);
        }

        public int CompareY(Point a, Point b)
        {
            return a.CompareTo(b, 1);
        }
        public int CompareX(Point a, Point b)
        {
            return a.CompareTo(b, 0);
        }

        private Tuple<double, Point[]> CheckShortestDistancePoints(Point[] points,int n)
        {
            if (n == 2)
                return Tuple.Create(DistanceSegment(points[0], points[1]), new Point[] { points[0], points[1] });

            var distance01 = DistanceSegment(points[0], points[1]);
            var distance12 = DistanceSegment(points[1], points[2]);
            var distance02 = DistanceSegment(points[0], points[2]);

            if (distance01 < distance12)
                if (distance01 < distance02)
                {
                    return Tuple.Create(distance01, new Point[] { points[0], points[1] });
                }
                else
                {
                    return Tuple.Create(distance02, new Point[] { points[0], points[2] });
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
            return a.Item1 < a.Item1 ? a : b;
        }
        private Tuple<double, Point[]> PairClosestPoints(Point[] pointsX, Point[] pointsY, int n)
        {
            if (n <= 3)
            {
                return CheckShortestDistancePoints(pointsX,n);
            }

            var divisionAt = n / 2;
            //if (n % 2 != 0) divisionAt += 1;
            var divisonPoint = pointsX[divisionAt];

            var pointsYl = new Point[divisionAt ]; // y sorted points on left of vertical line 
            var pointsYr = new Point[n - divisionAt]; // y sorted points on right of vertical line 
            int li = 0, ri = 0; // indexes of left and right subarrays 

            //var pointsYl = new ArraySegment<Point>(pointsX, 0, divisionAt).ToArray();
            //var pointsYr = new ArraySegment<Point>(pointsX, divisionAt, (pointsX.Length - divisionAt)).ToArray();

            //Array.Sort
            for (int i = 0; i < n; i++)
            {
                if (pointsY[i].GetX() < divisonPoint.GetX())
                    pointsYl[li++] = pointsY[i];
                else if (!(pointsY[i].GetX() != divisonPoint.GetX() && pointsY[i].GetY() != divisonPoint.GetY()))
                    pointsYr[ri++] = pointsY[i];
            }
            pointsYl[li] = divisonPoint;

            //var divisionLeftSide = PairClosestPoints(pointsX, pointsYl, divisionAt+1);
            //var divisionRightSide = PairClosestPoints(pointsX, pointsYr, n - divisionAt-1);

            var divisionLeftSide = PairClosestPoints(new ArraySegment<Point>(pointsX, 0, divisionAt).ToArray(), pointsYl, divisionAt + 1);
            var divisionRightSide = PairClosestPoints(new ArraySegment<Point>(pointsX, divisionAt, (pointsX.Length - divisionAt)).ToArray(), pointsYr, n - divisionAt - 1);

            var twoDivisionClosest = Min(divisionLeftSide, divisionRightSide);

            List<Point> listPointsFromStrip = new List<Point>();
            // closes points in strip only smaller than twoDivisonNearest
            for (int i = 0; i < n; i++)
            {
                if (Math.Abs(pointsY[i].GetX() -  divisonPoint.GetX()) < twoDivisionClosest.Item1)
                {
                    listPointsFromStrip.Add(pointsY[i]);
                }
            }

            return Min(twoDivisionClosest, StripClosest(listPointsFromStrip.ToArray(), twoDivisionClosest));
        }
        private Tuple<double, Point[]> StripClosest(Point[] points, Tuple<double, Point[]> minDistance)
        {
            for (int i = 0; i < points.Length; ++i)
                for (int j = i + 1; j < points.Length && (points[j].GetY() - points[i].GetY()) < minDistance.Item1; ++j)
                    if (DistanceSegment(points[i], points[j]) <= minDistance.Item1)
                        minDistance = Tuple.Create(DistanceSegment(points[i], points[j]), new Point[] { points[i], points[j] });

            return minDistance;
        }
        private double DistanceSegment(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.GetX() - b.GetX(), 2) + Math.Pow(a.GetY() - b.GetY(), 2));
        }
    }
}
