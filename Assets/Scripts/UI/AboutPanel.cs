//using UnityEngine;
//using UnityEngine.UI;

//public class AboutPanel : MonoBehaviour
//{
//    public GameObject infoWindow; // ��Ϣ���ڵ�UI���
//    public Text modelNameText; // ��ʾģ�����Ƶ��ı�

//    private void Update()
//    {
//        // ���������¼�
//        if (Input.GetMouseButtonDown(0))
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hit;

//            if (Physics.Raycast(ray, out hit))
//            {
//                // �ж��Ƿ�������ģ��
//                if (hit.collider.gameObject == gameObject)
//                {
//                    // �������ģ�ͣ���ʾ��Ϣ���ڲ�����ģ������
//                    infoWindow.SetActive(true);
//                    modelNameText.text = gameObject.name;
//                }
//                else
//                {
//                    // ������������ط���������Ϣ����
//                    infoWindow.SetActive(false);
//                }
//            }
//        }
//    }
//}

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
        Debug.Log("HAHAHAHAHAH");
        popupPanel.SetActive(!popupPanel.activeSelf);
    }
}