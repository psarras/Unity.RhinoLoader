using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RhinoLoader
{
    public class CurveRhinoObject : RhinoObject
    {
        public override Type Type { get; } = typeof(Rhino.Geometry.Curve);
        protected override GameObject CreateObject(RhinoUnityContext context)
        {
            var curve = (Rhino.Geometry.Curve) context.File3dmObject.Geometry;
            var nurbsCurve = curve.ToNurbsCurve();
            var points = nurbsCurve.ToHost();

            var rhinoCurveRenderer = Resources.Load("Prefabs/RhinoCurveRenderer") as GameObject;
            var go = Object.Instantiate(rhinoCurveRenderer, context.Transform);

            var lineRe = go.GetComponent<LineRenderer>();

            if (context.HasMaterial)
            {
                var thickness = context.GetThickness;
                if (thickness != float.NaN)
                {
                    lineRe.startWidth = thickness;
                    lineRe.endWidth = thickness;
                    lineRe.widthMultiplier = thickness;
                }
            }

            lineRe.positionCount = points.Length;
            lineRe.SetPositions(points);
            lineRe.material.color = context.DisplayColor;
            return go;
        }
    }
}