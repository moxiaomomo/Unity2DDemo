using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_EquipSlot : ItemSlotBase
{
    public override void updateSlot(InventoryItem item)
    {
        itemInSlot = item;
        if (itemInSlot == null)
        {
            itemIcon.sprite = null;
            itemIcon.color = Color.clear;
            return;
        }

        Color color = Color.white;
        color.a = .9f;
        itemIcon.color = color;
        itemIcon.sprite = itemInSlot.itemData.itemIcon;
    }
}
