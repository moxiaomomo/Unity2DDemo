using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : ItemSlotBase
{
    [SerializeField] public TextMeshProUGUI itemStackSize;
    [SerializeField] public GameObject menuObj;

    private void Awake()
    {
        menuObj = GameObject.FindWithTag("ItemPopupMenu");
    }

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
            GlobalEventSystem.Instance.TriggerEvent(
                "itemSlotSelected",
                "ITEM",
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
        // �ر��Ѵ��ڵĲ˵�
        CloseMenu();
        // ʵ�����²˵�
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

    // �رղ˵�
    public void CloseMenu()
    {
        CanvasGroup canvasGroup = menuObj?.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            UI_StatProperties.hideUI(canvasGroup);
        }
    }
}
