using UnityEngine;


public class PickupItem : MonoBehaviour
{
    public string itemID; // e.g., "TapeGun"
    public AudioClip pickupAudio;
    bool isCollected = false;
    public GameObject glowObject;

    void Awake()
    {
        if (string.IsNullOrEmpty(itemID)) itemID = gameObject.name;
    }

    public void OnPickedUp(Transform parent)
    {
        if (isCollected) return;
        isCollected = true;
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero; // adjust if you want offset
        var rb = GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;
        // play audio if assigned
        if (pickupAudio) AudioSource.PlayClipAtPoint(pickupAudio, transform.position);


        // notify manager
        // WarehouseManager.Instance.NotifyItemPicked(itemID, this);
    }
}
