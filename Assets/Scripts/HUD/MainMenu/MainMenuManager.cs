using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuManager : MonoBehaviour
{

    public AudioClip buttonClick;

    public void PlayGame()
    {
        AudioManager.Instance.PlaySFX(buttonClick);
        SceneManager.LoadScene("DifficultyTypes");
    }

    public async void QuitGame()
    {
        AudioManager.Instance.PlaySFX(buttonClick);
        await GameManager.instance.GuardarSesion();
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif

    }
    public void CreditLevel()
    {
        AudioManager.Instance.PlaySFX(buttonClick);
        SceneManager.LoadScene("Credits");
    }

    public void OptionLevel()
    {
        AudioManager.Instance.PlaySFX(buttonClick);
        SceneManager.LoadScene("Options");
    }
    
}
