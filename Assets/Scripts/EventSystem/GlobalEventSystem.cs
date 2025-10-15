// 全局事件系统（单例模式）
using System.Collections.Generic;
using System;
using UnityEngine;

public class GlobalEventSystem : MonoBehaviour
{
    // 单例实例
    public static GlobalEventSystem Instance { get; private set; }

    // 存储所有事件的字典：键是事件名，值是事件回调列表
    private Dictionary<string, Action<object[]>> eventDictionary = new Dictionary<string, Action<object[]>>();

    void Awake()
    {
        // 确保全局只有一个实例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 切换场景时不销毁
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 注册事件监听
    public void Subscribe(string eventName, Action<object[]> callback)
    {
        // 如果事件不存在，创建新条目
        if (!eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = null;
        }
        // 添加回调
        eventDictionary[eventName] += callback;
    }

    // 取消事件监听
    public void Unsubscribe(string eventName, Action<object[]> callback)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            // 移除回调
            eventDictionary[eventName] -= callback;

            // 如果没有回调了，移除事件条目
            if (eventDictionary[eventName] == null)
            {
                eventDictionary.Remove(eventName);
            }
        }
    }

    // 触发全局事件（可传递多个参数）
    public void TriggerEvent(string eventName, params object[] parameters)
    {
        if (eventDictionary.TryGetValue(eventName, out var callback))
        {
            // 调用所有订阅了该事件的回调
            callback?.Invoke(parameters);
        }
        else
        {
            Debug.LogWarning($"没有找到事件: {eventName} 的订阅者");
        }
    }
}
