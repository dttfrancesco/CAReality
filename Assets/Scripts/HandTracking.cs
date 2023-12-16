using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

public class HandTracking : MonoBehaviour
{
    public GameObject sphereMarker;
    public GameObject indexObject, wristObject, indexBaseObject;
    GameObject thumbObject, middleObject, ringObject, pinkyObject;
    MixedRealityPose pose;

    [SerializeField] private float projectileSpeed = 0.6f;
    [SerializeField] private GameObject projectilePrefab;

    // Threshold for detecting pinch and gun gestures
    [SerializeField] private float pinchThreshold = 0.04f;
    [SerializeField] private float gunThreshold = 0.05f;

    private bool hasShot = false;
    private bool isPinchDetected = false;
    private bool isGunGestureDetected = false;
    private bool wasPinchDetectedBeforeGunGesture = false;

    private void Start()
    {
        // Initialize finger objects
        thumbObject = GameObject.Instantiate(sphereMarker, this.transform);
        indexObject = GameObject.Instantiate(sphereMarker, this.transform);
        indexBaseObject = GameObject.Instantiate(sphereMarker, this.transform);
        middleObject = GameObject.Instantiate(sphereMarker, this.transform);
        ringObject = GameObject.Instantiate(sphereMarker, this.transform);
        pinkyObject = GameObject.Instantiate(sphereMarker, this.transform);
        wristObject = GameObject.Instantiate(sphereMarker, this.transform);
        wristObject.GetComponent<MeshRenderer>().enabled = false;
        indexBaseObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void Update()
    {
        //Disable renderers
        DisableRenderers();

        // Update finger positions
        UpdateJointPosition(TrackedHandJoint.ThumbTip, thumbObject);
        UpdateJointPosition(TrackedHandJoint.IndexTip, indexObject);
        UpdateJointPosition(TrackedHandJoint.MiddleTip, middleObject);
        UpdateJointPosition(TrackedHandJoint.RingTip, ringObject);
        UpdateJointPosition(TrackedHandJoint.PinkyTip, pinkyObject);
        UpdateJointPosition(TrackedHandJoint.IndexKnuckle, indexBaseObject);
        UpdateJointPosition(TrackedHandJoint.Wrist, wristObject);

        // Check for pinch and gun gestures
        if (IsPinch())
        {
            isPinchDetected = true;
            wasPinchDetectedBeforeGunGesture = true;
            hasShot = false;
        }
        else if (wasPinchDetectedBeforeGunGesture && IsGunGesture())
        {
            isGunGestureDetected = true;
            //Debug.Log("Shooting");
            ShootProjectile();
            wasPinchDetectedBeforeGunGesture = false; // Reset after shooting
        }
        else
        {
            isPinchDetected = false;
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
        indexBaseObject.GetComponent<MeshRenderer>().enabled = false;
    }

    void UpdateJointPosition(TrackedHandJoint joint, GameObject jointObject)
    {
        if (HandJointUtils.TryGetJointPose(joint, Handedness.Right, out pose))
        {
            jointObject.GetComponent<Renderer>().enabled = true;
            wristObject.GetComponent<Renderer>().enabled = false;
            indexBaseObject.GetComponent<MeshRenderer>().enabled = false;
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
        
        hasShot = false;
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
        if (hasShot) return; // Prevents shooting multiple projectiles

        // Instantiate the projectile at the index finger's position
        GameObject projectile = Instantiate(projectilePrefab, indexObject.transform.position, Quaternion.Euler(-90, 0, 0));
        projectile.AddComponent<Projectile>();
        if (projectile == null)
        {
            Debug.LogError("Projectile is not a valid object");
            return;
        }


        // Ensure the projectile has a Rigidbody component
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = projectile.AddComponent<Rigidbody>();
        }

        // Set initial Rigidbody properties
        rb.useGravity = false; 
        rb.isKinematic = false; // Allows the projectile to be affected by physics

        // Mark that a projectile has been shot
        hasShot = true;
    }



}
