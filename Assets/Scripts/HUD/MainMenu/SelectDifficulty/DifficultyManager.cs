using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour
{

    public AudioClip buttonClick;

    public void SetEasy()
    {
        AudioManager.Instance.PlaySFX(buttonClick);
        PlayerPrefs.SetInt("difficulty", (int)GameManager.Difficulty.Easy);
        SceneManager.LoadScene("SelectMap");
    }

    public void SetNormal()
    {
        AudioManager.Instance.PlaySFX(buttonClick);
        PlayerPrefs.SetInt("difficulty", (int)GameManager.Difficulty.Normal);
        SceneManager.LoadScene("SelectMap");
    }

    public void SetHard()
    {
        AudioManager.Instance.PlaySFX(buttonClick);
        PlayerPrefs.SetInt("difficulty", (int)GameManager.Difficulty.Hard);
        SceneManager.LoadScene("SelectMap");
    }

    public void GetBack()
    {
        AudioManager.Instance.PlaySFX(buttonClick);
        SceneManager.LoadScene("MainMenu");
    }

    public void ReturnGameOver()
    {
        AudioManager.Instance.PlaySFX(buttonClick);
        SceneManager.LoadScene("GameOver");
    }
}
