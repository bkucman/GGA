using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDTree
{
    class Program
    {
        public static int CompareY(Point a, Point b)
        {
            return a.CompareTo(b, 1);
        }

        static void Main(string[] args)
        {
            string file = "..\\..\\..\\files\\points3.csv";

            //Console.WriteLine(al[0]);
            var pointsX = DataReader.ReadFile(file);
            var pointsY = DataReader.ReadFile(file);
            //// Sortowanie //////////////////////////////////////
            pointsX.Sort(); // tablica po x
            pointsY.Sort(CompareY); // tablica po y
            //////////////////////////////////////////////////////
            ///
            Console.WriteLine("Po X");
            for (int i = 0; i < pointsX.Count; i++)
                Console.WriteLine(pointsX[i]);
            Console.WriteLine("____________________");
            Console.WriteLine("Po Y");
            for (int i = 0; i < pointsY.Count; i++)
                Console.WriteLine(pointsY[i]);
            Console.WriteLine("____________________");

            //// Budowanie drzewa/////////////////////////////////
            KDTree treeBuilder = new KDTree();
            var tree = treeBuilder.BuildKdTree(pointsX,pointsY, 0);
            treeBuilder.PrintTree(tree,0);
            ///////////////////////////////////////////////////////
            //// Ustawianie obszarów //////////////////////////////
            Region r = new Region(Double.MinValue, Double.MinValue, Double.MaxValue, Double.MaxValue);
            tree.SetRegion(r);
            tree = treeBuilder.MakeAreaBlock(tree);
            treeBuilder.PrintTree(tree, 0);
            ///////////////////////////////////////////////////////
            //// Obszar poszukiwań ////////////////////////////////
            //Region searchRegion = new Region(13, 14, 13, 23);
            Region searchRegion = new Region(7, 13, 11, 21);
            Console.WriteLine("Poszukiwania");
            treeBuilder.KdTreeSearch(tree, searchRegion);
            //////////////////////////////////////////////////////
            ///
            PrintTree.Print(tree);
            Console.WriteLine("Hello World!"+ Double.MaxValue);
            Console.ReadKey();

        }
    }
}
