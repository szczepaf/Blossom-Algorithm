using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blossom_Algorithm
{
    class Graph
    {
        private string name;
        public List<Node> nodes { get; private set; }
        public List<Edge> edges { get; private set; }
        public Dictionary<int, Edge> edgeIDMapping { get; private set; }

        public Dictionary<(Node, Node), Edge> edgeNodeMap { get; private set; }

        public Graph(string name)
        {
            this.name = name;
            this.nodes = new List<Node>();
            this.edges = new List<Edge>();
            this.edgeIDMapping = new Dictionary<int, Edge>();
            this.edgeNodeMap = new Dictionary<(Node, Node), Edge>();

        }





        public Edge ParseEdge(string edgeString, int edgeId)
        {
            //INPUT FORMAT:
            //One Edge will be in the format: <onevertex,secondvertex>
            //eg: <2,3>
            //The whole graph will in the format: Number of vertices: Edges seperated by newlines
            //eg: 3\n<1,2>\n<0,2>\n
            //Indices of Vertices (Nodes) are 0-based
            //For example, the complete graph with 4 vertices, K4, would look like this:
            //4\n<0,1>\n<0,2>\n<0,3>\n<1,2>\n<1,3>\n<2,3>\n



            edgeString = edgeString.Replace("<", "");
            edgeString = edgeString.Replace(">", "");

            string[] ids = edgeString.Split(',');
            int id1 = int.Parse(ids[0]);
            int id2 = int.Parse(ids[1]);

            return new Edge(nodes[id1], nodes[id2], edgeId);

        }

        public Graph ParseGraph(string graphAsString)
        {

            string[] data = graphAsString.Split('\n');
            int verticesCount = int.Parse(data[0]);

            for (int i = 0; i < verticesCount; i++)
            {
                this.nodes.Add(new Node(i));
            }

            if (data.Length > 1)
            {
                int edgeIdCounter = 0;
                string[] edgeData = data.Skip(1).ToArray();

                foreach (string edgeInput in edgeData)
                {
                    if (!String.IsNullOrWhiteSpace(edgeInput))
                    {
                        Edge newEdge = ParseEdge(edgeInput, edgeIdCounter);
                        this.edges.Add(newEdge);
                        newEdge.u.neigbours.Add(newEdge.v);
                        newEdge.v.neigbours.Add(newEdge.u);
                    }
                    edgeIdCounter++;
                }
                this.createEdgeMapping();
            }

            return this;
        }

        //Debugging tools for printing out the graph:
        public override string ToString()
        {
            string graphInfo = this.name + ": \n";
            graphInfo += "Number of vertices: " + nodes.Count().ToString() + "\n";
            graphInfo += "Number of edges: " + edges.Count().ToString() + "\n";
            foreach (Node vertex in nodes)
            {
                graphInfo += vertex.ToString() + "\n";
            }

            return graphInfo;

        }

        public void Clear()
        {
            nodes.Clear();
            edges.Clear();
        }

        public Graph contractBlossom(List<Edge> Blossom)
        {

            if (Blossom.Count() == 0) return this;


            Graph G_contracted = Graph.createCopy(this);
            HashSet<Node> NodesInBlossom = new HashSet<Node>();


            foreach (Edge edge in Blossom)
            {
                NodesInBlossom.Add(G_contracted.nodes[edge.u.id]);
                NodesInBlossom.Add(G_contracted.nodes[edge.v.id]);
            }

            List<Edge> toDeleteE = new List<Edge>();
            foreach (Edge edge in G_contracted.edges)
            {
                if (NodesInBlossom.Contains(edge.u) && NodesInBlossom.Contains(edge.v))
                {
                    toDeleteE.Add(edge);

                }
            }

            foreach (Edge edge in toDeleteE)
            {
                G_contracted.edges.Remove(edge);
                edge.u.neigbours.Remove(edge.v);
                edge.v.neigbours.Remove(edge.u);
            }
            toDeleteE.Clear();

            Node representant = new Node(-1);
            G_contracted.nodes.Add(representant);


            List<Edge> toDeleteU = new List<Edge>();
            List<Edge> toDeleteV = new List<Edge>();

            foreach (Edge edge in G_contracted.edges)
            {
                if (NodesInBlossom.Contains(edge.u))
                {
                    toDeleteU.Add(edge);
                }
                else if (NodesInBlossom.Contains(edge.v))
                {
                    toDeleteV.Add(edge);
                }

            }

            foreach (Edge edge in toDeleteU)
            {
                G_contracted.edges.Add(new Edge(representant, edge.v, edge.id));
                representant.neigbours.Add(edge.v);
                edge.v.neigbours.Add(representant);

                G_contracted.edges.Remove(edge);
                edge.u.neigbours.Remove(edge.v);
                edge.u.neigbours.Remove(edge.u);
            }


            foreach (Edge edge in toDeleteV)
            {

                G_contracted.edges.Add(new Edge(edge.u, representant, edge.id));
                representant.neigbours.Add(edge.u);
                edge.u.neigbours.Add(representant);

                G_contracted.edges.Remove(edge);
                edge.u.neigbours.Remove(edge.v);
                edge.u.neigbours.Remove(edge.u);
            }

            List<Node> NodesInBlossomList = NodesInBlossom.ToList();



            List<Node> toDelete = new List<Node>();
            foreach (Node node in NodesInBlossomList)
            {
                foreach (Node neighbour in node.neigbours)
                {
                    toDelete.Add(neighbour);
                }

                foreach (Node neighbour in toDelete)
                {
                    node.neigbours.Remove(neighbour);
                    neighbour.neigbours.Remove(node);
                }
                toDelete.Clear();
            }

            foreach (Node node in NodesInBlossomList)
            {
                G_contracted.nodes.Remove(node);
            }

            int representantIndex = G_contracted.nodes.IndexOf(representant);

            Node tmp = G_contracted.nodes[representantIndex];
            G_contracted.nodes[representantIndex] = G_contracted.nodes[0];
            G_contracted.nodes[0] = tmp;


            return Graph.createCopy(G_contracted);
        }

        public List<Edge> LiftPath(List<Edge> PathInG2, List<Edge> Blossom, Graph contracted, Matching m)
        {

            HashSet<Node> nodesInPath = new HashSet<Node>();
            List<Edge> resultingPath = new List<Edge>();

            List<Edge> pinG2equivaveltn = new List<Edge>();
            foreach (Edge edge in PathInG2) pinG2equivaveltn.Add(this.edgeIDMapping[edge.id]);

            if (Blossom.Count() == 0) return resultingPath;


            Node blossomRepresentant = contracted.nodes[0];
            List<Edge> edgesFromTheRepresentant = new List<Edge>();
            foreach (Edge edge in PathInG2) //EdgesFromTheRepresentant will have 0, 1 or 2 elements
            {
                if (edge.u == blossomRepresentant || edge.v == blossomRepresentant) edgesFromTheRepresentant.Add(edge);
            }



            Node from, to;

            Console.WriteLine("length: " + PathInG2.Count().ToString());

            if (PathInG2.Count() == 0) return resultingPath; //POTENTIAL SOURCE OF PROBLEMS DONT KNOW HOW THIS WORKS, with graph K3
            if (PathInG2.Count() == 1)
            {
                from = PathInG2[0].u;
                to = PathInG2[0].v;
            }

            else
            { 
                if (PathInG2[0].u != PathInG2[1].v && PathInG2[0].u != PathInG2[1].u) from = PathInG2[0].u;
                else from = PathInG2[1].v;

                if (PathInG2.Last().u != PathInG2[PathInG2.Count() - 2].v && PathInG2.Last().u != PathInG2[PathInG2.Count() - 2].u) to = PathInG2[0].u;
                else to = PathInG2.Last().v;
            }
           

            foreach (Edge edge in PathInG2)
            {
                nodesInPath.Add(edge.u);
                nodesInPath.Add(edge.v);
            }
            


            if (!nodesInPath.Contains(blossomRepresentant)) {             //case one - just lift it

                Console.WriteLine("case1");
                foreach (Edge edge in PathInG2)
                {
                    Edge currentEdge = this.edgeIDMapping[edge.id];
                    resultingPath.Add(currentEdge);
                }

                return resultingPath; }

            else if (blossomRepresentant == from || blossomRepresentant == to || edgesFromTheRepresentant.Count() == 1) //case two - is one of the endpoints
            {
                

                Console.WriteLine("Case2");
                Node blossomStart, blossomEnd;
                Edge rightEdge = this.edgeIDMapping[edgesFromTheRepresentant[0].id];

                if (edgesFromTheRepresentant[0].u == blossomRepresentant) blossomStart = rightEdge.u;
                else blossomStart = rightEdge.v;

                //if (Blossom.Count == 0) return PathInG2;

                blossomEnd = m.GetBlossomBase(Blossom);


                Console.WriteLine("base IS XXXXXXX" + m.GetBlossomBase(Blossom));
                Console.WriteLine("blossom start is " + blossomStart.ToString());

                List<Edge> blossomArc = this.GetEvenPortionOfBlossom(blossomStart, blossomEnd, Blossom);
                foreach (Edge edge in PathInG2)
                {
                    Edge currentEdge = this.edgeIDMapping[edge.id];
                    resultingPath.Add(currentEdge);
                }

                resultingPath.AddRange(blossomArc);
                return resultingPath;


            }
            else //case three - is in the middle
            {
                Console.WriteLine("case3");
                m.GetBlossomBase(Blossom);


                Node blossomStart, blossomEnd;
                if (edgesFromTheRepresentant[0].v == blossomRepresentant) blossomStart = this.edgeIDMapping[edgesFromTheRepresentant[0].id].v;
                else blossomStart = this.edgeIDMapping[edgesFromTheRepresentant[0].id].u;

                if (edgesFromTheRepresentant[1].v == blossomRepresentant) blossomEnd = this.edgeIDMapping[edgesFromTheRepresentant[1].id].v;
                else blossomEnd = this.edgeIDMapping[edgesFromTheRepresentant[1].id].u;


                List<Edge> blossomArc = this.GetEvenPortionOfBlossom(blossomStart, blossomEnd, Blossom);

                foreach (Edge edge in PathInG2)
                {
                    Edge currentEdge = this.edgeIDMapping[edge.id];
                    resultingPath.Add(currentEdge);
                }

                resultingPath.AddRange(blossomArc);
                return resultingPath;

                //SLEPIT DOHROMADY - nyni zkousim EXPERIMENTAL




            }
        }
        

        public static Graph createCopy(Graph g)
        {
            Graph copy = new Graph("copy");
            for (int i = 0; i < g.nodes.Count(); i++) copy.nodes.Add(new Node(i));

            foreach (Edge edge in g.edges)
            {
                Edge newEdge = new Edge(copy.nodes[g.nodes.IndexOf(edge.u)], copy.nodes[g.nodes.IndexOf(edge.v)], edge.id);
                copy.edges.Add(newEdge);
                newEdge.u.neigbours.Add(newEdge.v);
                newEdge.v.neigbours.Add(newEdge.u);

            }

            copy.createEdgeMapping();

            return copy;



        }


        public void createEdgeMapping()
        {
            foreach (Edge edge in this.edges)
            {
                this.edgeIDMapping.Add(edge.id, edge);
                if (!this.edgeNodeMap.ContainsKey((edge.u, edge.v))) this.edgeNodeMap.Add((edge.u, edge.v), edge);
                if (!this.edgeNodeMap.ContainsKey((edge.v, edge.u))) this.edgeNodeMap.Add((edge.v, edge.u), edge);


            }
        }

        public List<Edge> reconstructAugmentingPath(Node one, Node two)
        {
            List<Edge> augmentingPath = new List<Edge>() { };


            Node parent1 = Node.getParent(one);
            Node parent2 = Node.getParent(two);



            if (parent1 != one)
            {
                Node iterator = one;
                while (iterator != parent1)
                {

                    Edge currentEdge = this.edgeNodeMap[(iterator, iterator.successor)];
                    iterator = iterator.successor;
                    augmentingPath.Add(currentEdge);
                }

                augmentingPath.Reverse();
            }

            augmentingPath.Add(edgeNodeMap[(one, two)]);


            if (parent2 != two)
            {
                Node iterator = two;
                while (iterator != parent2)
                {

                    Edge currentEdge = this.edgeNodeMap[(iterator, iterator.successor)];
                    iterator = iterator.successor;
                    augmentingPath.Add(currentEdge);
                }

            }

            return augmentingPath;

        }

        public (List<Edge>, List<Edge>) reconstructBlossom(Node one, Node two)
        {
            List<Edge> flower = new List<Edge>() { };
            Node commonParent = Node.getParent(one);

            if (Node.getParent(one) != Node.getParent(two)) throw new Exception("This is not a blossom youre trying to reconstruct!");

            Node iterator = one;
            while(iterator != commonParent)
            {
                Edge currentEdge = this.edgeNodeMap[(iterator, iterator.successor)];
                iterator = iterator.successor;
                flower.Add(currentEdge);
            }

            flower.Reverse();
            flower.Add(this.edgeNodeMap[(one, two)]);

            iterator = two;
            while (iterator != commonParent)
            {
                Edge currentEdge = this.edgeNodeMap[(iterator, iterator.successor)];
                iterator = iterator.successor;
                flower.Add(currentEdge);
            }
            Console.WriteLine("This is the reconstructed Flower Bruv ");
            




            foreach (Edge edge in flower) Console.WriteLine(edge.ToString());
            Console.WriteLine("end of bruv. TRY TO MEND:");

            Dictionary<Edge, int> occurances = new Dictionary<Edge, int>();
            List<Edge> blossom = new List<Edge>();
            foreach(Edge edge in flower)
            {
                if (occurances.ContainsKey(edge)) occurances[edge]++;
                else
                {
                    occurances.Add(edge, 1);

                    blossom.Add(edge);
                }
                
            }
            List<Edge> toDelete = new List<Edge>(); 
            foreach (Edge edge in flower)
            {
                if (occurances[edge] > 1)toDelete.Add(edge);
            }



            foreach (Edge edge in toDelete) blossom.Remove(edge);




            return (flower.Distinct().ToList(), blossom);


        }




            public List<Edge> GetEvenPortionOfBlossom(Node from, Node to, List<Edge> Blossom) {

            List<Node> BlossomNodes = new List<Node>();
            List<Edge> EvenPortion = new List<Edge>();

            foreach (Edge edge in Blossom)
            {
                if (!BlossomNodes.Contains(edge.u)) BlossomNodes.Add(edge.u);
                if (!BlossomNodes.Contains(edge.v)) BlossomNodes.Add(edge.v);
            }

            int iterator = BlossomNodes.IndexOf(from);
            if (iterator == -1)return EvenPortion;
            int length = 0;
            for (int i = 0; i < BlossomNodes.Count(); i++)
            {
                //Console.WriteLine(iterator);
                //Console.WriteLine(iterator % Blossom.Count());
                //Console.WriteLine("xx\n\n");

                Console.WriteLine("Blossom nodes len" + BlossomNodes.Count().ToString() + " iterator " + (iterator).ToString()); 
                if (BlossomNodes[iterator % BlossomNodes.Count()] == to && length % 2 == 0) return EvenPortion;
                if (BlossomNodes[iterator % BlossomNodes.Count()] == to) break;

                Console.WriteLine("iterator " + iterator.ToString() + "   map size" + this.edgeNodeMap.Count());
                if (iterator == Blossom.Count()) iterator = 0;
                //XXX
                Console.WriteLine((iterator % Blossom.Count()).ToString() + "modulo" + " blossomNodes" + BlossomNodes.Count().ToString() + "size itself " + Blossom.Count().ToString());

                Console.WriteLine(iterator.ToString() + " iterator. BLOSSOM NODES COUNT" + BlossomNodes.Count().ToString() + "\nBLOSOSM COUNT" + Blossom.Count().ToString());
                Console.WriteLine((iterator%BlossomNodes.Count()).ToString() + " iterator modulo");
                Console.WriteLine(((iterator + 1) % BlossomNodes.Count()).ToString() + " iterator + 1 modulo2");
                foreach (Edge edge in Blossom) Console.WriteLine(edge.ToString() + "x");

                if (this.edgeNodeMap.ContainsKey((BlossomNodes[iterator % Blossom.Count()], BlossomNodes[(iterator + 1) % Blossom.Count()])))
                { //GET CORRRECTEDGE

                    Edge currentEdge = this.edgeNodeMap[(BlossomNodes[iterator % Blossom.Count()], BlossomNodes[(iterator + 1) % Blossom.Count()])];

                    EvenPortion.Add(currentEdge);
                }

                iterator++;
                
                length++;
            }
            EvenPortion.Clear();
            iterator = BlossomNodes.IndexOf(from);
            int helper = iterator - 1;
            if (helper < 0) helper = Blossom.Count() - 1;

            for (int i = 0; i < Blossom.Count() * 2; i++)
            {
                if (BlossomNodes[iterator] == to) return EvenPortion;

                if (this.edgeNodeMap.ContainsKey((BlossomNodes[iterator], BlossomNodes[helper])))
                {
                    Edge currentEdge = this.edgeNodeMap[(BlossomNodes[iterator], BlossomNodes[helper])];
                    EvenPortion.Add(currentEdge);
                }
                iterator--;
                if (iterator == -1) iterator = Blossom.Count() - 1;
                if (iterator == 0) helper = Blossom.Count() - 1;
                else helper = iterator - 1;


            }

            Console.WriteLine("something is wrong with the blossom");
            return null;





        }
    }

        
}









