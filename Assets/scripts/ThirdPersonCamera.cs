using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Player
    public Vector3 offset = new Vector3(0, 3, -5);

    public float mouseSensitivity = 3f;
    public float followSpeed = 10f;

    public float minY = -35f;
    public float maxY = 60f;

    private float rotX;
    private float rotY;

    void LateUpdate()
    {
        if (!target) return;

        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        rotY += mouseX;
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, minY, maxY);

        Quaternion rotation = Quaternion.Euler(rotX, rotY, 0f);

        Vector3 desiredPosition = target.position + rotation * offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSpeed * Time.deltaTime
        );

        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
