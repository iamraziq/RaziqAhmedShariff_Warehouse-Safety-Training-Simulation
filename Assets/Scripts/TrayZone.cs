using UnityEngine;
using System.Collections.Generic;


public class TrayZone : MonoBehaviour
{
    public List<string> requiredItems = new List<string> { "TapeGun", "BarcodeScanner", "Helmet", "Gloves" };
    HashSet<string> placedItems = new HashSet<string>();

    [Tooltip("Assign 4 spawn points (as child transforms) in the inspector.")]
    public Transform[] spawnPoints = new Transform[4];
    void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<PickupItem>();
        Debug.Log("TrayZone detected item: " + (item != null ? item.itemID : "null"));
        if (item == null) return;
        //if (requiredItems.Contains(item.itemID))
        //{
        int itemIndex = requiredItems.IndexOf(item.itemID);
        if (itemIndex >= 0 && itemIndex < spawnPoints.Length)
        {
            if (!placedItems.Contains(item.itemID))
            {
                placedItems.Add(item.itemID);
                // optionally snap item to a slot position
                WarehouseManager.Instance.NotifyItemPlaced(item.itemID);
                // detach this item from player
                item.transform.SetParent(transform);
                //item.transform.localPosition = Vector3.zero; // change per-slot if needed
                // Snap to the corresponding spawn point
                item.transform.position = spawnPoints[itemIndex].position;
                //item.transform.rotation = spawnPoints[itemIndex].rotation;
                if (placedItems.Count < requiredItems.Count)
                {
                    ToastNotification.Show("Now, collect and place the remaining items before the timer runs out!");
                    if (SoundManager.Instance != null)
                        SoundManager.Instance.PlaySound(SoundManager.Instance.guide_Remaining);
                }
            }
        }
    }


    public bool IsAllPlaced()
    {
        return placedItems.Count >= requiredItems.Count;
    }
}