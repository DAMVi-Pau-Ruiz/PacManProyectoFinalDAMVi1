using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValue : MonoBehaviour
{
    public Slider slider;
    public TMP_Text text;

    public enum VolumeType { Master, Music, SFX }
    public VolumeType type;

    private void Start()
    {
        UpdateValue(slider.value);
        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void OnSliderChanged(float value)
    {
        UpdateValue(value);

        if (AudioSettings.instance == null) return;

        int v = Mathf.RoundToInt(value);

        switch (type)
        {
            case VolumeType.Master:
                AudioSettings.instance.setGVolume(v);
                break;

            case VolumeType.Music:
                AudioSettings.instance.setMVolume(v);
                break;

            case VolumeType.SFX:
                AudioSettings.instance.setEVolume(v);
                break;
        }
    }

    private void UpdateValue(float value)
    {
        text.text = Mathf.RoundToInt(value) + "%";
    }
}