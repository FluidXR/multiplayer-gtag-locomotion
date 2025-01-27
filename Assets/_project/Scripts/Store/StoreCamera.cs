using System;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class StoreCamera : Singleton<StoreCamera>
{
    [SerializeField] private Camera _camera;
    private void Start()
    {
        _camera = GetComponent<Camera>();

    }

    public void TurnOnCamera()
    {
        _camera.enabled = true;
    }

    public void TurnOffCamera()
    {
        _camera.enabled = false;
    }

}
