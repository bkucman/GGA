using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvexHull
{
    public class Point : IComparable
    {
        private int x, y;
        private double a;
        private Point min;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;

        }
        public Point(Point a)
        {
            this.x = a.x;
            this.y = a.y;

        }
        public void setPoint(Point a)
        {
            this.x = a.x;
            this.y = a.y;
        }
        public int GetX()
        {
            return x;
        }
        public int GetY()
        {
            return y;
        }
        public void SetA(double a,Point min)
        {
            
            this.min = min;
            this.a = a;
        }
        public double GetA()
        {
            return this.a;
        }

        public override string ToString()
        {
            return "Point(" + x + "," + y + ")" + a;
        }
        

        public double GetDistance(Point point)
        {
            return Math.Pow(point.GetX() - this.x, 2) + Math.Pow(point.GetY() - this.y, 2);
        }
        // 1 - po y, else po x
        public int CompareTo(object obj, int v)
        {
            if (obj is Point)
            {
                if (v == 1)
                {
                    if (this.y == (obj as Point).GetY())
                        return this.x.CompareTo((obj as Point).GetX());
                    return this.y.CompareTo((obj as Point).GetY());
                }
                if(v == 2)
                {
                    if (this.a == (obj as Point).GetA())
                        return this.GetDistance(min).CompareTo((obj as Point).GetDistance(min));
                    return this.a.CompareTo((obj as Point).GetA());
                }
                else
                {
                    if (this.x == (obj as Point).GetX())
                        return this.y.CompareTo((obj as Point).GetY());
                    return this.x.CompareTo((obj as Point).GetX());
                }
            }
            throw new ArgumentException("Object is not a Point");
        }

        public int CompareTo(object obj)
        {
            if (obj is Point)
            {
                if (this.x == (obj as Point).GetX())
                    return this.y.CompareTo((obj as Point).GetY());
                return this.x.CompareTo((obj as Point).GetX());
            }
            throw new ArgumentException("Object is not a Point");
        }
    }
}
