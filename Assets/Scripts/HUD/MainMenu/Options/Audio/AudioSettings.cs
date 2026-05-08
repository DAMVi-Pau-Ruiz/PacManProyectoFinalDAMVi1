using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    [SerializeField]
    int defaultVolume = 50;

    public static AudioSettings instance;

    private int gVolume, mVolume, eVolume;

    private void Awake()
    {
        gVolume = defaultVolume;
        mVolume = defaultVolume;
        eVolume = defaultVolume;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setGVolume(int volume) { gVolume = volume; }
    public void setMVolume(int volume) { mVolume = volume; }
    public void setEVolume(int volume) { eVolume = volume; }
    public int getGVolume() { return gVolume; }
    public int getMVolume() { return mVolume; }
    public int getEVolume() { return eVolume; }
}
