using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public SCInventory playerInventory;
    public PlayerActions playerAction;
    InventoryUIController inventoryUI;

    bool isSwapping;
    int tempIndex;
    Slot tempSlot;

    private void Start()
    {
        inventoryUI = GetComponent<InventoryUIController>();
        inventoryUI.UpdateUI();
    }

    public void CurrentItem(int index)
    {
        if (playerInventory.inventorySlots[index].item != null)
        {
            playerAction.SetItem(playerInventory.inventorySlots[index].item.itemPrefab);
        }
    }

    public void DeleteItem()
    {
        if (isSwapping)
        {
            playerInventory.DeleteItem(tempIndex);
            isSwapping = false;
            inventoryUI.UpdateUI();
        }
    }

    public void DropItem()
    {
        if (isSwapping)
        {
            playerInventory.DropItem(tempIndex, transform.position + Vector3.forward);
            isSwapping = false;
            inventoryUI.UpdateUI();
        }
    }

    public void SwapItem(int index)
    {
        if (!isSwapping)
        {
            tempIndex = index;
            tempSlot = playerInventory.inventorySlots[tempIndex];
            isSwapping = true;
        }
        else
        {
            playerInventory.inventorySlots[tempIndex] = playerInventory.inventorySlots[index];
            playerInventory.inventorySlots[index] = tempSlot;
            isSwapping = false;
        }

        inventoryUI.UpdateUI();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Item worldItem = other.GetComponent<Item>();
            if (worldItem == null) return;

            if (playerInventory.AddItem(worldItem.item))
            {
                Destroy(other.gameObject);
                inventoryUI.UpdateUI();
            }
        }
    }
}
