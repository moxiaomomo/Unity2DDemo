using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPlayer : InventoryBase
{
    private EntityStats playerStats;
    public List<InventoryEquipmentSlot> equipList;

    protected override void Awake()
    {
        base.Awake();
        playerStats = GetComponent<EntityStats>();
    }

    public void TryEquipItem(InventoryItem item)
    {
        var inventoryItem = FindItem(item.itemData);
        var matchingSlots = equipList.FindAll(slot => slot.slotType == item.itemData.itemType);

        foreach (var slot in matchingSlots) 
        {
            if (slot.HasItem() == false)
                return;


        }
    }

    private void EquipItem(InventoryItem item, InventoryEquipmentSlot slot)
    {
        slot.equipedItem = item;
        //slot.equipedItem.AddModifiers();

        RemoveItem(item);
    }
}
