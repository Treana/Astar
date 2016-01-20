using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astar {
    class PriorityQueue<P, V> {
        private SortedDictionary<P, Queue<V>> dict = new SortedDictionary<P, Queue<V>>();

        public void Enqueue(P priority, V value) {
            Queue<V> q;
            if(!dict.TryGetValue(priority, out q)) {
                q = new Queue<V>();
                dict.Add(priority, q);
            }
            q.Enqueue(value);
        }

        public V Dequeue() {
            var pair = dict.First(); 
            var v = pair.Value.Dequeue();
            if(pair.Value.Count == 0) {
                dict.Remove(pair.Key);
            }
            return v;
        }

        public bool IsEmpty {
            get { return !dict.Any(); }
        }
    }
}
