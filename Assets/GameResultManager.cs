using UnityEngine;
using UnityEngine.UI;

public class GameResultManager : MonoBehaviour
{
    public GameObject victoryScreen;
    public GameObject gameOverScreen;
    public GameObject gameManager;

    void Start()
    {
        // Initially, hide both screens
        HideScreens();
    }

    public void Update()
    {
        if (gameManager.GetComponent<GameManager>().CurrentState == GameManager.GameState.Victory) HandleVictory();
        if (gameManager.GetComponent<GameManager>().CurrentState == GameManager.GameState.GameOver) HandleGameOver();
    }

    void HideScreens()
    {
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(false);
        }

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    void HandleVictory()
    {
        // Show victory screen
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
        }

        // Optionally, add logic for handling victory
    }

    void HandleGameOver()
    {
        // Show game over screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        // Optionally, add logic for handling game over
    }
}