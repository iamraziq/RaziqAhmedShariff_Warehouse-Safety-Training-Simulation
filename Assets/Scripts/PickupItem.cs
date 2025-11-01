using UnityEngine;


public class PickupItem : MonoBehaviour, IInteractable
{
    public string itemID;
    bool isCollected = false;
    public GameObject glowObject;

    void Awake()
    {
        if (string.IsNullOrEmpty(itemID)) itemID = gameObject.name;
    }
    public void OnHoverEnter()
    {
        if (isCollected) return;
        if (glowObject) glowObject.SetActive(true);
    }

    public void OnHoverExit()
    {
        if (isCollected) return;
        if (glowObject) glowObject.SetActive(false);
    }

    public void OnSelect()
    {

    }

    public void OnPickedUp(Transform parent)
    {
        if (isCollected) return;
        isCollected = true;
        if (glowObject) glowObject.SetActive(false);
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero; 
        var rb = GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;

        // notify manager
        WarehouseManager.Instance.NotifyItemPicked(itemID, this);
        ToastNotification.Show($"Place the {itemID} on the table");
        PlayAudioCustom(itemID);
    }

    public void PlayAudioCustom(string itemID)
    {
        AudioClip clipToPlay = null;

        switch (itemID)
        {
            case "Tape Gun":
                clipToPlay = SoundManager.Instance.guide_TapeGun;
                break;

            case "Safety Helmet":
                clipToPlay = SoundManager.Instance.guide_Helmet;
                break;

            case "Safety Gloves":
                clipToPlay = SoundManager.Instance.guide_Gloves;
                break;

            case "Barcode Scanner":
                clipToPlay = SoundManager.Instance.guide_Barcode;
                break;

            default:
                Debug.LogWarning($"No guide audio found for itemID: {itemID}");
                return;
        }

        if (clipToPlay != null)
            SoundManager.Instance.PlaySound(clipToPlay);
    }

}
