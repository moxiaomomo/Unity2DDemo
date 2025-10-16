using UnityEngine;

public class AboutPanel : MonoBehaviour
{
    public GameObject popupPanel; // 弹窗的引用

    public void Start()
    {
        if (popupPanel == null)
        {
            Debug.LogError("Popup panel is not assigned.");
        }
    }

    public void OnMouseDown()
    {
        // 当物体被点击时，显示弹窗
        popupPanel.SetActive(!popupPanel.activeSelf);
    }
}