using System;
using System.Collections.Generic;
using System.Linq;
using GraphBuilder;
using Navigation;
using UnityEngine;

namespace BinaryPartition
{
    public class Divider
    {
        private const float EdgeGap = 1;

        private readonly PartitionRunner _partitionRunner;
        private readonly Comparer<VertexId> _comparer;
        public readonly VertexId Start;
        public readonly VertexId End;
        private readonly List<VertexId> _below = new();
        private readonly List<VertexId> _above = new();
        
        public Divider(PartitionRunner partitionRunner, float axisValue, int parAxis, Rectangle rectangle)
        {
            _partitionRunner = partitionRunner;
            
            if (parAxis == 0)
            {
                Start = _partitionRunner.Builder.MakeVertex(new Vector2(rectangle.Min[parAxis], axisValue));
                End = _partitionRunner.Builder.MakeVertex(new Vector2(rectangle.Max[parAxis], axisValue));
            }
            else
            {
                Start = _partitionRunner.Builder.MakeVertex(new Vector2(axisValue, rectangle.Min[parAxis]));
                End = _partitionRunner.Builder.MakeVertex(new Vector2(axisValue, rectangle.Max[parAxis]));
            }

            _comparer = Comparer<VertexId>.Create( (a, b) => 
                _partitionRunner.Builder.GetPosition(a)[parAxis]
                    .CompareTo(_partitionRunner.Builder.GetPosition(b)[parAxis]));
            
            _partitionRunner.AddDivider(this);
        }

        public void AddBelow(VertexId vertex)
        {
            _below.Add(vertex);
        }

        public void AddAbove(VertexId vertex)
        {
            _above.Add(vertex);
        }

        public IEnumerable<EdgeId> GetEdges()
        {
            var incidentVertices = Utils.Merge(_above, _below, _comparer);
            
            if (incidentVertices.Count == 0)
            {
                return new[] {_partitionRunner.Builder.MakeEdge(Start, End, EdgeTag.Hallway)};
            }

            var segStarts =
                new[] {Start} .Concat(incidentVertices);
            
            var segEnds =
                incidentVertices.Concat(new[] { End });
            
            return segStarts.Zip(segEnds,
                (u, v) => _partitionRunner.Builder.MakeEdge(u, v, EdgeTag.Hallway));
        }
    }
}