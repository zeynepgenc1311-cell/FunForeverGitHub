using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RodSlot : MonoBehaviour
{
    [SerializeField] private Image slotIcon;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button slotButton;

    public SCItem currentItem;

    private void Start()
    {
        slotButton.onClick.AddListener(() =>
        {
            RodMarketManager.Instance.MarketRequest(currentItem, 1);
        });
    }

    public void Initialize(SCItem newItem)
    {
        currentItem = newItem;

        if (currentItem != null)
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
