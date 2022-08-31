using System;
using System.Linq;

namespace Blossom_Algorithm
{
    class Program
    {
        static void TestIfParsingIsCorrect() 
        {
            string k3 = System.IO.File.ReadAllText(@"TestData\K3.txt");
            string k4 = System.IO.File.ReadAllText(@"TestData\K4.txt");
            string k7 = System.IO.File.ReadAllText(@"TestData\K7.txt");
            string k4_3 = System.IO.File.ReadAllText(@"TestData\K4-3.txt");
            string emptyGraph = System.IO.File.ReadAllText(@"TestData\Empty6.txt");
            string c5 = System.IO.File.ReadAllText(@"TestData\C5.txt");
            string blossom = System.IO.File.ReadAllText(@"TestData\Blossom.txt");

            Graph graph = new Graph("testing Graph");
            graph.ParseGraph(emptyGraph);
            Console.WriteLine(graph.ToString());
            graph.Clear();

            graph.ParseGraph(k7);
            Console.WriteLine(graph.ToString());
            graph.Clear();

            graph.ParseGraph(blossom);
            Console.WriteLine(graph.ToString());
            graph.Clear();


            graph.ParseGraph(c5);
            Console.WriteLine(graph.ToString());
            graph.Clear();


        }


        static void Main(string[] args)
        {
            TestIfParsingIsCorrect();
            






        }

    }




}
