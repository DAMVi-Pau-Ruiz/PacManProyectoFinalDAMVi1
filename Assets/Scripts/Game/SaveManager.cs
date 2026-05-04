using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public float sessionDuration = 0f;
    public List<LevelData> levels = new List<LevelData>();
    public int totalPellets = 0;
    public int totalGhosts = 0;
    public int levelsCompleted = 0;
    public Dictionary<string, int> levelPlayCount = new Dictionary<string, int>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        sessionDuration += Time.deltaTime;
    }

    public void RegisterLevelEnter(string levelName)
    {
        if (!levelPlayCount.ContainsKey(levelName))
            levelPlayCount[levelName] = 0;

        levelPlayCount[levelName]++;
    }

    public void RegisterLevelComplete()
    {
        levelsCompleted++;
    }

    public void RegisterPellet()
    {
        totalPellets++;
    }

    public void RegisterGhost()
    {
        totalGhosts++;
    }

    public void AddLevelData(LevelData data)
    {
        levels.Add(data);
    }
}
