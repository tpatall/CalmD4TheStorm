using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public void VolumeSlider(float volume)
    {
        AudioListener.volume = volume;
    }
}
