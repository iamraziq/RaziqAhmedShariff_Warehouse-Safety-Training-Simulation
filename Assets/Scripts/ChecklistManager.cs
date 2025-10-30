using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChecklistManager : MonoBehaviour
{
    public static ChecklistManager Instance;
    public Transform contentParent; // where rows will be instantiated
    public GameObject checklistRowPrefab;
    public List<string> itemNames; // populate in inspector for the tray items

    Dictionary<string, Text> rowStatusMap = new Dictionary<string, Text>();

    void Awake() { Instance = this; }

    void Start()
    {
        foreach (var name in itemNames)
        {
            var rowGO = Instantiate(checklistRowPrefab, contentParent);
            var texts = rowGO.GetComponentsInChildren<Text>();
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
    }

    public bool AllCompleted()
    {
        foreach (var kv in rowStatusMap)
            if (kv.Value.text != "Inspection Completed") return false;
        return true;
    }
}
