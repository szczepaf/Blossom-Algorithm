using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blossom_Algorithm
{
    class Matching
    {
        List<Edge> edges;
        HashSet<Edge> edgeSet;

        public Matching()
        {
            edges = new List<Edge>();
            edgeSet = new HashSet<Edge>();
        }

        public Matching(List<Edge> ImportEdges)
        {
            edges = ImportEdges;
            edgeSet = edges.ToHashSet();
        }
        public void AddEdge(Edge edge)
        {
            edges.Add(edge);
            edgeSet.Add(edge);
        }

        public static bool EdgesHaveNoCommonVertex(Edge one, Edge two) //TODO test
        {
            if (one.u == two.u || one.u == two.v || one.v == two.u || one.v == two.v) return false;
            return true;
        }
        public bool MatchingIsValid() //TODO
        {
            bool matchingIsValid = true;

            foreach (Edge edge in edges)
            {
                foreach (Edge edgeI in edges)
                {
                    if (edge != edgeI && !EdgesHaveNoCommonVertex(edge, edgeI)) matchingIsValid = false;
                }

            }

            return matchingIsValid;
        }

        public bool PathIsAugmenting(List<Edge> path) //TODO write tests with assert - FINISH
        {
            bool isAugmenting = true;

            //see definition of augmenting path if this function is not clear
            for (int i = 1; i < path.Count; i += 2)
            {
                if (!edgeSet.Contains(path[i])) isAugmenting = false;
            }

            for (int j = 0; j < path.Count; j += 2)
            {
                if (edgeSet.Contains(path[j])) isAugmenting = false;
            }
            return isAugmenting;



        }
    
        public void FindAugmentingPath(Graph g)
        {
            //TODO
        }

        public void ImproveWithAugmentingPath(Graph g)
        {
            //TODO
        }
    }

}
