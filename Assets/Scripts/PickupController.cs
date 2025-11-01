using UnityEngine;

public class PickupController : MonoBehaviour
{
    public Transform carryAnchor; // assign an empty child in front of player camera
    public LayerMask interactableLayer;
    public float pickupRange = 2f;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        if (carryAnchor == null) carryAnchor = transform;
    }

    void Update()
    {
        // Only fire once per click, not continuously
        if (Input.GetMouseButtonDown(0))
        {
            TryPickup();
        }
    }
    void TryPickup()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        // Cast only against the specified interactable layers
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, interactableLayer))
        {
            var item = hit.collider.GetComponentInParent<PickupItem>();
            item?.OnPickedUp(carryAnchor);
        }
    }

}
