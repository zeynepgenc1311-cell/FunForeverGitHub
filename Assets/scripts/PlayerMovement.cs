using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 5f;
    public float jumpForce = 7f;
    public float rotationSmooth = 10f;

    [Header("Camera Reference")]
    public Transform cameraTransform; // Main Camera

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.2f;

    private Rigidbody rb;
    private CapsuleCollider capsule;
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();

        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Input
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Kameraya göre yön
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0f;
        camRight.Normalize();

        moveInput = (camForward * v + camRight * h).normalized;

        // Player kameraya doğru dönsün (SADECE YATAY)
        if (moveInput.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSmooth * Time.deltaTime
            );
        }

        // Jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        Vector3 targetVelocity = moveInput * movementSpeed;
        rb.velocity = new Vector3(
            targetVelocity.x,
            rb.velocity.y,
            targetVelocity.z
        );
    }

    bool IsGrounded()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        float distance = capsule.bounds.extents.y + groundCheckDistance;
        return Physics.Raycast(origin, Vector3.down, distance, groundLayer);
    }
}
