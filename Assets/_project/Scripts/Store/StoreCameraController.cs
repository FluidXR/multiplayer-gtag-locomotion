using System;
using UnityEngine;

public class StoreCameraController : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;

    private void Start()
    {
         meshRenderer.enabled = false;
         StoreCamera.Instance.TurnOffCamera();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        meshRenderer.enabled = true;
        StoreCamera.Instance.TurnOnCamera();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        meshRenderer.enabled = false;
        StoreCamera.Instance.TurnOffCamera();
    }
}
