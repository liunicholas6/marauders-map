using UnityEngine;

namespace Geom
{
    public interface IRegion
    {
        Vector2 RandPoint();
        float Area();
    }
}