using UnityEngine;

public class AboutPanel : MonoBehaviour
{
    public GameObject popupPanel; // ����������

    public void Start()
    {
        if (popupPanel == null)
        {
            Debug.LogError("Popup panel is not assigned.");
        }
    }

    public void OnMouseDown()
    {
        // �����屻���ʱ����ʾ����
        popupPanel.SetActive(!popupPanel.activeSelf);
    }
}