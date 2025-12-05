using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public int price; // item fiyatı
    public bool isGem; // true = gem, false = coin

    public Button buyButton;

    private void Start()
    {
        buyButton.onClick.AddListener(Buy);
    }

    void Buy()
    {
        if (isGem)
        {
            if (CurrencyManager.Instance.SpendGems(price))
            {
                Debug.Log("Gem ile satın alındı!");
                // item unlock kodunu buraya yazıcaz
            }
            else
            {
                Debug.Log("Yetersiz gem!");
            }
        }
        else
        {
            if (CurrencyManager.Instance.SpendCoins(price))
            {
                Debug.Log("Coin ile satın alındı!");
                // item unlock kodu buraya
            }
            else
            {
                Debug.Log("Yetersiz coin!");
            }
        }
    }
}
