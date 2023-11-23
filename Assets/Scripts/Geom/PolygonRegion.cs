using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Geom
{
    public class PolygonRegion : IRegion
    {
        private List<Vector2> _boundary;
        private Vector2 _centerPoint;
        private float _area;

        Vector2 IRegion.CenterPoint => _centerPoint;

        float IRegion.Area => _area;

        public PolygonRegion(List<Vector2> boundary)
        {
            _boundary = boundary;
            
            _centerPoint = new Vector2();
            foreach (var point in _boundary)
            {
                _centerPoint += point;
            }
            _centerPoint /= _boundary.Count;

            _area = 0;
            var p0 = _boundary[0];
            
            for (var i = 1; i < _boundary.Count - 1; i++)
            {
                var a = _boundary[i] - p0;
                var b = _boundary[i] - _boundary[(i + 1) % _boundary.Count];
                _area += Math.Abs(a.x * b.y - a.y * b.x);
            }

            _area /= 2;
        }

        public Vector2 RandPoint()
        {
            
            var res = new Vector2();
            foreach (var point in _boundary)
            {
                float t = Random.Range(0, 1);
                res += Vector2.Lerp(_centerPoint, point, t);
            }

            return res / _boundary.Count;
        }

        
    }
}