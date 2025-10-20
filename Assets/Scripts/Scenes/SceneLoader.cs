using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    // ������UI����ѡ��
    [SerializeField] public Slider progressBar;
    [SerializeField] public TextMeshProUGUI progressText;

    [SerializeField] protected string sceneName;

    // �첽���س���
    public void LoadSceneAsync(string sceneName)
    {
        // ����Э��ִ���첽����
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        GameObject loadingUI = GameObject.FindWithTag("SceneLoadingUI");
        CanvasGroup canvasGroup = loadingUI.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;

        // ��ʼ�첽���أ��ڶ����������Ƿ�����ǰ������Ĭ��ж�أ�
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        asyncOp.allowSceneActivation = false; // ��ͣ��������ȴ����������

        // �������ؽ��ȣ�0.0~0.9��ʾ�����У�1.0��ʾ�ɼ��
        while (!asyncOp.isDone)
        {
            // ����ֵӳ�䵽0~1����ΪasyncOp.progress���Ϊ0.9��
            float progress = Mathf.Clamp01(asyncOp.progress / 0.9f);

            // ����UI������
            if (progressBar != null)
                progressBar.value = progress;
            if (progressText != null)
                progressText.text = $"{(int)(progress * 100)}%";

            // �����ȴﵽ100%ʱ�������
            if (progress >= 1.0f)
            {
                asyncOp.allowSceneActivation = true; // ����������
            }

            // yield return null; // �ȴ���һ֡
            yield return new WaitForSeconds(0.5f);
        }
        canvasGroup.alpha = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(string.IsNullOrEmpty(sceneName))
        {
            return;
        }
        IDamageable targetDamageable = collision.GetComponent<IDamageable>();
        if (targetDamageable != null && targetDamageable.Tag()=="Player")
        {
            LoadSceneAsync(sceneName);
        }
    }
}