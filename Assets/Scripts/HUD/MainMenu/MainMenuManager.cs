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

        if (GameManager.instance != null)
            await GameManager.instance.GuardarSesion();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
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
