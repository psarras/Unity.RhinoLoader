﻿using System.Collections.Generic;
using System.IO;
using Rhino.FileIO;
using UnityEngine;

namespace RhinoLoader
{
    public class FileReader
    {
        public static GameObject Read(string path, Transform transform, out RhinoFileInfo rhinoFileInfo)
        {
            var f = new FileInfo(path);    
            var root = new GameObject($"File:{f.Name}");
            root.transform.parent = transform;
            rhinoFileInfo = root.AddComponent<RhinoFileInfo>();
            var file = File3dm.Read(path);
            rhinoFileInfo.File3dm = file;
            rhinoFileInfo.Description = file.Notes?.Notes;
            rhinoFileInfo.FullPath = path;
            var materials = file.AllMaterials;

            List<GameObject> layers = new List<GameObject>();
            foreach (var layer in file.AllLayers)
            {
                var go = new GameObject($"Layer: {layer.Name}");
                go.AddComponent<RhinoLayerController>().SetLayer(layer);
                go.transform.parent = root.transform;
                layers.Add(go);
            }
            
            foreach (var item in file.Objects)
            {
                var materialIndex = item.Attributes.MaterialIndex;
                var layerIndex = item.Attributes.LayerIndex;
                var mat = materials.FindIndex(materialIndex);
                
                var context = new RhinoUnityContext()
                {
                    File = file,
                    File3dmObject = item,
                    Transform = layers[layerIndex].transform
                };

                if (RhinoFactory.CreateInputs(context, out GameObject mobject))
                {
                }
            }

            return root;
        }
    }
}