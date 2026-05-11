using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class RankingSystem : MonoBehaviour
{
    [System.Serializable]
    public class ScoreEntry
    {
        public string username;
        public int score;
    }

    [System.Serializable]
    class ScoreList
    {
        public List<ScoreEntry> scores = new List<ScoreEntry>();
    }

    public TMP_Text[] rankText;
    public TMP_Text[] nameText;
    public TMP_Text[] scoreText;

    private ScoreList data = new ScoreList();

    void Start()
    {
        LoadFromDatabase();
        UpdateUI();
    }

    private void Update()
    {
        pressToRestart();
        returnToMenu();
    }

    void LoadFromDatabase()
    {
        ProcessStartInfo psi = new ProcessStartInfo();
        psi.FileName = "java";
        psi.Arguments = "-cp \"D:\\pacmanJava;D:\\pacmanJava\\mysql-connector-j-8.3.0.jar\" readScores";
        psi.CreateNoWindow = true;
        psi.UseShellExecute = false;
        psi.RedirectStandardOutput = true;

        Process p = Process.Start(psi);

        data.scores.Clear();

        while (!p.StandardOutput.EndOfStream)
        {
            string line = p.StandardOutput.ReadLine();

            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(':');
            if (parts.Length != 2) continue;

            ScoreEntry entry = new ScoreEntry();
            entry.username = parts[0];
            entry.score = int.Parse(parts[1]);

            data.scores.Add(entry);
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
            UnityEngine.Debug.Log("Espacio");
            string nivel = PlayerPrefs.GetString("LastLevel", "Level1");
            SceneManager.LoadScene(nivel);
        }
    }

    public void returnToMenu()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            UnityEngine.Debug.Log("Escapar");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
