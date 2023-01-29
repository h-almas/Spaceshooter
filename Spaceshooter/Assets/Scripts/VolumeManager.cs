using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume",-6);
            LoadSettings();
        }
        else
        {
            LoadSettings();
        }
    }

    public void ChangeMusicVolume(float musicVolume)
    {
        audioMixer.SetFloat("musicVolume", musicVolume);
        SaveSettings();
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    private void LoadSettings()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
}
