using System;
using BitsNBobs.Singleton;
using UnityEngine;
using UnityEngine.EventSystems;

public class RhinoObjectSelector : Singleton<RhinoObjectSelector>
{
    private Camera cam;
    [SerializeField] private LayerMask layerMask;
    public RhinoObjectData RhinoObjectData;
    public event Action<RhinoObjectData> OnUpdated;
    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject(-1))
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 99999,
                layerMask))
            {
                // Select
                RhinoObjectData = hitInfo.collider.GetComponent<RhinoObjectData>();
                if(RhinoObjectData != null)
                    OnUpdated?.Invoke(RhinoObjectData);
                //Debug.Log(hitInfo.collider.name);
            }
            else
            {
                // Deselect
                RhinoObjectData = null;
                OnUpdated?.Invoke(RhinoObjectData);
            }
        }
    }
}