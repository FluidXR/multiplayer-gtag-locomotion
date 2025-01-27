using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRig : MonoBehaviour
{
    public VRMap head;
    public VRMap lefthand;
    public VRMap rightHand;
    
    public Transform headConstraint;
    public Vector3 headBodyOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        // Update body position
        transform.position = headConstraint.position + headBodyOffset;

        // Calculate forward direction
        Vector3 headForward = headConstraint.forward;
        Vector3 bodyForward = new Vector3(headForward.x, 0, headForward.z).normalized;

        // Smoothly rotate the body
        Quaternion targetRotation = Quaternion.LookRotation(bodyForward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

        head.Map();
        lefthand.Map();
        rightHand.Map();
    }
}
