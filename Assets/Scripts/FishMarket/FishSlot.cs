using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class FishSlot : MonoBehaviour
{
   [SerializeField] private Image slotIcon;
   [SerializeField] private TextMeshProUGUI priceText;
   [SerializeField] private Button slotButton;
   public SCItem currentItem;
   private void Start()
    {
        slotButton.onClick.AddListener(() =>
        {
            FishMarketManager.Instance.MarketRequest(currentItem, 1);
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
        }
        else
        {
            slotIcon.enabled = false;
            priceText.text = "";
        }
    }
}
