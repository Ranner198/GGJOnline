using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // Sound
    public AudioMixer audioMixer;
    public Toggle toggleSound;
    public bool volumeEnabled = true;
    public float volume = 0, volumeAdder = 2;

    // Graphics
    public Resolution[] resolutions;
    public TMP_Dropdown QualityDropDown;        
    void Start()
    {       
        int Quality = PlayerPrefs.GetInt("_qualityIndex", 0);
        volume = PlayerPrefs.GetFloat("_volume", 0);
        volumeEnabled = (PlayerPrefs.GetFloat("_volumeEnabled", 1) == 1? true: false);
        
        SetVolume(volume);
        ToggleVolume();

        QualityDropDown.value = Quality;
        resolutions = Screen.resolutions;
        QualityDropDown.ClearOptions();

        List<string> res = new List<string>();

        for (int i = 0; i < resolutions.Length; i++) {
            res.Add(resolutions[i].width + " x " + resolutions[i].height);
        }

        QualityDropDown.AddOptions(res);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("_qualityIndex", qualityIndex);
    }

    public void SetVolume(float volume) 
    {
        if (volume < -20)
            volume = -80;

        PlayerPrefs.SetFloat("_volume", volume);
        audioMixer.SetFloat("Volume", volume * volumeAdder);
        this.volume = volume;
    }

    public void ToggleVolume()
    {
        volumeEnabled = !volumeEnabled;
        audioMixer.SetFloat("Volume", volumeEnabled ? -80: volume);
        PlayerPrefs.SetInt("_volumeEnabled", this.volumeEnabled? 1: 0);
    }
}
