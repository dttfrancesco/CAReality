using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    private Vector3 shootingDirection;

    private void Start()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            HandTracking handTrackingScript = gameManager.GetComponent<HandTracking>();
            if (handTrackingScript != null)
            {
                GameObject indexTipObject = handTrackingScript.indexObject;
                GameObject indexBaseObject = handTrackingScript.indexBaseObject;

                if (indexTipObject != null && indexBaseObject != null)
                {
                    // Calcola la direzione usando la posizione della punta e della base dell'indice
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
        transform.Translate(shootingDirection * speed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
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
