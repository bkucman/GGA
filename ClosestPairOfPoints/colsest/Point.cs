using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosestPairOfPoints
{
    public class Point : IComparable
    {
        private int x, y;
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
        public int GetX()
        {
            return x;
        }
        public int GetY()
        {
            return y;
        }
        public override string ToString()
        {
            return "Point(" + x + "," + y +") ";
        }

        // 1 - po y, else po x
        public int CompareTo(object obj,int v)
        {
            if (obj is Point)
            {
                if (v == 1)
                {
                    if (this.y == (obj as Point).GetY())
                        return this.x.CompareTo((obj as Point).GetX());
                    return this.y.CompareTo((obj as Point).GetY());
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
