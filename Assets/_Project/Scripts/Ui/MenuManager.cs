using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    private void Initialization()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    void Start()
    {
        Initialization();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
                Application.Quit();
#endif
    }
}
