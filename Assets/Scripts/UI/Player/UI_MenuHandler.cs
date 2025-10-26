using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_MenuHandler : MonoBehaviour
{
    private IEquippable[] equippables;

    private void Awake()
    {
        equippables = FindObjectsOfType<MonoBehaviour>().OfType<IEquippable>().ToArray();
    }

    // 要绑定的方法（必须是public）
    public void OnEuipBtnClick()
    {
        hideMenu();
        if (equippables != null)
        {
            for (int i = 0; i < equippables.Length; i++)
            {
                equippables[i].EquipWeaponFromItemSlot();
            }
        }
    }

    public void OnSaleBtnClick()
    {
        hideMenu();
    }

    public void OnUnloadEquipBtnClick()
    {
        hideMenu();
        if (equippables != null)
        {
            for (int i = 0; i < equippables.Length; i++)
            {
                equippables[i].UnloadEquipWeapon();
            }
        }
    }

    // 带参数的方法（参数类型需匹配）
    public void OnButtonClickWithParam(string message)
    {
        Debug.Log("收到消息：" + message);
    }

    private void hideMenu()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            UI_StatProperties.hideUI(canvasGroup);
        }
    }
}
