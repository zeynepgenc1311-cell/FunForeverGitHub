using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 7f;
    public float rotationSmooth = 10f;

    [Header("Camera")]
    public Transform cameraTransform;

    [Header("UI")]
    public GameObject hotbarUI;

    [Header("Ground Check")]
    public LayerMask groundLayer;

    private Rigidbody rb;
    private CapsuleCollider capsule;
    private Vector3 moveInput;
    private bool uiOpen;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();

        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        SetCursor(true);
    }

    void Update()
    {
        // HOTBAR AÇ / KAPA
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            uiOpen = !uiOpen;

            if (hotbarUI)
                hotbarUI.SetActive(uiOpen);

            SetCursor(!uiOpen);
        }

        if (uiOpen)
        {
            moveInput = Vector3.zero;
            return;
        }

        // INPUT
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        moveInput = (camForward * v + camRight * h).normalized;

        // PLAYER DÖNÜŞÜ (kameraya göre)
        if (moveInput.magnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                rotationSmooth * Time.deltaTime
            );
        }

        // ZIPLAMA
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(
            moveInput.x * speed,
            rb.velocity.y,
            moveInput.z * speed
        );
    }

    bool IsGrounded()
    {
        Vector3 pos = transform.position + Vector3.down * (capsule.height / 2f - 0.05f);
        float radius = capsule.radius * 0.9f;
        return Physics.CheckSphere(pos, radius, groundLayer);
    }

    void SetCursor(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }
}
