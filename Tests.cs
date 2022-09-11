using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blossom_Algorithm
{
    class TestModule
    {
        //SAMPLEDATA

        static Node one = new Node(1);
        static Node two = new Node(2);
        static Node three = new Node(3);
        static Node four = new Node(4);
        static Node five = new Node(5);
        static Node six = new Node(6);


        static Edge a = new Edge(one, two, 1);
        static Edge b = new Edge(three, four, 2);
        static Edge c = new Edge(three, four, 3);
        static Edge d = new Edge(one, three, 4);

        static Edge first = new Edge(one, two, 5);
        static Edge second = new Edge(two, three, 6);
        static Edge third = new Edge(three, four, 7);
        static Edge fourth = new Edge(four, five, 8);
        static Edge fifth = new Edge(five, six, 9);
        static Edge sixth = new Edge(six, one, 10);

        static Matching validMatching = new Matching(new List<Edge>() { a, b });
        static Matching badMatching = new Matching(new List<Edge>() { c, b });
        static Matching badMatching2 = new Matching(new List<Edge>() { c, d });
        static Matching badMatching3 = new Matching(new List<Edge>() { a, b, a, d });
        static Matching badMatching4 = new Matching(new List<Edge>() { a, a });
        static Matching validMatching2 = new Matching(new List<Edge>() { });

        static Matching possibleAugPath = new Matching(new List<Edge>() { second, fourth });
        static Matching possibleAugPath2 = new Matching(new List<Edge>() { fourth });
        static Matching impossibleAugPath = new Matching(new List<Edge>() { first, fourth });
        static Matching impossibleAugPath2 = new Matching(new List<Edge>() { fifth, fourth });
        static Matching impossibleAugPath3 = new Matching(new List<Edge>() { third, fourth, sixth });


        //END OF SAMPLE DATA




        public static void CheckIfParsingIsCorrect()
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

        public static void TestMatching_EdgesHaveNoCommonVertex()
        {
            

            Matching testMatching = new Matching();
            if (Matching.EdgesHaveNoCommonVertex(a, b) == false) Console.WriteLine("1 EdgesHaveNoCommonVertex doesnt work properly");
            if (Matching.EdgesHaveNoCommonVertex(a, c) == false) Console.WriteLine("2 EdgesHaveNoCommonVertex doesnt work properly");
            if (Matching.EdgesHaveNoCommonVertex(b, a) == false) Console.WriteLine("3 EdgesHaveNoCommonVertex doesnt work properly");
            if (Matching.EdgesHaveNoCommonVertex(b, c) == true) Console.WriteLine("4 EdgesHaveNoCommonVertex doesnt work properly");
            if (Matching.EdgesHaveNoCommonVertex(d, c) == true) Console.WriteLine("5 EdgesHaveNoCommonVertex doesnt work properly");

            Console.WriteLine("The fucntion EdgesHaveNoCommonVertex works properly.\n\n");
        }

        public static void TestMatching_MatchingIsValid()
        {

            if (!validMatching.MatchingIsValid() || !validMatching2.MatchingIsValid() || 
                badMatching.MatchingIsValid() || badMatching2.MatchingIsValid() || 
                badMatching3.MatchingIsValid() || badMatching4.MatchingIsValid())
            {
                Console.WriteLine("There is an issue in this function. Debug.");
                if (!validMatching.MatchingIsValid()) Console.WriteLine("1 MatchingIsValid has issues.");
                if (!validMatching2.MatchingIsValid()) Console.WriteLine("2 MatchingIsValid has issues.");
                if (badMatching.MatchingIsValid()) Console.WriteLine("3 MatchingIsValid has issues.");
                if (badMatching2.MatchingIsValid()) Console.WriteLine("4 MatchingIsValid has issues.");
                if (badMatching3.MatchingIsValid()) Console.WriteLine("5 MatchingIsValid has issues.");
                if (badMatching4.MatchingIsValid()) Console.WriteLine("6 MatchingIsValid has issues.");

            } else Console.WriteLine("The function MatchingIsValid Works properly.\n\n");


            



        }

        public static void TestMatching_PathIsAugmenting()
        {
            bool res1T = possibleAugPath.PathIsAugmenting(new List<Edge> { first, second, third, fourth, fifth });
            bool res2T = possibleAugPath2.PathIsAugmenting(new List<Edge> { third, fourth, fifth });
                
            bool res3F = impossibleAugPath.PathIsAugmenting(new List<Edge> { first, second, third, fourth });
            bool res4F = impossibleAugPath2.PathIsAugmenting(new List<Edge> { first, second, third, fourth, fifth });
            bool res5F = impossibleAugPath3.PathIsAugmenting(new List<Edge> { second, third, fourth, fifth, sixth, first });
            bool res6F = impossibleAugPath2.PathIsAugmenting(new List<Edge> { first, second, third, fourth, fifth, sixth, first });

            Console.WriteLine("Testing the Function Matching.PathIsAugmenting:\n");
            if (!res1T || !res2T) Console.WriteLine("Error!");
            else Console.WriteLine("Paths that should be augmenting are in fact augmenting");

            
            if (res3F || res4F || res5F || res6F) Console.WriteLine("Error!");
            else Console.WriteLine("Paths that should be not augmenting are in fact not augmenting. \n" +
                "The Function PathIsAugmenting works well.\n\n");




        }

        public static void TestMatching_ImproveWithAugmentingPath()
        {

            Console.WriteLine("Testing the function ImproveWithAugmentingPath:\n\n");
            possibleAugPath.ImproveWithAugmentingPath(new List<Edge>() { first, second,third,fourth,fifth});

            if (possibleAugPath.edgeSet.Contains(first) && possibleAugPath.edgeSet.Contains(third) && possibleAugPath.edgeSet.Contains(fifth))
            {
                Console.WriteLine("The matching contains the required edges.");
            }
            else Console.WriteLine("Something went wrong. Debug.");


            if (possibleAugPath.edges.Count() == 3) Console.WriteLine("And it does not contain any wrong edges. The function works well.\n\n");
            else { Console.WriteLine("Something went wrong. Debug"); }
        }
            
        public static void TestGraph_ContractBlossom()
        {

            string blossomGraph = System.IO.File.ReadAllText(@"TestData\Blossom.txt");
            Graph graph = new Graph("testing Graph");
            graph.ParseGraph(blossomGraph);

            List<Edge> blossom = new List<Edge>() { graph.edges[4], graph.edges[5], graph.edges[6], graph.edges[7], graph.edges[8]};


            Graph Contracted = graph.contractBlossom(blossom);
            Console.WriteLine(Contracted.ToString());


           






        }

        public static void TestGraph_DoubleBlossom()
        {
            string doubleBlossomGraph = System.IO.File.ReadAllText(@"TestData\DoubleBlossom.txt");
            Graph graph = new Graph("testing Graph");
            graph.ParseGraph(doubleBlossomGraph);
            Console.WriteLine(graph.ToString());

            List<Edge> blossom1 = new List<Edge>() { graph.edges[2], graph.edges[3], graph.edges[4] };


            Graph Contracted1 = graph.contractBlossom(blossom1);
            Console.WriteLine(Contracted1.ToString());
            foreach (Edge edge in Contracted1.edges) Console.WriteLine(edge.u.ToString() + " x " + edge.v.ToString() + " edge id: " + edge.id); ;

            List<Edge> blossom2 = new List<Edge>() { Contracted1.edges[1], Contracted1.edges[2], Contracted1.edges[3] };


            Graph Contracted2 = Contracted1.contractBlossom(blossom2);
            Console.WriteLine(Contracted2.ToString());


        }



        public static void Test_reconstructAugmentingPath()
        {
            string p5 = System.IO.File.ReadAllText(@"TestData\P5.txt");
            Graph graph = new Graph("testing Graph");
            graph.ParseGraph(p5);

            Console.WriteLine(graph);
            graph.nodes[0].successor = graph.nodes[0];
            graph.nodes[1].successor = graph.nodes[0];
            graph.nodes[2].successor = graph.nodes[1];
            graph.nodes[3].successor = graph.nodes[4];
            graph.nodes[4].successor = graph.nodes[4];

            List<Edge> path = graph.reconstructAugmentingPath(graph.nodes[3], graph.nodes[2]);

            foreach (Edge edge in path)
            {
                Console.WriteLine(edge.u.id.ToString() + edge.v.id.ToString());
            } 







        }

        public static void Test_GetEvenPortionOfBlossom() //WORKS WELL
        {
            string k3 = System.IO.File.ReadAllText(@"TestData\K3.txt");
            Graph graph = new Graph("testing Graph");
            graph.ParseGraph(k3);
            Console.WriteLine(graph.ToString());

            List<Edge> blossom = graph.edges;
            List<Edge> EvenPortion = graph.GetEvenPortionOfBlossom(graph.nodes[1], graph.nodes[0], blossom);
            foreach(Edge edge in EvenPortion) { Console.WriteLine(edge.id.ToString()); }
        }

        public static void Test_ContractionOfMatching()
        {
            string blossom = System.IO.File.ReadAllText(@"TestData\Blossom.txt");
            Graph graph = new Graph("testing Graph");
            graph.ParseGraph(blossom);

            List<Edge> Blossom = new List<Edge>() { graph.edges[4], graph.edges[5], graph.edges[6], graph.edges[7], graph.edges[8] };
            Graph Contracted = graph.contractBlossom(Blossom);

            Matching first = new Matching(new List<Edge>() { graph.edges[0], graph.edges[4], graph.edges[9], graph.edges[3]});
            Matching second = first.contractMatchingOnBlossom(Blossom, Contracted, graph);

            foreach(Edge edge in second.edges) Console.WriteLine(edge.id.ToString());
        }

        public static void Test_LiftingPathAllThreeCases()
        {
            string e6 = System.IO.File.ReadAllText(@"TestData\Blossom.txt");
            Graph graph = new Graph("testing Graph");
            graph.ParseGraph(e6);
            Console.WriteLine(graph.ToString());


            Matching matching = new Matching(new List<Edge>() { graph.edges[5] , graph.edges[8] });


            List<Edge> blossom = new List<Edge>() { graph.edges[4], graph.edges[5], graph.edges[6], graph.edges[7], graph.edges[8] };
            Graph Contracted = graph.contractBlossom(blossom);

            List<Edge> path1 = new List<Edge>() { Contracted.edgeIDMapping[0], Contracted.edgeIDMapping[1], Contracted.edgeIDMapping[2] };
            List<Edge> path3 = new List<Edge>() { Contracted.edgeIDMapping[0], Contracted.edgeIDMapping[1], Contracted.edgeIDMapping[2], Contracted.edgeIDMapping[3], Contracted.edgeIDMapping[9] };
            List<Edge> path2 = new List<Edge>() { Contracted.edgeIDMapping[1], Contracted.edgeIDMapping[2], Contracted.edgeIDMapping[3] };

            List<Edge> lifted = graph.LiftPath(path1, blossom, Contracted, matching);
            List<Edge> lifted3 = graph.LiftPath(path3, blossom, Contracted, matching);
            List<Edge> lifted2 = graph.LiftPath(path2, blossom, Contracted, matching);

            foreach (Edge edge in lifted) Console.WriteLine(edge.ToString());
            foreach (Edge edge in lifted2) Console.WriteLine(edge.ToString());
            foreach (Edge edge in lifted3) Console.WriteLine(edge.ToString());
        }
    }

     
}
