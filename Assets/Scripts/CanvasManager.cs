using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    public Canvas[] quizCanvases;  

    void Start()
    {
        DeactivateAllCanvases();

        int randomCanvasIndex = Random.Range(0, quizCanvases.Length);
        ActivateCanvas(randomCanvasIndex);
    }

    void ActivateCanvas(int canvasIndex)
    {
        if (canvasIndex >= 0 && canvasIndex < quizCanvases.Length)
        {
            quizCanvases[canvasIndex].gameObject.SetActive(true);
        }
    }

    void DeactivateAllCanvases()
    {
        foreach (Canvas canvas in quizCanvases)
        {
            canvas.gameObject.SetActive(false);
        }
    }

}
