using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public bool activateCanva = false;
    public Canvas[] quizCanvases;  

    void Start()
    {
        DeactivateAllCanvases();

       

    }

    void Update()
    {
        if(activateCanva){
            int randomCanvasIndex = Random.Range(0, quizCanvases.Length);
            ActivateCanvas(1);
        }
    }

    void ActivateCanvas(int canvasIndex)
    {
        if (canvasIndex >= 0 && canvasIndex < quizCanvases.Length)
        {
            quizCanvases[canvasIndex].gameObject.SetActive(true);
            activateCanva = false;
        }
    }

    public void canvaSetter()
    {
        activateCanva = true;
    }

    void DeactivateAllCanvases()
    {
        foreach (Canvas canvas in quizCanvases)
        {
            canvas.gameObject.SetActive(false);
        }
    }

}
