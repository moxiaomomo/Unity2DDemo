using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Ui_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainScene";
    [SerializeField] private GameObject continueBtn;
    [SerializeField] UIFadeScreen fadeScreen;

    // Start is called before the first frame update

    private void Start()
    {
        StartCoroutine(CheckSaveDataAfterFrame());
    }

    private IEnumerator CheckSaveDataAfterFrame()
    {
        yield return null; // 等待一帧，确保 SaveManager 初始化完成
        if (!SaveManager.instance.HasSavedData())
        {
            continueBtn.SetActive(false);
        }
    }

    public void ContinueGame()
    {
        SessionFlags.isLoadingFromSave = true;
        StartCoroutine(LoadScreenWithFadeEffect(1.5f));
    }

    public void NewGame()
    {
        SaveManager.instance.DeleteSaveData();
        StartCoroutine(LoadScreenWithFadeEffect(1.5f));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadScreenWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(sceneName);
    }
}
