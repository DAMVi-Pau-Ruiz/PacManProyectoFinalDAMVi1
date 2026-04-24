using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour
{
    public void SetEasy()
    {
        PlayerPrefs.SetInt("difficulty", (int)GameManager.Difficulty.Easy);
        SceneManager.LoadScene("Level1");
    }

    public void SetNormal()
    {
        PlayerPrefs.SetInt("difficulty", (int)GameManager.Difficulty.Normal);
        SceneManager.LoadScene("Level1");
    }

    public void SetHard()
    {
        PlayerPrefs.SetInt("difficulty", (int)GameManager.Difficulty.Hard);
        SceneManager.LoadScene("Level1");
    }
}
