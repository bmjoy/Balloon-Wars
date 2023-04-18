using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    private VolumeController m_VolumeController;
    private Image m_Icon;
    [SerializeField] Sprite VoiceImage;
    [SerializeField] Sprite MuteImage;
    [SerializeField] public bool IsMusic;
    [SerializeField] public Slider matchingSlider;
    public bool IsMuted {set; get;}
    private void Start()
    {
        m_VolumeController = VolumeController.Instance;
        if(IsMusic)
        {
            IsMuted = m_VolumeController.MusicLevel == VolumeController.muteValue;
            m_VolumeController.MusicLevelChanged += changeStateIfNeeded;
        }
        else
        {
            IsMuted = m_VolumeController.SFXLevel == VolumeController.muteValue;
            m_VolumeController.SFXLevelChanged += changeStateIfNeeded;
        }
        m_Icon = GetComponent<Image>();
        m_Icon.sprite = IsMuted ? MuteImage : VoiceImage;
    }

    private void SwichState()
    {
        IsMuted = !IsMuted;
        m_Icon.sprite = IsMuted ? MuteImage : VoiceImage;
    }

    private void changeStateIfNeeded(float newLevel)
    {
        if (IsMuted || newLevel == VolumeController.muteValue)
        {
            SwichState();
        }
    }

    private void OnDestroy()
    {
        if(IsMusic)
        {
            m_VolumeController.MusicLevelChanged -= changeStateIfNeeded;
        }
        else
        {
            m_VolumeController.SFXLevelChanged -= changeStateIfNeeded;
        }
    }
}
