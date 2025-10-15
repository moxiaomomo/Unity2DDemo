// ȫ���¼�ϵͳ������ģʽ��
using System.Collections.Generic;
using System;
using UnityEngine;

public class GlobalEventSystem : MonoBehaviour
{
    // ����ʵ��
    public static GlobalEventSystem Instance { get; private set; }

    // �洢�����¼����ֵ䣺�����¼�����ֵ���¼��ص��б�
    private Dictionary<string, Action<object[]>> eventDictionary = new Dictionary<string, Action<object[]>>();

    void Awake()
    {
        // ȷ��ȫ��ֻ��һ��ʵ��
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �л�����ʱ������
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ע���¼�����
    public void Subscribe(string eventName, Action<object[]> callback)
    {
        // ����¼������ڣ���������Ŀ
        if (!eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = null;
        }
        // ��ӻص�
        eventDictionary[eventName] += callback;
    }

    // ȡ���¼�����
    public void Unsubscribe(string eventName, Action<object[]> callback)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            // �Ƴ��ص�
            eventDictionary[eventName] -= callback;

            // ���û�лص��ˣ��Ƴ��¼���Ŀ
            if (eventDictionary[eventName] == null)
            {
                eventDictionary.Remove(eventName);
            }
        }
    }

    // ����ȫ���¼����ɴ��ݶ��������
    public void TriggerEvent(string eventName, params object[] parameters)
    {
        if (eventDictionary.TryGetValue(eventName, out var callback))
        {
            // �������ж����˸��¼��Ļص�
            callback?.Invoke(parameters);
        }
        else
        {
            Debug.LogWarning($"û���ҵ��¼�: {eventName} �Ķ�����");
        }
    }
}
