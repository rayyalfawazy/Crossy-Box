using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider BGMSlider, SFXSlider;

    private void Start()
    {
        // Load Value Slider
        BGMSlider.value = PlayerPrefs.GetFloat("bgmSlider");
        SFXSlider.value = PlayerPrefs.GetFloat("sfxSlider");
    }

    public void SetBGMVolume()
    {
        //Adjust Volume By Slider
        float BGMVolume = Mathf.Log10(BGMSlider.value) * 20; // Mengikuti Metode Volume Mixer yang Logarithmic
        audioMixer.SetFloat("bg_music", BGMVolume);

        //Save Slider Value by PlayerPref
        PlayerPrefs.SetFloat("bgmSlider", BGMSlider.value);
    }

    public void SetSFXVolume()
    {
        //Adjust Volume By Slider
        float SFXVolume = Mathf.Log10(SFXSlider.value) * 20; // Mengikuti Metode Volume Mixer yang Logarithmic
        audioMixer.SetFloat("sfx", SFXVolume);

        //Save Slider Value by PlayerPref
        PlayerPrefs.SetFloat("sfxSlider", SFXSlider.value);
    }
}
