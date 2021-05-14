using System;
using System.Linq;
using Rhino.DocObjects;
using UnityEngine;
using Object = UnityEngine.Object;
using Texture = Rhino.DocObjects.Texture;

namespace RhinoLoader
{
    public class MeshRhinoObject : RhinoObject
    {
        public override Type Type { get; } = typeof(Rhino.Geometry.Mesh);
        private const string Lit = "Prefabs/RhinoMesh";
        private const string LitVertex = "Prefabs/RhinoMeshLitVertex";
        private const string UnlitVertexWireframe = "Prefabs/RhinoMeshUnlitWireframe";
        private const string UnlitVertex = "Prefabs/RhinoMeshUnlit";

        protected override GameObject CreateObject(RhinoUnityContext context)
        {
            var m = (Rhino.Geometry.Mesh) context.File3dmObject.Geometry;
            var mesh = m.ToHost();
            var thickness = context.GetThickness;
            var hasThickness = thickness > 0.001f;

            var IsUnlit = false;
            if (context.HasMaterial)
            {
                var parts = context.Material.Name.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                IsUnlit = parts.Length > 2 && parts.Last().Equals("1");
            }

            var chosenResource = Lit;

            if (IsUnlit)
            {
                chosenResource = !hasThickness ? UnlitVertex : UnlitVertexWireframe;
            }
            else
            {
                var hasVertexColours = mesh.colors.Length > 0;
                chosenResource = hasVertexColours ? LitVertex : Lit;
            }

            var goPointObj = Resources.Load(chosenResource) as GameObject;
            var go = Object.Instantiate(goPointObj, context.Transform);
            var meshFilter = go.GetComponent<MeshFilter>();
            var meshRenderer = go.GetComponent<MeshRenderer>();
            var meshCollider = go.GetComponent<MeshCollider>();

            if (context.HasMaterial && IsUnlit && hasThickness)
            {
                meshRenderer.material.SetFloat("_WireframeVal", thickness / 5);
            }

            meshFilter.sharedMesh = mesh;
            meshCollider.sharedMesh = mesh;
            meshRenderer.material.color = context.DisplayColor;
            return go;

            return null;
        }
    }
}