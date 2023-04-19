using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{   
    public void setMusicAudioLevel(float sliderValue)
    {
        VolumeController.Instance.setMusicAudioLevel(sliderValue);
    }

    public void setSFXAudioLevel(float sliderValue)
    {
        VolumeController.Instance.setSFXAudioLevel(sliderValue);
    }

    public void setMuteOrUnMute(AudioButton button)
    {
        VolumeController.Instance.setMuteOrUnMute(button);
    }
}
