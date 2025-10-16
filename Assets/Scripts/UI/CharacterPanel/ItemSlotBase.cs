
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotBase : MonoBehaviour, IPointerDownHandler
{
    public InventoryItem itemInSlot { get; protected set; }

    [Header("UI Slot Setup")]
    [SerializeField] protected Image itemIcon;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
    }

    public virtual void updateSlot(InventoryItem item)
    {

    }
}
