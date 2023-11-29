using System.Collections;
using System.Collections.Generic;
using Geom;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Navigation
{
    public class Graph
    {
        private List<List<EdgeInfo>> _adjList;
        private List<VertexInfo> _vertices;

        public Graph(List<List<EdgeInfo>> adjList, List<VertexInfo> vertices)
        {
            _adjList = adjList;
            _vertices = vertices;
        }

        public IEnumerable<ICurve> Curves()
        {
            foreach (var adj in _adjList)
            {
                foreach (var edge in adj)
                {
                    yield return edge.Curve;
                }
            }
        }

        // TODO: Insha implements pathfinding algos
    }
}