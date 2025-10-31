using UnityEngine;
using System;

public class Interactor : MonoBehaviour
{
    public float maxDistance = 6f;
    public LayerMask interactableLayer;
    Camera cam;
    IInteractable lastHit;
    Collider lastHitCollider; //store actual collider for reference

    void Awake() => cam = Camera.main;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Click: first check UI via EventSystem (UI handles itself).
            TrySelect();
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactableLayer))
        {
            var interactable = hit.collider.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
                if (lastHit == null || !ReferenceEquals(interactable, lastHit))
                {
                    lastHit?.OnHoverExit();
                    interactable.OnHoverEnter();
                    lastHit = interactable;
                    lastHitCollider = hit.collider; 
                }
                return;
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);


        //// nothing hit
        //if (lastHit != null) { lastHit.OnHoverExit(); lastHit = null; }
        // nothing hit
        if (lastHit != null)
        {
            lastHit.OnHoverExit();
            lastHit = null;
            lastHitCollider = null;
        }

    }

    void TrySelect()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactableLayer))
        {
            var interactable = hit.collider.GetComponentInParent<IInteractable>();
            interactable?.OnSelect();
        }
    }
    //his method now safely returns the collider you’re hovering over
    public Collider GetHoveredCollider()
    {
        return lastHitCollider;
    }

}

public interface IInteractable
{
    void OnHoverEnter();
    void OnHoverExit();
    void OnSelect();
}
