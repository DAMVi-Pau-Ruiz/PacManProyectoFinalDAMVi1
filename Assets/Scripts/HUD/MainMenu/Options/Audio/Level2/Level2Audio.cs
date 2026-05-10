using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Audio : MonoBehaviour
{
    public AudioClip menuMusic;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(menuMusic);
    }
}
