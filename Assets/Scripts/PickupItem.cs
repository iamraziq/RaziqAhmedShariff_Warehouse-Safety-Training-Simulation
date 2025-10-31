using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupItem : MonoBehaviour
{
    public string itemName;
    Rigidbody rb;
    bool isPicked = false;
    Transform originalParent;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = true;
            Debug.Log("[PickupItem] Rigidbody found and set kinematic on " + name);
        }
        else
        {
            Debug.LogWarning("[PickupItem] No Rigidbody on " + name);
        }

        originalParent = transform.parent;
    }

    public void OnPickUp(Transform holdParent)
    {
        if (isPicked)
        {
            Debug.Log("[PickupItem] Already picked: " + name);
            return;
        }

        Debug.Log("[PickupItem] Picking up " + name);
        isPicked = true;
        if (rb) rb.isKinematic = true;
        transform.SetParent(holdParent, true);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        var col = GetComponent<Collider>();
        if (col)
        {
            col.enabled = false;
            Debug.Log("[PickupItem] Collider disabled for: " + name);
        }
    }

    public void OnDrop(Vector3 worldPosition)
    {
        Debug.Log("[PickupItem] Dropping " + name);
        isPicked = false;
        transform.SetParent(originalParent, true);
        transform.position = worldPosition;

        if (rb) rb.isKinematic = false;

        var col = GetComponent<Collider>();
        if (col)
        {
            col.enabled = true;
            Debug.Log("[PickupItem] Collider re-enabled for: " + name);
        }
    }

    public bool IsPicked() => isPicked;
}
