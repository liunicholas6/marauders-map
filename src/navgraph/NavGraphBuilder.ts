import Curve from "../Curves/Curve";
import CurveDrawable from "../geometry/CurveDrawable";
import Drawable from "../rendering/gl/Drawable";
import { Edge, Vertex, NavGraph, VertexType } from "./NavGraph";

export class BEdge {
    v0 : Vertex
    v1 : Vertex
    curve : Curve
}

export class NavGraphBuilder {
    vertCount : number = 0;
    vertTypes : VertexType[] = [];
    edges : BEdge[] = [];

    makeVertex(type: VertexType) : Vertex {
        let res : Vertex = this.vertCount as Vertex;
        this.vertTypes.push(type);
        this.vertCount += 1;
        return res;
    }

    makeEdge(v0 : Vertex, v1: Vertex, curve: Curve) : BEdge {
        let edge : BEdge = {v0, v1, curve};
        this.edges.push(edge);
        return edge;
    }

    splitEdge(edge: BEdge, type: VertexType, t: number) : [BEdge, BEdge] {
        let v = this.makeVertex(type);
        let [c0, c1] = edge.curve.split(t);

        let newEdge : BEdge = this.makeEdge(v, edge.v1, c1);

        edge.v1 = v;
        edge.curve = c0;
        
        return [edge, newEdge];
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
        let adjList : Edge[][] = Array.from({length: this.vertCount}, () => []);
        for (const edge of this.edges) {
            adjList[edge.v0 as number].push({v : edge.v1, curve: edge.curve});
            adjList[edge.v1 as number].push({v : edge.v0, curve: edge.curve.reverse()})
        }
        return new NavGraph(this.vertTypes, adjList);
    }
}