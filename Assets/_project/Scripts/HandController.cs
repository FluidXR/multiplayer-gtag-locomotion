using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandController : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;
    // private ClassWithGrabFunctionGoesHere currentObject;
    
    private Vector3 previousPosition;
    private float velocityThreshold = 1.5f; // Minimum velocity to consider a throw

    private void Start()
    {
        TryInitialize();
        previousPosition = transform.position;
    }

    private void TryInitialize()
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    private void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripPressed))
        {
            if (gripPressed)
            {
                // if (currentObject == null)
                // {
                    // TryGrab();
                // }
            }
            else
            {
                // if (currentObject != null)
                // {
                    // Release();
                // }
            }
        }
        
        previousPosition = transform.position;
    }

    private void TryGrab()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                continue;
            }
            
            // ModPhoneController grabbable = collider.GetComponent<ModPhoneController>();
            // if (grabbable != null && !grabbable.isGrabbed)
            // {
                // currentObject = grabbable;
                // currentObject.Grab(transform);
                // break;
            // }
        }
    }

    private void Release()
    {
        // if (currentObject != null)
        // {
        //     // Calculate hand velocity
        //     Vector3 handVelocity = (transform.position - previousPosition) / Time.deltaTime;
        //     if (handVelocity.magnitude > velocityThreshold)
        //     {
        //         
        //     }
        //     currentObject.Release();
        //     currentObject = null;
        // }
    }
}