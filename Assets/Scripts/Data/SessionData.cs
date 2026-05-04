using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public string name;
    public float time;
    public List<string> fruits = new List<string>();
    public int pellets;
    public int ghosts;
    public int restarts;
}

[System.Serializable]
public class SessionData
{
    public string player;
    public float duration;
    public List<LevelData> levels = new List<LevelData>();
    public int totalPellets;
    public int totalGhosts;
    public int levelsCompleted;
    public Dictionary<string, int> levelPlayCount = new Dictionary<string, int>();
}
