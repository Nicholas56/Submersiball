using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public static int currentScene;

    public void GoToLevel(int Level)
    {
        Time.timeScale = 1;
        currentScene = Level;
        SceneManager.LoadScene(Level);
    }

    public void ReturnToLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene);
    }

    public void AddScene(int level)
    {
        SceneManager.LoadScene(level, LoadSceneMode.Additive);
    }
}