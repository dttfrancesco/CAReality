using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.up; // Set the axis of rotation (up, right, forward, etc.)
    public float rotationSpeed = 5f; // Rotation speed

    [Header("Action Probabilities")]
    [Range(0, 100)] public int action1Probability = 60; // Probability for action 1
    [Range(0, 100)] public int action2Probability = 25; // Probability for action 2
    // Probability for action 3 is implied (100 - action1Probability - action2Probability)

    void Update()
    {
        // Rotate the object around its axis
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Randomly decide which action to perform
        int randomValue = Random.Range(0, 100);
        if (randomValue < action1Probability)
        {
            PerformAction1();
        }
        else if (randomValue < action1Probability + action2Probability)
        {
            PerformAction2();
        }
        else
        {
            PerformAction3();
        }
    }

    void PerformAction1()
    {
        // Define what action 1 does
        Debug.Log("Action 1 executed.");
    }

    void PerformAction2()
    {
        // Define what action 2 does
        Debug.Log("Action 2 executed.");
    }

    void PerformAction3()
    {
        // Define what action 3 does
        Debug.Log("Action 3 executed.");
    }
}
