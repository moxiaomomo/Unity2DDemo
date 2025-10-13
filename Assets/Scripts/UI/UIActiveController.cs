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
        // 显示UI
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1; // 完全不透明
            canvasGroup.interactable = true; // 可交互
            canvasGroup.blocksRaycasts = true; // 可以阻挡射线
        }
    }

    private void hideUI(CanvasGroup canvasGroup)
    {
        // 隐藏UI
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0; // 完全透明
            canvasGroup.interactable = false; // 不可交互
            canvasGroup.blocksRaycasts = false; // 不阻挡射线
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