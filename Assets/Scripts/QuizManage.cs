using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManage : MonoBehaviour
{

    public GameObject cube;
    public Canvas canvas;

    	public void showCube()
	{
        if (cube != null)
        {
            cube.SetActive(true);
        }
	
	}

    public void hideCanvas()
	{
		canvas.enabled = false; 
	}
}
