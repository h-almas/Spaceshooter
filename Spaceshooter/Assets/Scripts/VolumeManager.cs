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
    public AudioMixer fxMixer;
    public Slider fxSlider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume") || !PlayerPrefs.HasKey("fxVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume",-6);
            PlayerPrefs.SetFloat("fxVolume", -4);
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

    public void ChangeFXVolume(float fxVolume)
    {
        fxMixer.SetFloat("fxVolume", fxVolume);
        SaveSettings();
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        PlayerPrefs.SetFloat("fxVolume", fxSlider.value);
    }

    private void LoadSettings()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        fxSlider.value = PlayerPrefs.GetFloat("fxVolume");
    }
}
