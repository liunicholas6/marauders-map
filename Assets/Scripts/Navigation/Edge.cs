using Geom;

namespace Navigation
{
    public struct Edge
    {
        public int ToVertex;
        public Geom.ICurve Curve;
        public EdgeTag Tag;

        public Edge(int toVertex, ICurve curve, EdgeTag tag)
        {
            ToVertex = toVertex;
            Curve = curve;
            Tag = tag;
        }
    }
}