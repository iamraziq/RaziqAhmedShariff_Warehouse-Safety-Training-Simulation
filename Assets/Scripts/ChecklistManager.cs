using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ChecklistManager : MonoBehaviour
{
    public static ChecklistManager Instance;
    public Transform contentParent; // where rows will be instantiated
    public GameObject checklistRowPrefab;
    public List<string> itemNames; // populate in inspector for the tray items
    public GameObject sceneFinalPanel;
    public ProgressBar progressBar; // assign in inspector


    Dictionary<string, TMP_Text> rowStatusMap = new Dictionary<string, TMP_Text>();

    void Awake() { Instance = this; }

    void Start()
    {
        foreach (var name in itemNames)
        {
            var rowGO = Instantiate(checklistRowPrefab, contentParent);
            var texts = rowGO.GetComponentsInChildren<TMP_Text>();
            // assume order [itemName, status]
            texts[0].text = name;
            texts[0].color = Color.black;
            texts[1].text = "Pending";
            texts[1].color = Color.red;
            rowStatusMap[name] = texts[1];
        }
    }

    public void MarkCompleted(string itemName)
    {
        if (rowStatusMap.ContainsKey(itemName))
        {
            rowStatusMap[itemName].text = "Inspection Completed";
            rowStatusMap[itemName].color = Color.white;
        }

        UpdateProgressBar();
        if (AllCompleted())
        {
            Debug.Log("All inspections completed!");
            // Trigger further actions here
            Invoke("FinalPanel", 2f);
        }
    }

    public void FinalPanel()
    {
        sceneFinalPanel.SetActive(true);
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.guide_FinishClassroom);
        }
    }
    void UpdateProgressBar()
    {
        if (progressBar == null) return;

        int completed = 0;
        foreach (var kv in rowStatusMap)
            if (kv.Value.text == "Inspection Completed") completed++;

        float progress = completed / (float)rowStatusMap.Count;
        progressBar.SetProgress(progress);
    }

    public bool AllCompleted()
    {
        foreach (var kv in rowStatusMap)
            if (kv.Value.text != "Inspection Completed") return false;
        return true;
    }

    public float GetProgressPercent()
    {
        int completed = 0;
        foreach (var kv in rowStatusMap)
            if (kv.Value.text == "Inspection Completed") completed++;

        return (float)completed / itemNames.Count;
    }

}
