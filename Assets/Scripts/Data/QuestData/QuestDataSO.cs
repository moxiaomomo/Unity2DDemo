using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Quest Data/New Quest", fileName = "Quest -")]
public class QuestDataSO : ScriptableObject
{
    public string questSaveId;
    [Space]
    public string questName;
    [TextArea] public string description;
    [TextArea] public string questGoal;

    public string questTargetId; // Enemy name. NPC name, ItemName
    public int requiredAmount;

    [Header("Reward")]
    public RewardType rewardType;
    public InventoryItem[] rewardItems;

    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        questSaveId = AssetDatabase.AssetPathToGUID(path);
#endif
    }
}
