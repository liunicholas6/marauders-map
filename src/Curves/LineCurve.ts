import { vec2 } from "gl-matrix";
import Curve from "./Curve";

export class LineCurve extends Curve {
    p0 : vec2;
    p1 : vec2;

    constructor(p0 : vec2, p1 : vec2) {
        super();
        this.p0 = p0;
        this.p1 = p1;
    }

    length() {
        let d = vec2.create();
        vec2.sub(d, this.p0, this.p1);
        return vec2.len(d);
    }

    point(t : number) {
        let result = vec2.create();
        vec2.lerp(result, this.p0, this.p1, t);
        return result;
    }

    reverse() {
        return new LineCurve(this.p1, this.p0);
    }

    split(t : number) {
        let m = this.point(t);
        let res : [LineCurve, LineCurve] = [new LineCurve(this.p0, m), new LineCurve(m, this.p1)];
        return res;
    }
}