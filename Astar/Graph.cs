using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Astar {
    class Graph {
        private int VertexId = 0;
        public Dictionary<int, Vertex> Vertexes = new Dictionary<int, Vertex>();
        public Vertex start, end;

        public Graph() {

        }

        public Graph(string file) {
            var xml = new XmlDocument();
            xml.Load(file);

            // Vertex
            foreach(XmlNode cell in xml.SelectNodes("mxGraphModel/root/mxCell")) {
                XmlNode mxGeom = cell.SelectSingleNode("mxGeometry");
                if(mxGeom != null) {
                    var w = mxGeom.Attributes["width"];
                    var h = mxGeom.Attributes["height"];
                    var x = mxGeom.Attributes["x"];
                    var y = mxGeom.Attributes["y"];
                    var id = cell.Attributes["id"];
                    if(w != null && h != null && x != null && y != null && id!=null) {
                        var vertex = AddVertex(int.Parse(id.Value));
                        vertex.X = double.Parse(x.Value) + double.Parse(w.Value) / 2;
                        vertex.Y = double.Parse(y.Value) + double.Parse(h.Value) / 2;

                        // добавить начало и конец
                        var value = cell.Attributes["value"];
                        if(value!=null) {
                            var v = value.Value;
                            if(v=="start") {
                                start = vertex;
                            } else if(v=="end") {
                                end = vertex;
                            }
                        }

                    }
                }
            }

            // Веса
            var weights = new Dictionary<int, double>(); // ключ - id ребра
            foreach(XmlNode cell in xml.SelectNodes("mxGraphModel/root/mxCell")) {
                XmlNode mxGeom = cell.SelectSingleNode("mxGeometry");
                if(mxGeom != null) {
                    var connectable = cell.Attributes["connectable"];
                    var parent = cell.Attributes["parent"];
                    var value = cell.Attributes["value"];
                    if(connectable != null && parent != null && value != null) {
                        var pid = int.Parse(parent.Value);
                        if(value.Value!="" && pid>1) {
                            weights.Add(pid, int.Parse(value.Value));
                        }
                    }
                }
            }

            // Egdes
            foreach(XmlNode cell in xml.SelectNodes("mxGraphModel/root/mxCell")) {
                XmlNode mxGeom = cell.SelectSingleNode("mxGeometry");
                if(mxGeom != null) {
                    var s = cell.Attributes["source"];
                    var t = cell.Attributes["target"];
                    var id = cell.Attributes["id"];
                    if(s != null && t != null && id != null) {
                        var source = Vertexes[int.Parse(s.Value)];
                        var target = Vertexes[int.Parse(t.Value)];

                        // Определяем вес ребра
                        var weight = 0.0;
                        if(!weights.TryGetValue(int.Parse(id.Value), out weight)) {
                            weight = Heuristics.EuclideanDistance(source, target);
                        }
                        AddEdge(source, target, weight);
                    }
                }
            }


        }

        public Vertex AddVertex() {
            var v = new Vertex(VertexId);
            Vertexes.Add(VertexId, v);
            VertexId++;
            return v;
        }

        public Vertex AddVertex(int id) {
            var v = new Vertex(id);
            Vertexes.Add(id, v);
            if(id>=VertexId) {
                VertexId = id + 1;
            }
            return v;
        }

        public void AddEdge(Vertex a, Vertex b, double weight) {
            a.AddNeighbor(b, weight);
            b.AddNeighbor(a, weight);
        }

        public void AddOneWayEdge(Vertex from, Vertex to, double weight) {
            from.AddNeighbor(to, weight);
        }
    }
}
