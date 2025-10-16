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
        // 判断是否为鼠标右键点击
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("检测到鼠标右击");

            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("选项 1"), false, () => Debug.Log("选项 1 被点击"));
            menu.AddItem(new GUIContent("选项 2"), false, () => Debug.Log("选项 2 被点击"));
            menu.ShowAsContext();
        }
    }
}
