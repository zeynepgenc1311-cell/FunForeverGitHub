using UnityEngine;

public class OtterTrigger : MonoBehaviour
{
    public GameObject otterPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            otterPanel.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            otterPanel.SetActive(false);
    }
}
