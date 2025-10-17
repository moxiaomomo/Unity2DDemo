using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UI_MessageBox : MonoBehaviour
{
    // 单例模式，确保全局唯一
    public static UI_MessageBox Instance;

    // 提示框预制体
    public GameObject messageBoxPrefab;
    // Canvas引用（用于显示提示框）
    public Canvas canvas;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // 显示仅含确认按钮的提示框
    public void ShowMessage(string message, Action onConfirm = null)
    {
        // 实例化提示框
        GameObject msgBox = Instantiate(messageBoxPrefab, canvas.transform);

        // 设置消息文本
        TextMeshProUGUI messageText = msgBox.GetComponentInChildren<TextMeshProUGUI>();
        if (messageText != null)
            messageText.text = message;

        //// 绑定确认按钮事件
        //Button confirmBtn = msgBox.transform.Find("ConfirmButton")?.GetComponent<Button>();
        //if (confirmBtn != null)
        //{
        //    confirmBtn.onClick.AddListener(() =>
        //    {
        //        Destroy(msgBox); // 关闭提示框
        //        onConfirm?.Invoke(); // 执行确认后的回调
        //    });
        //}
        Destroy(msgBox, 2);
    }

    //// 显示含确认和取消按钮的提示框
    //public void ShowConfirmBox(string message, Action onConfirm, Action onCancel)
    //{
    //    GameObject msgBox = Instantiate(messageBoxPrefab, canvas.transform);

    //    Text messageText = msgBox.GetComponentInChildren<Text>();
    //    if (messageText != null)
    //        messageText.text = message;

    //    // 确认按钮
    //    Button confirmBtn = msgBox.transform.Find("ConfirmButton")?.GetComponent<Button>();
    //    if (confirmBtn != null)
    //    {
    //        confirmBtn.onClick.AddListener(() =>
    //        {
    //            Destroy(msgBox);
    //            onConfirm?.Invoke();
    //        });
    //    }

    //    // 取消按钮
    //    Button cancelBtn = msgBox.transform.Find("CancelButton")?.GetComponent<Button>();
    //    if (cancelBtn != null)
    //    {
    //        cancelBtn.onClick.AddListener(() =>
    //        {
    //            Destroy(msgBox);
    //            onCancel?.Invoke();
    //        });
    //    }
    //}
}