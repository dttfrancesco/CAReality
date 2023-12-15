using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 4f; // Speed of the projectile
    [SerializeField] private float maxDistance = 1f; // Maximum travel distance of the projectile
    private Vector3 shootingDirection; // Direction in which the projectile will be shot
    private Vector3 startPosition; // Starting position of the projectile

    private Vector3 gravity = new Vector3(0, -9.81f, 0); // Gravity effect
    private float timeSinceShot = 0f; // Time since the projectile was shot

    private void Start()
    {
        startPosition = transform.position; // Save the starting position
        GameObject gameManager = GameObject.Find("GameManager");

        // Find and set the shooting direction based on the index finger position
        if (gameManager != null)
        {
            HandTracking handTrackingScript = gameManager.GetComponent<HandTracking>();
            if (handTrackingScript != null)
            {
                GameObject indexTipObject = handTrackingScript.indexObject;
                GameObject indexBaseObject = handTrackingScript.indexBaseObject;

                if (indexTipObject != null && indexBaseObject != null)
                {
                    shootingDirection = (indexTipObject.transform.position - indexBaseObject.transform.position).normalized;
                }
                else
                {
                    Debug.LogError("Index tip or base object not found.");
                }
            }
            else
            {
                Debug.LogError("HandTracking script not found on GameManager.");
            }
        }
        else
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    private void Update()
    {
        timeSinceShot += Time.deltaTime;
        Vector3 gravityEffect = gravity * timeSinceShot * timeSinceShot / 2; // Calculate the effect of gravity

        Vector3 currentDirection = shootingDirection * speed * timeSinceShot + gravityEffect;
        transform.position = startPosition + currentDirection; // Update position

        // Check if the projectile has traveled beyond the maximum distance
        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
        {
            gameObject.SetActive(false); // Destroy the object if it exceeds the maximum distance
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle collision with objects
        if (collision.gameObject.CompareTag("track"))
        {
            // Stop the object
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
            }
        }
    }
}
