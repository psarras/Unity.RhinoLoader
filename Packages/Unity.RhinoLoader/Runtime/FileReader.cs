using System.Collections.Generic;
using System.IO;
using Rhino.FileIO;
using UnityEngine;

namespace RhinoLoader
{
    public class FileReader
    {
        public GameObject Read(string path, Transform transform)
        {
            var f = new FileInfo(path);
            var root = new GameObject($"File:{f.Name}");
            root.transform.parent = transform;
            var file = File3dm.Read(path);
            var materials = file.AllMaterials;

            List<GameObject> layers = new List<GameObject>();
            foreach (var layer in file.AllLayers)
            {
                var go = new GameObject($"Layer: {layer.Name}");
                go.transform.parent = root.transform;
                layers.Add(go);
            }
            
            foreach (var item in file.Objects)
            {
                var materialIndex = item.Attributes.MaterialIndex;
                var layerIndex = item.Attributes.LayerIndex;
                var mat = materials.FindIndex(materialIndex);
                
                
                // var childGo = new GameObject("Object: " + item.Id);
                // childGo.transform.parent = root.transform;

                var rhinoContext = new RhinoUnityContext()
                {
                    File = file,
                    File3dmObject = item,
                    Transform = layers[layerIndex].transform
                };

                if (RhinoFactory.CreateInputs(rhinoContext, out GameObject mobject))
                {
                    Debug.Log("Success!");
                }
            }

            return root;
        }
    }
}