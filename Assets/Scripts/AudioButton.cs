using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Sprite VoiceImage;
    [SerializeField] Sprite MuteImage;
    [SerializeField] public bool IsMusic;
    public bool IsMuted {set; get;}

    private void Start()
    {
        if (IsMusic)
        {
            IsMuted = PlayerPrefs.GetFloat(VolumeController.MUSIC_KEY, 1f) == VolumeController.muteValue;
        }
        else
        {
            IsMuted = PlayerPrefs.GetFloat(VolumeController.SFX_KEY, 1f) == VolumeController.muteValue;
        }

        icon.sprite = IsMuted ? MuteImage : VoiceImage;
    }

    public void SwichState()
    {
        IsMuted = !IsMuted;
        icon.sprite = IsMuted ? MuteImage : VoiceImage;
    }
}
