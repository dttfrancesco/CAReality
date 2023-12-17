using UnityEngine;
using System.Collections;

public class ObjectBounce : MonoBehaviour
{
    [SerializeField] float bounceHeight = 0.006f;
    [SerializeField] float bounceLenght = -0.006f;
    private bool isBouncing = false;
    private Vector3 originalPosition;
    [SerializeField] PowerUpEffect nerfEffect;
    [SerializeField] PowerUpEffect nerfEffectRemoval;

    void Start()
    {
        originalPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isBouncing && !other.gameObject.CompareTag("track"))
        {
            isBouncing = true;
            nerfEffect.Apply(other.gameObject);
            StartCoroutine(BounceObject(other.gameObject));
        }
    }

    IEnumerator BounceObject(GameObject collidedObject)
    {
        float timer = 0;
        while (timer < 1f)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition + new Vector3(0, bounceHeight, 0), timer);
            timer += Time.deltaTime;
            yield return null;
        }

        ApplyRemovalEffect(collidedObject); // Apply removal effect with the reference to the collided object

        timer = 0;
        while (timer < 1f)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition + new Vector3(bounceLenght, 0, bounceLenght), timer);
            timer += Time.deltaTime;
            yield return null;
        }

        isBouncing = false;
    }

    private void ApplyRemovalEffect(GameObject collidedObject)
    {
        // Check if the collided object still exists
        if (collidedObject != null)
        {
            nerfEffectRemoval.Apply(collidedObject);
        }
    }
}
