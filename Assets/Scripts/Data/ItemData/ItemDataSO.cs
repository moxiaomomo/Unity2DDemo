using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Material item", fileName = "Material data -")]
public class ItemDataSO : ScriptableObject
{
    public string saveId;

    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    // ��Ʒ���пɴ洢��item���������
    public int maxStackSize;
}

