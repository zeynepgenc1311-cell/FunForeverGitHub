using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RodMarketManager : MonoBehaviour
{
    [SerializeField] private GameObject buyContent;
    [SerializeField] private List<SCItem> buyItems = new();
    [SerializeField] private RodSlot RodSlotPrefab;
    [SerializeField] private TextMeshProUGUI CoinText;

    public int CoinPanel = 0;


    private void Start()
    {
        BuyFillSlots();
        CoinText.text = CoinPanel.ToString(); 
    }

    public RodMarketState currentState = RodMarketState.Buy;

    public void MarketRequest(SCItem item, int amount)
    {
        switch (currentState)
        {
            case RodMarketState.Buy:
            bool enoughMoney = CurrencyManager.Instance.SpendCoins(item.itemPrice);
            if (enoughMoney)
            {
                bool success = Inventory.Instance.AddItem(item, amount);
                    if (!success)
                        Debug.Log("Envanter dolu.");
            }
            else Debug.Log("paran yetmiyo");
            break;
        }
    }


    public void BuyFillSlots()
    {
        foreach (SCItem item in buyItems)
        {
            RodSlot slot = Instantiate(RodSlotPrefab, buyContent.transform.position, Quaternion.identity);
            slot.transform.SetParent(buyContent.transform);
            slot.Initialize(item);
        }
    }
    
    public static RodMarketManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}

[System.Serializable]
public enum RodMarketState
{
    Buy
}