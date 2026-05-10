using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAudio : MonoBehaviour
{
    public AudioClip menuMusic;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(menuMusic);
    }
}
