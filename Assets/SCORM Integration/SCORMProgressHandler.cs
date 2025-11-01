using System.Collections;
using UnityEngine;

public class SCORMProgressHandler : MonoBehaviour
{
    public int currentLevel = 1;

    private void OnEnable()
    {
        if (SCORMManager.Instance != null)
        {
            SCORMManager.Instance.ReportLevelProgress(currentLevel);
            Debug.Log($"Reported progress to SCORM.");
        }
        else
        {
            Debug.LogWarning("SCORMManager instance not found on OnEnable!");
        }       
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.N))
    //    {
    //        currentLevel++;
    //        if (currentLevel > 15) currentLevel = 15;
    //        if (SCORMManager.Instance != null)
    //            SCORMManager.Instance.ReportLevelProgress(currentLevel);
    //        Debug.Log($"Advanced to Level {currentLevel} and reported progress to SCORM.");
    //    }
    //}

    //public void OnProgressSCORM()
    //{
    //    if (SCORMManager.Instance != null)
    //        SCORMManager.Instance.ReportLevelProgress(currentLevel);
    //    Debug.Log($"OnProgressSCORM executed and Reported progress to SCORM.");
    //}
    //public void OnProgressSCORM()
    //{
    //    StartCoroutine(DelayedSCORMUpdate());
    //}

    //private IEnumerator DelayedSCORMUpdate()
    //{
    //    yield return null; // wait 1 frame to ensure we're on the main thread
    //    if (SCORMManager.Instance != null)
    //        SCORMManager.Instance.ReportLevelProgress(currentLevel);
    //    Debug.Log($"OnProgressSCORM executed and Reported progress to SCORM (main thread).");
    //}

}

//using UnityEngine;

//public class SCORMProgressHandler : MonoBehaviour
//{
//    public int currentLevel = 1;

//    // ?? Flag to trigger SCORM reporting in Update
//    private bool reportProgressFlag = false;

//    private void Update()
//    {
//        // Example: automatically trigger level advancement with N
//        if (Input.GetKeyDown(KeyCode.N))
//        {
//            currentLevel++;
//            if (currentLevel > 15) currentLevel = 15;
//            reportProgressFlag = true; // Set flag instead of calling directly
//        }

//        // ? Main thread call to SCORM
//        if (reportProgressFlag)
//        {
//            if (SCORMManager.Instance != null)
//                SCORMManager.Instance.ReportLevelProgress(currentLevel);

//            Debug.Log($"Reported progress to SCORM for Level {currentLevel}.");
//            reportProgressFlag = false; // Reset flag
//        }
//    }

//    // Call this method from anywhere; it just sets the flag
//    public void OnProgressSCORM()
//    {
//        if (SCORMManager.Instance != null)
//        {
//            reportProgressFlag = true; // flag to report in Update
//        }
//        else
//        {
//            Debug.LogWarning("SCORMManager instance not found!");
//        }
//    }

//}

