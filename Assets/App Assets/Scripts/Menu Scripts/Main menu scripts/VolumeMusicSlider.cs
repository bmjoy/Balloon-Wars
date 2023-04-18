using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeMusicSlider : MonoBehaviour
{
    Slider m_MusicSlider;
    VolumeController m_VolumeController;
    void Start()
    {
        m_VolumeController = VolumeController.Instance;
        m_MusicSlider = GetComponent<Slider>();
        m_MusicSlider.value = m_VolumeController.MusicLevel;
        m_VolumeController.MusicLevelChanged += setAudioLevelToController;
    }

    private void setAudioLevelToController(float newLevel)
    {
        if (m_MusicSlider.value != newLevel)
        {
            m_MusicSlider.SetValueWithoutNotify(newLevel);
        }
    }
}
