using System;
using System.Linq;
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

            var parts = context.Material.Name.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
            var IsUnlit = parts.Length > 2 && parts.Last().Equals("1");
            
            var goPointObj = Resources.Load(IsUnlit ? "Prefabs/RhinoMeshUnlit" : "Prefabs/RhinoMesh") as GameObject;
            var go = Object.Instantiate(goPointObj, context.Transform);
            var meshFilter = go.GetComponent<MeshFilter>();
            var meshRenderer = go.GetComponent<MeshRenderer>();
            var meshCollider = go.GetComponent<MeshCollider>();
            meshFilter.sharedMesh = mesh;
            meshCollider.sharedMesh = mesh;
            meshRenderer.material.color = context.Material.DiffuseColor.ToUnity();
            return go;
        }
    }
}