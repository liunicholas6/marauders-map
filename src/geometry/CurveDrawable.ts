import {vec3, vec4} from 'gl-matrix';
import Drawable from '../rendering/gl/Drawable';
import {gl} from '../globals';
import Curve from '../Curves/Curve';

const MIN_SEG_DENSITY = 32;

class CurveDrawable extends Drawable {
  indices: Uint32Array;
  positions: Float32Array;
  normals: Float32Array;
  curves: Curve[];

  constructor(curves: Curve[]) {
    super(); // Call the constructor of the super class. This is required.
    this.curves = curves;
  }

  create() {
    let indices : number[] = [];
    let positions : number[] = [];
    let normals : number[] = [];
    
    for (const curve of this.curves) {
      let indicesOffset = positions.length / 4;
      let length = curve.length();
      let segmentCount = Math.ceil(MIN_SEG_DENSITY * length);
      let segLen = length / segmentCount;
      
      for (let i = 0; i < segmentCount; i++) {
          indices.push(indicesOffset + i, indicesOffset + i + 1);
      }
      
      for (let i = 0; i <= segmentCount; i++) {
          let point = curve.point(i * segLen);
          positions.push(point[0], point[1], 0, 1);
          normals.push(0, 0, 1, 0);
      }
    }

    console.log(indices);
    
    this.indices = new Uint32Array(indices);
    this.normals = new Float32Array(normals);

    this.positions = new Float32Array(positions);

    this.generateIdx();
    this.generatePos();
    this.generateNor();

    this.count = this.indices.length;
    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, this.bufIdx);
    gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, this.indices, gl.STATIC_DRAW);

    gl.bindBuffer(gl.ARRAY_BUFFER, this.bufNor);
    gl.bufferData(gl.ARRAY_BUFFER, this.normals, gl.STATIC_DRAW);

    gl.bindBuffer(gl.ARRAY_BUFFER, this.bufPos);
    gl.bufferData(gl.ARRAY_BUFFER, this.positions, gl.STATIC_DRAW);
  }

  drawMode(): GLenum {
    return gl.LINES;
  }
};

export default CurveDrawable;
