using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class InventoryPlayer : InventoryBase, IEquippable
{
    private EntityStats playerStats;
    public List<InventoryEquipment> equipList;
    public event Action OnEquipmentsChange;

    protected void Awake()
    {
        playerStats = GetComponent<EntityStats>();
        GlobalEventSystem.Instance.Subscribe("itemSlotSelected", onItemSlotSelected);
    }

    public bool TryEquipItem(InventoryItem item)
    {
        // var inventoryItem = FindItem(item.itemData);
        var matchingEquips = equipList.FindAll(equip => equip.itemData.itemType == item.itemData.itemType);
        if (matchingEquips!=null && matchingEquips.Count>0)
        {
            return false;
        }

        EquipmentDataSO equipDataSO = new EquipmentDataSO();
        foreach (PropertyInfo item1 in typeof(ItemDataSO).GetProperties())
        {
            item1.SetValue(equipDataSO, item1.GetValue(item.itemData));
        }

        InventoryEquipment equip = new InventoryEquipment(equipDataSO);
        foreach (PropertyInfo item1 in typeof(InventoryItem).GetProperties())
        {
            item1.SetValue(equip, item1.GetValue(item));
        }

        equipList.Add(equip);
        return true;
    }

    // 某个格子物品被选中
    public void onItemSlotSelected(object[] parameters)
    {
        InventoryItem itemInSlot = (InventoryItem)parameters[0];
        if (itemInSlot != null)
        {
            foreach (var item in itemList)
            {
                if (item.itemData.itemType == itemInSlot.itemData.itemType)
                {
                    item.selected = true;
                    break;
                }
            }
        }
    }

    public void EquipWeaponFromItemSlot(InventoryItem item)
    {

    }

    public void EquipWeaponFromItemSlot()
    {
        InventoryItem itemSelected = null;
        foreach (var item in itemList)
        {
            if (item.selected)
            {
                itemSelected = item;
                break;
            }
        }

        int cnt1 = itemList.Count;
        if (itemSelected != null)
        {
            bool suc = TryEquipItem(itemSelected);
            if (suc)
            {
                itemList.Remove(itemSelected);
                OnEquipmentsChange?.Invoke();
            }
        }
    }

    //private void EquipItem(InventoryItem item, InventoryEquipment slot)
    //{
    //    slot.equipedItem = item;
    //    //slot.equipedItem.AddModifiers();

    //    RemoveItem(item);
    //}
}
