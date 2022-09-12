using System;
using System.Linq;

namespace Blossom_Algorithm
{
    class Program
    {
        public static Matching FindMaximumMatching(Graph g, Matching m)
        {
            
            List<Edge> augmentingPath = m.FindAugmentingPath(g);

            if (augmentingPath.Count > 0)
            {
                m.ImproveWithAugmentingPath2(augmentingPath);
                Console.WriteLine(".");


                
                return FindMaximumMatching(g, m);
            }
            else return m;
        }



        static void Main(string[] args)
        {
            //TestModule.RunAllTests();
            //Uncomment if you want to check the functionality.
            //Unit tests for functions are stored in the class Tests.cs



            RandomGraphGenerator gen = new RandomGraphGenerator();

            Console.WriteLine("This is the Edmonds Blossom Algorithm. It computes the maximum matching on a given graph.");
            Console.WriteLine("On which graph would you like to run the algorithm?\nA python script will visualize the resulting edges computed by this C# program.");
            Console.WriteLine("1. Path\n2. Complete Graph with odd number of vertices\n3. Complete Bipartite Graph\n4. Graph which will contain a Blossom\n5. Graph which will contain two blossoms (or a double blossom)\n6. Empty Graph\n7. A randomly Generated Graph");
            int choice = int.Parse(Console.ReadLine());
            
            string e6 = "";


            switch (choice)
            {
                case 1:
                    e6 = System.IO.File.ReadAllText(@"TestData/P5.txt");
                    break;
                case 2:
                    e6 = System.IO.File.ReadAllText(@"TestData/K7.txt");
                    break;
                case 3:
                    e6 = System.IO.File.ReadAllText(@"TestData/K4-3.txt");
                    break;
                case 4:
                    e6 = System.IO.File.ReadAllText(@"TestData/Blossom.txt");
                    break;
                case 5:
                    e6 = System.IO.File.ReadAllText(@"TestData/DoubleBlossom.txt");
                    break;
                case 6:
                    e6 = System.IO.File.ReadAllText(@"TestData/Empty6.txt");
                    break;
                case 7:
                    gen.GetInput();
                    gen.GenerateGraph();
                    e6 = System.IO.File.ReadAllText(@"input.txt");
                    break;
                default:
                    Console.WriteLine("Bad input.");
                    break;



            }
            


            //Change the input file if you want to run it on your own graph.

            Graph graph = new Graph("testing Graph");
            graph.ParseGraph(e6);

            Matching matching = new Matching();
            Matching final = FindMaximumMatching(graph, matching);          
            Console.WriteLine(final.edges.Count());

            gen.EncodeResult(final, graph);











        }

    }




}
