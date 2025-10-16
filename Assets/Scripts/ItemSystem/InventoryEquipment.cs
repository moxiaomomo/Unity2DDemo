using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryEquipment: InventoryItem
{
    public ItemModifier[] modifiers { get; private set; }
    // public InventoryItem equipedItem;

    public InventoryEquipment(EquipmentDataSO itemData): base(itemData)
    {
        modifiers = EquipmentData()?.modifiers;
    }

    // public bool HasItem() => equipedItem != null && equipedItem.itemData != null;

    public void AddModifers(EntityStats playerStats)
    {
        foreach (var modifer in modifiers)
        {
            Stat statToModify = playerStats.GetStatByType(modifer.statType);
            // TODO statToModify.AddModifier();
        }
    }

    private EquipmentDataSO EquipmentData()
    {
        if (itemData is EquipmentDataSO equipment)
        {
            return equipment;
        }
        return null;
    }
}
