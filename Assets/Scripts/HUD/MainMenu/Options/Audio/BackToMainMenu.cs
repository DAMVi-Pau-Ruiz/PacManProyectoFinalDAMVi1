using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMainMenu : MonoBehaviour
{
    public Slider generalSlider;
    public Slider musicSlider;
    public Slider effectsSlider;

    public void GetBack()
    {
        int g = Mathf.RoundToInt(generalSlider.value);
        int m = Mathf.RoundToInt(musicSlider.value);
        int e = Mathf.RoundToInt(effectsSlider.value);

        AudioSettings.instance.setGVolume(g);
        AudioSettings.instance.setMVolume(m);
        AudioSettings.instance.setEVolume(e);

        Debug.Log($"Audio guardado: G={g}, M={m}, E={e}");
        SceneManager.LoadScene("MainMenu");
    }
}
