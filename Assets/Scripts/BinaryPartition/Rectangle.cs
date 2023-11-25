namespace BinaryPartition
{
    public class Rectangle
    {
        public float MinX;
        public float MinY;
        public float MaxX;
        public float MaxY;

        public static Rectangle UnitSquare()
        {
            return new Rectangle { MinX = -1, MaxX = 1, MinY = -1, MaxY = 1 };
        }

        public Rectangle Clone()
        {
            return new Rectangle { MinX = MinX,MinY = MinY, MaxX = MaxX, MaxY = MaxY };
        }

        public float GetWidth()
        {
            return MaxX - MinX;
        }

        public float GetHeight()
        {
            return MaxY - MinY;
        }
    }
    
}