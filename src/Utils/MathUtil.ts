import { vec2 } from "gl-matrix";

export function lerp(a : number, b: number, t: number) : number {
    return a * (1 - t) + b * t;
}

export function lerp2(a : vec2, b: vec2, t : number) {
    return vec2.fromValues(lerp(a[0], b[0], t), lerp(a[1], b[1], t));
}