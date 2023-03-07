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
    [SerializeField] AudioButton musicButton;
    [SerializeField] AudioButton sfxButton;

    public const string MUSIC_KEY = "music_key";
    public const string SFX_KEY = "sfx_key";
    public const float muteValue = 0.0001f;

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
        setAudioLevel(sliderValue, "MusicVolume", musicButton);
    }

    public void setSFXAudioLevel(float sliderValue)
    {
        setAudioLevel(sliderValue, "SFXVolume", sfxButton);
    }

    public void setAudioLevel(float sliderValue, string MixerVolumeName, AudioButton button)
    {
        bool wasMuted = button.IsMuted;
        bool nowMuted = sliderValue == muteValue;

        mixer.SetFloat(MixerVolumeName, Mathf.Log10(sliderValue)*20);

        if (wasMuted || nowMuted)
        {
            button.SwichState();
        }
    }

    public void setMuteOrUnMute(AudioButton button)
    {
        string Key = button.IsMusic ? MUSIC_KEY : SFX_KEY;
        Slider slider = button.IsMusic ? musicSlider : sfxSlider;

        if(!button.IsMuted) // need to mute
        {
            PlayerPrefs.SetFloat(Key, slider.value);
            slider.value = muteValue;
        }
        else // need to unmute
        {
            slider.value = PlayerPrefs.GetFloat(Key, 1f);
        }
    }
}
