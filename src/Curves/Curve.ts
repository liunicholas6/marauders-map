import {vec2, vec3} from "gl-matrix"
import Drawable from "../rendering/gl/Drawable";
import CurveDrawable from "../geometry/CurveDrawable";

abstract class Curve {
    abstract length() : number;
    abstract point(t : number) : vec2;
    abstract reverse() : Curve;
    abstract split(t : number) : [Curve, Curve]
    toDrawable() : Drawable {
        return new CurveDrawable([this]);
    }
}

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



export default Curve;