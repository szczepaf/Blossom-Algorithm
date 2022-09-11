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
            bool isvalid = true;
            foreach (Edge edge in edges)
            {
                foreach (Edge edgeI in edges)
                {
                    if (edge != edgeI && !EdgesHaveNoCommonVertex(edge, edgeI)) {
                        Console.WriteLine("THE EDGES IN COMMON ARE : " + edge.ToString() + edgeI.ToString());
                        isvalid = false;
                        
                        
                    }
                }

            }

            return isvalid;
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

            
            //INIT
            HashSet<Node> F = new HashSet<Node>();
            foreach (Edge edge in g.edges)
            {
                F.Add(edge.u);
                F.Add(edge.v);

                edge.u.height = -1;
                edge.u.successor = null;

                edge.v.height = -1;
                edge.v.successor = null;
            }

            foreach (Edge edge in this.edges)
            {
                F.Remove(edge.u);
                F.Remove(edge.v);
            }

            foreach(Node root in F)
            {
                root.successor = root;
                root.height = 0;
            }
            //END OF INIT

            while (F.Count() > 0)
            {
                Node x = F.First();
                F.Remove(x);

                if(x.height % 2 == 1) //is not unbound, free
                {
                    foreach(Node neighbour in x.neigbours)
                    {
                        Edge lookedFor = g.edgeNodeMap[(x, neighbour)];
                        if (this.edgeSet.Contains(lookedFor))
                        {
                            if (neighbour.height == -1)
                            {
                                neighbour.height = x.height + 1;
                                neighbour.successor = x;
                                F.Add(neighbour);
                            }
                            else
                            {                                       //Augmenting Path
                                Node parent1 = Node.getParent(x);
                                Node parent2 = Node.getParent(neighbour);
                                if (parent1 != parent2)
                                {
                                    List<Edge> path = g.reconstructAugmentingPath(x, neighbour);
                                    
                                    return path;
                                }
                                else
                                {                                   //Blossom
                                    


                                    (List<Edge> flower, List<Edge> blossom) = g.reconstructBlossom(x, neighbour);

                                    //XX
                                    Graph contractedGraph = g.contractBlossom(flower);
                                    Matching contractedMatching = this.contractMatchingOnBlossom(flower, contractedGraph, g);

                                    List<Edge> path = contractedMatching.FindAugmentingPath(contractedGraph);

                                   

                                    

                                    List<Edge> liftedPath = g.LiftPath(path, blossom, contractedGraph, this); //MAYBE LIFT WITH FLOWER?
                                   
                                    return liftedPath;
                                }

                            }
                        }
                        
                    }
                }
                else if (x.height % 2 == 0)
                {
                    foreach (Node neighbour in x.neigbours)
                    {
                        if (!this.edgeSet.Contains(g.edgeNodeMap[(neighbour, x)]))
                        {
                            if (neighbour.height == -1)
                            {
                                neighbour.height = x.height + 1;
                                neighbour.successor = x;
                                F.Add(neighbour);
                            }
                            else if (neighbour.height % 2 == 0)
                            {                                               //Found Augmenting Path
                                Node parent1 = Node.getParent(x);
                                Node parent2 = Node.getParent(neighbour);
                                if (parent1 != parent2)
                                {

                                    List<Edge> path = g.reconstructAugmentingPath(x, neighbour);
                                    
                                    return path;
                                }
                                else {

                                    (List<Edge> flower, List<Edge> blossom) = g.reconstructBlossom(x, neighbour);

                                    //XX
                                    Graph contractedGraph = g.contractBlossom(flower);
                                    Matching contractedMatching = this.contractMatchingOnBlossom(flower, contractedGraph, g);

                                    List<Edge> path = contractedMatching.FindAugmentingPath(contractedGraph);



                                    
                                    

                                    List<Edge> liftedPath = g.LiftPath(path, blossom, contractedGraph, this); //MAYBE LIFT WITH FLOWER?
                                
                                    return liftedPath;

                                }
                            }
                        }
                    }
                }

            }



            Console.WriteLine("M is the biggest");
            return new List<Edge>();
        }

        public List<Edge> FindAugmentingPath2(Graph g)
        {


            //INIT
            
            //END OF INIT


            return new List<Edge>();
        }


        public Matching ImproveWithAugmentingPath(List<Edge> augmentingPath)
        {
            

            for (int i = 1; i < augmentingPath.Count; i += 2)
            {
                this.edgeSet.Remove(augmentingPath[i]);
                this.edges.Remove(augmentingPath[i]);
            }

            for (int j = 0; j < augmentingPath.Count; j += 2)
            {
                this.edges.Add(augmentingPath[j]);
                this.edgeSet.Add(augmentingPath[j]);
            }


            if (!this.MatchingIsValid()) throw new Exception("Error: Matching is not valid!");

            return this;
        }

        public Matching ImproveWithAugmentingPathExperimental(List<Edge> augmentingPath)
        {
            List<Edge> toDelete = new List<Edge>();
            List<Edge> toAdd = new List<Edge>();
            foreach(Edge edge in augmentingPath)
            {
                if (this.edgeSet.Contains(edge)) toDelete.Add(edge);
                else toAdd.Add(edge);
            }

            foreach (Edge edge in toDelete)
            {
                edgeSet.Remove(edge);
                edges.Remove(edge);
            }

            foreach (Edge edge in toAdd)
            {
                edgeSet.Add(edge);
                edges.Add(edge);
            }

            this.RepairMatching();
            return this;
        }
        public void RepairMatching()
        {
            HashSet<Edge> toRemove = new HashSet<Edge>();
            foreach (Edge edge in edges)
            {
                foreach (Edge edgeI in edges)
                {
                    if (edge != edgeI && !EdgesHaveNoCommonVertex(edge, edgeI))
                    {
                        toRemove.Add(edge);
                        Console.WriteLine("MENDING");

                    }
                }

            }

            foreach(Edge edge in toRemove)
            {
                this.edgeSet.Remove(edge);
                this.edges.Remove(edge);
            }
        }
        public Matching contractMatchingOnBlossom(List<Edge> blossom, Graph Contracted, Graph Original) //TEST
        {
            Matching contractedMatching = new Matching();
            foreach (Edge edge in this.edges)
            {
                if (!blossom.Contains(edge)) {
                    if (Contracted.edgeIDMapping.ContainsKey(edge.id)){ 
                        Edge equivalent = Contracted.edgeIDMapping[edge.id];
                        contractedMatching.AddEdge(equivalent); 
                    }
                }
            }

            return contractedMatching;
        }

        public Node GetBlossomBase(List<Edge> Blossom)
        {
            Dictionary<Node, int> occurances = new Dictionary<Node, int>();
            HashSet<Node> nodesInBlossom = new HashSet<Node>(); 

            foreach (Edge e in Blossom)
            {
                nodesInBlossom.Add(e.u);
                nodesInBlossom.Add(e.v);
            }

            foreach (Node node in nodesInBlossom) occurances.Add(node, 0);
            foreach (Edge e in Blossom)
            {
                if (this.edgeSet.Contains(e))
                {
                    occurances[e.u] += 1;
                    occurances[e.v] += 1;
                }
            }

            Node blossomBase = occurances.MinBy(kvp => kvp.Value).Key;

            return blossomBase;






        }


    }

}
