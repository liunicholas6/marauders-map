using BinaryPartition;
using UnityEngine;
namespace MyDebug
{
    public class DebugRect : IDebugDrawable
    {
        public Rectangle Rectangle;

        public DebugRect()
        {
            Rectangle = Rectangle.UnitSquare();
        }

        public DebugRect(Rectangle rectangle)
        {
            Rectangle = rectangle;
        }

        public void Draw(Color color)
        {
            Debug.DrawLine(new Vector3(Rectangle.Min[0], Rectangle.Min[1]), new Vector3(Rectangle.Min[0], Rectangle.Max[1]), color);
            Debug.DrawLine(new Vector3(Rectangle.Min[0], Rectangle.Max[1]), new Vector3(Rectangle.Max[0], Rectangle.Max[1]), color);
            Debug.DrawLine(new Vector3(Rectangle.Max[0], Rectangle.Max[1]), new Vector3(Rectangle.Max[0], Rectangle.Min[1]), color);
            Debug.DrawLine(new Vector3(Rectangle.Max[0], Rectangle.Min[1]), new Vector3(Rectangle.Min[0], Rectangle.Min[1]), color);
        }
        
    }
}