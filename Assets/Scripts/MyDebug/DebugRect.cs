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
            Debug.DrawLine(new Vector3(Rectangle.MinX, Rectangle.MinY), new Vector3(Rectangle.MinX, Rectangle.MaxY), color);
            Debug.DrawLine(new Vector3(Rectangle.MinX, Rectangle.MaxY), new Vector3(Rectangle.MaxX, Rectangle.MaxY), color);
            Debug.DrawLine(new Vector3(Rectangle.MaxX, Rectangle.MaxY), new Vector3(Rectangle.MaxX, Rectangle.MinY), color);
            Debug.DrawLine(new Vector3(Rectangle.MaxX, Rectangle.MinY), new Vector3(Rectangle.MinX, Rectangle.MinY), color);
        }
        
    }
}