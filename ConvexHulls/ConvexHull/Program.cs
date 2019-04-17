using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDotNet;

namespace ConvexHull
{
    class Program
    {
        public static int CompareY(Point a, Point b)
        {
            return a.CompareTo(b, 1);
        }
        static void Main(string[] args)
        {
            string file = "..\\..\\..\\files\\";
            string fileName = "points.csv";
            ConvexHull xH = new ConvexHull();

            file = file + fileName;
            var points = DataReader.ReadFile(file);

            points.Sort(CompareY);

            Console.WriteLine("Po Y");
            for (int i = 0; i < points.Count; i++)
                Console.WriteLine(points[i]);
            Console.WriteLine("____________________");

            var polygon = xH.GrahamScan(points);

            var scriptpath = "D://Inforamtyka//Grafy//OtoczkaWypukla//otoczka//a.R";

            ExecuteScriptFile(scriptpath,fileName);
        }
        public static void ExecuteScriptFile(string scriptFilePath,string fileName)
        {
            var rpath = @"C:\Program Files\R\R-3.5.1\bin\x64";
            var rpath2 = @"C:\Program Files\R\R-3.5.1";
            REngine.SetEnvironmentVariables(rpath,rpath2);

            using (var en = REngine.GetInstance())
            {
                var args_r = new string[1] {fileName};
                var execution = "source('" + scriptFilePath + "')";
                en.SetCommandLineArguments(args_r);
                en.Evaluate(execution);
            }
        }
    }
}
