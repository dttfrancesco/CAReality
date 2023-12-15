using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

public class HandTracking : MonoBehaviour
{
    public GameObject sphereMarker;
    GameObject thumbObject, indexObject, middleObject, ringObject, pinkyObject, wristObject;
    MixedRealityPose pose;
    [SerializeField] private float projectileSpeed = 6f;

    // Threshold for detecting pinch
    [SerializeField] private float pinchThreshold = 0.03f;
    // Threshold for detecting gunGesture
    [SerializeField] private float gunThreshold = 0.05f;

    // Gesture state
    private bool isPinchDetected = false;
    private bool isGunGestureDetected = false;
    private bool wasPinchDetectedBeforeGunGesture = false;

    private void Start()
    {
        thumbObject = GameObject.Instantiate(sphereMarker, this.transform);
        indexObject = GameObject.Instantiate(sphereMarker, this.transform);
        middleObject = GameObject.Instantiate(sphereMarker, this.transform);
        ringObject = GameObject.Instantiate(sphereMarker, this.transform);
        pinkyObject = GameObject.Instantiate(sphereMarker, this.transform);
        wristObject = GameObject.Instantiate(sphereMarker, this.transform);
        wristObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void Update()
    {
        // Disabling the renderer for all objects
        DisableRenderers();

        // Updating the positions of all joints
        UpdateJointPosition(TrackedHandJoint.ThumbTip, thumbObject);
        UpdateJointPosition(TrackedHandJoint.IndexTip, indexObject);
        UpdateJointPosition(TrackedHandJoint.MiddleTip, middleObject);
        UpdateJointPosition(TrackedHandJoint.RingTip, ringObject);
        UpdateJointPosition(TrackedHandJoint.PinkyTip, pinkyObject);
        UpdateJointPosition(TrackedHandJoint.Wrist, wristObject);

        if (IsPinch())
        {
            isPinchDetected = true;
            wasPinchDetectedBeforeGunGesture = true;
        }
        else
        {
            isPinchDetected = false;
        }

        // If there was a pinch, check for a gun gesture
        if (wasPinchDetectedBeforeGunGesture)
        {
            if (IsGunGesture())
            {
                isGunGestureDetected = true;
                Debug.Log("Shooting");
                //ShootProjectile();
                wasPinchDetectedBeforeGunGesture = false; // Reset after shooting
            }
        }
        else
        {
            isGunGestureDetected = false;
        }
    }

    void DisableRenderers()
    {
        thumbObject.GetComponent<Renderer>().enabled = false;
        indexObject.GetComponent<Renderer>().enabled = false;
        middleObject.GetComponent<Renderer>().enabled = false;
        ringObject.GetComponent<Renderer>().enabled = false;
        pinkyObject.GetComponent<Renderer>().enabled = false;
        wristObject.GetComponent<Renderer>().enabled = false;
    }

    void UpdateJointPosition(TrackedHandJoint joint, GameObject jointObject)
    {
        if (HandJointUtils.TryGetJointPose(joint, Handedness.Right, out pose))
        {
            jointObject.GetComponent<Renderer>().enabled = true;
            wristObject.GetComponent<Renderer>().enabled = false;
            jointObject.transform.position = pose.Position;
        }
    }

    bool IsPinch()
    {
        // Check the distance between the thumb and the index finger
        float thumbIndexDistance = Vector3.Distance(thumbObject.transform.position, indexObject.transform.position);
        if (thumbIndexDistance >= pinchThreshold) // pinchThreshold might be around 0.02f to 0.03f based on your scale
        {
            return false;
        }

        // Ensure that other fingers are not close to the thumb or index finger
        if (IsFingerCloseToThumbOrIndex(middleObject) ||
            IsFingerCloseToThumbOrIndex(ringObject) ||
            IsFingerCloseToThumbOrIndex(pinkyObject))
        {
            return false;
        }

        return true;
    }

    bool IsFingerCloseToThumbOrIndex(GameObject fingerObject)
    {
        float distanceToThumb = Vector3.Distance(fingerObject.transform.position, thumbObject.transform.position);
        float distanceToIndex = Vector3.Distance(fingerObject.transform.position, indexObject.transform.position);
        return distanceToThumb < 0.05f || distanceToIndex < 0.05f; // Threshold might be around 0.05f
    }


    bool IsGunGesture()
    {
        if (IsFingerCloseToThumbOrIndex(middleObject) ||
            IsFingerCloseToThumbOrIndex(ringObject) ||
            IsFingerCloseToThumbOrIndex(pinkyObject))
        {
            return false;
        }

        float thumbIndexDistance = Vector3.Distance(thumbObject.transform.position, indexObject.transform.position);
        if (thumbIndexDistance < gunThreshold) 
        {
            return false;
        }

        return true;
    }

    void ShootProjectile()
    {
        /*GameObject projectile = Instantiate(projectilePrefab, indexObject.transform.position, Quaternion.identity);
        projectile.AddComponent<Projectile>();
        Rigidbody rb = projectile.AddComponent<Rigidbody>();
        rb.useGravity = false;
        Vector3 shootingDirection = (indexObject.transform.position - wristObject.transform.position).normalized;
        rb.velocity = shootingDirection * projectileSpeed;*/
    }

}
