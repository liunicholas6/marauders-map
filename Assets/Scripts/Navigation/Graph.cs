using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Navigation
{
    public class Graph
    {
        private List<List<Edge>> _adjList;
        private List<VertexInfo> _vertices;

        public Graph(List<List<Edge>> adjList, List<VertexInfo> vertices)
        {
            _adjList = adjList;
            _vertices = vertices;
        }

        // TODO: Insha implements pathfinding algos
    }
}