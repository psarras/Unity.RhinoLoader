using System;
using UnityEngine;

namespace RhinoLoader
{
    public class MeshRhinoObject : RhinoObject
    {
        public override Type Type { get; } = typeof(Rhino.Geometry.Mesh);

        protected override GameObject CreateObject(RhinoUnityContext context)
        {
            var m = (Rhino.Geometry.Mesh) context.File3dmObject.Geometry;
            var mesh = m.ToHost();
            var go = GameObject.CreatePrimitive(PrimitiveType.Plane);
            go.transform.parent = context.Transform;
            var meshFilter = go.GetComponent<MeshFilter>();
            var meshRenderer = go.GetComponent<MeshRenderer>();
            meshFilter.mesh = mesh;
            meshRenderer.material.color = context.Material.DiffuseColor.ToUnity();
            return go;
        }
    }
}