using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RankingSystem : MonoBehaviour
{
    [SerializeField]
    public class ScoreEntry
    {
        public string username;
        public int score;
    }

    [SerializeField]
    class ScoreList
    {
        public List<ScoreEntry> scores = new List<ScoreEntry>();
    }

    public TMP_Text[] rankText;
    public TMP_Text[] nameText;
    public TMP_Text[] scoreText;

    private ScoreList data = new ScoreList();
    private string saveKey = "HIGHSCORES";

    // Start is called before the first frame update
    void Start()
    {

        Load();
        UpdateUI();
    }

    void Update()
    {
        pressToRestart();
        returnToMenu();
    }
    public void AddScore(string username, int newScore)
    {
        ScoreEntry existing = null;

        for (int i = 0; i < data.scores.Count; i++)
        {
            if (data.scores[i].username == username)
            {
                existing = data.scores[i];
                break;
            }
        }

        if (existing != null)
        {
            if (newScore > existing.score)
            {
                existing.score = newScore;
            }
        }
        else
        {
            ScoreEntry entry = new ScoreEntry();
            entry.username = username;
            entry.score = newScore;

            data.scores.Add(entry);
        }

        Sort();
        Save();
        UpdateUI();

    }

    void Sort()
    {
        data.scores.Sort((a, b) => b.score.CompareTo(a.score));
    }

    void Save()
    {
        string save = "";

        for (int i = 0; i < data.scores.Count; i++)
        {
            save += data.scores[i].username + "|" + data.scores[i].score;

            if (i < data.scores.Count - 1)
                save += ";";
        }

        PlayerPrefs.SetString(saveKey, save);
        PlayerPrefs.Save();
    }

    void Load()
    {
        if (!PlayerPrefs.HasKey(saveKey)) return;

        string save = PlayerPrefs.GetString(saveKey);
        string[] entries = save.Split(';');

        data.scores.Clear();

        foreach (string e in entries)
        {
            string[] parts = e.Split('|');
            
            if (parts.Length == 2)
            {
                ScoreEntry entry = new ScoreEntry();
                entry.username = parts[0];
                entry.score = int.Parse(parts[1]);

                data.scores.Add(entry);
            }
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < rankText.Length; i++)
        {
            int rank = i + 1;

            string suffix = "TH";

            if (rank == 1) suffix = "ST";
            else if (rank == 2) suffix = "ND";
            else if (rank == 3) suffix = "RD";

            if (i < data.scores.Count)
            {
                rankText[i].text = rank + suffix;
                nameText[i].text = data.scores[i].username;
                scoreText[i].text = data.scores[i].score.ToString();
            }
            else
            {
                rankText[i].text = rank + suffix;
                nameText[i].text = "---";
                scoreText[i].text = "0";
            }
        }
    }

    public void pressToRestart()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            string nivel = PlayerPrefs.GetString("LastLevel", "Level1");
            SceneManager.LoadScene(nivel);
        }
    }

    public void returnToMenu()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
