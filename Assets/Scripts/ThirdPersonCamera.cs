using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // karakter transformu
    public Vector3 offset = new Vector3(0f, 2f, -4f);

    [Header("Sensitivity")]
    public float mouseSensitivityX = 5f;
    public float mouseSensitivityY = 3.5f;

    [Header("Clamp")]
    public float minYAngle = -35f;
    public float maxYAngle = 60f;

    [Header("Smooth")]
    public float followSpeed = 10f;
    public float rotationSmoothSpeed = 10f;

    [Header("Options")]
    [Tooltip("Eğer true ise fareyle dönerken sadece fare sağ tuşunu (RMB) basılı tutunca dönecek. False ise her zaman döner.")]
    public bool holdRightMouseToRotate = true;

    float yaw;   // horizontal
    float pitch; // vertical

    void Start()
    {
        if (target == null && GameObject.FindWithTag("Player") != null)
            target = GameObject.FindWithTag("Player").transform;

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        // Lock cursor optionally
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Rotation input
        bool canRotate = !holdRightMouseToRotate || Input.GetMouseButton(1);

        if (canRotate)
        {
            float mx = Input.GetAxis("Mouse X") * mouseSensitivityX;
            float my = -Input.GetAxis("Mouse Y") * mouseSensitivityY;

            yaw += mx;
            pitch += my;
            pitch = Mathf.Clamp(pitch, minYAngle, maxYAngle);
        }

        // Desired rotation and position
        Quaternion targetRot = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = target.position + targetRot * offset;

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 1f - Mathf.Exp(-followSpeed * Time.deltaTime));
        Quaternion smoothedRot = Quaternion.Slerp(transform.rotation, targetRot, 1f - Mathf.Exp(-rotationSmoothSpeed * Time.deltaTime));
        transform.rotation = smoothedRot;

        // If we want character to follow camera's yaw, we can optionally expose that by setting a flag in character script.
        // The character script (provided separately) can read camera's yaw (transform.eulerAngles.y) and rotate the character accordingly.
    }
}
