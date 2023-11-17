// import { vec2 } from "gl-matrix";
// import { BEdge, NavGraphBuilder } from "../navgraph/NavGraphBuilder";
// import { NavGraph } from "../navgraph/NavGraph";
// import { Maybe } from "../Utils/Maybe";

// enum Cardinal {
//     North = 0,
//     East = 1,
//     South = 2,
//     West = 3,
// }

// type Four<T> = [T, T, T, T]

// export class Block {
//     builder : NavGraphBuilder;
//     borders : Four<Maybe<BEdge>>;

//     private constructor(builder: NavGraphBuilder, borders : Four<Maybe<BEdge>>) {
//         this.builder = builder;
//         this.borders = borders;
//     }

//     static createInstance(builder : NavGraphBuilder, nw : vec2, se : vec2) : Block {
//         let ne = vec2.fromValues(se[0], nw[1]);
//         let sw = vec2.fromValues(nw[0], se[1]);
//         let points = [nw, ne, se, sw];
//         let verts : [vec2, Vertex][] = points.map((p) => [p, builder.makeVertex(VertexType.Hall)]);
//         let edges : [BEdge, BEdge, BEdge, BEdge] = [new BEdge(), new BEdge(), new BEdge(), new BEdge()];
//         for (let d in Cardinal) {
//             if (isNaN(Number(d))) {
//                 break;
//             }
//             let [p0, v0] = verts[Number(d)];
//             let [p1, v1] = verts[(Number(d) + 1) % 4];
//             edges[d] = builder.makeEdge(v0, v1, new LineCurve(p0, p1));
//         }
//         return new Block(builder, edges);
//     }

//     splitNS(t : number) {
//         let [n1, vn, n2] = this.builder.splitEdge(this.borders[Cardinal.North], VertexType.Hall, t);
//         let [s2, vs, s1] = this.builder.splitEdge(this.borders[Cardinal.South], VertexType.Hall, 1 - t);
//     }
// }