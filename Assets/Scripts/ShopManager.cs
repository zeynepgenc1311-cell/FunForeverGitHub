using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject slotPrefab; // ShopSlot prefabı
    public Transform contentParent; // Panelin child olarak slotların ekleneceği yer
    public SCItem[] shopItems; // Shopta satılacak itemler

    void Start()
    {
        foreach (SCItem item in shopItems)
        {
            GameObject slotGO = Instantiate(slotPrefab, contentParent);
            slotGO.GetComponent<ShopSlot>().Setup(item);
        }
    }
}
