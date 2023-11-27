using System;
using System.Collections.Generic;
using System.Linq;
using Geom;
using Unity.VisualScripting;
using UnityEngine;

namespace BinaryPartition
{
    public class Divider : IComparable<Divider>
    {
        private const float EdgeGap = 1;
        public int ParAxis;
        public float AxisValue { get; }
        private float _start;
        private float _end;
        private List<Divider> _below = new();
        private List<Divider> _above = new();

        public Divider(float axisValue, int parAxis, Rectangle rectangle)
        {
            AxisValue = axisValue;
            ParAxis = parAxis;
            _start = rectangle.Min[ParAxis];
            _end = rectangle.Max[ParAxis];
        }

        public void AddBelow(Divider divider)
        {
            _below.Add(divider);
        }

        public void AddAbove(Divider divider)
        {
            _above.Add(divider);
        }

        public IEnumerable<(Vector2, Vector2)> GetSegments()
        {
            var ax = AxisValue;
            Func<float, Vector2> toPoint = ParAxis == 0 ?
                n => new Vector2(n, ax) :
                n => new Vector2(ax, n);

            List<Divider> incidentDividers = Utils.Merge(_above, _below);
            
            if (incidentDividers.Count == 0)
            {
                return new[] {(toPoint(_start), toPoint(_end))};
            }

            var segStarts =
                new[] {_start}
                    .Concat(incidentDividers.Select(divider => divider.AxisValue))
                    .Select(v => v + EdgeGap);

            var segEnds =
                incidentDividers.Select(divider => divider.AxisValue)
                    .Concat(new[] {_end})
                    .Select(v => v - EdgeGap);

            var segments = segStarts.Zip<float, float, (Vector2, Vector2)>(segEnds,
                (a, b) => (toPoint(a), toPoint(b)));

            return incidentDividers.Aggregate(segments,
                (current, divider) => current.Concat(divider.GetSegments()));
        }
        public int CompareTo(Divider other)
        {
            return AxisValue.CompareTo(other.AxisValue);
        }
    }
}