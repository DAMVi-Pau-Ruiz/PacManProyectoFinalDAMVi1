using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("DifficultyTypes");
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif

    }
    public void CreditLevel()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OptionLevel()
    {
        SceneManager.LoadScene("Options");
    }

}
