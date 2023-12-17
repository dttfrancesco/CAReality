using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] PowerUpEffect powerUpEffect;
    [SerializeField] PowerUpEffect powerUpEffectRemoval;
    private GameObject collidedObject; // Reference to the object that interacted with the power-up

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("track") && other.GetComponent<Waypoints>().isUnderBananaDominance==false)
        {
            collidedObject = other.gameObject; // Store the object that triggered the power-up
            powerUpEffect.Apply(collidedObject);
            gameObject.SetActive(false);

            StartCoroutine(DelayedRemovalEffect());
        }
    }

    IEnumerator DelayedRemovalEffect()
    {
        yield return new WaitForSeconds(2.0f); // Wait for 2 seconds

        // Check if the object still exists before applying the removal effect
        if (collidedObject != null)
        {
            powerUpEffectRemoval.Apply(collidedObject);
        }
    }
}
