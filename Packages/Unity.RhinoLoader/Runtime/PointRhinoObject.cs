using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RhinoLoader
{
    public class PointRhinoObject : RhinoObject
    {
        public override Type Type { get; } = typeof(Rhino.Geometry.Point);
        protected override GameObject CreateObject(RhinoUnityContext context)
        {
            var p = (Rhino.Geometry.Point) context.File3dmObject.Geometry;
            var goPointObj = Resources.Load("Prefabs/RhinoPoint") as GameObject;
            var go = Object.Instantiate(goPointObj, context.Transform);
            go.transform.position = p.ToHost();
            go.GetComponent<MeshRenderer>().material.color = context.DisplayColor;
            return go;
        }
    }
}