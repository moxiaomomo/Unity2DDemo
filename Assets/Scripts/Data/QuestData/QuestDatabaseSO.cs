using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Quest Data/New Quest DB", fileName = "Quest DB -")]
public class QuestDatabaseSO : ScriptableObject
{
    public QuestDataSO[] allQuests;

    public QuestDataSO GetQuestById(string id)
    {
        return allQuests.FirstOrDefault(q => q!=null && q.questSaveId == id);
    }

#if UNITY_EDITOR
    [ContextMenu("Auto-fill wirh all QuestDataSO")]
    public void CollectItemsData()
    {
        string[] guids = AssetDatabase.FindAssets("t:QuestDataSO");
        allQuests = guids
            .Select(guid => AssetDatabase.LoadAssetAtPath<QuestDataSO>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(quest => quest != null)
            .ToArray();

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
#endif
}
