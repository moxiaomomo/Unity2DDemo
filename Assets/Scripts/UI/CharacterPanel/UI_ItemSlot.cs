using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : ItemSlotBase
{
    [SerializeField] protected TextMeshProUGUI itemStackSize;

    public override void updateSlot(InventoryItem item)
    {
        itemInSlot = item;
        if (itemInSlot == null )
        {
            itemStackSize.text = "";
            itemIcon.sprite = null;
            itemIcon.color = Color.clear;
            return;
        }

        Color color = Color.white;
        color.a = .9f;
        itemIcon.color = color;
        itemIcon.sprite = itemInSlot.itemData.itemIcon;
        itemStackSize.text = itemInSlot.stackSize+"";
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        // �ж��Ƿ�Ϊ����Ҽ����
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("��⵽����һ�");

            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("ѡ�� 1"), false, () => Debug.Log("ѡ�� 1 �����"));
            menu.AddItem(new GUIContent("ѡ�� 2"), false, () => Debug.Log("ѡ�� 2 �����"));
            menu.ShowAsContext();
        }
    }
}
