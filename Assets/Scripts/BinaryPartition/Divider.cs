using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BinaryPartition
{
    public struct Divider
    {
        private const float EdgeGap = 1;
        private readonly bool isHorizontal;
        public float AxisValue { get; }
        private float _start;
        private float _end;
        private List<Divider> _incidentDividers;

        public Divider(float axisValue, int parAxis, Rectangle rectangle)
        {
            AxisValue = axisValue;
            isHorizontal = parAxis == 0;
            _start = rectangle.Min[parAxis];
            _end = rectangle.Max[parAxis];
            _incidentDividers = new List<Divider>();
        }

        public void AddIncidentDivider(Divider divider)
        {
            _incidentDividers.Add(divider);
        }

        public IEnumerable<(Vector2, Vector2)> GetSegments()
        {
            var ax = AxisValue;
            Func<float, Vector2> toPoint = isHorizontal ?
                n => new Vector2(n, ax) :
                n => new Vector2(ax, n);
            
            if (_incidentDividers.Count == 0)
            {
                return new[] {(toPoint(_start), toPoint(_end))};
            }

            var segStarts =
                new[] {_start}
                    .Concat(_incidentDividers.Select(divider => divider.AxisValue))
                    .Select(v => v).ToList();

            var segEnds =
                _incidentDividers.Select(divider => divider.AxisValue)
                    .Concat(new[] {_end})
                    .Select(v => v).ToList();

            var monotonic = segStarts.Zip(segEnds, (a, b) => a < b).All(x => x);
            Debug.Log($"Monotonic: {monotonic}");

            var segments = segStarts.Zip<float, float, (Vector2, Vector2)>(segEnds,
                (a, b) => (toPoint(a + EdgeGap), toPoint(b - EdgeGap)));

            return _incidentDividers.Aggregate(segments,
                (current, divider) => current.Concat(divider.GetSegments()));
        }
    }
}