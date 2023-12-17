using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewTimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    private bool startTimeInitialized = false;
    public GameObject gameManager;

    void Update()
    {
        GameManager.GameState currentState = gameManager.GetComponent<GameManager>().CurrentState;

        if (currentState == GameManager.GameState.Running)
        {
            // Initialize startTime once when the game state changes to Running
            if (!startTimeInitialized)
            {
                startTime = Time.time;
                startTimeInitialized = true;
            }

            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            timerText.text = minutes + ":" + seconds;
        }
        else
        {
            startTimeInitialized = false; // Reset for the next time the game enters Running state
        }
    }
}
