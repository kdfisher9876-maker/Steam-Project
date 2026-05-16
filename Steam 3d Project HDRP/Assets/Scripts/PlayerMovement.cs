using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float sneakSpeed = 2.5f;
    public float mouseSensitivity = 2f;

    public Transform cameraHolder;

    private CharacterController controller;
    private float verticalLookRotation;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        Look();
    }

    void Move()
    {
        Keyboard keyboard = Keyboard.current;

        float x = 0f;
        float z = 0f;

        if (keyboard.aKey.isPressed) x -= 1f;
        if (keyboard.dKey.isPressed) x += 1f;
        if (keyboard.wKey.isPressed) z += 1f;
        if (keyboard.sKey.isPressed) z -= 1f;

        float currentSpeed = walkSpeed;

        if (keyboard.leftShiftKey.isPressed)
            currentSpeed = runSpeed;

        if (keyboard.leftCtrlKey.isPressed)
            currentSpeed = sneakSpeed;

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move.normalized * currentSpeed * Time.deltaTime);
    }

    void Look()
    {
        Mouse mouse = Mouse.current;
        if (mouse == null) return;

        Vector2 mouseDelta = mouse.delta.ReadValue();

        float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.localEulerAngles = Vector3.right * verticalLookRotation;
    }
}