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
        bool showUI = (bool)parameters[0]; // ��������1
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
            propKeyText.text = "�� �������� ��\nmaxHP:\nÿ��ָ�HP:\n����:\n����:\n����:\n" +
                "�� �������� ��\n�����˺�:\n����:\n��������:\n��������:\n�Ƽ�:\n��ϵ����:\n��ϵ����:\n��ϵ����:\n" +
                "�� �������� ��\n����:\n����:\n��ϵ����:\n��ϵ����:\n��ϵ����:\n"
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
        // ��ʾUI
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1; // ��ȫ��͸��
            canvasGroup.interactable = true; // �ɽ���
            canvasGroup.blocksRaycasts = true; // �����赲����
        }
    }

    public static void hideUI(CanvasGroup canvasGroup)
    {
        // ����UI
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0; // ��ȫ͸��
            canvasGroup.interactable = false; // ���ɽ���
            canvasGroup.blocksRaycasts = false; // ���赲����
        }
    }
}
