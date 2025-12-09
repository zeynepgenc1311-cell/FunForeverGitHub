using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishMarketManager : MonoBehaviour
{
    [SerializeField] private GameObject sellContent;
    [SerializeField] private List<SCItem> sellItems = new();
    [SerializeField] private FishSlot FishSlotPrefab;
    [SerializeField] private TextMeshProUGUI CoinText;

    public int CoinPanel = 0;


    private void Start()
    {
        SellFillSlots();
        CoinText.text = CoinPanel.ToString(); 
    }

    public FishMarketState currentState = FishMarketState.Sell;

    public void MarketRequest(SCItem item, int amount)
    {
        switch (currentState)
        {
            case FishMarketState.Sell:
            bool condition = Inventory.Instance.RemoveItem(item, amount);
            if (condition)
            {
                CurrencyManager.Instance.AddCoins(item.itemPrice);
            }
            else Debug.Log("item sende yok satılmadı");
            break;
        }
    }


    public void SellFillSlots()
    {
        foreach (SCItem item in sellItems)
        {
            FishSlot slot = Instantiate(FishSlotPrefab, sellContent.transform.position, Quaternion.identity);
            slot.transform.SetParent(sellContent.transform);
            slot.Initialize(item);
        }
    }
    
    public static FishMarketManager Instance;

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
public enum FishMarketState
{
    Sell
}