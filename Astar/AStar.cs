using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astar {
    class Path : IEnumerable {
        public Vertex LastStep { get; private set; }
        public Path PreviousSteps { get; private set; }
        public double TotalCost { get; private set; }

        private Path(Vertex lastStep, Path previousSteps, double totalCost) {
            LastStep = lastStep;
            PreviousSteps = previousSteps;
            TotalCost = totalCost;
        }

        public Path(Vertex start) : this(start, null, 0) {
        }

        public Path AddStep(Vertex step, double stepCost) {
            return new Path(step, this, TotalCost + stepCost);
        }

        public IEnumerator GetEnumerator() {
            for(Path p = this; p != null; p = p.PreviousSteps)
                yield return p.LastStep;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
    }

    class AStar {

        static public Path FindPath(Vertex start, Vertex end, Func<Vertex, Vertex, double> estimate) {
            var closed = new HashSet<Vertex>();
            var queue = new PriorityQueue<double, Path>();

            queue.Enqueue(0, new Path(start));

            while(!queue.IsEmpty) {
                var path = queue.Dequeue();

                if(closed.Contains(path.LastStep)) {
                    continue;
                }

                if(path.LastStep.Equals(end)) {
                    return path;
                }

                closed.Add(path.LastStep);

                foreach(EdgeTo e in path.LastStep.Neighbors) {
                    double d = e.Weight;
                    Vertex n = e.To;
                    var newPath = path.AddStep(n, d);
                    queue.Enqueue(newPath.TotalCost + estimate(n, end), newPath);
                }
            }

            return null;
        }

    }
}
