using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astar {
    struct EdgeTo {
        public double Weight;
        public Vertex To;
    }

    class Vertex {
        public LinkedList<EdgeTo> Neighbors = new LinkedList<EdgeTo>();

        public int Id {
            get;
            private set;
        }

        // дополнительные данные для эвристики
        public double X;
        public double Y;

        public Vertex(int id) {
            Id = id;
        }

        public void AddNeighbor(Vertex v, double weight) {
            Neighbors.AddFirst(new EdgeTo { To=v, Weight=weight });
        }

    }
}
