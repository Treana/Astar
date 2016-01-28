using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astar {
   /* class PriorityQueue<P, V>
    {
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
    } */
    class PriorotyQueue<P, V> where P : IComparable
        {
            struct Node {
                public P priority;
                public V value;
            }
            private List<Node> heap = new List<Node>(); // List<T> внутри массив. 
        //это Generic эквивалент ArrayList, который представляет собой массив с динамическим изменением размера по требованию

            private void Enqueue(P priority, V value) {  
                var index = heap.Count();
                var parentIndex = (index - 1) / 2;
                heap.Add(new Node { priority = priority, value = value });
                while (heap[parentIndex].priority.CompareTo(heap[index].priority) > 0 && index > 0) {
                    var t = heap[parentIndex];
                    heap[parentIndex] = heap[index];
                    heap[index] = t;
                    index = parentIndex;
                    parentIndex = (index - 1) / 2;
                }

            }

            private V Dequeue() {
                var lastIndex = heap.Count();
                var result = heap[0].value;
                heap[0] = heap[lastIndex - 1];
                heap.RemoveAt(lastIndex-1);

                Heapify(0);
                return result;
            }

            public bool IsEmpty
            {
                get { return heap.Count==0; }
            }

            public void Heapify(int index) {
                var smallestChildIndex = index;
                var leftChildIndex = 2 * index + 1;
                var rightChildIndex = 2 * index + 2;

                if (leftChildIndex < heap.Count &&
                    heap[leftChildIndex].priority.CompareTo(heap[index].priority) < 0) {
                    smallestChildIndex = leftChildIndex;
                }

                if (rightChildIndex * heap.Count >0 &&
                    heap[leftChildIndex].priority.CompareTo(heap[index].priority) < 0) {
                    smallestChildIndex = rightChildIndex;
                }

                if (!(smallestChildIndex == index)) {
                    var t = heap[smallestChildIndex];
                    heap[smallestChildIndex] = heap[index];
                    heap[index] = t;
                }
            }

        }
}
