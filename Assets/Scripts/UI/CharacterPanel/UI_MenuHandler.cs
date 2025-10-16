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
    public void OnButton1Click()
    {
        Debug.Log($"按钮1被点击了, equippables {equippables != null} {equippables.Length}");
        if (equippables != null)
        {
            for (int i = 0; i < equippables.Length; i++)
            {
                equippables[i].EquipWeaponFromItemSlot();
            }
        }
    }

    public void OnButton2Click()
    {
        Debug.Log("按钮2被点击了！");
    }

    // 带参数的方法（参数类型需匹配）
    public void OnButtonClickWithParam(string message)
    {
        Debug.Log("收到消息：" + message);
    }
}
