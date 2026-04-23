using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour
{
    public void SetEasy()
    {
        GameManager.instance.SetEasy();
        SceneManager.LoadScene("Level1");
    }

    public void SetNormal()
    {
        GameManager.instance.SetNormal();
        SceneManager.LoadScene("Level1");
    }

    public void SetHard()
    {
        GameManager.instance.SetHard();
        SceneManager.LoadScene("Level1");
    }
}
