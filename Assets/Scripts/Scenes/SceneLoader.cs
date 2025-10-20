using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    // 进度条UI（可选）
    [SerializeField] public Slider progressBar;
    [SerializeField] public TextMeshProUGUI progressText;

    [SerializeField] protected string sceneName;

    // 异步加载场景
    public void LoadSceneAsync(string sceneName)
    {
        // 启动协程执行异步加载
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        GameObject loadingUI = GameObject.FindWithTag("SceneLoadingUI");
        CanvasGroup canvasGroup = loadingUI.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;

        // 开始异步加载（第二个参数：是否保留当前场景，默认卸载）
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        asyncOp.allowSceneActivation = false; // 暂停场景激活，等待进度条完成

        // 监听加载进度（0.0~0.9表示加载中，1.0表示可激活）
        while (!asyncOp.isDone)
        {
            // 进度值映射到0~1（因为asyncOp.progress最大为0.9）
            float progress = Mathf.Clamp01(asyncOp.progress / 0.9f);

            // 更新UI进度条
            if (progressBar != null)
                progressBar.value = progress;
            if (progressText != null)
                progressText.text = $"{(int)(progress * 100)}%";

            // 当进度达到100%时，激活场景
            if (progress >= 1.0f)
            {
                asyncOp.allowSceneActivation = true; // 允许场景激活
            }

            // yield return null; // 等待下一帧
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