import {vec2} from "gl-matrix"
import {Curve} from "../Curves/Curve"

export enum VertexType {
    Exterior,
    Hall,
    Door,
    Room,
}

export type Vertex =
    number & { readonly __tag: unique symbol };

export type Edge = {
    v : Vertex;
    curve : Curve;
}

export class NavGraph {
    vertexTypes : VertexType[];
    adjList : Edge[][]

    constructor(typeList : VertexType[], adjList : Edge[][]) {
        this.vertexTypes = typeList;
        this.adjList = adjList;
    }
    
    getType(v : Vertex) : VertexType {
        return this.vertexTypes[v as number];
    }

    getEdges(v : Vertex) : Edge[] {
        return this.adjList[v as number]
    }
}