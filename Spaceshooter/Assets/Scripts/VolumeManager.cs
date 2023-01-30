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
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume",-6);
            LoadSettingsMusic();
        }
        else
        {
            LoadSettingsMusic();
        }

        if (!PlayerPrefs.HasKey("fxVolume"))
        {
            PlayerPrefs.SetFloat("fxVolume", -4);
            LoadSettingsFX();
        }
        else
        {
            LoadSettingsFX();
        }
    }

    public void ChangeMusicVolume(float musicVolume)
    {
        audioMixer.SetFloat("musicVolume", musicVolume);
        SaveSettingsMusic();
    }

    public void ChangeFXVolume(float fxVolume)
    {
        fxMixer.SetFloat("fxVolume", fxVolume);
        SaveSettingsFX();
    }

    private void SaveSettingsMusic()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    private void LoadSettingsMusic()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    
    private void SaveSettingsFX()
    {
        PlayerPrefs.SetFloat("fxVolume", fxSlider.value);
    }

    private void LoadSettingsFX()
    {
        fxSlider.value = PlayerPrefs.GetFloat("fxVolume");
    }
}
