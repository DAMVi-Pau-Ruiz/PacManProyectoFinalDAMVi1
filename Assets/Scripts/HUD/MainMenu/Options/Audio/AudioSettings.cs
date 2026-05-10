using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private int defaultVolume = 50;
    [SerializeField] private AudioMixer mixer;

    public static AudioSettings instance;

    private int gVolume, mVolume, eVolume;


    private void Start()
    {
        ApplyAllVolumes();
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        gVolume = defaultVolume;
        mVolume = defaultVolume;
        eVolume = defaultVolume;

        ApplyAllVolumes();
    }

    public void setGVolume(int volume)
    {
        gVolume = volume;
        SetMixerVolume("Master", volume);
    }

    public void setMVolume(int volume)
    {
        mVolume = volume;
        SetMixerVolume("Music", volume);
    }

    public void setEVolume(int volume)
    {
        eVolume = volume;
        SetMixerVolume("Effects", volume);
    }

    public int getGVolume() => gVolume;
    public int getMVolume() => mVolume;
    public int getEVolume() => eVolume;

    private void ApplyAllVolumes()
    {
        SetMixerVolume("Master", gVolume);
        SetMixerVolume("Music", mVolume);
        SetMixerVolume("Effects", eVolume);
    }

    private void SetMixerVolume(string parameter, int value)
    {
        float volume = Mathf.Log10(Mathf.Clamp(value, 1, 100) / 100f) * 20f;
        mixer.SetFloat(parameter, volume);
    }
}