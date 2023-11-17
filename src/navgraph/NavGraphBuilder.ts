import { vec2 } from "gl-matrix";
import { Curve, curvePoint } from "../Curves/Curve";
import CurveDrawable from "../geometry/CurveDrawable";
import Drawable from "../rendering/gl/Drawable";
import { Edge, Vertex, NavGraph, VertexId, VertexTag, EdgeTag } from "./NavGraph";
import { Region, newPointRegion } from "../Regions/Regions";

export type BEdge = Edge & {u : VertexId}

export class NavGraphBuilder {
    numVerts : number = 0;
    verts : Vertex[] = []
    edges : BEdge[] = [];

    makeVertex(region : Region, tag : VertexTag) : VertexId {
        let res = this.numVerts;
        this.verts.push({region, tag});
        this.numVerts += 1;
        return res;
    }

    makeEdge(u : VertexId, v: VertexId, curve: Curve, tag?: EdgeTag) : BEdge {
        let edge : BEdge = {u: u, v: v, curve: curve, tag: tag};
        this.edges.push(edge);
        return edge;
    }

    splitEdge(edge: BEdge, tag: VertexTag, t: number) : [BEdge, VertexId, BEdge] {
        let [c0, c1] = spitCurve(edge.curve, t);
        let pos = curvePoint(c0, 1);
        let m = this.makeVertex(newPointRegion(pos), tag);

        let newEdge : BEdge = this.makeEdge(m, edge.v, c1, edge.tag);

        edge.v = m;
        edge.curve = c0;
        
        return [edge, m, newEdge];
    }

    toDrawable() : Drawable {
        let curves = [];
        for (const edge of this.edges) {
            curves.push(edge.curve);
        }
        console.log(this.edges);
        return new CurveDrawable(curves);
    }

    toNavGraph() : NavGraph {
        let adjList : Edge[][] = Array.from({length: this.numVerts}, () => []);
        for (const edge of this.edges) {
            adjList[edge.u].push({v : edge.v, curve: edge.curve});
            adjList[edge.v as number].push({v : edge.u, curve: curveReverse(edge.curve)})
        }
        return {numVerts: this.numVerts, verts: this.verts, adjList};
    }
}

function spitCurve(curve: Curve, t: number): [any, any] {
    throw new Error("Function not implemented.");
}
function curveReverse(curve: Curve): Curve {
    throw new Error("Function not implemented.");
}

