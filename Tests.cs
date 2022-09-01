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


        static Edge a = new Edge(one, two);
        static Edge b = new Edge(three, four);
        static Edge c = new Edge(three, four);
        static Edge d = new Edge(one, three);

        static Edge first = new Edge(one, two);
        static Edge second = new Edge(two, three);
        static Edge third = new Edge(three, four);
        static Edge fourth = new Edge(four, five);
        static Edge fifth = new Edge(five, six);
        static Edge sixth = new Edge(six, one);

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
    }

     
}
