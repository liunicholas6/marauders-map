import { vec2 } from "gl-matrix";
import Curve from "./Curve";

const TAU = Math.PI * 2;

export class ArcCurve extends Curve {
    radius : number;
    theta0 : number;
    theta1 : number;

    constructor(radius: number = 1, theta0: number = 0, theta1: number = TAU) {
        super();
        this.radius = radius;
        this.theta0 = theta0;
        this.theta1 = theta1;
    }

    length() {
        let length = this.radius * ((this.theta1 - this.theta0) % TAU);
        if (length == 0) {
            return TAU;
        }
        return length;
    }

    lerpTheta(t: number) : number {
        return this.theta0 * (1 - t) + this.theta1 * t;
    }

    point(t : number) {
        let theta = this.lerpTheta(t);
        return vec2.fromValues(this.radius * Math.cos(theta), this.radius * Math.sin(theta));
    }

    reverse() {
        return new ArcCurve(this.radius, this.theta1, this.theta0);
    }

    split(t : number) {
        let m = this.lerpTheta(t);
        let res : [ArcCurve, ArcCurve] = [new ArcCurve(this.radius, this.theta0, m), new ArcCurve(this.radius, m, this.theta1)];
        return res;
    }
}