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
        public float AxisValue { get; }
        private float _start;
        private float _end;
        private List<Divider> _incidentDividers;

        public void AddDivider(Divider divider)
        {
            _incidentDividers.Add(divider);
        }

        private void AppendLines(bool isVertical, ICollection<LineCurve> lines)
        {
            var ax = AxisValue;
            Func<float, Vector2> toPoint = isVertical ? 
                n => new Vector2(n, ax) :
                n => new Vector2(ax, n);

            if (_incidentDividers.Count == 0)
            {
                lines.Add( new LineCurve(toPoint(_start), toPoint(_end)));
            }
            else
            {
                var segStarts =
                    new[] {_start}
                    .Concat(_incidentDividers.Select(divider => divider.AxisValue))
                    .Select(v => v + EdgeGap);
                
                var segEnds =
                    _incidentDividers.Select(divider => divider.AxisValue)
                    .Concat(new[] {_end})
                    .Select(v => v - EdgeGap);

                var segments = segStarts.Zip(segEnds,
                    (a, b) => new LineCurve(toPoint(a), toPoint(b)));

                lines.AddRange(segments);

                foreach (var divider in _incidentDividers)
                {
                    divider.AppendLines(!isVertical, lines);
                }
            }
        }
    }
    
    
    
}