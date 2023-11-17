import {vec2} from "gl-matrix"
import {Curve} from "../Curves/Curve"
import { Region } from "../Regions/Regions"

export enum VertexTag {
    Intersection,
    Room
}

export enum EdgeTag {
    Hallway,
    Door
}

export type VertexId = number

export type Vertex = {
    region : Region
    tag?: VertexTag
}

export type Edge = {
    v : VertexId;
    curve : Curve;
    tag?: EdgeTag;
}

export type NavGraph = {
    numVerts : VertexId
    verts: Vertex[]
    adjList: Edge[][]
}