using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject circuit;

    private Dictionary<GameObject, Vector3> originalScales;

    // Start is called before the first frame update
    void Start()
    {
        originalScales = new Dictionary<GameObject, Vector3>();
        StoreOriginalScales();
        scaleTransform();
        ScaleUp(GameObject.FindGameObjectWithTag("car1"));
        ScaleUp(GameObject.FindGameObjectWithTag("car2"));
        ScaleUp(GameObject.FindGameObjectWithTag("car3"));
        circuit.SetActive(false);
    }

    void StoreOriginalScales()
    {
        StoreScale(GameObject.FindGameObjectWithTag("car1"));
        StoreScale(GameObject.FindGameObjectWithTag("car2"));
        StoreScale(GameObject.FindGameObjectWithTag("car3"));
    }

    void StoreScale(GameObject car)
    {
        if (car != null && !originalScales.ContainsKey(car))
        {
            originalScales.Add(car, car.transform.localScale);
        }
    }

    void ScaleUp(GameObject car)
    {
        if (car != null)
        {
            car.transform.localScale *= 5f;  // Scale multiplied by 5
        }
    }

    void RestoreOriginalScale(GameObject car)
    {
        if (car != null && originalScales.ContainsKey(car))
        {
            car.transform.localScale = originalScales[car];
        }
    }

    void scaleTransform()
    {
        transform.position = new Vector3(0, 1.2f, 40f);
        transform.localScale *= 5f;
    }

    public void StartGame()
    {
        transform.localScale *= 0.2f;
        circuit.SetActive(true);
        RestoreOriginalScale(GameObject.FindGameObjectWithTag("car1"));
        RestoreOriginalScale(GameObject.FindGameObjectWithTag("car2"));
        RestoreOriginalScale(GameObject.FindGameObjectWithTag("car3"));
        GameObject.Find("Circuit").SetActive(true);
        GameObject.FindGameObjectWithTag("nextButton").SetActive(false);
        GameObject.FindGameObjectWithTag("previousButton").SetActive(false);
        transform.position = new Vector3(0, 0, 9);
        GameObject.FindGameObjectWithTag("startButton").SetActive(false);
    }
}
