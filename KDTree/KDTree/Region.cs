using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDTree
{
    public class Region
    {
        double minX;
        double minY;
        double maxX;
        double maxY;

        public double MinX { get => minX; set => minX = value; }
        public double MinY { get => minY; set => minY = value; }
        public double MaxX { get => maxX; set => maxX = value; }
        public double MaxY { get => maxY; set => maxY = value; }

        public Region(double minX, double minY, double maxX, double maxY)
        {
            this.minX = minX;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;
        }
        public Region(Region region)
        {
            this.minX = region.minX;
            this.minY = region.minY;
            this.maxX = region.maxX;
            this.maxY = region.maxY;
        }

        public Region()
        {
        }

        public override string ToString()
        {
            return "miX "+minX + ", minY "+minY+", maxX "+maxX+", maxY "+maxY;
        }

    }
}
