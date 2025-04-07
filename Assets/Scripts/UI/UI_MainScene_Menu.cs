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
            Time.timeScale = 0f; // ‘›Õ£”Œœ∑
            Character.SetActive(true); // œ‘ æ≤Àµ•
        }
        else
        {
            Time.timeScale = 1f; // ª÷∏¥”Œœ∑
            Character.SetActive(false); // “˛≤ÿ≤Àµ•
        }
    }


    public void RestartGameButton()
    {
        GameManager.instance.RestartScene();
     }
}
