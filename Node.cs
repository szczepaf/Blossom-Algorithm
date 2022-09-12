using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blossom_Algorithm
{
    class Node
    {
        public int id;         //Will be useful when debugging. Starts from 0.
                               //Regular vertices will have positive ids, contracted vertices will have negative ids.
        public int height;
        public Node? successor; //Successor in the tree structure of the matching. Roots have themselves as successors.
        public List<Node> neigbours;

        
        public Node(int id)
        {
            this.height = -1;
            this.successor = null;
            this.neigbours = new List<Node>();
            this.id = id;

        }

        public override string ToString()
        {
            string ngbhrs = "";
            foreach (Node node in neigbours)
            {
                ngbhrs += node.id.ToString() + " ";
            }
            return this.id.ToString() + ": " + ngbhrs;
        }
        public static Node getParent(Node node)
        {
            if (node.successor == node) return node; //We have reached the top of the tree
            if (node.successor == null) throw new Exception
                    ("Node " + node.id.ToString() + "does not have a successor.");
            else return getParent(node.successor);
        }



    }
}
