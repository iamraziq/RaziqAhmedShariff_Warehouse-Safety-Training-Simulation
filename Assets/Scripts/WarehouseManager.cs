using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;


public class WarehouseManager : MonoBehaviour
{
    public static WarehouseManager Instance;

    public TMP_Text timerText; // assign
   // public Slider progressBar; // 0..1
    public float totalTime = 300f; // 5 minutes
    float timeLeft;
    public ProgressBar progressBarScript;

    int totalRequired = 4;
    HashSet<string> picked = new HashSet<string>();
    HashSet<string> placed = new HashSet<string>();


    public GameObject quizPanel;
    public GameObject failPanel;
    public GameObject[] objsToEnable;

    bool taskActive = false;


    void Awake()
    {
        if (Instance == null) Instance = this; else Destroy(gameObject);
    }


    void Start()
    {
        timeLeft = totalTime;
        //if(timerText)
        //    timerText.gameObject.SetActive(false);
        //if (objsToEnable != null)
        //{
        //    foreach (GameObject go in objsToEnable)
        //    {
        //        go.SetActive(false);
        //    }
        //}
        UpdateProgressUI();
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySound(SoundManager.Instance.guide_WelcomeWarehouse);
    }
    public void OnClickNext_Wel()
    {
        taskActive = true;
        //if (timerText)
        //    timerText.gameObject.SetActive(true);
        //if(objsToEnable != null)
        //{
        //    foreach (GameObject go in objsToEnable)
        //    {
        //        go.SetActive(true);
        //    }
        //}
        ToastNotification.Show("Collect and place all warehouse items before time expires!");
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySound(SoundManager.Instance.guide_Collect);
    }

    void Update()
    {
        if (!taskActive) return;
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0) timeLeft = 0;
        UpdateTimerUI();

        if (timeLeft <= 0) OnTimeUp();
    }


    void UpdateTimerUI()
    {
        if (timerText) timerText.text = FormatTime(timeLeft);
    }


    string FormatTime(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60);
        int seconds = Mathf.FloorToInt(t % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void NotifyItemPicked(string id, PickupItem item = null)
    {
        picked.Add(id);
        UpdateProgressUI();
    }


    public void NotifyItemPlaced(string id)
    {
        placed.Add(id);
        UpdateProgressUI();
        CheckCompletion();
    }


    void UpdateProgressUI()
    {
        float p = (float)placed.Count / (float)totalRequired;
        //if (progressBar) progressBar.value = p;
        if(progressBarScript) progressBarScript.SetProgress(p);
    }


    void CheckCompletion()
    {
        if (placed.Count >= totalRequired)
        {
            taskActive = false;
            ToastNotification.Hide();
            // show quiz after a short delay
            Invoke("ShowQuiz", 3f);
        }
    }
    void OnTimeUp()
    {
        taskActive = false;
        // handle fail (offer retry)
        // show retry modal or restart scene
        Debug.Log("Time is up - show retry prompt");
        ShowFailPanel();
    }

    public void ShowFailPanel()
    {
        if (failPanel) failPanel.SetActive(true);
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySound(SoundManager.Instance.guide_Failed);
    }
    void ShowQuiz()
    {
        if (quizPanel) quizPanel.SetActive(true);
        ToastNotification.Show("Select the correct answer from the options above.");
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySound(SoundManager.Instance.guide_Quiz);
    }

    public void OnLevelFail()
    {
        SceneManager.LoadScene("Scene_Warehouse");
    }

    public void OnLevelPass()
    {
        SceneManager.LoadScene("Scene_Classroom");
    }
}