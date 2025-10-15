using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    // 面板中的物品格
    private UI_ItemSlot[] uiItemSlots;
    // 玩家中的库存物品
    private InventoryBase inventory;

    private void Awake()
    {
        uiItemSlots = GetComponentsInChildren<UI_ItemSlot>();
        inventory = FindFirstObjectByType<InventoryBase>();
        inventory.OnInventoryChange += updateInventorySlots;
    }

    private void updateInventorySlots()
    {
        List<InventoryItem> itemList = inventory.itemList;
        for (int i = 0; i < uiItemSlots.Length; i++)
        {
            if (i<itemList.Count)
            {
                uiItemSlots[i].updateSlot(itemList[i]);
            }
            else
            {
                uiItemSlots[i].updateSlot(null);
            }
        }
    }
}
