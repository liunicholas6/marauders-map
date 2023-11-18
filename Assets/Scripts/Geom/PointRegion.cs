using UnityEngine;
namespace Geom
{
    public class PointRegion : IRegion
    {
        private Vector2 _point;


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