using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -5);
    public float sensitivity = 3f;

    private float rotX;
    private float rotY;

    void LateUpdate()
    {
        if (!target) return;

        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity;

        rotY += mouseX;
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, -35f, 60f);

        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.position = target.position + rot * offset;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
