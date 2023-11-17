import {vec2, vec3} from "gl-matrix"
import Drawable from "../rendering/gl/Drawable";
import CurveDrawable from "../geometry/CurveDrawable";
import {lerp} from "../Utils/MathUtil"

type LineCurve = {
    kind: "line",
    p0: vec2,
    p1: vec2
}

export function newLineCurve(p0: vec2, p1: vec2) : LineCurve {
    return {kind: "line", p0: p0, p1: p1};
}

type ArcCurve = {
    kind: "arc",
    radius: number,
    theta0: number,
    theta1: number,
}

export function newArcCurve(radius: number, theta0: number, theta1: number) : ArcCurve {
    return {kind: "arc", radius: radius, theta0: theta0, theta1: theta1};
}

export type Curve = LineCurve | ArcCurve

export function curvePoint(curve: Curve, t: number) : vec2 {
    switch (curve.kind) {
        case "line" : {
            let result = vec2.create();
            vec2.lerp(result, curve.p0, curve.p1, t);
            return result;
        }
        case "arc" : {
            let theta = lerp(curve.theta0, curve.theta1, t);
            return vec2.fromValues(curve.radius * Math.cos(theta), curve.radius * Math.sin(theta));
        }
    }
}

export function curveLength(curve : Curve) : number {
    switch (curve.kind) {
        case "line" : {
            let d = vec2.create();
            vec2.sub(d, curve.p0, curve.p1);
            return vec2.len(d);
        }
        case "arc" : {
            return curve.radius * (curve.theta1 - curve.theta0);
        }
    }
}

function curveSplit(curve : Curve, t: number) : [Curve, Curve] {
    switch (curve.kind) {
        case "line" : {
            let m = curvePoint(curve, t);
            let res : [LineCurve, LineCurve] = [newLineCurve(curve.p0, m), newLineCurve(m, curve.p1)];
            return res;
        }
        case "arc" : {
            let m = lerp(curve.theta0, curve.theta1, t);
            let res : [ArcCurve, ArcCurve] =
                [newArcCurve(curve.radius, curve.theta0, m), newArcCurve(curve.radius, m, curve.theta1)];
            return res;
        }
    }
}

function curveReverse(curve: Curve) : Curve {
    switch (curve.kind) {
        case "line" : {
            return newLineCurve(curve.p1, curve.p0);
        }
        case "arc" : {
            let theta0 = curve.theta1;
            let theta1 = curve.theta0;
            newArcCurve(curve.radius, curve.theta1, curve.theta0 + 2 * Math.PI)
        }
    }
}