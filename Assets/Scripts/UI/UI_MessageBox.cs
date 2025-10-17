using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UI_MessageBox : MonoBehaviour
{
    // ����ģʽ��ȷ��ȫ��Ψһ
    public static UI_MessageBox Instance;

    // ��ʾ��Ԥ����
    public GameObject messageBoxPrefab;
    // Canvas���ã�������ʾ��ʾ��
    public Canvas canvas;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // ��ʾ����ȷ�ϰ�ť����ʾ��
    public void ShowMessage(string message, Action onConfirm = null)
    {
        // ʵ������ʾ��
        GameObject msgBox = Instantiate(messageBoxPrefab, canvas.transform);

        // ������Ϣ�ı�
        TextMeshProUGUI messageText = msgBox.GetComponentInChildren<TextMeshProUGUI>();
        if (messageText != null)
            messageText.text = message;

        //// ��ȷ�ϰ�ť�¼�
        //Button confirmBtn = msgBox.transform.Find("ConfirmButton")?.GetComponent<Button>();
        //if (confirmBtn != null)
        //{
        //    confirmBtn.onClick.AddListener(() =>
        //    {
        //        Destroy(msgBox); // �ر���ʾ��
        //        onConfirm?.Invoke(); // ִ��ȷ�Ϻ�Ļص�
        //    });
        //}
        Destroy(msgBox, 2);
    }

    //// ��ʾ��ȷ�Ϻ�ȡ����ť����ʾ��
    //public void ShowConfirmBox(string message, Action onConfirm, Action onCancel)
    //{
    //    GameObject msgBox = Instantiate(messageBoxPrefab, canvas.transform);

    //    Text messageText = msgBox.GetComponentInChildren<Text>();
    //    if (messageText != null)
    //        messageText.text = message;

    //    // ȷ�ϰ�ť
    //    Button confirmBtn = msgBox.transform.Find("ConfirmButton")?.GetComponent<Button>();
    //    if (confirmBtn != null)
    //    {
    //        confirmBtn.onClick.AddListener(() =>
    //        {
    //            Destroy(msgBox);
    //            onConfirm?.Invoke();
    //        });
    //    }

    //    // ȡ����ť
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