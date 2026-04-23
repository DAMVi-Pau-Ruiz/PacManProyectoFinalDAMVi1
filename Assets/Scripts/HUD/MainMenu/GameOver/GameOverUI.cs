using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI valueHighscore;

    // Start is called before the first frame update
    void Start()
    {
        valueHighscore.text = GameManager.instance.getScoreActual().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        pressToRestart();
    }

    public void pressToRestart()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
