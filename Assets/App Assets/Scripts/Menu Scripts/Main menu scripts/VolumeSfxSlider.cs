using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSfxSlider : MonoBehaviour
{
    Slider m_SfxSlider;
    VolumeController m_VolumeController;
    void Start()
    {
        m_VolumeController = VolumeController.Instance;
        m_SfxSlider = GetComponent<Slider>();
        m_SfxSlider.value = m_VolumeController.SFXLevel;
        m_VolumeController.SFXLevelChanged += setAudioLevelToController;
    }

    private void setAudioLevelToController(float newLevel)
    {
        if (m_SfxSlider.value != newLevel)
        {
            m_SfxSlider.SetValueWithoutNotify(newLevel);
        }
    }
}
