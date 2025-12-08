using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMarketManager : MonoBehaviour
{
    [SerializeField] private GameObject sellContent;
    [SerializeField] private List<SCItem> sellItems = new();
    [SerializeField] private FishSlot FishSlotPrefab;

    private void Start()
    {
        SellFillSlots();
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
}

public enum FishMarketState
{
    Sell
}