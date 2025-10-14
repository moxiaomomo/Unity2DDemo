using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBase : MonoBehaviour
{
    public event Action OnInventoryChange;
    private UI_ItemSlot[] uiItemSlots;

    public int maxInventorySize = 10;
    public List<InventoryItem> itemList = new List<InventoryItem>();

    private void Start()
    {
        uiItemSlots = FindObjectsOfType<UI_ItemSlot>();
        if (uiItemSlots != null)
        {
            maxInventorySize = uiItemSlots.Length;
        }
    }

    public bool CanAddItem() => itemList.Count < maxInventorySize;

    public void AddItem(InventoryItem itemToAdd)
    {
        InventoryItem itemExists = FindItem(itemToAdd.itemData);
        if (itemExists != null)
        {
            itemExists.AddStack();
        }
        else
        {
            if(!CanAddItem())
            { 
                return; // 所有的物品格都已经装满
            }
            itemList.Add(itemToAdd);
        }

        OnInventoryChange?.Invoke();
    }

    public InventoryItem FindItem(ItemDataSO itemSO)
    {
        return itemList.Find(item => item.itemData == itemSO && item.CanAddStack());
    }
}
