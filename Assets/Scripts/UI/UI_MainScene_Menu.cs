using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_MainScene_Menu : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainMenu";
    [SerializeField] private GameObject Character;
    [SerializeField] public GameObject YouDie;
    private bool isPaused = false;
    // Start is called before the first frame update

    private void Start()
    {
        SessionFlags.isLoadingFromSave = true; //调试用，正式版删掉

        SaveManager.instance.LoadGame();
        SessionFlags.isLoadingFromSave = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }


    public void ReturnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void SaveGameBtn()
    {
        SaveManager.instance.SaveGame();
        TogglePause();
        Debug.Log("Game Saved");
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // 暂停游戏
            Character.SetActive(true); // 显示菜单
        }
        else
        {
            Time.timeScale = 1f; // 恢复游戏
            Character.SetActive(false); // 隐藏菜单
        }
    }


    public void RestartGameButton()
    {
        GameManager.instance.RestartScene();
    }
}
