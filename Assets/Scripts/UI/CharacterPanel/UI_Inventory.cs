using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Inventory : MonoBehaviour
{
    // ����еĿ����Ʒ
    public InventoryPlayer inventory {  get; private set; }
    // ����е���Ʒ��
    private UI_ItemSlot[] uiItemSlots;
    private UI_EquipSlot[] uiEquipSlots;

    private void Awake()
    {
        uiItemSlots = GetComponentsInChildren<UI_ItemSlot>();
        uiEquipSlots = GetComponentsInChildren<UI_EquipSlot>();
        inventory = FindFirstObjectByType<InventoryPlayer>();
        inventory.OnInventoryChange += updateInventorySlots;
        inventory.OnEquipmentsChange += updateEquipmentSlots;
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

    private void updateEquipmentSlots()
    {
        updateInventorySlots();
        Debug.Log(inventory.itemList.Count);

        List<InventoryEquipment> equipList = inventory.equipList;
        for (int i = 0; i < uiEquipSlots.Length; i++)
        {
            if (i < equipList.Count)
            {
                uiEquipSlots[i].updateSlot(equipList[i]);
            }
            else
            {
                uiEquipSlots[i].updateSlot(null);
            }
        }
    }
}
