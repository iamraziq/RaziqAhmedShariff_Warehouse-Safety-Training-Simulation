using UnityEngine;

public class PickupController : MonoBehaviour
{
    public Transform holdTransform; // child of camera or player where item appears while held
    PickupItem held;

    Interactor interactor;

    void Awake()
    {
        interactor = FindObjectOfType<Interactor>();
        Debug.Log("[PickupController] Interactor found: " + (interactor != null));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // pick/drop toggle
        {
            Debug.Log("[PickupController] E pressed. Trying toggle...");
            TryTogglePickup();
        }
    }

    void TryTogglePickup()
    {
        if (held != null)
        {
            Debug.Log("[PickupController] Dropping item: " + held.name);
            Vector3 dropPos = holdTransform.position + Camera.main.transform.forward * 1f;
            held.OnDrop(dropPos);
            held = null;
            return;
        }

        // Try picking up
        var hit = interactor.GetHoveredCollider();
        if (hit == null)
        {
            Debug.LogWarning("[PickupController] No collider hovered. Can't pick up.");
            return;
        }

        Debug.Log("[PickupController] Hovered collider: " + hit.name);
        var pickup = hit.GetComponentInParent<PickupItem>();
        if (pickup != null)
        {
            Debug.Log("[PickupController] Found pickup item: " + pickup.name);
            pickup.OnPickUp(holdTransform);
            held = pickup;
        }
        else
        {
            Debug.LogWarning("[PickupController] Hovered object is not a PickupItem.");
        }
    }
}
