namespace BinaryPartition
{
    public struct Rectangle
    {
        public float MinX;
        public float MinY;
        public float MaxX;
        public float MaxY;

        public static Rectangle UnitSquare()
        {
            return new Rectangle { MinX = -1, MaxX = 1, MinY = -1, MaxY = 1 };
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