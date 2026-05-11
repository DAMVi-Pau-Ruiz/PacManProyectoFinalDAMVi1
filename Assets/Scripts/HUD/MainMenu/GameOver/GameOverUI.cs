using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI valueHighscore;
    [SerializeField] RankingSystem highscoreSystem; 

    void Start()
    {
        int score = GameManager.instance.getScoreActual();
        valueHighscore.text = score.ToString();

        // guardar en ranking
        string username = PlayerPrefs.GetString("username", "AAA");
    }

    public void goScoreTable()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("HighscoreTable");
    }

    public void goUnblockedFruits()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("UnblockedFruits");
    }
}