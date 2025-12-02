using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;           // Player transform
    public Vector3 offset = new Vector3(0, 3, -5); // Kamera konumu player’a göre
    public float rotationSpeed = 5f;   // Mouse ile dönüş hızı
    public float followSpeed = 10f;    // Kamera yumuşak takip hızı

    private float rotX = 0f;

    void LateUpdate()
    {
        if (target == null) return;

        // Mouse input
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        // Kamera dikey rotasyonu
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, -35f, 60f); // Third person için uygun açılar

        // Kamerayı yatay döndür (player etrafında)
        target.Rotate(Vector3.up * mouseX);

        // Hedef pozisyonu hesapla
        Vector3 desiredPosition = target.position + target.rotation * offset;

        // Kamera yumuşak şekilde hareket etsin
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Kamera her zaman player’a baksın
        transform.LookAt(target.position + Vector3.up * 1.5f); // Player yüksekliğine bak
    }
}
