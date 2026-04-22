using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValue : MonoBehaviour
{
    public Slider slider;
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        UpdateValue(slider.value);

        slider.onValueChanged.AddListener(UpdateValue);
    }

    // Update is called once per frame
    void UpdateValue(float value)
    {
        text.text = Mathf.RoundToInt(value).ToString() + "%";
    }
}
