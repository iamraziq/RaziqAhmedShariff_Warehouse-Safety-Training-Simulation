//using System.Collections.Generic;
//using UnityEngine;

//public class GameSession : MonoBehaviour
//{
//    public static GameSession Instance { get; private set; }

//    // Items and their completed state
//    public List<string> itemNames = new List<string>();
//    public Dictionary<string, bool> inspected = new Dictionary<string, bool>();

//    public int totalItems => itemNames.Count;
//    public int completedCount
//    {
//        get
//        {
//            int c = 0;
//            foreach (var kv in inspected) if (kv.Value) c++;
//            return c;
//        }
//    }

//    void Awake()
//    {
//        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
//        Instance = this;
//        DontDestroyOnLoad(gameObject);
//    }

//    public void InitFromChecklist(List<string> items)
//    {
//        itemNames = new List<string>(items);
//        inspected.Clear();
//        foreach (var s in itemNames) inspected[s] = false;
//    }

//    public void MarkInspected(string name)
//    {
//        if (inspected.ContainsKey(name)) inspected[name] = true;
//    }

//    public float GetProgress01()
//    {
//        if (totalItems == 0) return 0f;
//        return completedCount / (float)totalItems;
//    }
//}
