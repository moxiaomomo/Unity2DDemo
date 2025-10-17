using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        if (item is not InventoryEquipment)
        {
            UI_MessageBox.Instance.ShowMessage("����Ʒ��֧�ִ���~");
            return false;
        }
        var matchingEquips = equipList.FindAll(equip => equip.itemData == item.itemData);
        if (matchingEquips != null && matchingEquips.Count > 0)
        {
            UI_MessageBox.Instance.ShowMessage("�Ѵ���ͬ��װ��, ��֧���ظ���ӡ�");
            return false;
        }

        InventoryEquipment equip = (InventoryEquipment)item;
        equipList.Add(equip);

        // ����Player�������
        updatePlayerPanelData(equip, 1);

        return true;
    }

    public bool TryUnloadEquipItem(InventoryItem item)
    {
        if (item is not InventoryEquipment)
        {
            UI_MessageBox.Instance.ShowMessage("��Ʒ���ɴ���������ʧ��~");
            return false;
        }
        var matchingEquips = equipList.FindAll(equip => equip.itemData == item.itemData);
        if (matchingEquips == null || matchingEquips.Count <= 0)
        {
            return false;
        }
        equipList.RemoveAll(item1 => item.itemData == item1.itemData);
        // ����Player�������
        updatePlayerPanelData((InventoryEquipment)item, 0);
        return true;
    }

    // ĳ��������Ʒ��ѡ��
    public void onItemSlotSelected(object[] parameters)
    {
        string slotType = (string)parameters[0];
        InventoryItem itemInSlot = (InventoryItem)parameters[1];
        if (itemInSlot != null)
        {
            if(slotType == "EQUIP")
            {
                equipList.ForEach(item => { item.selected = false; });
                foreach (var item in equipList.Where(item => item.itemData == itemInSlot.itemData))
                {
                    item.selected = true;
                    break;
                };
            } 
            else if (slotType == "ITEM")
            {
                itemList.ForEach(item => { item.selected = false; });
                foreach (var item in itemList.Where(item => item.itemData == itemInSlot.itemData))
                {
                    item.selected = true;
                    break;
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
            UI_MessageBox.Instance.ShowMessage("����Ʒ��֧�ִ���");
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
            InventoryItem itemInSlot = itemList.FirstOrDefault(item => item.itemData == equipSelected.itemData);
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

    private void updatePlayerPanelData(InventoryEquipment equip, int opType)
    {
        if (opType==1) // ���
        {
            foreach(ItemModifier modifier in equip.modifiers)
            {
                Stat stat = playerStats.GetStatByType(modifier.statType);
                if (stat != null)
                {
                    stat.AddModifier(modifier.value, equip.itemData.itemName);
                }
            }
        }
        else // ж��
        {
            foreach (ItemModifier modifier in equip.modifiers)
            {
                Stat stat = playerStats.GetStatByType(modifier.statType);
                if (stat != null)
                {
                    stat.RemoveModifier(equip.itemData.itemName);
                }
            }
        }
    }
}
