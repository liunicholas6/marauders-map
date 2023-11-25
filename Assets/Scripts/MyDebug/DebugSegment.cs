using UnityEngine;

namespace MyDebug
{
    public struct DebugSegment : IDebugDrawable
    {
        public Vector2 P0;
        public Vector2 P1;

        public void Draw(Color color)
        {
            Debug.DrawLine(P0, P1, color);
        }
    }
}