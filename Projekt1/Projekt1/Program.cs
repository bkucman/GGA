using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Projekt1
{
    public class Point
        {
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public int x;
            public int y;
        public override string ToString()
        {
            return "Punkt " + x + "," + y;
        }

    }
    class Program
    {
        public static Point[] ReadFile()
        {
            List<string> listA = new List<string>();
            List<string> listB = new List<string>();

            //using (var reader = new StreamReader(@"..\\..\\..\\dziwny.csv"))
            using (var reader = new StreamReader(@"..\\..\\..\\liniowePunkty.csv"))
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
            for(int i = 0; i < listA.Count; i++)
            {
                arrayOfPoint[i] = new Point(Int32.Parse(listA.ElementAt(i)) , Int32.Parse(listB.ElementAt(i))); 
            }

            return arrayOfPoint;
        }


        public static int[,] MakeMatrix(Point a, Point b, Point c)
        {
            int[,] matrix = new int[3, 3];

            matrix[0, 0] = a.x; matrix[0, 1] = a.y; matrix[0, 2] = 1;
            matrix[1, 0] = b.x; matrix[1, 1] = b.y; matrix[1, 2] = 1;
            matrix[2, 0] = c.x; matrix[2, 1] = c.y; matrix[2, 2] = 1;

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

            for (int i = length -1 ; i >=0 ; i--)
            {
                //Console.WriteLine(arrayToDet[0, i] + " " + arrayToDet[1, (i - 1 + 3) % length ] + " " + arrayToDet[2, (i - 2 + 3) % length ]);
                roznica += arrayToDet[0, i] * arrayToDet[1, (i - 1 + 3) % length] * arrayToDet[2, (i - 2 + 3) % length];
            }

            return suma - roznica;
        }
        public static Boolean IsMin(Point a, Point b, Point c)
        {
            if (a.y > b.y && c.y > b.y)
                return true;
            else
                return false;
        }
        public static Boolean IsMax(Point a, Point b, Point c)
        {
            if (a.y < b.y && c.y < b.y)
                return true;
            else
                return false;
        }
        public static Boolean OrientationRight(int[,] arrayToDet)
        {
            var orientation = Det3x3(arrayToDet);

            if (orientation > 0)
                return true;
            else
                return false;
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
        public static Point[] FindMaxMin(Point[] arrayOfPoints)
        {
            var length = arrayOfPoints.Length;
            Point[] foundExtremes = new Point[2]; // 0 - min, 1 - max

            for (int i = 0; i < length; i++)
            {
                var leftPoint = arrayOfPoints[(i + 1 + length) % length];
                var middlePoint = arrayOfPoints[i];
                var rigthPoint = arrayOfPoints[(i - 1 + length) % length];

                if (middlePoint.y == rigthPoint.y)
                    rigthPoint = arrayOfPoints[(i - 2 + length) % length];
                else if (middlePoint.y == leftPoint.y)
                    leftPoint = arrayOfPoints[(i + 2 + length) % length];

                //var matrixToSign = MakeMatrix(arrayOfPoints[(i + 1 + length) % length], arrayOfPoints[i], arrayOfPoints[(i - 1 + length) % length]);
                var matrixToSign = MakeMatrix(leftPoint, middlePoint, rigthPoint);


                if (OrientationRight(matrixToSign))
                {
                    if (IsMax(leftPoint, middlePoint, rigthPoint))
                    {
                        if (foundExtremes[1] == null)
                        {
                            foundExtremes[1] = middlePoint;
                        }
                        else if (middlePoint.y > foundExtremes[1].y)
                        {
                            foundExtremes[1] = middlePoint;
                        }
                    }
                    else if (IsMin(leftPoint, middlePoint, rigthPoint))
                    {
                        if (foundExtremes[0] == null)
                        {
                            foundExtremes[0] = middlePoint;
                        }
                        else if (middlePoint.y < foundExtremes[0].y)
                        {
                            foundExtremes[0] = middlePoint;
                        }
                    }
                }

                //Console.WriteLine( arrayOfPoints[(i - 1 + length) % length] + " " + arrayOfPoints[i] + " " + arrayOfPoints[(i + 1 + length) % length] );

                //var arrayToSign = MakeMatrix(arrayOfPoints[(i + 1 + length) % length ], arrayOfPoints[i], arrayOfPoints[(i - 1 + length) % length ]);

                //Console.WriteLine(Det3x3(arrayToSign) +" "+"Max = "+ IsMax(arrayOfPoints[(i - 1 + length) % length], arrayOfPoints[i], arrayOfPoints[(i + 1 + length) % length]) + " Min = "+IsMin(arrayOfPoints[(i - 1 + length) % length], arrayOfPoints[i], arrayOfPoints[(i + 1 + length) % length]));
            }

            // Jeœli nie ma jakiegoœ extremum to szukam najwy¿szego lub najni¿szego pkt
            if (foundExtremes[0] == null)
            {
                foundExtremes[0] = arrayOfPoints[0];
                for (int i = 1; i < length; i++)
                {
                    if (arrayOfPoints[i].y > foundExtremes[0].y)
                        foundExtremes[0] = arrayOfPoints[i];
                }

            }
            if (foundExtremes[1] == null)
            {
                foundExtremes[1] = arrayOfPoints[0];
                for (int i = 1; i < length; i++)
                {
                    if (arrayOfPoints[i].y < foundExtremes[1].y)
                        foundExtremes[1] = arrayOfPoints[i];
                }
            }
            return foundExtremes;
        }

        public static bool IsKernelExist(Point[] extremes)
        {

            if (extremes[0] != null && extremes[1] != null)
            {
                if (extremes[0].y < extremes[1].y)
                {
                    Console.WriteLine("J¹dro nie istnieje");
                    return false;
                }
            }

           Console.WriteLine("J¹dro istnieje ");

            return true;
        }

        static public double[,] Przecinanie1(Point[] extremes, Point[] points)
        {
            var pointsBorder = PointsOfLine(points);
            Point[] minPrzeciecie = new Point[8];
            Point[] maxPrzeciecie = new Point[4];
            double[,] cos = new double[4, 2];
            int kmin = 0;
            int kmax = 0;
            int w = 0;
            for (int j = 0; j < 2; j++) // dla min i max
            {
                var leftBorder = new Point(pointsBorder[0], extremes[j].y);
                var rightBorder = new Point(pointsBorder[1], extremes[j].y);


                for (int i = 0; i < points.Length; i++)
                {
                    // pq - prosta z max lub min, rs - odcinek
                    if ((points[i].x != extremes[j].x || points[i].y != extremes[j].y) && (points[(i + 1 + points.Length) % points.Length].x != extremes[j].x || points[(i + 1 + points.Length) % points.Length].y != extremes[j].y)) // pomijam odcinki zawieraj¹ce max lub min zale¿nie od tego, które sprawdzam
                    {
                        if (j == 0 &&  !((points[i].y == extremes[j].y && points[(i + 1 + points.Length) % points.Length].y > extremes[j].y))) // sprawdzenie czy nie bierze odcinka który zaczyna siê na prostej ale drugi koniec ma ponad minimum ,ten mnie nie interesuje
                        {
                            var signPQR = Orientation(MakeMatrix(leftBorder, rightBorder, points[i]));
                            var signPQS = Orientation(MakeMatrix(leftBorder, rightBorder, points[(i + 1 + points.Length) % points.Length]));
                            var signRSP = Orientation(MakeMatrix(points[i], points[(i + 1 + points.Length) % points.Length], leftBorder));
                            var signRSQ = Orientation(MakeMatrix(points[i], points[(i + 1 + points.Length) % points.Length], rightBorder));

                            if (signPQR != signPQS && signRSP != signRSQ)
                                //if( minPrzeciecie[0] == null)
                                {
                                    minPrzeciecie[kmin] = points[i];
                                    minPrzeciecie[kmin+1] = points[(i + 1 + points.Length) % points.Length];
                                    kmin += 2;
                                var z = prz(points[i], points[(i + 1 + points.Length) % points.Length], new Point(pointsBorder[0], extremes[j].y), new Point(pointsBorder[1], extremes[j].y));
                                cos[w, 0] = z[0]; cos[w, 1] = z[1];
                                w++;
                                Console.WriteLine("Przeciêcie odcinka " + points[i] + " " + points[(i + 1 + points.Length) % points.Length]);
                            } //else if (points[i].x - minPrzeciecie[0].x && points[(i + 1 + points.Length) % points.Length] <= minPrzeciecie[1].x)
                                

                        }else if (j == 1 && !( (points[i].y == extremes[j].y && points[(i + 1 + points.Length) % points.Length].y < extremes[j].y))) // sprawdzenie czy nie bierze odcinka który zaczyna siê na prostej ale drugi koniec ma pod maximum ,ten mnie nie interesuje
                        {
                            var signPQR = Orientation(MakeMatrix(leftBorder, rightBorder, points[i]));
                            var signPQS = Orientation(MakeMatrix(leftBorder, rightBorder, points[(i + 1 + points.Length) % points.Length]));
                            var signRSP = Orientation(MakeMatrix(points[i], points[(i + 1 + points.Length) % points.Length], leftBorder));
                            var signRSQ = Orientation(MakeMatrix(points[i], points[(i + 1 + points.Length) % points.Length], rightBorder));

                            if (signPQR != signPQS && signRSP != signRSQ)
                            {

                                minPrzeciecie[kmax] = points[i];
                                minPrzeciecie[kmax + 2] = points[(i + 1 + points.Length) % points.Length];
                                kmax += 2;

                                var z = prz(points[i], points[(i + 1 + points.Length) % points.Length], new Point(pointsBorder[0], extremes[j].y), new Point(pointsBorder[1], extremes[j].y));
                                cos[w, 0] = z[0]; cos[w, 1] = z[1];
                                w++;
                                Console.WriteLine("Przeciêcie odcinka " + points[i] + " " + points[(i + 1 + points.Length) % points.Length]);
                            }
                        }
                    }

                    
                }
                for(int i = 4; i < 8; i++)
                {
                    minPrzeciecie[i] = maxPrzeciecie[i - 4];
                }
                Console.WriteLine();
            }
            //Teraz muszê sprawdziæ które braæ pod uwagê xD jak mam wiêcej nie dwa odcinki przeciêcia na stronê 

            return cos;
        }

        //static public void obwod(Point[] segment, Point[] extremes, Point[] points)
        //{
        //    var pointsBorder = PointsOfLine(points);

        //    var intersectionLeftMax = prz(segment[2].x, segment[2].y,new Point(pointsBorder[0],extremes[1].y), new Point(pointsBorder[1], extremes[1].y));
                

        //}

        static public double[] prz(Point a, Point b, Point c, Point d) // ,p¿e dzia³a
        {
            double[] point = new double[2];
            int A1 = b.y - a.y;
            int B1 = a.x - b.x;
            int C1 = A1 * a.x + B1 * a.y;

            int A2 = d.y - c.y;
            int B2 = c.x - d.x;
            int C2 = A2 * c.x + B2 * c.y;

            double delta = A1 * B2 - A2 * B1;

            if (delta == 0)
                throw new ArgumentException("Lines are parallel");

            double x = (B2 * C1 - B1 * C2) / delta;
            double y = (A1 * C2 - A2 * C1) / delta;
            point[0] = x;
            point[1] = y;
            Console.WriteLine(x + " bla " + y);

            return point;

        }

        // zanjduje najbardziej wysuniêty x na lewo i prawo, sposób na oko³o do sprawdzania przecinania siê prostej z odcinkiem :(
        static public int[] PointsOfLine(Point[] points)
        {
            int[] pointsBorder = new int[2];
            pointsBorder[0] = pointsBorder[1] = points[0].x;

            for (int i = 1; i < points.Length; i++)
            {
                if ( points[i].x < pointsBorder[0] )
                    pointsBorder[0] = points[i].x;
                else if ( points[i].x> pointsBorder[1] )
                    pointsBorder[1] = points[i].x;
            }
            pointsBorder[0]--;
            pointsBorder[1]++;

            return pointsBorder;
        }

        public static void abc(Point[] points, Point[] extremas)
        {
            Point[] founded = new Point[points.Length];
            int j = 0;
            var foundedIn = Przecinanie1(extremas, points);
            double[] sas = new double[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].y >= extremas[1].y && points[i].y <= extremas[0].y)
                {
                    founded[j] = points[i];
                    Console.WriteLine(points[i]);
                    j++;
                }
                //else if (points[i].y > extremas)
            }
            var przec = Przecinanie1(extremas, points);



            double obwod = 0;
            for (int i = 0; i < j; i++)
            {
                if (founded[i].x == extremas[0].x && founded[i].y == extremas[0].y && founded[(i+1 +j) % j].y  != e)
                {
                    //obwod =
                }
            }

        }

        public static double LengthSegment(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2));
        }

        public static double LengthSegment(Point a, double[] b)
        {
            return Math.Sqrt(Math.Pow(a.x - b[0], 2) + Math.Pow(a.y - b[1], 2));
        }


        static void Main(string[] args)
        {

            var x = ReadFile();
            //Console.WriteLine(x[7]);
            for (int i = 0; i < x.Length; i++)
            {
                Console.WriteLine(x[i]);

            }

            var foundExtrems = FindMaxMin(x);

            if(IsKernelExist(foundExtrems));
                Console.WriteLine("Min" + foundExtrems[0] +"Max "+foundExtrems[1]);


            var asd = PointsOfLine(x);
            Console.WriteLine("Min- " + asd[0] + " Max- " + asd[1]);

            Przecinanie1(foundExtrems, x);
            //abc(x, foundExtrems);

            // int[,] m = new int[3, 3];
            // m = MakeMatrix(new Point(2, 0), new Point(4, 2), new Point(2, 4));
            // Console.WriteLine( OrientationRight(m));

            //prz(new Point(3, 2), new Point(3, 20), new Point(-1, 4), new Point(4, 4));
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //        Console.Write(m[i, j]);
            //    Console.WriteLine();
            //}
            Console.ReadKey();
        }
    }
}
