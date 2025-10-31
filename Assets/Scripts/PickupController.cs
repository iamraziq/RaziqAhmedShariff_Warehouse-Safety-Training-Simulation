using UnityEngine;


public class PickupController : MonoBehaviour
{
    public Transform carryAnchor; // assign an empty child in front of player camera
    public float pickupRange = 2f;
    Camera cam;

    IInteractable lastHovered; // track previous hovered object

    void Start()
    {
        cam = Camera.main;
        if (carryAnchor == null) carryAnchor = transform;
    }


    void Update()
    {
        // left mouse / E to pick up - adapt to your input system

        //if (Input.GetKeyDown(KeyCode.E))
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
            {
                var item = hit.collider.GetComponent<PickupItem>();
                if (item != null)
                {
                    item.OnPickedUp(carryAnchor);
                }
            }
        }
    }
}