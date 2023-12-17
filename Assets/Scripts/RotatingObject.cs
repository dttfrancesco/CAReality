using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RotatingObject : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.up; // Set the axis of rotation (up, right, forward, etc.)
    public float rotationSpeed = 5f; // Rotation speed

    [Header("Action Probabilities")]
    [Range(0, 100)] public int lightningProbability = 50; // Probability for action 1
    [Range(0, 100)] public int rocketProbability = 50; // Probability for action 2
    List<GameObject> scaledObjects = new List<GameObject>();
    [SerializeField] private GameObject thunderObject;
    private bool isPowerUpActive = false;


    void Update()
    {
        // Rotate the object around its axis
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isPowerUpActive) return;
        // Randomly decide which action to perform
        int randomValue = Random.Range(0, 100);
        if (other.gameObject.CompareTag("car1") || other.gameObject.CompareTag("car2") || other.gameObject.CompareTag("car3"))
        {
            if (randomValue < lightningProbability)
            {
                Lightning(other);
            }
            else if (randomValue < lightningProbability + rocketProbability)
            {
                Rocket(other);
            }
        }
    }

    void Lightning(Collider other)
    {
        // Find the GameObject with the tag "thunder" and make it active
        if (thunderObject != null)
        {
            thunderObject.SetActive(true);
            isPowerUpActive = true;
        }

        // Clear the list for new entries if it's the first interaction
        if (!scaledObjects.Any())
        {
            scaledObjects.Clear();
        }

        // Function to scale and add to list if not already scaled
        void ScaleAndAdd(GameObject car)
        {
            if (car != null && !scaledObjects.Contains(car))
            {
                car.transform.localScale *= 0.5f;  // Scale divided by 2
                scaledObjects.Add(car);
            }
        }

        // Check if the collider tag is "car1"
        if (other.gameObject.CompareTag("car1"))
        {
            // Find objects with tag "car2" and "car3" and apply scale if not already done
            ScaleAndAdd(GameObject.FindGameObjectWithTag("car2"));
            ScaleAndAdd(GameObject.FindGameObjectWithTag("car3"));
        }
        // Check if the collider tag is "car2"
        else if (other.gameObject.CompareTag("car2"))
        {
            // Find objects with tag "car1" and "car3" and apply scale if not already done
            ScaleAndAdd(GameObject.FindGameObjectWithTag("car1"));
            ScaleAndAdd(GameObject.FindGameObjectWithTag("car3"));
        }
        // Check if the collider tag is "car3"
        else if (other.gameObject.CompareTag("car3"))
        {
            // Find objects with tag "car1" and "car2" and apply scale if not already done
            ScaleAndAdd(GameObject.FindGameObjectWithTag("car1"));
            ScaleAndAdd(GameObject.FindGameObjectWithTag("car2"));
        }

        // Start the Coroutine to reset the scale only if there are scaled objects
        if (scaledObjects.Any())
        {
            StartCoroutine(ResetScaleCoroutine(scaledObjects));
        }
    }

    IEnumerator ResetScaleCoroutine(List<GameObject> objectsToScale)
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Loop through each object and multiply its scale by 2 to reset it
        foreach (var obj in objectsToScale)
        {
            if (obj != null)
                obj.transform.localScale *= 2f;  // Scale multiplied by 2 to reset
        }

        // Find the GameObject with the tag "thunder" and make it inactive
        if (thunderObject != null)
        {
            thunderObject.SetActive(false);
        }

        // Clear the list after resetting scale
        scaledObjects.Clear();
        isPowerUpActive = false;
    }




    void Rocket(Collider other)
    {

        if (other.gameObject.CompareTag("car1"))
        {

            isPowerUpActive = true;
            Transform rocketTransform = other.gameObject.transform.Find("rocket1");
            if (rocketTransform != null)
            {
                rocketTransform.gameObject.SetActive(true);
            }

            Waypoints waypointsScript = other.gameObject.GetComponent<Waypoints>();
            if (waypointsScript != null)
            {
                waypointsScript.maxSpeed = 0.1f;
                waypointsScript.acceleration = 0.05f;
            }

            StartCoroutine(WaitAndReverse(other));
        }

        if (other.gameObject.CompareTag("car2"))
        {

            isPowerUpActive = true;
            Transform rocketTransform = other.gameObject.transform.Find("rocket2");
            if (rocketTransform != null)
            {
                rocketTransform.gameObject.SetActive(true);
            }

            Waypoints waypointsScript = other.gameObject.GetComponent<Waypoints>();
            if (waypointsScript != null)
            {
                waypointsScript.maxSpeed = 0.1f;
                waypointsScript.acceleration = 0.05f;
            }

            StartCoroutine(WaitAndReverse(other));
        }

        if (other.gameObject.CompareTag("car3"))
        {

            isPowerUpActive = true;
            Transform rocketTransform = other.gameObject.transform.Find("rocket3");
            if (rocketTransform != null)
            {
                rocketTransform.gameObject.SetActive(true);
            }

            Waypoints waypointsScript = other.gameObject.GetComponent<Waypoints>();
            if (waypointsScript != null)
            {
                waypointsScript.maxSpeed = 0.1f;
                waypointsScript.acceleration = 0.05f;
            }

            StartCoroutine(WaitAndReverse(other));
        }
    }

    private IEnumerator WaitAndReverse(Collider other)
    {
        yield return new WaitForSeconds(6);
        if (other.gameObject.CompareTag("car1"))
        {
            
            Transform rocketTransform = other.gameObject.transform.Find("rocket1");
            if (rocketTransform != null)
            {
                rocketTransform.gameObject.SetActive(false);
            }

            Waypoints waypointsScript = other.gameObject.GetComponent<Waypoints>();
            if (waypointsScript != null)
            {
                waypointsScript.maxSpeed = 0.06f;
                waypointsScript.acceleration = 0.01f;
            }
        }

        if (other.gameObject.CompareTag("car2"))
        {

            Transform rocketTransform = other.gameObject.transform.Find("rocket2");
            if (rocketTransform != null)
            {
                rocketTransform.gameObject.SetActive(false);
            }

            Waypoints waypointsScript = other.gameObject.GetComponent<Waypoints>();
            if (waypointsScript != null)
            {
                waypointsScript.maxSpeed = 0.06f;
                waypointsScript.acceleration = 0.01f;
            }
        }

        if (other.gameObject.CompareTag("car3"))
        {

            Transform rocketTransform = other.gameObject.transform.Find("rocket3");
            if (rocketTransform != null)
            {
                rocketTransform.gameObject.SetActive(false);
            }

            Waypoints waypointsScript = other.gameObject.GetComponent<Waypoints>();
            if (waypointsScript != null)
            {
                waypointsScript.maxSpeed = 0.06f;
                waypointsScript.acceleration = 0.01f;
            }
        }
        isPowerUpActive = false;
    }
}
