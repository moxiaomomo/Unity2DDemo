using System;
using System.Collections.Generic;
using System.Linq;
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
        if (matchingEquips != null && matchingEquips.Count > 0)
        {
            UI_MessageBox.Instance.ShowMessage("已戴上同类装备, 不支持重复添加。");
            return false;
        }

        InventoryEquipment equip;
        if (item is InventoryEquipment)
        {
            equip = (InventoryEquipment)item;
        }
        else
        {
            equip = new InventoryEquipment(item.itemData);
        }

        equipList.Add(equip);
        return true;
    }

    public bool TryUnloadEquipItem(InventoryItem item)
    {
        var matchingEquips = equipList.FindAll(equip => equip.itemData.itemType == item.itemData.itemType);
        if (matchingEquips == null || matchingEquips.Count <= 0)
        {
            return false;
        }
        equipList.RemoveAll(item1 => item.itemData.itemType == item1.itemData.itemType);
        return true;
    }

    // 某个格子物品被选中
    public void onItemSlotSelected(object[] parameters)
    {
        string slotType = (string)parameters[0];
        InventoryItem itemInSlot = (InventoryItem)parameters[1];
        if (itemInSlot != null)
        {
            if(slotType == "EQUIP")
            {
                equipList.ForEach(item => { item.selected = false; });
                foreach (var item in equipList.Where(item => item.itemData.itemType == itemInSlot.itemData.itemType))
                {
                    item.selected = true;
                };
            } 
            else if (slotType == "ITEM")
            {
                itemList.ForEach(item => { item.selected = false; });
                foreach (var item in itemList.Where(item => item.itemData.itemType == itemInSlot.itemData.itemType))
                {
                    item.selected = true;
                };
            }
        }
    }

    public void EquipWeaponFromItemSlot(InventoryItem item)
    {
    }

    public void EquipWeaponFromItemSlot()
    {
        InventoryItem itemSelected = itemList.FirstOrDefault(item => item.selected && (item.itemData.itemType==ItemType.Weapon || item.itemData.itemType == ItemType.Armor));
        if (itemSelected != null)
        {
            bool suc = TryEquipItem(itemSelected);
            if (suc)
            {
                if (itemSelected.stackSize > 1)
                {
                    itemSelected.stackSize--;
                }
                else
                {
                    itemList.Remove(itemSelected);
                }
                OnEquipmentsChange?.Invoke();
            }
        }
        else
        {
            UI_MessageBox.Instance.ShowMessage("该物品不支持穿戴");
        }
    }

    public void UnloadEquipWeapon()
    {
        InventoryItem equipSelected = equipList.FirstOrDefault(item => item.selected);
        if (equipSelected == null)
        {
            Debug.Log("No equipment is selected.");
            return;
        }

        bool suc = TryUnloadEquipItem(equipSelected);
        if (suc)
        {
            InventoryItem itemInSlot = itemList.FirstOrDefault(item => item.itemData.itemType == equipSelected.itemData.itemType);
            if (itemInSlot != null)
            {
                itemInSlot.stackSize++;
            }
            else
            {
                itemList.Add(equipSelected);
            }
            OnEquipmentsChange?.Invoke();
        }
    }
}
