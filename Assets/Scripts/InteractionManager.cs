using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InspectionManager : MonoBehaviour
{
    public static InspectionManager Instance;
    public Transform inspectionRoot; // create an empty in front of camera
    public float moveTime = 0.25f;
    public Vector3 inspectLocalPos = Vector3.zero;
    public Vector3 inspectLocalRot = Vector3.zero;
    public bool isBeingInspected = false;
    public Button closeButton; // assign in inspector

    InteractableItem current;
    Vector3 originalPos;
    Quaternion originalRot;
    Transform originalParent;
    Rigidbody rb;
    Collider col;
    AudioSource audioSource;


    void Awake() { Instance = this; }

    private void Start()
    {
        if (closeButton)
        {
            closeButton.onClick.AddListener(() =>
            {
                EndInspection();
            });
        }
    }
    public void StartInspection(InteractableItem item)
    {
        if (current != null) return; // one at a time
        
        isBeingInspected = true;
        current = item;
        originalParent = item.transform.parent;
        originalPos = item.transform.position;
        originalRot = item.transform.rotation;
        // disable physics
        rb = item.GetComponent<Rigidbody>();
        if (rb) { rb.isKinematic = true; rb.velocity = Vector3.zero; }
        col = item.GetComponent<Collider>();
        if (col) col.enabled = false;

        // parent to inspection root
        item.transform.SetParent(inspectionRoot, true);
        StartCoroutine(MoveToInspect(item.transform));
        // optionally play narration
        if (item.narrationClip)
        {
            audioSource = item.GetComponent<AudioSource>();
            audioSource?.Stop(); // stop if already playing
            audioSource?.PlayOneShot(item.narrationClip);
        }
        if (closeButton)
            closeButton.gameObject.SetActive(true);
        //Shwo Toast message about the item
        ToastNotification.Hide();
        if (!string.IsNullOrEmpty(item.itemName))
        {
            ToastNotification.Show("Inspecting: " + item.itemName + "\nRotate the item with left mouse drag.", 10f, "avatar");
        }
    }

    IEnumerator MoveToInspect(Transform t)
    {
        Vector3 startPos = t.localPosition;
        Quaternion startRot = t.localRotation;
        Quaternion targetRot = Quaternion.Euler(inspectLocalRot);
        float elapsed = 0f;
        while (elapsed < moveTime)
        {
            float p = elapsed / moveTime;
            t.localPosition = Vector3.Lerp(startPos, inspectLocalPos, p);
            t.localRotation = Quaternion.Slerp(startRot, targetRot, p);
            elapsed += Time.deltaTime;
            yield return null;
        }
        t.localPosition = inspectLocalPos;
        t.localRotation = targetRot;
        // enable rotate via ItemInspector component
        var rotComp = t.gameObject.GetComponent<ItemInspector>();
        if (!rotComp) rotComp = t.gameObject.AddComponent<ItemInspector>();
        rotComp.BeginInspect(() => EndInspection());
    }

    public void EndInspection()
    {
        if (current == null) return;
        // mark inspected (updates checklist)
        isBeingInspected = false;
        current.MarkInspected();
        ChecklistManager.Instance.MarkCompleted(current.itemName);

        // return to original position
        current.transform.SetParent(originalParent, true);
        current.transform.position = originalPos;
        current.transform.rotation = originalRot;

        if (rb) rb.isKinematic = false;
        if (col) col.enabled = true;
        if (audioSource) audioSource.Stop();
        // cleanup
        var rotComp = current.GetComponent<ItemInspector>();
        if (rotComp) Destroy(rotComp);

        current = null;
        ToastNotification.Hide();
        if (closeButton)
            closeButton.gameObject.SetActive(false);
    }
}
