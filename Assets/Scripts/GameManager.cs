using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance of the GameManager

    // Enum to represent different game states
    public enum GameState
    {
        Running,
        Paused,
        GameOver,
        Victory,
    }

    public GameState CurrentState { get; private set; } // Current state of the game

    void Awake()
    {
        // Ensure there's only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    void Start()
    {
        // Set the initial game state
        CurrentState = GameState.Paused;
    }

    void Update()
    {
        // Handle input to toggle the game pause state
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        // Switch between Running and Paused states
        if (CurrentState == GameState.Running)
        {
            CurrentState = GameState.Paused;
            // Add additional logic for when the game is paused
        }
        else if (CurrentState == GameState.Paused)
        {
            CurrentState = GameState.Running;
            // Add additional logic for when the game resumes
        }
    }

    public void GoRunning()
    {
        CurrentState = GameState.Running;
    }

    public void GameOver()
    {
        // Set the game state to GameOver
        CurrentState = GameState.GameOver;
        // Add logic for when the game ends
    }

    public void Victory()
    {
        CurrentState = GameState.Victory;
    }

    public void ApplyShield()
    {
        if (GameObject.FindGameObjectWithTag("car1").GetComponent<Waypoints>().isSelectedCharacter) GameObject.FindGameObjectWithTag("car1").GetComponent<Waypoints>().makeUnderShieldProtection();
        if (GameObject.FindGameObjectWithTag("car2").GetComponent<Waypoints>().isSelectedCharacter) GameObject.FindGameObjectWithTag("car2").GetComponent<Waypoints>().makeUnderShieldProtection();
        if (GameObject.FindGameObjectWithTag("car3").GetComponent<Waypoints>().isSelectedCharacter) GameObject.FindGameObjectWithTag("car3").GetComponent<Waypoints>().makeUnderShieldProtection();
    }

    public void ApplyPowerUpRemoval()
    {
        if (GameObject.FindGameObjectWithTag("car1").GetComponent<Waypoints>().isSelectedCharacter)
        {
            GameObject.FindGameObjectWithTag("car2").GetComponent<Waypoints>().skipOnePowerUp();
            GameObject.FindGameObjectWithTag("car3").GetComponent<Waypoints>().skipOnePowerUp();
        }
        if (GameObject.FindGameObjectWithTag("car2").GetComponent<Waypoints>().isSelectedCharacter)
        {
            GameObject.FindGameObjectWithTag("car1").GetComponent<Waypoints>().skipOnePowerUp();
            GameObject.FindGameObjectWithTag("car3").GetComponent<Waypoints>().skipOnePowerUp();
        }
        if (GameObject.FindGameObjectWithTag("car3").GetComponent<Waypoints>().isSelectedCharacter)
        {
            GameObject.FindGameObjectWithTag("car1").GetComponent<Waypoints>().skipOnePowerUp();
            GameObject.FindGameObjectWithTag("car2").GetComponent<Waypoints>().skipOnePowerUp();
        }
    }
}
