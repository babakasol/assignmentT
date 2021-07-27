using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text questionText;
    [SerializeField]
    private Toggle[] answerButtons;

    [SerializeField]
    private GameObject correctAnswerPopup;
    [SerializeField]
    private GameObject wrongAnswerPopup;

    public ScreenManager screen;

    public void SetupUIForQuestion(QuizQuestion question)
    {
        correctAnswerPopup.SetActive(false);
        wrongAnswerPopup.SetActive(false);

        questionText.text = question.Question;

        for (int i = 0; i < question.Answers.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = question.Answers[i];
            answerButtons[i].gameObject.SetActive(true);
        }

        for (int i = question.Answers.Length; i < answerButtons.Length; i++)
        {
            answerButtons[i].gameObject.SetActive(false);
            
        }
    }

    public bool run = false;

    public void HandleSubmittedAnswer(bool isCorrect)
    {
        //ToggleAnswerButtons(false);
        if (isCorrect)
        {
            StartCoroutine(ShowCorrectAnswerPopup());

        }
        else
        {
            StartCoroutine(ShowWrongAnswerPopup());
        }
        run = false;
        
    }

    private void ToggleAnswerButtons(bool value)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].gameObject.SetActive(value);
        }
    }

    IEnumerator ShowCorrectAnswerPopup()
    {
        yield return new WaitForSeconds(0f);
        correctAnswerPopup.SetActive(true);
        StartCoroutine(ClosePopUp());
    }

    IEnumerator ShowWrongAnswerPopup()
    {
        yield return new WaitForSeconds(0f);
        wrongAnswerPopup.SetActive(true);
        StartCoroutine(ClosePopUp());
    }

    IEnumerator ClosePopUp()
    {
        yield return new WaitForSeconds(3f);
        correctAnswerPopup.SetActive(false);
        wrongAnswerPopup.SetActive(false);
        
        screen.answerManager.SetActive(false);
        screen.levelManager.SetActive(true);
        
        ToggleAnswerButtons(false);
    }


}