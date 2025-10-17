using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StatProperties : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] public TextMeshProUGUI propKeyText;
    [SerializeField] public TextMeshProUGUI propValueText;

    EntityStats playerStats = null;

    private void Start()
    {
        playerStats = PlayerManager.instance.player.stats;
    }

    private void OnEnable()
    {
        GlobalEventSystem.Instance.Subscribe("showPlayerStatPanel", onShowPlayerStatPanel);
    }

    private void onShowPlayerStatPanel(object[] parameters)
    {
        bool showUI = (bool)parameters[0]; // 解析参数1
        if(showUI)
        {
            UpdatePanel();
        }
    }

    private void UpdatePanel()
    {
        if (playerStats == null)
            playerStats = PlayerManager.instance.player.stats;

        if (playerStats == null)
            return;

        if (propKeyText != null)
        {
            propKeyText.text = "【 基础属性 】\nmaxHP:\n每秒恢复HP:\n力量:\n敏捷:\n智力:\n" +
                "【 攻击属性 】\n基础伤害:\n攻速:\n暴击倍率:\n暴击几率:\n破甲:\n火系攻击:\n冰系攻击:\n电系攻击:\n" +
                "【 防御属性 】\n护甲:\n闪避:\n火系抗性:\n冰系抗性:\n电系抗性:\n"
                ;
        }
        if (propValueText != null)
        {
            propValueText.text = $"\n{playerStats.resources.maxHP.GetValue()}\n"
                + $"{playerStats.resources.healthRegen.GetValue()}\n"
                + $"{playerStats.major.strength.GetValue()}\n"
                + $"{playerStats.major.agility.GetValue()}\n"
                + $"{playerStats.major.intelligence.GetValue()}\n"

                + $"\n{playerStats.offense.damage.GetValue()}\n"
                + $"{playerStats.offense.attackSpeed.GetValue()}\n"
                + $"{playerStats.offense.critPower.GetValue()}\n"
                + $"{playerStats.offense.critChance.GetValue()}\n"
                + $"{playerStats.offense.armorReduction.GetValue()}\n"
                + $"{playerStats.offense.fireDamage.GetValue()}\n"
                + $"{playerStats.offense.iceDamage.GetValue()}\n"
                + $"{playerStats.offense.lightningDamage.GetValue()}\n"

                + $"\n{playerStats.defense.armor.GetValue()}\n"
                + $"{playerStats.defense.evasion.GetValue()}\n"
                + $"{playerStats.defense.fireRes.GetValue()}\n"
                + $"{playerStats.defense.iceRes.GetValue()}\n"
                + $"{playerStats.defense.lightningRes.GetValue()}\n"
                ;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject menuObj = GameObject.FindWithTag("ItemPopupMenu");
        CanvasGroup canvasGroup = menuObj.GetComponent<CanvasGroup>();
        hideUI(canvasGroup);

        GameObject equipMenuObj = GameObject.FindWithTag("EquipPopupMenu");
        CanvasGroup canvasGroup2 = equipMenuObj.GetComponent<CanvasGroup>();
        hideUI(canvasGroup2);
    }

    public static void showUI(CanvasGroup canvasGroup)
    {
        // 显示UI
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1; // 完全不透明
            canvasGroup.interactable = true; // 可交互
            canvasGroup.blocksRaycasts = true; // 可以阻挡射线
        }
    }

    public static void hideUI(CanvasGroup canvasGroup)
    {
        // 隐藏UI
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0; // 完全透明
            canvasGroup.interactable = false; // 不可交互
            canvasGroup.blocksRaycasts = false; // 不阻挡射线
        }
    }
}
