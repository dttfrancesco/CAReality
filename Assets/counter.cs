using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro for UI text handling



public class counter : MonoBehaviour
{
    public int globalRoundCounter = 0;
    [SerializeField] TextMeshProUGUI roundText;


    private void Update() {
        UpdateRoundText();

    }
        
    // Update the UI text with the current round count
     private void UpdateRoundText()
    {
        if (roundText != null)
        {
            roundText.text = "Current Round: " + globalRoundCounter.ToString();
        }
    }
}
