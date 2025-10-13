using UnityEngine;

public class ObjectActiveController : MonoBehaviour
{
    [System.Serializable]
    public class ActiveUIKeys
    {
        [SerializeField] public GameObject targetObject;
        [SerializeField] public KeyCode keyCode;
        [SerializeField] public bool showAtFirst = false;
    }

    [SerializeField] private ActiveUIKeys[] activeUIKeys;

    private void Awake()
    {
        foreach (ActiveUIKeys activeKey in activeUIKeys)
        {
            if (activeKey.targetObject == null)
            {
                continue;
            }
            CanvasGroup canvasGroup = activeKey.targetObject.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                if (activeKey.showAtFirst)
                {
                    showUI(canvasGroup);
                }
                else
                {
                    hideUI(canvasGroup);
                }
            }
        }
    }

    private void showUI(CanvasGroup canvasGroup)
    {
        // ��ʾUI
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1; // ��ȫ��͸��
            canvasGroup.interactable = true; // �ɽ���
            canvasGroup.blocksRaycasts = true; // �����赲����
        }
    }

    private void hideUI(CanvasGroup canvasGroup)
    {
        // ����UI
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0; // ��ȫ͸��
            canvasGroup.interactable = false; // ���ɽ���
            canvasGroup.blocksRaycasts = false; // ���赲����
        }
    }

    private void toggleShowUI(CanvasGroup canvasGroup)
    {
        if (canvasGroup == null)
            return;

        if (canvasGroup.alpha!=0)
        {
            hideUI(canvasGroup);
        }
        else
        {
            showUI(canvasGroup);
        }
    }

    private void Update()
    {
        foreach (ActiveUIKeys activeKey in activeUIKeys)
        {
            if (activeKey.targetObject == null)
            {
                continue;
            }
            
            if (Input.GetKeyDown(activeKey.keyCode))
            {
                CanvasGroup canvasGroup = activeKey.targetObject.GetComponent<CanvasGroup>();
                toggleShowUI(canvasGroup);
                break;
            }
        }
    }
}