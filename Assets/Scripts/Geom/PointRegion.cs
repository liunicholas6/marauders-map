using UnityEngine;
namespace Geom
{
    public class PointRegion : IRegion
    {
        private readonly Vector2 _point;

        public PointRegion(Vector2 point)
        {
            _point = point;
        }

        public Vector2 RandPoint()
        {
            return _point;
        }

        public float Area()
        {
            return 0;
        }
    }
}