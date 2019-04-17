using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvexHull
{
    public class ConvexHull
    {
        public static int CompareA(Point a, Point b)
        {
            return a.CompareTo(b, 2);
        }
        /*

         It doesn't matter which point comes first, it still works out the same. 

         */
        public double CalculateAtan(Point point, Point min)
        {
            // 
            //Console.WriteLine((point.GetY() - min.GetY()) + "," + (point.GetX() - min.GetX()));
            return Math.Atan2(point.GetY() - min.GetY(), point.GetX() - min.GetX());
        }

        public Stack<Point> GrahamScan(List<Point> points)
        {
            Stack<Point> stack = new Stack<Point>();
            var min = points.ElementAt(0);
            
            // Wyliczenie a // nie pomijam min bo będzie i tak najmniejsze
            foreach(Point point in points)
            {
                point.SetA(CalculateAtan(point, min),min);
                
            }
            //Sortuję po a i usuwam powtarzające sie "a" ..
            points.Sort(CompareA);

            List<Point> pointsPrepared = new List<Point>();
            Console.WriteLine("Ilość punktów "+points.Count());
            Console.WriteLine("Usunięte");
            for (int i=0; i < points.Count(); i++)
            {
                while(i < points.Count() - 1 && points.ElementAt(i).GetA() == points.ElementAt(i+1).GetA())
                {
                    Console.WriteLine(points.ElementAt(i));
                    i++;
                }
                pointsPrepared.Add(points.ElementAt(i));
            }
            Console.WriteLine("____________________\nIlość punktów po usunięciu");
            Console.WriteLine(pointsPrepared.Count());
            foreach (Point point in pointsPrepared)
            {
                Console.WriteLine(point);

            }
            Console.WriteLine("____________________\n");
            if (pointsPrepared.Count() > 2)
            {
                stack.Push(pointsPrepared.ElementAt(0));
                stack.Push(pointsPrepared.ElementAt(1));
                stack.Push(pointsPrepared.ElementAt(2));

                for (int i = 3; i < pointsPrepared.Count(); i++)
                {
                    while (stack.Count() > 1 && Orientation(MakeMatrix(stack.Peek(), stack.ElementAt(1), pointsPrepared.ElementAt(i))) != -1)
                    {
                        stack.Pop();
                    }
                    stack.Push(pointsPrepared.ElementAt(i));
                }
            }
            else
            {
                for (int i = 0; i < pointsPrepared.Count(); i++)
                    stack.Push(pointsPrepared.ElementAt(i));
            }
            Console.WriteLine("Rozmiar stosu(otoczka)"+stack.Count());
            foreach (Point p in stack)
            {
                Console.WriteLine(p);
            }

            DataReader.WriteFile(stack);
            return stack;
        }
        public static int Orientation(int[,] arrayToDet)
        {
            var orientation = Det3x3(arrayToDet);

            if (orientation > 0)
                return 1;
            else if (orientation == 0)
                return 0;
            else
                return -1;
        }
        public static int[,] MakeMatrix(Point a, Point b, Point c)
        {
            int[,] matrix = new int[3, 3];

            matrix[0, 0] = a.GetX(); matrix[0, 1] = a.GetY(); matrix[0, 2] = 1;
            matrix[1, 0] = b.GetX(); matrix[1, 1] = b.GetY(); matrix[1, 2] = 1;
            matrix[2, 0] = c.GetX(); matrix[2, 1] = c.GetY(); matrix[2, 2] = 1;

            return matrix;
        }
        public static int Det3x3(int[,] arrayToDet)
        {
            int suma = 0;
            int roznica = 0;
            int length = arrayToDet.GetLength(0);

            for (int i = 0; i < length; i++)
            {
                //Console.WriteLine(arrayToDet[0, i] + " " + arrayToDet[1, (i + 1) % length] + " " + arrayToDet[2, (i + 2) % length]);
                suma += arrayToDet[0, i] * arrayToDet[1, (i + 1) % length] * arrayToDet[2, (i + 2) % length];
            }

            for (int i = length - 1; i >= 0; i--)
            {
                //Console.WriteLine(arrayToDet[0, i] + " " + arrayToDet[1, (i - 1 + 3) % length ] + " " + arrayToDet[2, (i - 2 + 3) % length ]);
                roznica += arrayToDet[0, i] * arrayToDet[1, (i - 1 + 3) % length] * arrayToDet[2, (i - 2 + 3) % length];
            }

            return suma - roznica;
        }
        public static Boolean OrientationRight(int[,] arrayToDet)
        {
            var orientation = Det3x3(arrayToDet);

            if (orientation > 0)
                return true;
            else
                return false;
        }
        public List<Point> FindSmalles(List<Point> points)
        {
            Point min = points.ElementAt(0);
            int ind = 0;
            for(int i=1; i < points.Count(); i++)
            {
                if (!(min.GetY() < points.ElementAt(i).GetY()))
                    if (min.GetY() == points.ElementAt(i).GetY())
                    {
                        if (min.GetX() > points.ElementAt(i).GetX())
                        {
                            min = points.ElementAt(i);
                            ind = i;
                        }
                    }
                    else
                    {
                        min = points.ElementAt(i);
                        ind = i;
                    }
            }
            Point tmp = new Point(points.ElementAt(0));
            points.ElementAt(0).setPoint(points.ElementAt(ind));
            points.ElementAt(ind).setPoint(tmp);

            return points;
        }
    }
}
