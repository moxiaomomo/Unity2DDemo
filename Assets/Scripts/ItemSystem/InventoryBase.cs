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
        itemList.Add(itemToAdd);
        OnInventoryChange?.Invoke();
    }
}
