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
            return CreateObject(context as RhinoUnityContext);
        }

        protected abstract GameObject CreateObject(RhinoUnityContext context);
    }
}