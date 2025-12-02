using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public SCInventory playerInventory;
    InventoryUIController inventoryUI;
    bool isSwapping;
    int tempIndex;
    Slot tempSlot;
    private void Start()
    {
        inventoryUI=gameObject.GetComponent<InventoryUIController>();
        inventoryUI.UpdateUI();
    }
    public void SwapItem(int index)
    {
        if (isSwapping == false)
        {
            tempIndex = index;
            tempSlot = playerInventory.inventorySlots[tempIndex];
            isSwapping = true;
        }
        else if (isSwapping == true)
        {
            playerInventory.inventorySlots[tempIndex]=playerInventory.inventorySlots[index];
            playerInventory.inventorySlots[index] = tempSlot;
            isSwapping = false;
        }
        inventoryUI.UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            if (playerInventory.AddItem(other.gameObject.GetComponent<Item>().item))
            {
                Destroy(other.gameObject);
                inventoryUI.UpdateUI();
            }
        }
    }
}
