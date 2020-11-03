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
            var goMesh = GameObject.CreatePrimitive(PrimitiveType.Plane);
            goMesh.transform.parent = context.Transform;
            var meshFilter = goMesh.GetComponent<MeshFilter>();
            var meshRenderer = goMesh.GetComponent<MeshRenderer>();
            meshFilter.mesh = mesh;
            meshRenderer.material.color = context.Material.DiffuseColor.ToUnity();
            return goMesh;
        }
    }
}