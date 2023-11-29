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
        private readonly Comparer<VertexId> _comparer;
        public readonly VertexId Start;
        public readonly VertexId End;
        private readonly List<VertexId> _below = new();
        private readonly List<VertexId> _above = new();

        public static readonly List<Divider> AllDividers = new();

        public Divider(float axisValue, int parAxis, Rectangle rectangle)
        {
            if (parAxis == 0)
            {
                Start = Builder.Instance.MakeVertex(new Vector2(rectangle.Min[parAxis], axisValue));
                End = Builder.Instance.MakeVertex(new Vector2(rectangle.Max[parAxis], axisValue));
            }
            else
            {
                Start = Builder.Instance.MakeVertex(new Vector2(axisValue, rectangle.Min[parAxis]));
                End = Builder.Instance.MakeVertex(new Vector2(axisValue, rectangle.Max[parAxis]));
            }

            _comparer = Comparer<VertexId>.Create( (a, b) => 
                    Builder.Instance.GetPosition(a)[parAxis]
                    .CompareTo(Builder.Instance.GetPosition(b)[parAxis]));
            AllDividers.Add(this);
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
                return new[] {Builder.Instance.MakeEdge(Start, End, EdgeTag.Hallway)};
            }

            var segStarts =
                new[] {Start} .Concat(incidentVertices);
            
            var segEnds =
                incidentVertices.Concat(new[] { End });
            
            return segStarts.Zip(segEnds,
                (u, v) => Builder.Instance.MakeEdge(u, v, EdgeTag.Hallway));
        }
    }
}