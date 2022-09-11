using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blossom_Algorithm
{
    
    class RandomGraphGenerator
    {
        public int edgeCount;
        public int nodeCount;


        public void GetInput()
        {
            Console.WriteLine("enter number of vertices");
            int nodeCountCandidate = int.Parse(Console.ReadLine());
            Console.WriteLine("enter number of edges");
            int edgeCountCandidate = int.Parse(Console.ReadLine());

            if (nodeCountCandidate < 0 || edgeCountCandidate < 0)
            { 
                Console.WriteLine("cant have negative number of edges or nodes");
                GetInput();
            }
            else if (edgeCountCandidate > (nodeCountCandidate * (nodeCountCandidate - 1)) / 2)
            {
                Console.WriteLine("Too many edges. n*(n-1)/2 is the max (combinatorics bruv).");
                GetInput();
            }

            nodeCount = nodeCountCandidate;
            edgeCount = edgeCountCandidate;
        }

        public string parseEdge(HashSet<int> nodes)
        {
            List<int> nodesL = nodes.ToList();
            string parsedEdge = "<" + nodesL[0].ToString() + "," + nodesL[1].ToString() + ">";
            return parsedEdge;

        }
        public void GenerateGraph()
        {
            List<HashSet<int>> final = new List<HashSet<int>>(edgeCount);
            List<HashSet<int>> allPossibleEdges = new List<HashSet<int>>();

            for(int i = 0; i < this.nodeCount; i++)
            {
                for (int j = i +1; j < this.nodeCount; j++)
                {
                    allPossibleEdges.Add(new HashSet<int>(2) { i, j } );
                }
            }

            Random random = new Random();
            List<int> indices = Enumerable.Range(0, allPossibleEdges.Count()).OrderBy(x => random.Next()).Take(this.edgeCount).ToList();


            for (int i = 0; i < indices.Count(); i++)
            {
                final.Add(allPossibleEdges[indices[i]]);
            }
            using (StreamWriter writer = new StreamWriter("input.txt"))
            {
                writer.WriteLine(nodeCount.ToString());
                foreach (HashSet<int> edge in final) writer.WriteLine(parseEdge(edge));
                writer.Close();

            }



        }
    }
}
