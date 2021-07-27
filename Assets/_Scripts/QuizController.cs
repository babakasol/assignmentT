using System.Collections;
using UnityEngine;

public class QuizController : MonoBehaviour
{
    private ScreenManager questionCollection;
    private QuizQuestion currentQuestion;
    private UIController uiController;
    public GameObject[] allOptions;

    [SerializeField]
    private float delayBetweenQuestions = 3f;

    private void Awake()
    {
        questionCollection = FindObjectOfType<ScreenManager>();
        uiController = FindObjectOfType<UIController>();
    }

    private void Start()
    {
        //PresentQuestion();
    }

    public void PresentQuestion()
    {
        currentQuestion = questionCollection.GetUnaskedQuestion();
        uiController.SetupUIForQuestion(currentQuestion);
       
    }

    

    
    public void SubmitAnswer(int answerNumber)
    {
        
            bool isCorrect = answerNumber == currentQuestion.CorrectAnswer;
            uiController.HandleSubmittedAnswer(isCorrect);
            UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject
                .GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            StartCoroutine(ShowNextQuestionAfterDelay());
        

    }




    private IEnumerator ShowNextQuestionAfterDelay()
    {
        yield return new WaitForSeconds(3);
        PresentQuestion();
        
        
    }
}