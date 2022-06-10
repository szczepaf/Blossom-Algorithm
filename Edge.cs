using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blossom_Algorithm
{
    class Edge
    {
        public Node u { get; private set; }
        public Node v { get; private set; }
        public Edge (Node u, Node v)
        {
            this.u = u;
            this.v = v;
        }
    }
}
