using UnityEngine;
using System;

public class Interactor : MonoBehaviour
{
    public float maxDistance = 6f;
    public LayerMask interactableLayer;
    Camera cam;
    IInteractable lastHit;

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
                }
                return;
            }
        }

        // nothing hit
        if (lastHit != null) { lastHit.OnHoverExit(); lastHit = null; }
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
}

public interface IInteractable
{
    void OnHoverEnter();
    void OnHoverExit();
    void OnSelect();
}
