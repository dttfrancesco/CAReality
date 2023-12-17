using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject canvas;
    public int selectedCharacter = 0;
    [SerializeField] GameObject gameManager;

    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }

    public void StartGame()
    {
        gameManager.GetComponent<GameManager>().GoRunning();
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        canvas.SetActive(false); // Hide the canvas
        characters[selectedCharacter].GetComponent<Waypoints>().makeChacraterSelected();
        foreach (var character in characters)
        {
            character.SetActive(true);
        }

    }
}