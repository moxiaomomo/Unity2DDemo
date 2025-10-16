using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemDataSO itemData;
    public uint stackSize = 1;
    public bool selected = false;
    
    public InventoryItem(ItemDataSO itemData)
    {
        this.itemData = itemData;
    }

    public bool CanAddStack() => stackSize < itemData.maxStackSize;

    public void AddStack()
    {
        if(CanAddStack())
        {
            stackSize++;
        }
    }

    public void RemoveStack()
    {
        if (stackSize > 0)
        {
            stackSize--;
        }
    }
}
