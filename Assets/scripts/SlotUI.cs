using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public Image itemImage;
    public Text itemCountText;
    public Text priceText;




    [HideInInspector] public Item item;  // Slotta hangi item var
    public Button button;                // Slot butonu

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnSlotClicked);
    }

    public void OnSlotClicked()
    {
        if (item == null) return; // boş slot

        // Bu item satılabilir mi kontrol
        SellableItem sellable = item.GetComponent<SellableItem>();

        if (sellable != null)
        {
            // Bu item balık yani satılabilir
            FishSellPanel.Instance.SetItem(item, sellable.sellPrice);
        }
        else
        {
            // Balık değilse panel açılmayacak
            Debug.Log("Bu item balık değil, satılamaz.");
        }
    }
}
