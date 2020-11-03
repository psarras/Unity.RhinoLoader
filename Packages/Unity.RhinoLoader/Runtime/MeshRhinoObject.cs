using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RhinoLoader
{
    public class MeshRhinoObject : RhinoObject
    {
        public override Type Type { get; } = typeof(Rhino.Geometry.Mesh);

        protected override GameObject CreateObject(RhinoUnityContext context)
        {
            var m = (Rhino.Geometry.Mesh) context.File3dmObject.Geometry;
            var mesh = m.ToHost();
            var goPointObj = Resources.Load("Prefabs/RhinoMesh") as GameObject;
            var go = Object.Instantiate(goPointObj, context.Transform);
            var meshFilter = go.GetComponent<MeshFilter>();
            var meshRenderer = go.GetComponent<MeshRenderer>();
            meshFilter.mesh = mesh;
            meshRenderer.material.color = context.Material.DiffuseColor.ToUnity();
            return go;
        }
    }
}