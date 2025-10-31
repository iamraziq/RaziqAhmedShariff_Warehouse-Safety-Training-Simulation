using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuizManager : MonoBehaviour
{
    public GameObject quizPanel;
    public TMP_Text resultText;
    public GameObject submitButton;

    int correctAnswers = 0;
    int buttonsPressed = 0;


    // Hook each answer button to call this with true/false
    public void Answer(bool isCorrect)
    {
        if (isCorrect) correctAnswers++;

        buttonsPressed++;
        if(buttonsPressed >= 3)
        {
            // all questions answered, enable submit
            // assuming there's a submit button to enable
            submitButton.SetActive(true);
        }        
    }


    public void Submit()
    {
        if (correctAnswers >= 2)
        {
            // pass
            resultText.text = "Passed!";
            // trigger certificate
            CertificateManager.Instance.ShowCertificate();
            quizPanel.SetActive(false);
        }
        else
        {
            resultText.text = "Failed — please retry.";
            // optionally show retry button
        }
        // reset for next attempt
        correctAnswers = 0;
    }
}