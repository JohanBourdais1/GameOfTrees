using UnityEngine;
using UnityEngine.InputSystem;

public class PcPlayer : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpForce = 5.0f;
    public float mouseSensitivity = 100.0f;
    public Transform playerCamera;
    public Transform Axe;
    private Rigidbody rb;
    private float xRotation = 0f;
    private Vector2 moveInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    void Update()
    {
        RotateCamera();
        CheckGrounded();
        attack();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    bool isAttacking = false;
    void attack()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            if (isAttacking == false)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit))
                {
                    // Check if the hit object has a Tree component
                    TreeScript tree = hit.transform.GetComponent<TreeScript>();
                    if (tree != null)
                    {
                        // Do damage to the tree
                        if (TryGetComponent(out AudioSource source))
                            source.Play();

                        tree.takeDamage();
                    }
                }
                isAttacking = true;
            }
            // Left mouse button is currently being pressed
            Axe.transform.eulerAngles = new Vector3(-60, Axe.transform.eulerAngles.y, Axe.transform.eulerAngles.z);

        }
        else
        {
            isAttacking = false;

            // Left mouse button is not currently being pressed
            Axe.transform.eulerAngles = new Vector3(0, Axe.transform.eulerAngles.y, Axe.transform.eulerAngles.z);
        }
    }
    void MovePlayer()
    {
        moveInput = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) moveInput.y += 1;
        if (Keyboard.current.sKey.isPressed) moveInput.y -= 1;
        if (Keyboard.current.aKey.isPressed) moveInput.x -= 1;
        if (Keyboard.current.dKey.isPressed) moveInput.x += 1;
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
    }

    void RotateCamera()
    {
        Vector2 mouseInput = Mouse.current.delta.ReadValue() * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseInput.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseInput.x);
    }

    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
