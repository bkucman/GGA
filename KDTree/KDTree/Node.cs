using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDTree
{
    public class Node
    {
        private double id;
        private string typeOfNode;
        private Node left;
        private Node right;
        private Point point;
        private Node parent;
        private Region region;

        public Node(double id,string typeOfNode, Node left,Node right,Point point)
        {
            this.id = id;
            this.typeOfNode = typeOfNode;
            this.left = left;
            this.right = right;
            //this.parent = parent;
            this.point = point;
            //this.Region = new Region(Double.MinValue, Double.MinValue, Double.MaxValue, Double.MaxValue);
        }
        public static Node NodeWithCreateFamili(double id, string typeOfNode, Node left, Node right,Point point)
        {
            Node newNode = new Node(id, typeOfNode, left, right, point);
            if(left != null)
            {
                left.parent = newNode;
                newNode.left = left;
            }
            if (right != null)
            {
                right.parent = newNode;
                newNode.right = right;
            }
            return newNode;
        }

        public double Id { get => id; set => id = value; }
        public Node Left { get => left; set => left = value; }
        public Node Right { get => right; set => right = value; }
        public Point Point { get => point; set => point = value; }
        public Node Parent { get => parent; set => parent = value; }
        public string TypeOfNode { get => typeOfNode; set => typeOfNode = value; }
        public Region Region { get => region; set => region = value; }

        public Boolean IsLeaf()
        {
            return this.left == null && this.right == null;
        }
        public Boolean IsLefSon()
        {
            return this == this.parent.left;
        }
        public Boolean IsRightSon()
        {
            return this == this.parent.right;
        }
        public void SetRegion(Region region)
        {
            Region n = new Region(region.MinX, region.MinY, region.MaxX, region.MaxY);
            //this.region.MinX = region.MinX;
            //this.region.MinY = region.MinY;
            //this.region.MaxX = region.MaxX;
            //this.region.MaxY = region.MaxY;
            this.region = n;
        }

}
    }
