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
            
            if (context.HasMaterial)
            {
                var thickness = context.GetThickness;
                if (thickness != float.NaN)
                {
                    go.transform.localScale = thickness * Vector3.one;
                }
            }
            
            go.GetComponent<MeshRenderer>().material.color = context.DisplayColor;
            return go;
        }
    }
}