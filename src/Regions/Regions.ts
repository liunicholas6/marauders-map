import { vec2 } from "gl-matrix";

export type PointRegion = {
    kind: "point",
    pt: vec2
}

export function newPointRegion(pt : vec2) : PointRegion {
    return {kind: "point", pt: pt};
}

export type RectangleRegion = {
    kind: "rectangle",
    nw : vec2,
    se : vec2
}

export function newRectangleRegion(nw: vec2, se : vec2) {
    return {kind: "rectangle", nw: nw, se: se};
}

// export type ArcRegion = {kind: "arc", theta0: number, theta1: number, rIn : number, rOut: number}
export type Region = PointRegion | RectangleRegion // | ArcRegion

