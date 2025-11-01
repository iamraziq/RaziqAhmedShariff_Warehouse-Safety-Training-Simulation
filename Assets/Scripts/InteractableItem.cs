using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractableItem : MonoBehaviour, IInteractable
{
    public string itemName;
    public AudioClip narrationClip;
    public GameObject glowObject; // child with glow material or outline
    AudioSource audioSource;
    bool isInspected = false;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = narrationClip;
        if (glowObject) glowObject.SetActive(false);
    }

    public void OnHoverEnter()
    {
        if (isInspected || InspectionManager.Instance.isBeingInspected) return;
        if (glowObject) glowObject.SetActive(true);
        // Optionally play a hover sound or begin narration
    }

    public void OnHoverExit()
    {
        if (isInspected) return;
        if (glowObject) glowObject.SetActive(false);
        // stop hover narration if playing
    }

    public void OnSelect()
    {
        if (isInspected) return;
        // Trigger inspection via InspectionManager
        if(InspectionManager.Instance != null)
            InspectionManager.Instance.StartInspection(this);
        if (SoundManager.Instance != null)
            SoundManager.Instance.StopSound();
        //audioSource.Play();  // if you want narration on select
    }

    public void MarkInspected()
    {
        isInspected = true;
        if (glowObject) glowObject.SetActive(false);
    }
}
