using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDTree
{
    public class KDTree
    {
        private double MedianX(List<Point> points)
        {
            int middleElement = points.Count / 2;
            if (points.Count % 2 == 0)
            {
                return (points[middleElement].GetX() + points[middleElement - 1].GetX()) / 2;
            }
            else
            {
                return points[middleElement].GetX();
            }
        }
        private double MedianY(List<Point> points)
        {
            int middleElement = points.Count / 2;
            if (points.Count % 2 == 0)
            {
                return (points[middleElement].GetY() + points[middleElement - 1].GetY()) / 2;

            }
            else
            {
                return points[middleElement].GetY();
            }
        }

        public Node BuildKdTree(List<Point> pointsX,List<Point> pointsY, int depth)
        {
            double median;
            //Console.WriteLine(pointsX.Count);
            if (pointsX.Count == 0) return null;

            if (pointsX.Count == 1)
            {
               // Console.WriteLine(pointsX.ElementAt(0)+" xx "+ pointsY.ElementAt(0));
                return new Node(0, "leaf", null, null, pointsX.ElementAt(0));
            }
                
            if(depth % 2 == 0)
            {
                median = MedianX(pointsX);
            }
            else
            {
                median = MedianY(pointsY);
            }
            List<Point> leftSideX = new List<Point>();
            List<Point> rightSideX = new List<Point>();
            List<Point> leftSideY = new List<Point>();
            List<Point> rightSideY = new List<Point>();

            for (int i = 0; i < pointsY.Count; i++)
                {
                    if (depth % 2 == 0)
                    {
                        if (pointsX.ElementAt(i).GetX() <= median)
                            leftSideX.Add(pointsX.ElementAt(i));
                        else
                            rightSideX.Add(pointsX.ElementAt(i));

                        if (pointsY.ElementAt(i).GetX() <= median)
                            leftSideY.Add(pointsY.ElementAt(i));
                        else
                            rightSideY.Add(pointsY.ElementAt(i));
                    }
                    else
                    {
                        if (pointsY.ElementAt(i).GetY() <= median)
                            leftSideY.Add(pointsY.ElementAt(i));
                        else
                            rightSideY.Add(pointsY.ElementAt(i));

                        if (pointsX.ElementAt(i).GetY() <= median)
                            leftSideX.Add(pointsX.ElementAt(i));
                        else
                            rightSideX.Add(pointsX.ElementAt(i));
                    }
                }
                

            if(depth % 2 == 0)
            {
                return Node.NodeWithCreateFamili(median, "vertical", BuildKdTree(leftSideX,leftSideY, depth + 1), BuildKdTree(rightSideX,rightSideY, depth + 1), null);
            }
            else
            {
                return Node.NodeWithCreateFamili(median, "horizontal", BuildKdTree(leftSideX, leftSideY, depth + 1), BuildKdTree(rightSideX, rightSideY, depth + 1), null);
            }
        }
        //// Dla liści nie ustawiam !!!!!!!!! i dla rote też nie 
        public Node MakeAreaBlock(Node node)
        {
            if (!node.IsLeaf())
            {
                if(node.Parent != null){
                    node.SetRegion(node.Parent.Region);
                    if(node.Region != null)
                    {
                        Region newRegion = node.Region;
                        if(node.TypeOfNode == "horizontal")
                        {
                            if (node.IsLefSon())
                            {
                                newRegion.MaxX = node.Parent.Id;
                            }
                            else
                            {
                                newRegion.MinX = node.Parent.Id;
                            }
                        }
                        if (node.TypeOfNode == "vertical")
                        {

                            if (node.IsLefSon())
                            {
                                newRegion.MaxY = node.Parent.Id;

                            }
                            else
                            {
                                newRegion.MinY = node.Parent.Id;
                            }
                        }
                        node.Region = newRegion;
                       // Console.WriteLine(node.Region);
                    }
                }
                //Console.WriteLine(node.Region);
                if (node.Left != null)
                    MakeAreaBlock(node.Left);
                if (node.Right != null)
                    MakeAreaBlock(node.Right);
            }
            return node;
        }

        public void KdTreeSearch(Node node,Region region)
        {
            Console.WriteLine(node.TypeOfNode);
            // pierwsz krok jak liść
            if (node.IsLeaf())
            {
                CheckLeafInRegion(node.Point, region);
            }
            else 
            {
                if (node.Left != null)
                {
                    if (node.Left.IsLeaf())
                    {
                        KdTreeSearch(node.Left, region);
                    }else
                    if (FullyRegionIncluded(node.Left.Region, region))
                    {
                        ReportSubTree(node.Left);
                    }
                    else if (IntersectRegionBySon(node.Left.Region, region))
                    {
                        KdTreeSearch(node.Left, region);
                    }
   
                }
                if (node.Right != null)
                {
                    if (node.Right.IsLeaf())
                    {
                        KdTreeSearch(node.Right, region);
                    }
                    else
                    if (FullyRegionIncluded(node.Right.Region, region))
                    {
                        ReportSubTree(node.Right);
                    }
                    else if (IntersectRegionBySon(node.Right.Region, region))
                    {
                        KdTreeSearch(node.Right, region);
                    }

                }
            }
        }

        private bool IntersectRegionBySon(Region node, Region region)
        {
            //if(!node.IsLeaf())

            if (node != null)
            {
                // jeśli minimalny x z obszaru jest większy od maksymalnego x node to nie, R ma node po lewej
                // jeśli maksymalny x z obszaru jest mniejszy od minimalnego x node to nie, R ma go po prawej
                if (region.MinX > node.MaxX || region.MaxX < node.MinX)
                {
                    return false;
                }
                // jeśli minimalny y z obszaru jest większy od maksymalnego y node to nie, R ma node pod sobą
                // jeśli maksymalny y z obszaru jest mniejszy od minimalnego y node to nie, R ma node na sobą
                if (region.MinY > node.MaxY || region.MaxY < node.MinY)
                {
                    return false;
                }
                return true;
            }
            return false;

        }

        private void ReportSubTree(Node node)
        {
            Console.WriteLine("Zawiera całkowicie poddrzewo");
            PrintTree(node,1);
            Console.WriteLine("__________________________________________________");
        }

        private bool FullyRegionIncluded(Region nodeRegion, Region region)
        {
            // bo moż być liściem
            if(nodeRegion != null)
            {
               // if (nodeRegion.MinX >= region.MinX && nodeRegion.MaxX <= region.MaxX &&
                  //  nodeRegion.MinY >= region.MinY && nodeRegion.MaxY <= region.MaxY)
                if (nodeRegion.MinX >= region.MinX && nodeRegion.MaxX <= region.MaxX &&
                    nodeRegion.MinY >= region.MinY && nodeRegion.MaxY <= region.MaxY)
                        return true;
            }

            return false;
        }

        private void CheckLeafInRegion(Point point, Region region)
        {
            if (point.GetX() >= region.MinX && point.GetX() <= region.MaxX
                && point.GetY() >= region.MinY && point.GetY() <= region.MaxY)
                Console.WriteLine(point + " included");
        }
        public void PrintTree(Node node,int c)
        {
            if (!node.IsLeaf())
            {
                if(c!=0) Console.WriteLine("Węzeł" + ", (" + node.TypeOfNode + ") ID: " + node.Id + " Parent " + node.Parent.Id + " Region "+node.Region);
                else Console.WriteLine("Węzeł" + ", (" + node.TypeOfNode + ") ID: " + node.Id + " Root");
                c++;
                if (node.Left != null) PrintTree(node.Left,c);
                if (node.Right != null) PrintTree(node.Right,c);
            }
            else
            {
                Console.WriteLine(node.Point + " (" + node.TypeOfNode + ") Parent " + node.Parent.Id);
            }
        }



    }

}
