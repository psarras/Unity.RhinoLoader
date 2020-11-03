using System;
using BitsNBobs.Patterns.Factory;
using UnityEngine;

namespace RhinoLoader
{
    public abstract class RhinoObject : IObject<GameObject>
    {
        public abstract Type Type { get; }

        public GameObject CreateObject(IContext context)
        {
            var rhinoUnityContext = context as RhinoUnityContext;
            if (rhinoUnityContext != null)
            {
                var go = CreateObject(rhinoUnityContext);
                go.name = rhinoUnityContext.GoName;
                BindRhinoObjectData(go, rhinoUnityContext);
                return go;
            }
            return null;
        }

        private static void BindRhinoObjectData(GameObject go, RhinoUnityContext context)
        {
            var rhinoObjectData = go.GetComponent<RhinoObjectData>();
            rhinoObjectData.File3dmObject = context.File3dmObject;
        }

        protected abstract GameObject CreateObject(RhinoUnityContext context);
    }
}