using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtons : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Sprite VoiceImage;
    [SerializeField] Sprite MuteImage;
    [SerializeField] VolumeController volumeController;
    [SerializeField] bool IsMusic;
    public bool IsMuted {set; get;} = false;

    public void SwichState()
    {
        IsMuted = !IsMuted;
        icon.sprite = IsMuted ? MuteImage : VoiceImage;
        volumeController.setMute(IsMusic, IsMuted);
    }
}
