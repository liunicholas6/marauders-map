using UnityEngine;

namespace Geom
{
    public interface ICurve
    {
        float Length();
        Vector2 Point(float t);
        (ICurve, ICurve) Split(float t);
    }
}