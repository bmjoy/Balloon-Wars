using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    VolumeController m_VolumeController;

    private void Start()
    {
        m_VolumeController = VolumeController.Instance;   
    }
    
    public void setMusicAudioLevel(float sliderValue)
    {
        m_VolumeController.setMusicAudioLevel(sliderValue);
    }

    public void setSFXAudioLevel(float sliderValue)
    {
        m_VolumeController.setSFXAudioLevel(sliderValue);
    }

    public void setMuteOrUnMute(AudioButton button)
    {
        m_VolumeController.setMuteOrUnMute(button);
    }
}
