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
    public Button finalizeButton;

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
            texts[1].text = "Pending";
            rowStatusMap[name] = texts[1];
        }
    }

    public void MarkCompleted(string itemName)
    {
        if (rowStatusMap.ContainsKey(itemName))
        {
            rowStatusMap[itemName].text = "Inspection Completed";
        }
        if(AllCompleted())
        {
            Debug.Log("All inspections completed!");
            // Optionally trigger further actions here
            finalizeButton.gameObject.SetActive(true);
        }
    }

    public bool AllCompleted()
    {
        foreach (var kv in rowStatusMap)
            if (kv.Value.text != "Inspection Completed") return false;
        return true;
    }
}
