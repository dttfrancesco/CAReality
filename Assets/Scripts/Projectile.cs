using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    private Vector3 shootingDirection;
    private GameObject indexTipObject;

    private void Start()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            HandTracking handTrackingScript = gameManager.GetComponent<HandTracking>();
            if (handTrackingScript != null)
            {
                indexTipObject = handTrackingScript.indexObject;
            }
        }

        if (indexTipObject != null)
        {
            // Calcola la direzione usando la posizione della punta dell'indice
            shootingDirection = (indexTipObject.transform.position - transform.position).normalized;
        }
        else
        {
            Debug.LogError("Index tip object non trovato.");
        }
    }

    private void Update()
    {
        if (indexTipObject != null)
        {
            transform.Translate(shootingDirection * speed * Time.deltaTime, Space.World);
        }
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
