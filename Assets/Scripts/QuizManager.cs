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
            ToastNotification.Show("Click Submit to view your results.");
            if(SoundManager.Instance != null)
                SoundManager.Instance.PlaySound(SoundManager.Instance.guide_Results);
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
            WarehouseManager.Instance.ShowFailPanel();
            resultText.text = "Failed — please retry.";        
            // optionally show retry button
        }
        // reset for next attempt
        correctAnswers = 0;
        ToastNotification.Hide();
    }
}