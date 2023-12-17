using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject circuit;
    [SerializeField] private GameObject car1;
    [SerializeField] private GameObject car2;
    [SerializeField] private GameObject car3;

    private Dictionary<GameObject, Vector3> originalScales;

    // Start is called before the first frame update
    void Start()
    {
        originalScales = new Dictionary<GameObject, Vector3>();
        StoreOriginalScales();
        scaleTransform();
        ScaleUp(car1);
        ScaleUp(car2);
        ScaleUp(car3);
        circuit.SetActive(false);
    }

    void StoreOriginalScales()
    {
        StoreScale(car1);
        StoreScale(car2);
        StoreScale(car3);
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
        //RestoreOriginalScale(GameObject.FindGameObjectWithTag("car1"));
        //RestoreOriginalScale(GameObject.FindGameObjectWithTag("car2"));
        //RestoreOriginalScale(GameObject.FindGameObjectWithTag("car3"));
        car1.transform.localScale *= 0.2f;
        car2.transform.localScale *= 0.2f;
        car3.transform.localScale *= 0.2f;
        GameObject.FindGameObjectWithTag("nextButton").SetActive(false);
        GameObject.FindGameObjectWithTag("previousButton").SetActive(false);
        transform.position = new Vector3(0, 0, 9);
        GameObject.FindGameObjectWithTag("startButton").SetActive(false);
    }
}
