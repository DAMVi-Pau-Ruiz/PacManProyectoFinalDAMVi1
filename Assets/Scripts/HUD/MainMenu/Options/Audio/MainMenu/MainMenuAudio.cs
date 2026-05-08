using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    public AudioClip menuMusic;


    private void Start()
    {
        AudioManager.Instance.PlayMusic(menuMusic);
    }
}