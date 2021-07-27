using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

public class ScreenManager : MonoBehaviour
{
    public QuizController quizControl;
    public GameObject levelManager, answerManager;
    private QuizQuestion[] allQuestions;
    public GameObject[] options;

    int counter = -1;

    public void loadLevel() {
        levelManager.SetActive(false);
        answerManager.SetActive(true);
        counter++;
        LoadAllQuestions();
        quizControl.PresentQuestion();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }

    private void LoadAllQuestions()
    {
        if (!File.Exists("Questions.xml"))
        {
            WriteSampleQuestionsToXml();
        }

        XmlSerializer serializer = new XmlSerializer(typeof(QuizQuestion[]));
        using (StreamReader streamReader = new StreamReader("Questions.xml"))
        {
            allQuestions = (QuizQuestion[])serializer.Deserialize(streamReader);
        }
        
    }

    public QuizQuestion GetUnaskedQuestion()
    {
        ResetQuestionsIfAllHaveBeenAsked();
        Debug.Log("Counter is :" + counter);
        var question = allQuestions
            .Where(t => t.Asked == false)
            .ElementAt(counter);
        
        question.Asked = true;
        return question;
    }

    private void ResetQuestionsIfAllHaveBeenAsked()
    {
        if (allQuestions.Any(t => t.Asked == false) == false)
        {
            ResetQuestions();
        }
    }

    private void ResetQuestions()
    {
        foreach (var question in allQuestions)
            question.Asked = false;
    }

    /// <summary>
    /// This method is used to generate a starting sample xml file if none exists
    /// </summary>
    private void WriteSampleQuestionsToXml()
    {
        allQuestions = new QuizQuestion[] {
            new QuizQuestion { Question = "What is the capital of India",
                Answers = new string[] { "Kolkata", "Gurgaon", "Delhi", "Mumbai" }, CorrectAnswer = 2 },
            new QuizQuestion { Question = "What are the natural numbers?",
                Answers = new string[] { "All Numbers", "0-10", "0-100", "All Positive Numbers" }, CorrectAnswer = 3 },
            new QuizQuestion { Question = "What is the color of Sky?",
                Answers = new string[] { "Red", "Green", "Blue", "White" }, CorrectAnswer = 2 },
            new QuizQuestion { Question = "Who is India's Prime minister?",
                Answers = new string[] { "Naredra Modi", "Manmohan Singh", "Digvijay Singh", "Amit Shah" }, CorrectAnswer = 0 },
            new QuizQuestion { Question = "Who is the lead actor in John wick?",
                Answers = new string[] { "Tom holland", "Robert Downey", "Kneau Revees", "Chris Evans" }, CorrectAnswer = 2 },
             new QuizQuestion { Question = "Who is the lead actor in John wick?",
                Answers = new string[] { "Tom holland", "Robert Downey", "Kneau Revees", "Chris Evans" }, CorrectAnswer = 2 },
             new QuizQuestion { Question = "Who is the lead actor in John wick?",
                Answers = new string[] { "Tom holland", "Robert Downey", "Kneau Revees", "Chris Evans" }, CorrectAnswer = 2 }
        };

        XmlSerializer serializer = new XmlSerializer(typeof(QuizQuestion[]));
        using (StreamWriter streamWriter = new StreamWriter("Questions.xml"))
        {
            serializer.Serialize(streamWriter, allQuestions);
        }
    }
}

public class QuizQuestion
{
    public string Question { get; set; }
    public string[] Answers { get; set; }
    public int CorrectAnswer { get; set; }

    public bool Asked { get; set; }
}
