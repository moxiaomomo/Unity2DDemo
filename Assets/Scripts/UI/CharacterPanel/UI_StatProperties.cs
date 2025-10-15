using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatProperties : MonoBehaviour
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
}
