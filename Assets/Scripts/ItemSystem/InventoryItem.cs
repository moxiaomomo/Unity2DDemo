using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemDataSO itemData;
    public InventoryItem(ItemDataSO itemData)
    {
        this.itemData = itemData;
    }
}
