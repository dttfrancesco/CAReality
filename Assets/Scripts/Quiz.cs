using System.Collections.Generic;
using System.Collections;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{

    public Text questionText;
    public Button AnswerA;
    public Button AnswerB;

     private List<string> questions = new List<string>
    {
        "When was Formula 1 founded?",
        "Which car currently holds the title for the fastest top speed in the world?",
        "How many Formula 1 World Championships has Michael Schumacher won?",
        "How many brands does the Volkswagen Group own?"
       
    };
   
    void Start()
    {
        showRandomQuestion();
        
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {

        Debug.Log("in event");
        // Handle air-tap or click events here
        if (eventData.MixedRealityInputAction.Description == "Select")
        {
            // Check which button was clicked
            if (eventData.selectedObject == AnswerA.gameObject)
            {
                Debug.Log("Option A selected!");
            }
            else if (eventData.selectedObject == AnswerB.gameObject)
            {
                Debug.Log("Option B selected!");
            }

            // Hide the dialog window
            HideDialog();
        }
    }

    private void HideDialog()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void showRandomQuestion(){
        string randomQuestion = questions[Random.Range(0, questions.Count)];
        questionText.text = randomQuestion;

        //set answers
        setAnswersForQuestion(randomQuestion);
    }

    private void setAnswersForQuestion(string question){
    switch (question){
        case "When was Formula 1 founded?":
            AnswerA.GetComponentInChildren<Text>().text = "1950"; //correct
            AnswerB.GetComponentInChildren<Text>().text = "1947";
            break;
        case "Which car currently holds the title for the fastest top speed in the world?":
            AnswerA.GetComponentInChildren<Text>().text = "Bugatti";
            AnswerB.GetComponentInChildren<Text>().text = "Jesko"; //correct
            break;
        case "How many Formula 1 World Championships has Michael Schumacher won?":
            AnswerA.GetComponentInChildren<Text>().text = "8";
            AnswerB.GetComponentInChildren<Text>().text = "7"; //correct
            break;
        case "How many brands does the Volkswagen Group own?":
            AnswerA.GetComponentInChildren<Text>().text = "10"; //correct
            AnswerB.GetComponentInChildren<Text>().text = "12";
            break;
    }
    }
}
