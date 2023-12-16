using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float gravityScale = 0.4f; // Adjust this value to change the gravity effect on the projectile
    private Vector3 initialGravity; // Custom gravity force
    private Rigidbody rb; // Cache the Rigidbody component

    private void Start()
    {
        // Initialize the custom gravity force based on Unity's gravity and the gravity scale
        initialGravity = Physics.gravity * gravityScale;

        // Ensure the Rigidbody is set up correctly
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>(); // Add a Rigidbody if not present
        }
        rb.useGravity = false; // Disable the default gravity
        rb.isKinematic = false; // Ensure the object is not kinematic
    }

    private void FixedUpdate()
    {
        // Manually apply the custom gravity force
        if (rb != null)
        {
            rb.AddForce(initialGravity, ForceMode.Acceleration);
        }
        else
        {
            // For debugging
            Debug.LogError("Rigidbody component not found on the projectile");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Handle collision with objects
        if (other.gameObject.CompareTag("track"))
        {
            // Stop the object and make it a child of the "track" object
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
                transform.SetParent(other.transform);
                transform.rotation = Quaternion.Euler(-90, 0, 0);
                transform.localPosition = new Vector3(transform.localPosition.x, 0.7f, transform.localPosition.z);
                Debug.Log("Projectile collided with track and is now a child of it.");
            }
        }
    }
}
