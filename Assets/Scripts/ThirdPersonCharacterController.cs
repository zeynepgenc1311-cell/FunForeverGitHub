using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonCharacterController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float acceleration = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.2f;

    [Header("Rotation")]
    [Tooltip("Eğer true ise karakter kameranın yatay yönüne (yaw) göre döner.")]
    public bool rotateWithCameraYaw = true;
    public float rotationSmoothTime = 0.08f;

    [Header("References")]
    public Transform cameraTransform;

    // internal
    CharacterController cc;
    float currentSpeed;
    Vector3 velocity;
    float rotationVelocity;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        HandleMovement();
        ApplyGravity();
        MoveCharacter();
    }

    void HandleMovement()
    {
        // Input
        float h = Input.GetAxisRaw("Horizontal");   // A/D or Left/Right
        float v = Input.GetAxisRaw("Vertical");     // W/S or Up/Down
        bool run = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Desired speed
        float targetSpeed = (run ? runSpeed : walkSpeed) * Mathf.Clamp01(new Vector2(h, v).magnitude);
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        // Movement direction relative to camera
        Vector3 camForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;
        Vector3 moveDir = (camForward * v + camRight * h).normalized;

        // If there's movement input, move that direction
        Vector3 horizontalVelocity = moveDir * currentSpeed;

        // Stick to ground when nearly not moving
        if (cc.isGrounded && horizontalVelocity.magnitude < 0.01f)
            horizontalVelocity = Vector3.zero;

        // Set horizontal part of velocity, keep vertical velocity separate
        velocity.x = horizontalVelocity.x;
        velocity.z = horizontalVelocity.z;

        // Rotation: if enabled, align character yaw with camera yaw when mouse/rotation active OR when moving
        if (rotateWithCameraYaw)
        {
            float targetYaw = cameraTransform.eulerAngles.y;
            // Smoothly rotate y
            float smoothedYaw = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetYaw, ref rotationVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothedYaw, 0f);
        }
        else
        {
            // If rotateWithCameraYaw false, optionally rotate toward movement direction for natural turning
            if (moveDir.sqrMagnitude > 0.001f)
            {
                float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
                float smoothed = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationSmoothTime);
                transform.rotation = Quaternion.Euler(0f, smoothed, 0f);
            }
        }

        // Jump
        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }
    }

    void ApplyGravity()
    {
        if (cc.isGrounded && velocity.y < 0f)
            velocity.y = -2f; // small downward force to keep grounded
        else
            velocity.y += gravity * Time.deltaTime;
    }

    void MoveCharacter()
    {
        cc.Move(velocity * Time.deltaTime);
    }
}
