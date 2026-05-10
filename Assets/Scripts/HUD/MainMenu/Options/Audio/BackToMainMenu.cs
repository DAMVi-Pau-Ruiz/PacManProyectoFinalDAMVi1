using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMainMenu : MonoBehaviour
{
    public Slider generalSlider;
    public Slider musicSlider;
    public Slider effectsSlider;

    private void Start()
    {
        // Opcional: cargar valores actuales
        generalSlider.value = AudioSettings.instance.getGVolume();
        musicSlider.value = AudioSettings.instance.getMVolume();
        effectsSlider.value = AudioSettings.instance.getEVolume();
    }

    public void GetBack()
    {
        AudioSettings.instance.setGVolume((int)generalSlider.value);
        AudioSettings.instance.setMVolume((int)musicSlider.value);
        AudioSettings.instance.setEVolume((int)effectsSlider.value);

        Debug.Log($"Audio guardado: G={generalSlider.value}, M={musicSlider.value}, E={effectsSlider.value}");

        SceneManager.LoadScene("MainMenu");
    }
}