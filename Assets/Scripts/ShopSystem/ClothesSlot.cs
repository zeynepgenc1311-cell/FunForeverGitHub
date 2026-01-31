using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class ClothesShopSlot : MonoBehaviour
{
   [SerializeField] private Image slotIcon;
   [SerializeField] private TextMeshProUGUI priceText;
   [SerializeField] private Button slotButton;
   [SerializeField] private Image priceIcon;

   public SCItem currentItem;
   private void Start()
    {
        slotButton.onClick.AddListener(() =>
        {
            ClothesShopManager.Instance.MarketRequest(currentItem, 1);
        });
        Initialize(currentItem);
    }

    public void Initialize(SCItem newItem)
    {
        currentItem = newItem;
        if(currentItem != null)
        {
            slotIcon.enabled = true;
            slotIcon.sprite = currentItem.itemSprite;
            priceText.text = currentItem.itemPrice.ToString();
            priceIcon.sprite =
        CurrencyManager.Instance.GetCurrencyIcon(currentItem.costType);

        }
        else
        {
            slotIcon.enabled = false;
            priceText.text = "";
        }
    }
    
}
