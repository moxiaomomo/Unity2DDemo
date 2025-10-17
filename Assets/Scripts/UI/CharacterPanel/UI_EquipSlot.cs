using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_EquipSlot : ItemSlotBase
{
    [SerializeField] public GameObject menuObj;

    private void Awake()
    {
        menuObj = GameObject.FindWithTag("EquipPopupMenu");
    }

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

    public override void OnPointerDown(PointerEventData eventData)
    {
        // 判断是否为鼠标右键点击
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            GlobalEventSystem.Instance.TriggerEvent(
                "itemSlotSelected",
                "EQUIP",
                itemInSlot
            );
            ShowCustomMenu();
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            CloseMenu();
        }
    }

    private void ShowCustomMenu()
    {
        // 关闭已存在的菜单
        CloseMenu();
        // 实例化新菜单
        if (menuObj != null)
        {
            CanvasGroup canvasGroup = menuObj.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                Vector2 mousePos = Input.mousePosition;
                menuObj.transform.position = mousePos;
                UI_StatProperties.showUI(canvasGroup);
            }
        }
    }

    // 关闭菜单
    public void CloseMenu()
    {
        CanvasGroup canvasGroup = menuObj?.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            UI_StatProperties.hideUI(canvasGroup);
        }
    }
}
