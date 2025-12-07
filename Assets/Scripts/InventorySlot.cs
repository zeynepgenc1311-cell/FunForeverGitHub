using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(OnClickSlot);
    }

    private void OnClickSlot()
    {
        if(item != null)
        {
            // FishSellPanel’e ekle
            FishSellPanel.Instance.AddFish(item, item.price);
        }
    }
}
