using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Material item", fileName = "Material data -")]
public class ItemDataSO : ScriptableObject
{
    public string saveId;

    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    // 物品格中可存储该item的最大数量
    public int maxStackSize;
}

