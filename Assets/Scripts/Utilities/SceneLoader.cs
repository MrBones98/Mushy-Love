using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public static SceneLoader instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public int GetSceneBuildIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    public void RelodScene()
    {
        SceneManager.LoadScene(GetSceneBuildIndex());
    }
    public void QuitApplication()
    {
        Application.Quit();
    }
    public void GoToMainMenu()
    {
        LoadScene(0);
    }
}
