using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishSellPanel : MonoBehaviour
{
    public static FishSellPanel Instance;

    [Header("Refs")]
    public Transform content;            // Content (Grid/Vertical) inside panel
    public GameObject fishSlotPrefab;    // FishSlot prefab
    public Text totalPriceText;

    private void Awake()
    {
        Instance = this;
    }

    // geri uyumluluk için SetItem ekledik
    public void SetItem(Item item, int price)
    {
        AddFish(item, price);
    }

    // asıl iş yapan fonksiyon
    public void AddFish(Item item, int price)
{
    if (item == null) return;

    GameObject go = Instantiate(fishSlotPrefab, content);
    SlotUI slot = go.GetComponent<SlotUI>();

    // İkonu koy
    slot.itemImage.sprite = item.icon;


    // Miktar (şimdilik 1)
    slot.itemCountText.text = "1";

    // Fiyat
    if (slot.priceText != null)
        slot.priceText.text = price.ToString();

    // Item referansını kaydet
    slot.item = item;

    // Toplam fiyat güncelle
    UpdateTotalPrice();
}

public void UpdateTotalPrice()
{
    int total = 0;

    foreach (Transform child in content)
    {
        SlotUI slot = child.GetComponent<SlotUI>();
        if (slot != null && slot.priceText != null)
        {
            int p = int.Parse(slot.priceText.text);
            total += p;
        }
    }

    totalPriceText.text = total.ToString();
}


    // toplu satma için placeholder
    public void SellAll()
    {
        Debug.Log("SellAll tetiklendi implement et");
    }
}
