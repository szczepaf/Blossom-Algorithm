using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blossom_Algorithm
{
    class Matching
    {
        public List<Edge> edges { get; private set; }
        public HashSet<Edge> edgeSet { get; private set; }

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

        public static bool EdgesHaveNoCommonVertex(Edge one, Edge two) 
        {
            if (one.u == two.u || one.u == two.v || one.v == two.u || one.v == two.v) return false;
            return true;
        }
        public bool MatchingIsValid() 
        {



            if (edges.Count != edges.Distinct().Count()) return false; //Has Duplicates

            foreach (Edge edge in edges)
            {
                foreach (Edge edgeI in edges)
                {
                    if (edge != edgeI && !EdgesHaveNoCommonVertex(edge, edgeI))  return false;
                }

            }

            return true;
        }

        public bool PathIsAugmenting(List<Edge> path)
        {

            if (path.Count != path.Distinct().Count()) return false; //Has Duplicates

            //XX Problem s tim, ze netestuji, jestli koncove vrcholy cesty jsou volne?


            //see definition of augmenting path if this function is not clear
            for (int i = 1; i < path.Count; i += 2)
            {
                if (!edgeSet.Contains(path[i])) return false;
            }

            for (int j = 0; j < path.Count; j += 2)
            {
                if (edgeSet.Contains(path[j])) return false;
            }
            return true;



        }
    
        public List<Edge> FindAugmentingPath(Graph g)
        {
            return null;
            //TODO
        }

        public void ImproveWithAugmentingPath(List<Edge> augmentingPath)
        {
            if (!this.PathIsAugmenting(augmentingPath))
            {
                throw new Exception("Error! Path is not Augmenting.");
            }

            for (int i = 1; i < augmentingPath.Count; i += 2)
            {
                edgeSet.Remove(augmentingPath[i]);
                edges.Remove(augmentingPath[i]);
            }

            for (int j = 0; j < augmentingPath.Count; j += 2)
            {
                edges.Add(augmentingPath[j]);
                edgeSet.Add(augmentingPath[j]);
            }

            if (!this.MatchingIsValid()) throw new Exception("Error: Matching is not valid!");
        }
    }

}
