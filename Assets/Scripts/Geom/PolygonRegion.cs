using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Geom
{
    public class PolygonRegion : IRegion
    {
        private List<LineCurve> _boundary;

        public Vector2 RandPoint()
        {
            var res = new Vector2();
            foreach (var curve in _boundary)
            {
                float t = Random.Range(0, 1);
                res += curve.Point(t);
            }

            return res / _boundary.Count;
        }

        public float Area()
        {
            float res = 0;
            var p0 = _boundary[0].P0;
            
            for (var i = 1; i < _boundary.Count - 1; i++)
            {
                var a = _boundary[i].P0 - p0;
                var b = _boundary[i].AsVector();
                res += Math.Abs(a.x * b.y - a.y * b.x);
            }

            return res / 2;
        }
    }
}