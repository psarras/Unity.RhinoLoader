using System.Collections;
using System.Collections.Generic;
using Rhino.DocObjects;
using UnityEngine;

public class RhinoLayerController : MonoBehaviour
{
    public Layer layer { get; private set; }

    public void SetLayer(Layer layer)
    {
        this.layer = layer;
    }
}
