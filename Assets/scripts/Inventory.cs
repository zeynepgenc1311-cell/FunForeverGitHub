using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public SCInventory playerInventory; // SCInventory, SCItem listesi içeriyor
    public PlayerActions playerAction;
    InventoryUIController inventoryUI;

    bool isSwapping;
    int tempIndex;
    Slot tempSlot;

    public static Inventory Instance;

private void Awake()
{
    Instance = this;
}

public bool RemoveItem(SCItem item, int amount)
{
    for (int i = 0; i < playerInventory.inventorySlots.Count; i++)
    {
        if (playerInventory.inventorySlots[i].item == item)
        {
            playerInventory.inventorySlots[i].itemCount -= amount;

            if (playerInventory.inventorySlots[i].itemCount <= 0)
            {
                playerInventory.inventorySlots[i].itemCount = 0;
                playerInventory.inventorySlots[i].item = null;
            }

            inventoryUI.UpdateUI();
            return true;
        }
    }

    return false;
}

public bool AddItem(SCItem item, int amount = 1)
{
    // Önce stacklenebilir ve envanterde aynı item varsa ona ekleyelim
    for (int i = 0; i < playerInventory.inventorySlots.Count; i++)
    {
        Slot slot = playerInventory.inventorySlots[i];

        // Slot dolu ve aynı item ise
        if (slot.item == item)
        {
            // Eğer stacklenebilir ise ekle
            if (item.canStackable)
            {
                slot.itemCount += amount;
                inventoryUI.UpdateUI();
                return true;
            }
        }
    }

    // Eğer stack mevcut değilse, boş slot bulup item yerleştir
    for (int i = 0; i < playerInventory.inventorySlots.Count; i++)
    {
        Slot slot = playerInventory.inventorySlots[i];

        if (slot.item == null)
        {
            slot.item = item;
            slot.itemCount = amount;
            inventoryUI.UpdateUI();
            return true;
        }
    }

    // Hiç boş slot yoksa
    Debug.Log("ENVANTER DOLU! Item eklenemedi.");
    return false;
}



    private void Start()
    {
        inventoryUI = GetComponent<InventoryUIController>();
        inventoryUI.UpdateUI();
    }

    // Burada artık itemPrefab değil SCItem gönderiyoruz
    public void CurrentItem(int index)
    {
        SCItem item = playerInventory.inventorySlots[index].item; // SCItem
        if (item != null)
        {
            playerAction.SetItem(item); // ❌ artık prefab değil, SCItem asset
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
