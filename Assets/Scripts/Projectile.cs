using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float gravityScale = 0.4f; // Adjust this value to change the gravity effect on the projectile
    private Vector3 initialGravity; // Custom gravity force
    private Rigidbody rb; // Cache the Rigidbody component
    [SerializeField] PowerUpEffect nerfEffect;
    [SerializeField] PowerUpEffect nerfEffectRemoval;
    private bool bananaApplied = false;

    private void Start()
    {
        // Initialize the custom gravity force based on Unity's gravity and the gravity scale
        initialGravity = Physics.gravity * gravityScale;

        nerfEffect = Resources.Load<PowerUpEffect>("MinorSpeedNerf");
        nerfEffectRemoval = Resources.Load<PowerUpEffect>("MinorSpeedBuff");

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
            }
        }
        if (other.gameObject.CompareTag("car1") || other.gameObject.CompareTag("car2") || other.gameObject.CompareTag("car3"))
        {
            nerfEffect.Apply(other.gameObject);
            bananaApplied = true;
            transform.GetComponent<MeshRenderer>().enabled = false;
            NerfRemoval(other.gameObject);
        }
    }

    IEnumerator NerfRemoval(GameObject collidedObject)
    {
        Debug.Log(collidedObject.name);
        // Wait for 1 seconds before applying the effect
        float timer = 0;
        while (timer < 1f)
        {
            // Check if the collided object still exists
            if (collidedObject != null)
            {
                nerfEffectRemoval.Apply(collidedObject);
                gameObject.SetActive(false);
            }
            timer += Time.deltaTime;
            yield return null;
        }

        bananaApplied = false;

    }



}
