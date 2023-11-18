using System;
using System.Collections.Generic;
using Geom;
using Grammar;
using Navigation;

namespace GraphBuilder
{
    public class GraphBuilder
    {
        private List<Edge> _edges = new();
        private List<VertexInfo> _vertexInfos = new();
        private int _vertCount = 0;

        public int MakeVertex(VertexInfo vertexInfo)
        {
            int res = _vertCount;
            _vertexInfos.Add(vertexInfo);
            _vertCount += 1;
            return res;
        }

        public void AddEdge(Edge edge)
        {
            _edges.Add(edge);
        }

        public (Edge, int, Edge) SplitEdge(Edge edge, float t, VertexTag vertexTag = VertexTag.None)
        {
            VertexInfo vertexInfo = new VertexInfo
            {
                tag = vertexTag
            };
            int vertexId = MakeVertex(vertexInfo);
            
            var (point, newEdge) = edge.Split(t, vertexId);
            AddEdge(newEdge);
            vertexInfo.region = new PointRegion(point);

            return (edge, vertexId, newEdge);
        }

        public Graph ToGraph()
        {
            List<List<Navigation.Edge>> adjList = new();
            for (var i = 0; i < _vertCount; i++)
            {
                adjList.Add(new List<Navigation.Edge>());
            }
            foreach (var edge in _edges)
            {
                if (edge.Navigable)
                {
                    var (forwards, backwards) = edge.ToNavEdges();
                    adjList[edge.FromVertex].Add(forwards);
                    adjList[edge.ToVertex].Add(backwards);
                }
            }

            return new Graph(adjList, _vertexInfos);
        }

    }
}