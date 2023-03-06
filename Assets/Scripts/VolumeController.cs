using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] AudioSource backgroundMusic;

    private const string MUSIC_KEY = "music_key";
    private const string SFX_KEY = "sfx_key";

    private void Start() 
    {
        musicSlider.value = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(SFX_KEY, 1f);
        backgroundMusic.PlayDelayed(0.5f);
    }

    private void OnDisable() 
    {
        PlayerPrefs.SetFloat(MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(SFX_KEY, sfxSlider.value);
    }

    public void setMusicAudioLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue)*20);
    }

    public void setSFXAudioLevel(float sliderValue)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue)*20);
    }

    public void setMute(bool isMusic, bool toMute)
    {
        if(isMusic)
        {
            setMusicMute(toMute);
        }
        else
        {
            setSFXMute(toMute);
        }
    }
    public void setMusicMute(bool toMute)
    {
        if (toMute)
        {
            PlayerPrefs.SetFloat(MUSIC_KEY, musicSlider.value);
            musicSlider.value = 0.0001f;
        }
        else
        {
            musicSlider.value = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        }
    }

        public void setSFXMute(bool toMute)
    {
        if (toMute)
        {
            PlayerPrefs.SetFloat(SFX_KEY, sfxSlider.value);
            musicSlider.value = 0.0001f;
        }
        else
        {
            sfxSlider.value = PlayerPrefs.GetFloat(SFX_KEY, 1f);
        }
    }
}
