import { vec2 } from "gl-matrix";
import { BEdge, NavGraphBuilder } from "../navgraph/NavGraphBuilder";
import { NavGraph, Vertex, VertexType } from "../navgraph/NavGraph";
import { LineCurve } from "../Curves/Curve";

enum Cardinal {
    North = 0,
    East = 1,
    South = 2,
    West = 3,
}

export class Block {
    builder : NavGraphBuilder;
    edges : [BEdge, BEdge, BEdge, BEdge];

    private constructor(builder: NavGraphBuilder, edges : [BEdge, BEdge, BEdge, BEdge]) {
        this.builder = builder;
        this.edges = edges;
    }

    static createInstance(builder : NavGraphBuilder, nw : vec2, se : vec2) : Block {
        let ne = vec2.fromValues(se[0], nw[1]);
        let sw = vec2.fromValues(nw[0], se[1]);
        let points = [nw, ne, se, sw];
        let verts : [vec2, Vertex][] = points.map((p) => [p, builder.makeVertex(VertexType.Hall)]);
        let edges : [BEdge, BEdge, BEdge, BEdge] = [new BEdge(), new BEdge(), new BEdge(), new BEdge()];
        for (let d in Cardinal) {
            if (isNaN(Number(d))) {
                break;
            }
            let [p0, v0] = verts[Number(d)];
            let [p1, v1] = verts[(Number(d) + 1) % 4];
            edges[d] = builder.makeEdge(v0, v1, new LineCurve(p0, p1));
        }
        return new Block(builder, edges);
    }
}