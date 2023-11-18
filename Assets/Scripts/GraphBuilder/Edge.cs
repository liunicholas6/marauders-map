using System;
using Geom;
using Navigation;
using UnityEngine;

namespace GraphBuilder
{
    public struct Edge
    {
        public int FromVertex { get; internal set; }
        public int ToVertex { get; internal set; }
        public ICurve Curve { get; internal set; }
        public EdgeTag Tag;
        public bool Navigable;

        public Edge(EdgeTag tag, bool navigable, int fromVertex, int toVertex, ICurve curve)
        {
            Tag = tag;
            Navigable = navigable;
            FromVertex = fromVertex;
            ToVertex = toVertex;
            Curve = curve;
        }
        
        public Edge Clone()
        {
            return new Edge(Tag, Navigable, FromVertex, ToVertex, Curve);
        }

        public (Navigation.Edge, Navigation.Edge) ToNavEdges()
        {
            return (new Navigation.Edge(ToVertex, Curve, Tag), new Navigation.Edge(FromVertex, Curve.Reverse(), Tag));
        }
        
        internal (Vector2, Edge) Split(float t, int midVertex)
        {
            var other = Clone();
            var (leftCurve, midPoint, rightCurve) = Curve.Split(t);
            
            Curve = leftCurve;
            ToVertex = midVertex;

            other.Curve = rightCurve;
            other.FromVertex = midVertex;
            
            return (midPoint, other);
        }        
    }
}