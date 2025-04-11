using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static  GameManager instance;


    public void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void RestartScene()
    {
        // Restart the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }


}
