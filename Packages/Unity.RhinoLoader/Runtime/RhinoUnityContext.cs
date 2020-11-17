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

        public string GoName => $"{File3dmObject.Geometry.ObjectType}:{File3dmObject.Id}";

        public Rhino.DocObjects.Material Material =>
            File.AllMaterials.FindIndex(File3dmObject.Attributes.MaterialIndex);

        public bool HasMaterial => Material != null;

        public Color DisplayColor
        {
            get
            {
                if (HasMaterial)
                {
                    return Material.DiffuseColor.ToUnity();
                }
                else
                {
                    var layer = File.AllLayers.FindIndex(File3dmObject.Attributes.LayerIndex);

                    var color = layer.Color.ToUnity();
                    if (color == Color.black)
                        return Color.white;
                    return color;
                }
                
                
                return File3dmObject.Attributes.ObjectColor.ToUnity();
                
            }
        }
    }

}