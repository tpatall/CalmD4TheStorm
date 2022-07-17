using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public Slider slider;

    public float sliderValue;

    public void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volume", sliderValue);
    }
    public void VolumeSlider(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }
}
