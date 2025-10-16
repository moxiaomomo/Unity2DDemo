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

    // Ҫ�󶨵ķ�����������public��
    public void OnButton1Click()
    {
        Debug.Log($"��ť1�������, equippables {equippables != null} {equippables.Length}");
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
        Debug.Log("��ť2������ˣ�");
    }

    // �������ķ���������������ƥ�䣩
    public void OnButtonClickWithParam(string message)
    {
        Debug.Log("�յ���Ϣ��" + message);
    }
}
