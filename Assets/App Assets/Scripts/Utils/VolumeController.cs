using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    public static VolumeController Instance { get; private set; }
    public event Action<float> MusicLevelChanged;
    public event Action<float> SFXLevelChanged;
    public float MusicLevel { get; set; }
    public float SFXLevel { get; set; }
    private AudioSource backgroundMusic;
    public const string MUSIC_KEY = "music_key";
    public const string SFX_KEY = "sfx_key";
    public const float muteValue = 0.0001f;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() 
    {
        backgroundMusic = GameObject.FindGameObjectWithTag("GameMusic").GetComponent<AudioSource>();
        MusicLevel = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        SFXLevel = PlayerPrefs.GetFloat(SFX_KEY, 1f);
        setMusicAudioLevel(MusicLevel);
        setSFXAudioLevel(SFXLevel);
        backgroundMusic.PlayDelayed(0.5f);
    }

    public void setMusicAudioLevel(float sliderValue)
    {
        MusicLevel = sliderValue;
        setAudioLevel(MusicLevel, "MusicVolume");
        MusicLevelChanged?.Invoke(MusicLevel);
    }

    public void setSFXAudioLevel(float sliderValue)
    {
        SFXLevel = sliderValue;
        setAudioLevel(SFXLevel, "SFXVolume");
        SFXLevelChanged?.Invoke(SFXLevel);
    }

    private void setAudioLevel(float sliderValue, string MixerVolumeName)
    {
        mixer.SetFloat(MixerVolumeName, Mathf.Log10(sliderValue)*20);
    }

    public void setMuteOrUnMute(AudioButton button)
    {
        string Key = button.IsMusic ? MUSIC_KEY : SFX_KEY;
        Slider slider = button.matchingSlider;

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

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat(MUSIC_KEY, MusicLevel);
        PlayerPrefs.SetFloat(SFX_KEY, SFXLevel);
    }
}
