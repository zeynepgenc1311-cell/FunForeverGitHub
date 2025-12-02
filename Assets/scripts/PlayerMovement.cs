using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Camera")]
    public Transform cameraTransform;
    public float mouseSensitivity = 150f;

    [Header("Inventory")]
    public GameObject userInventory;
    private bool isInventoryOpen = false;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.1f;

    private Rigidbody rb;
    private CapsuleCollider capsule;
    private Vector3 moveInput;
    private float rotX = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();

        rb.freezeRotation = true;

        if (userInventory != null)
            userInventory.SetActive(false);
    }

    void Update()
    {
        // Inventory aç/kapa
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryOpen = !isInventoryOpen;
            userInventory.SetActive(isInventoryOpen);
        }

        if (isInventoryOpen)
        {
            moveInput = Vector3.zero;
            return;
        }

        // Kamera dönüşü (third person)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, -45f, 45f);

        if (cameraTransform != null)
            cameraTransform.localRotation = Quaternion.Euler(rotX, 0, 0);

        transform.Rotate(Vector3.up * mouseX);

        // Hareket input
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveInput = (transform.forward * v + transform.right * h).normalized;

        // Zıplama
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Y sıfırla
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Rigidbody ile hareket
        Vector3 targetVelocity = moveInput * movementSpeed;
        rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);
    }

    bool IsGrounded()
    {
        // CapsuleCollider tabanına göre raycast ground check
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        float distance = capsule.bounds.extents.y + groundCheckDistance;
        return Physics.Raycast(origin, Vector3.down, distance, groundLayer);
    }
}
