using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Invetory", menuName = "Scriptable/Invetory")]

public class SCInventory : ScriptableObject
{
    public List<Slot> inventorySlots = new List<Slot>();
    int stackLimit = 50;
    public void DeleteItem(int index)
    {
        inventorySlots[index].isFull = false;
        inventorySlots[index].itemCount = 0;
        inventorySlots[index].item = null;
    }
    public void DropItem(int index, Vector3 position)
    {
        for(int i = 0; i < inventorySlots[index].itemCount; i++)
        {
            GameObject tempItem = Instantiate(inventorySlots[index].item.itemPrefab);
        tempItem.transform.position = position+new Vector3(i,0,0);
        }
        DeleteItem(index);
    }
    public bool AddItem(SCItem item)
    {
        foreach(Slot slot in inventorySlots)
        {
            if (slot.item == item)
            {
                if (slot.item.canStackable)
                {
                    if (slot.itemCount < stackLimit)
                    {
                        slot.itemCount++;
                        if (slot.itemCount >= stackLimit)
                        {
                            slot.isFull = true;
                        }
                        return true;
                    }
                }
            }
            else if(slot.itemCount==0)
            {
                slot.AddItemToSlot(item);
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class Slot
{
    public bool isFull;
    public int itemCount;
    public SCItem item;
    public void AddItemToSlot(SCItem item)
    {
        this.item = item;
        if(item.canStackable == false)
        {
            isFull = true;
        }
        itemCount++;

    }
}