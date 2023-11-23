using System.Collections.Generic;
using Geom;
using GraphBuilder;
using Navigation;
using Unity.VisualScripting;
using UnityEngine;

namespace Grammar
{
    public class BlockSymbol : Symbol
    {
        public override bool IsTerminal => false;
        private readonly EdgeId[] _edges = new EdgeId[4];

        public BlockSymbol(Builder builder, Vector2 nw, Vector2 se) : base(builder)
        {
            Vector2[] corners = new Vector2[4];
            corners[(int)Diagonal.NorthWest] = nw;
            corners[(int)Diagonal.NorthEast] = new Vector2(se.x, nw.x);
            corners[(int)Diagonal.SouthEast] = se;
            corners[(int)Diagonal.SouthWest] = new Vector2(nw.x, se.y);

            var vertices = new VertexId[4];
            for (var i = 0; i < 4; i++)
            {
                vertices[i] = Builder.MakeVertex(
                    new VertexInfo(new PointRegion(corners[i])));
            }

            for (var i = 0; i < 4; i++)
            {
                var j = (i + 1) % 4;
                _edges[i] = Builder.MakeEdge(vertices[i], vertices[j],
                    false, EdgeTag.None, new LineCurve(corners[i], corners[j]));
            }
        }

        public BlockSymbol(Builder builder) : base(builder)
        {
        }

        private (BlockSymbol, BlockSymbol) splitVertical(float t)
        {
            var (nwEdge, nVertex, neEdge) =
                Builder.SplitEdge(_edges[(int)Cardinal.North], t, VertexTag.Intersection);
            var (seEdge, sVertex, swEdge) =
                Builder.SplitEdge(_edges[(int)Cardinal.South], 1 - t, VertexTag.Intersection);

            var westBlock = new BlockSymbol(Builder);
            westBlock._edges[(int)Cardinal.North] = nwEdge;
            westBlock._edges[(int)Cardinal.East] =
                Builder.MakeEdge(nVertex, sVertex, true, EdgeTag.Hallway);
            westBlock._edges[(int)Cardinal.South] = swEdge;
            westBlock._edges[(int)Cardinal.West] = _edges[(int)Cardinal.West];

            var eastBlock = new BlockSymbol(Builder);
            eastBlock._edges[(int)Cardinal.North] = neEdge;
            eastBlock._edges[(int)Cardinal.East] = _edges[(int)Cardinal.East];
            eastBlock._edges[(int)Cardinal.South] =
                Builder.MakeEdge(sVertex, nVertex, true, EdgeTag.Hallway);
            eastBlock._edges[(int)Cardinal.West] = seEdge;

            return (westBlock, eastBlock);
        }

        private (BlockSymbol, BlockSymbol) splitHorizontal(float t)
        {
            var (swEdge, wVertex, nwEdge) =
                Builder.SplitEdge(_edges[(int)Cardinal.West], t, VertexTag.Intersection);
            var (neEdge, eVertex, seEdge) =
                Builder.SplitEdge(_edges[(int)Cardinal.East], 1 - t, VertexTag.Intersection);

            var northBlock = new BlockSymbol(Builder);
            northBlock._edges[(int)Cardinal.North] = _edges[(int)Cardinal.North];
            northBlock._edges[(int)Cardinal.East] = neEdge;
            northBlock._edges[(int)Cardinal.South] =
                Builder.MakeEdge(eVertex, wVertex, true, EdgeTag.Hallway);
            northBlock._edges[(int)Cardinal.West] = nwEdge;

            var southBlock = new BlockSymbol(Builder);
            southBlock._edges[(int)Cardinal.North] = Builder.MakeEdge(wVertex, eVertex, true, EdgeTag.Hallway);
            southBlock._edges[(int)Cardinal.East] = seEdge;
            southBlock._edges[(int)Cardinal.South] = _edges[(int)Cardinal.East];
            southBlock._edges[(int)Cardinal.West] = swEdge;

            return (northBlock, southBlock);
        }

        public override List<Symbol> ApplyRule()
        {
            throw new System.NotImplementedException();
        }
    }
}