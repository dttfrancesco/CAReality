using System.Collections;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.up; // Set the axis of rotation (up, right, forward, etc.)
    public float rotationSpeed = 5f; // Rotation speed

    [Header("Action Probabilities")]
    [Range(0, 100)] public int action1Probability = 50; // Probability for action 1
    [Range(0, 100)] public int rocketProbability = 30; // Probability for action 2
    // Probability for action 3 is implied (100 - action1Probability - rocketProbability)

    void Update()
    {
        // Rotate the object around its axis
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Randomly decide which action to perform
        int randomValue = Random.Range(0, 100);
        if (other.gameObject.CompareTag("car1") || other.gameObject.CompareTag("car2") || other.gameObject.CompareTag("car3"))
        {
            if (randomValue < action1Probability)
            {
                PerformAction1();
            }
            else if (randomValue < action1Probability + rocketProbability)
            {
                Rocket(other);
            }
            else
            {
                PerformAction3();
            }
        }
    }

    void PerformAction1()
    {
        // Define what action 1 does
        Debug.Log("Action 1 executed.");
    }

    void Rocket(Collider other)
    {

        if (other.gameObject.CompareTag("car1"))
        {
            

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
    }


    void PerformAction3()
    {
        // Define what action 3 does
        Debug.Log("Action 3 executed.");
    }
}
