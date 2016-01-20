using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astar {
    static class Heuristics {
        // Евклидова метрика, обычное двумерное расстояние, эвристика для произвольных графов
        public static double EuclideanDistance(Vertex a, Vertex b) {
            return Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2));
        }

        // Расстояние Чебышёва, "метрикой хода короля", эвристика для сетки, где каждая вершина соединена с 8 соседними
        public static double ChebyshevDistance(Vertex a, Vertex b) {
            return Math.Max(Math.Abs(b.X - a.X), Math.Abs(b.Y - a.Y));
        }

        // Манхэттенское расстояние, эвристика для сетки, где каждая вершина соединена с 4 соседними
        public static double ManhattanDistance(Vertex a, Vertex b) {
            return Math.Abs(b.X - a.X) + Math.Abs(b.Y - a.Y);
        }

        // Нулевая эвристика, превращает A* в алгоритм Дейкстры
        public static double NullDistance(Vertex a, Vertex b) {
            return 0;
        }
    }
}
