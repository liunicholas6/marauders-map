using System;
using System.Collections.Generic;
using System.Linq;
using Geom;
using Unity.VisualScripting;
using UnityEngine;

namespace BinaryPartition
{
    public struct Divider
    {
        private const float EdgeGap = 0.25f;
        public int ParAxis;
        public float AxisValue { get; }
        private float _start;
        private float _end;
        private List<Divider> _incidentDividers;

        public Divider(float axisValue, int parAxis, Rectangle rectangle)
        {
            AxisValue = axisValue;
            ParAxis = parAxis;
            _start = rectangle.Min[ParAxis];
            _end = rectangle.Max[ParAxis];
            _incidentDividers = new List<Divider>();
        }

        public void AddDivider(Divider divider)
        {
            _incidentDividers.Add(divider);
        }

        public IEnumerable<(Vector2, Vector2)> GetSegments()
        {
            var ax = AxisValue;
            Func<float, Vector2> toPoint = ParAxis == 0 ?
                n => new Vector2(ax, n) :
                n => new Vector2(n, ax);
            
            if (_incidentDividers.Count == 0)
            {
                return new[] {(toPoint(_start), toPoint(_end))};
            }

            var segStarts =
                new[] {_start}
                    .Concat(_incidentDividers.Select(divider => divider.AxisValue))
                    .Select(v => v + EdgeGap);

            var segEnds =
                _incidentDividers.Select(divider => divider.AxisValue)
                    .Concat(new[] {_end})
                    .Select(v => v - EdgeGap);

            var segments = segStarts.Zip<float, float, (Vector2, Vector2)>(segEnds,
                (a, b) => (toPoint(a), toPoint(b)));

            return _incidentDividers.Aggregate(segments,
                (current, divider) => current.Concat(divider.GetSegments()));
        }
    }
}