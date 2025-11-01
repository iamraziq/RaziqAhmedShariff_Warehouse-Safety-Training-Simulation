//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    public float moveSpeed = 5f;
//    public float lookSensitivity = 2f;
//    public Transform cameraTransform;

//    private float rotationX;
//    private float rotationY;

//    void Update()
//    {
//        HandleMovement();
//        HandleMouseLook();
//    }

//    void HandleMovement()
//    {
//        float moveX = Input.GetAxis("Horizontal");
//        float moveZ = Input.GetAxis("Vertical");

//        Vector3 move = transform.right * moveX + transform.forward * moveZ;
//        transform.position += move * moveSpeed * Time.deltaTime;
//    }

//    void HandleMouseLook()
//    {
//        if (Input.GetMouseButton(1))
//        {
//            rotationX += Input.GetAxis("Mouse X") * lookSensitivity;
//            rotationY -= Input.GetAxis("Mouse Y") * lookSensitivity;
//            rotationY = Mathf.Clamp(rotationY, -80f, 80f);

//            transform.rotation = Quaternion.Euler(0, rotationX, 0);
//            cameraTransform.localRotation = Quaternion.Euler(rotationY, 0, 0);
//        }
//    }
//}

using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSensitivity = 2f;
    public Transform cameraTransform;

    private float rotationX;
    private float rotationY;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
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

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);
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

