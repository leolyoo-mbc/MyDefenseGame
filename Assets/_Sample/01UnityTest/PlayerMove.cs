using UnityEngine;
using UnityEngine.InputSystem;

public class EventTest : MonoBehaviour
{
    InputAction moveAction;
    InputAction jumpAction;
    InputAction interactAction;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isjumpRequested = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    private void Update()
    {
        if (jumpAction.WasPressedThisFrame())
        {
            isjumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        Vector2 inputDirection = moveAction.ReadValue<Vector2>();
        Vector3 targetVelocity = new(inputDirection.x * moveSpeed, rb.linearVelocity.y, inputDirection.y * moveSpeed);
        rb.linearVelocity = targetVelocity;

        if (isjumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isjumpRequested = false;
        }

        if (interactAction.WasPressedThisFrame())
        {
            transform.position = Vector3.zero;
        }
    }
}
