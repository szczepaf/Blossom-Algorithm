﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blossom_Algorithm
{
    class Graph
    {
        private string name;
        List<Node> nodes;
        List<Edge> edges;

        public Graph(string name)
        {
            this.name = name;
            this.nodes = new List<Node>();
            this.edges = new List<Edge>();
        }

        public Graph(List<Node> nodes, string name)
        {
            this.name = name;
            this.nodes = new List<Node>();
            foreach (Node node in nodes) this.nodes.Add(node);

            this.edges = new List<Edge>();

        }

        //INPUT FORMAT:
        //One Edge will be in the format: (onevertex,secondvertex)
        //eg: <2,3>
        //The whole graph will in the format: Number of vertices: Edges seperated by newlines
        //eg: 3: <1,2>\n<0,2>\n
        //Indices of Vertices (Nodes) are 0-based
        //Example for K4: 4: <0,1>\n<0,2>\n<0,3>\n<1,2>\n<1,3>\n<2,3>\n
        public Edge ParseEdge(string edgeString)
        {
            edgeString = edgeString.Replace("<", "");
            edgeString = edgeString.Replace(">", "");

            string[] ids = edgeString.Split(',');
            int id1 = int.Parse(ids[0]);
            int id2 = int.Parse(ids[1]);

            return new Edge(nodes[id1], nodes[id2]);

        }

        public Graph ParseGraph(string inputGraph, string name)
        {
            Graph graph = new Graph(name);

            string[] data = inputGraph.Split(' ');
            int verticesCount = int.Parse(data[0].Replace(":", ""));
            string[] edgeData = data[1].Split('\n');

            for (int i = 0; i < verticesCount; i++)
            {
                graph.nodes.Add(new Node(i));
            }

            foreach (string edgeInput in edgeData)
            {
                Edge newEdge = ParseEdge(edgeInput);
                graph.edges.Add(newEdge);
                newEdge.u.neigbours.Add(newEdge.v);
                newEdge.v.neigbours.Add(newEdge.u);
            }

            return graph;
        }

        //Debugging tools for printing out the graph:
        public override string ToString()
        {
            string graphInfo =  this.name + ": \n";
            graphInfo += "Number of vertices: " + nodes.Count().ToString() + "\n";
            graphInfo += "Number of edges: " + edges.Count().ToString() + "\n";
            foreach (Node vertex in nodes)
            {
                graphInfo += vertex.ToString() + "\n";
            }

            return graphInfo;

        }
    }
}

