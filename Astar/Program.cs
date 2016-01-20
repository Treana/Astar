using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astar {
    class Program {
        static void Main(string[] args) {
            var heuristicName = "";
            string inputFile = "", outFile = "";

            if(args.Length>3 || args.Length<2) {
                Console.WriteLine("Error: wrong arguments.");
                Console.WriteLine("Usage: astar inputFile outputFile [Euclid|Chebyshev|Manhattan|Null]");
                return;
            }

            heuristicName = "Euclid"; // эвристика по умолчанию
            inputFile = args[0];
            outFile = args[1];

            if (args.Length == 3) {
                heuristicName = args[2];
            }

            Func<Vertex, Vertex, double> heuristic = null;
            switch(heuristicName) {
                case "Euclid":
                    heuristic = Heuristics.EuclideanDistance;
                    break;
                case "Chebyshev":
                    heuristic = Heuristics.ChebyshevDistance;
                    break;
                case "Manhattan":
                    heuristic = Heuristics.ManhattanDistance;
                    break;
                case "Null":
                    heuristic = Heuristics.NullDistance;
                    break;
                default:
                    Console.WriteLine("Wrong heuristuc name. Should be Euclid|Chebyshev|Manhattan|Null");
                    return;
            }

            var g = new Graph(inputFile);
            var path = AStar.FindPath(g.start, g.end, heuristic);
            
            var result = new List<string>();
            foreach(Vertex p in path) {
                result.Add(string.Format("{0} {1}", p.X, p.Y));
            }
            result.Reverse(); // путь обходится в обратном порядке, поэтому переворачиваем список
            File.WriteAllLines(outFile, result);

            //Console.ReadLine();
        }
    }
}
