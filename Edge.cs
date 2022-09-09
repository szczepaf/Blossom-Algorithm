using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blossom_Algorithm
{
    class Edge
    {
        public int id;
        public Node u { get; private set; }
        public Node v { get; private set; }
        public Edge (Node u, Node v, int id)
        {
            this.u = u;
            this.v = v;
            this.id = id;
        }
        
        public override string ToString()
        {
            return "id: " + this.id.ToString() + ", from " + u.id.ToString() + " to " + v.id.ToString();
        }
    }
}
