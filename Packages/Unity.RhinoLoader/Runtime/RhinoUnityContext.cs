using System;
using BitsNBobs.Patterns.Factory;
using Rhino.FileIO;
using UnityEngine;

namespace RhinoLoader
{
    public class RhinoUnityContext : IContext
    {
        public File3dm File { get; set; }
        public Transform Transform { get; set; }
        public File3dmObject File3dmObject { get; set; }
        public Type Type => File3dmObject.Geometry.GetType();

        public Rhino.DocObjects.Material Material =>
            File.AllMaterials.FindIndex(File3dmObject.Attributes.MaterialIndex);
    }
}