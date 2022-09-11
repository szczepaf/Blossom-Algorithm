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
                m.ImproveWithAugmentingPathExperimental(augmentingPath);


                
                return FindMaximumMatching(g, m);
            }
            else return m;
        }



        static void Main(string[] args)
        {
            /*
            TestModule.TestMatching_EdgesHaveNoCommonVertex();
            TestModule.TestMatching_MatchingIsValid();
            TestModule.TestMatching_PathIsAugmenting();
            TestModule.TestMatching_ImproveWithAugmentingPath();
            TestModule.TestGraph_ContractBlossom();
            */

            //TestModule.TestGraph_DoubleBlossom();
            //TestModule.Test_contractMatchingOnBlossom();

            //TestModule.Test_getAugmentingPath();
            //TestModule.Test_GetEvenPortionOfBlossom();
            //TestModule.Test_ContractionOfMatching();



            RandomGraphGenerator gen = new RandomGraphGenerator();


            gen.GetInput();
            gen.GenerateGraph();

            string e6 = System.IO.File.ReadAllText(@"input.txt");
            //string e6 = System.IO.File.ReadAllText("TestData/K7.txt");

            Graph graph = new Graph("testing Graph");
            graph.ParseGraph(e6);

            Matching matching = new Matching();
            Matching final = FindMaximumMatching(graph, matching);
           




            Console.WriteLine(": --- )");
            if (!final.MatchingIsValid())
            {
                Console.WriteLine("OH NO YOU SHOULD GET A BREAK HONESTLY BRUV GOOD WORK THOUGH");
            }

           

            
            
            
            
            
            
            
            Console.WriteLine(final.edges.Count());











        }

    }




}
