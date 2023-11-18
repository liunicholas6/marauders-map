using UnityEngine;

namespace Geom
{
    public class LineCurve : ICurve
    {
        public Vector2 P0;
        public Vector2 P1;

        public LineCurve(Vector2 p0, Vector2 p1)
        {
            P0 = p0;
            P1 = p1;
        }

        public Vector2 AsVector()
        {
            return P1 - P0;
        }

        public float Length()
        {
            return AsVector().magnitude;
        }

        public Vector2 Point(float t)
        {
            return Vector2.Lerp(P0, P1, t);
        }

        public (ICurve, ICurve) Split(float t)
        {
            Vector2 point = Point(t);
            return (new LineCurve(P0, point), new LineCurve(point, P1));
        }
    }
}