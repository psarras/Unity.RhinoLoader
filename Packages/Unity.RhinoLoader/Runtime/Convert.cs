using System.Collections.Generic;
using System.Linq;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using UnityEngine;

namespace RhinoLoader
{
    public static class Convert
    {
        #region ToRhino
        public static Point3d ToRhino(this Vector3 p) => new Point3d((double)p.x, (double)p.z, (double)p.y);

        static public IEnumerable<Point3d> ToRhino(this ICollection<Vector3> points)
        {
            var result = new List<Point3d>(points.Count);
            foreach (var p in points)
                result.Add(p.ToRhino());

            return result;
        }

        static public System.Drawing.Color ToRhino(this UnityEngine.Color color)
        {
            return System.Drawing.Color.FromArgb((int)(color.a * 255), (int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255));
        }
        
        static public Rhino.Geometry.Mesh ToRhino(this UnityEngine.Mesh mesh)
        {
            var m = new Rhino.Geometry.Mesh();

            foreach (var v in mesh.vertices)
            {
                m.Vertices.Add(v.ToRhino());
            }

            for (int i = 0; i < mesh.triangles.Length; i+=3)
            {
                m.Faces.AddFace(i, i + 1, i + 2);
            }

            foreach (var c in mesh.colors)
            {
                m.VertexColors.Add(c.ToRhino());
            }
        
            //m.RebuildNormals();

            return m;
        }

        #endregion

        #region ToHost
        static public Vector3 ToHost(this Point3d p) => new Vector3((float)p.X, (float)p.Z, (float)p.Y);
        public static Vector3 ToHost(this Point p) => new Vector3((float)p.Location.X, (float)p.Location.Z, (float)p.Location.Y);
        static public Vector3 ToHost(this Point3f p) => new Vector3(p.X, p.Z, p.Y);
        static public Vector3 ToHost(this Vector3d p) => new Vector3((float)p.X, (float)p.Z, (float)p.Y);
        static public Vector3 ToHost(this Vector3f p) => new Vector3(p.X, p.Z, p.Y);

        static public List<Vector3> ToHost(this ICollection<Point3f> points)
        {
            var result = new List<Vector3>(points.Count);
            foreach (var p in points)
                result.Add(p.ToHost());

            return result;
        }

        static public List<Vector3> ToHost(this ICollection<Vector3f> vectors)
        {
            var result = new List<Vector3>(vectors.Count);
            foreach (var p in vectors)
                result.Add(p.ToHost());

            return result;
        }

        static public UnityEngine.Mesh ToHost(this Rhino.Geometry.Mesh _mesh)
        {
            var result = new UnityEngine.Mesh();
            using (var mesh = _mesh.DuplicateMesh())
            {
                mesh.Faces.ConvertQuadsToTriangles();

                result.SetVertices(mesh.Vertices.ToHost());
                result.SetNormals(mesh.Normals.ToHost());

                int i = 0;
                int[] indices = new int[mesh.Faces.Count * 3];
                foreach (var face in mesh.Faces)
                {
                    indices[i++] = (face.C);
                    indices[i++] = (face.B);
                    indices[i++] = (face.A);
                }

                result.SetColors(mesh.VertexColors.ToHost());
                result.SetIndices(indices, MeshTopology.Triangles, 0);
            }
            result.RecalculateBounds();
            result.RecalculateNormals();
            result.RecalculateTangents();
            return result;
        }

        // static public UnityEngine.Mesh ToHost(this Brep brep)
        // {
        //     var meshes = MeshCompute.CreateFromBrep(brep);
        //     var mesh = new Rhino.Geometry.Mesh();
        //
        //     foreach (var m in meshes)
        //     {
        //         mesh.Append(m);
        //     }
        //
        //     return mesh.ToHost();
        // }


        static public Vector3[] ToHost(this NurbsCurve curve)
        {
            if(curve.TryGetPolyline(out Polyline poly))
            {
                return poly.Select(x => x.ToHost()).ToArray();
            }
            else
            {
                //var rebuild = curve.Rebuild((int)(curve.GetLength() * 2), 1, true);
                //curve.MaxCurvaturePoints
                if (curve.TryGetPolyline(out Polyline poly2))
                {
                    return poly2.Select(x => x.ToHost()).ToArray();
                }
            }
            return new Vector3[0];
        }

        static public List<UnityEngine.Color> ToHost(this MeshVertexColorList colors)
        {

            var result = new List<UnityEngine.Color>();
            foreach (var c in colors)
                result.Add(ToHost(c));

            return result;
        }

        static public UnityEngine.Color ToHost(System.Drawing.Color c)
        {
            return new UnityEngine.Color(c.R / 255f, c.G / 255f, c.B / 255f, c.A / 255f);
        }

        #endregion
    }
}
