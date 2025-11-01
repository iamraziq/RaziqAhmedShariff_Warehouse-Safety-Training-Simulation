using UnityEngine;
using UnityEngine.UI;

public class ItemInspector : MonoBehaviour
{
    public float rotationSpeed = 200f;
    System.Action onDone;
    bool inspecting = false;

    public void BeginInspect(System.Action doneCallback)
    {
        onDone = doneCallback;
        inspecting = true;
    }

    void Update()
    {
        if (!inspecting) return;

        // rotate while left mouse held and moving
        if (Input.GetMouseButton(0))
        {
            float dx = Input.GetAxis("Mouse X");
            float dy = Input.GetAxis("Mouse Y");
            transform.Rotate(Vector3.up, -dx * rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, dy * rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    public void EndInspect()
    {
        onDone?.Invoke();
    }
}
