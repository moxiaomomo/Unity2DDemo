using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemDataSO itemData;
    public uint stackSize = 1;
    public ItemModifier[] modifiers {  get; private set; }

    public InventoryItem(ItemDataSO itemData)
    {
        this.itemData = itemData;
        modifiers = EquipmentData()?.modifiers;
    }

    public void AddModifers(EntityStats playerStats)
    {
        foreach (var modifer in modifiers)
        {
            Stat statToModify = playerStats.GetStatByType(modifer.statType);
            // statToModify.AddModifier()
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
