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
            propKeyText.text = "最大生命值:\n\n力量:\n\n敏捷:\n\n智力:\n\n";
        }
        if (propValueText != null)
        {
            propValueText.text = $"{playerStats.resources.maxHP.GetValue()}\n\n" +
                $"{playerStats.major.strength.GetValue()}\n\n" +
                $"{playerStats.major.agility.GetValue()}\n\n" +
                $"{playerStats.major.intelligence.GetValue()}\n\n";
        }
    }
}
