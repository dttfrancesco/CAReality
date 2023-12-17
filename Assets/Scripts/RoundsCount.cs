using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsCount : MonoBehaviour
{
    public int globalRoundCounter = 0;
    private int car1Counter = 0;
    private int car2Counter = 0;
    private int car3Counter = 0;
    private bool raceFinished = false;
    [SerializeField] GameObject gameManager;


    private void Update()
    {
        globalRoundCounter = GetHighestRound();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("car1"))
        {
            car1Counter++;
            CheckForWinner(car1Counter, other);
        }
        if (other.gameObject.CompareTag("car2"))
        {
            car2Counter++;
            CheckForWinner(car1Counter, other);
        }
        if (other.gameObject.CompareTag("car3"))
        {
            car3Counter++;
            CheckForWinner(car1Counter, other);
        }
    }

    private void CheckForWinner(int carCounter, Collider other)
    {
        if (carCounter >= 5 && !raceFinished && other.GetComponent<Waypoints>().isSelectedCharacter == true)
        {
            raceFinished = true;
            gameManager.GetComponent<GameManager>().Victory();

        }
        else if (carCounter >= 5 && !raceFinished && other.GetComponent<Waypoints>().isSelectedCharacter == false)
        {
            raceFinished = true;
            gameManager.GetComponent<GameManager>().GameOver();

        }
    }
    private int GetHighestRound()
    {
        return Mathf.Max(car1Counter, car2Counter, car3Counter);
    }

}

