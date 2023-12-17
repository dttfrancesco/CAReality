using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewTimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    private bool isTimerRunning;
    [SerializeField] private GameManager gameManager; // Reference to GameManager script

    void Start()
    {
        // Get a reference to the GameManager script
        gameManager = GameManager.Instance;

        // Initialize the timer state
        isTimerRunning = false;
        startTime = Time.time;
    }

    void Update()
    {
        // Check the game state from GameManager directly
        if (gameManager.CurrentState == GameManager.GameState.Running)
        {
            if (!isTimerRunning)
            {
                // Start the timer when the game state transitions to "Running"
                startTime = Time.time;
                isTimerRunning = true;
            }

            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            timerText.text = minutes + ":" + seconds;
        }
        else
        {
            // Reset the timer when the game is not running
            isTimerRunning = false;
        }
    }
}
