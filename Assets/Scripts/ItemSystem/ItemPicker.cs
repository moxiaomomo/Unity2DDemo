using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private ItemDataSO itemData;

    private InventoryItem itemToAdd;
    private InventoryBase inventory;

    private void Awake()
    {
        if (itemData.itemType==ItemType.Weapon)
        {
            itemToAdd = new InventoryEquipment(itemData);
        }
        else
        {
            itemToAdd = new InventoryItem(itemData);
        }
    }

    private void OnValidate()
    {
        if (itemData == null)
        {
            return;
        }

        sr = GetComponent<SpriteRenderer>();
        sr.sprite = itemData.itemIcon;
        gameObject.name = "Pickup - " + itemData.itemName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inventory = collision.GetComponent<InventoryBase>();
        if (inventory != null && inventory.CanAddItem())
        {
            inventory.AddItem(itemToAdd);
            Destroy(gameObject);
        }
    }
}
