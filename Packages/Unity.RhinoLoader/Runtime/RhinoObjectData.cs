using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rhino.FileIO;
using UnityEngine;

public class RhinoObjectData : MonoBehaviour
{
    public File3dmObject File3dmObject { get; private set; }
    [SerializeField] private List<ListStrings> Values;

    public void Bind(File3dmObject contextFile3dmObject)
    {
        File3dmObject = contextFile3dmObject;
        //Keys = new List<string>();
        Values = new List<ListStrings>();
        //ValuesStrings = new List<string>();

        if (contextFile3dmObject.Attributes.UserStringCount == 0)
            return;

        var pairs = contextFile3dmObject.Attributes.GetUserStrings();
        var Keys = pairs.AllKeys.ToList();
        foreach (var key in Keys)
        {
            var strings = pairs.GetValues(key);
            Values.Add(new ListStrings() {Key = key, Values = strings});
        }
    }

    [Serializable]
    public class ListStrings
    {
        public string Key;
        public string[] Values;
    }
}