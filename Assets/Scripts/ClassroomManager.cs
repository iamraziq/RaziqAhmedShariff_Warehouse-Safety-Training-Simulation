using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ClassroomManager : MonoBehaviour
{
    public GameObject videoPlayerObject;
    public GameObject[] enableObjs;
    public GameObject[] disableObjs;
    // Start is called before the first frame update
    void Start()
    {
        ToastNotification.Show("Welcome to Warehouse Safety Training", 5f,"avatar");
        //play audio of welcome message
        Invoke("PlayVideo", 5f);
    }

    public void PlayVideo()
    {
        ToastNotification.Hide();
        videoPlayerObject.SetActive(true);
    }

    public void OnVideoComplete()
    {
        videoPlayerObject.SetActive(false);

        foreach (var obj in enableObjs)
            obj.SetActive(true);

        foreach (var obj in disableObjs)
            obj.SetActive(false);

        ToastNotification.Show("Complete the safety inspection checklist. Hover over each item and click to inspect and learn about it.", 10f, "avatar");
    }

    public void OnProceedPressed()
    {
        if (ChecklistManager.Instance.AllCompleted())
        {
            // Load next scene or enable proceed UI
            GameSession.Instance.InitFromChecklist(ChecklistManager.Instance.itemNames);//Saving Game Session for next scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_Warehouse");
        }
        else
        {
            // show a toast: "Complete all inspections first"
        }
    }

}
