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
        if(SaveManager.instance.HasNoSavedData() == false)
        {
            continueBtn.SetActive(false);
        }
    }
    public void ContinueGame()
    {
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
