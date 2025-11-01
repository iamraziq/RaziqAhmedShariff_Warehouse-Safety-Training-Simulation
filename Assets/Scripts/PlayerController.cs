using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSensitivity = 2f;
    public Transform cameraTransform;

    private float rotationX;
    private float rotationY;
    private Rigidbody rb;
    //private CharacterController controller;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = 4f;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }
    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Direction only on XZ plane — no Y movement ever
        Vector3 move = (transform.right * moveX + transform.forward * moveZ).normalized;
        move.y = 0f;

        // Move using velocity (no vertical component)
        Vector3 targetVelocity = move * moveSpeed;
        rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);
        // Instantly stop horizontal movement when no input
        if (moveX == 0 && moveZ == 0)
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
    }

    void HandleMouseLook()
    {
        if (Input.GetMouseButton(1))
        {
            rotationX += Input.GetAxis("Mouse X") * lookSensitivity;
            rotationY -= Input.GetAxis("Mouse Y") * lookSensitivity;
            rotationY = Mathf.Clamp(rotationY, -80f, 80f);

            transform.rotation = Quaternion.Euler(0, rotationX, 0);
            cameraTransform.localRotation = Quaternion.Euler(rotationY, 0, 0);
        }
    }
}

