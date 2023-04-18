using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    Button m_Button;
    [SerializeField] Animator m_Animator;
    void Start()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(openSettings);
    }

    private void openSettings()
    {
        m_Animator.SetTrigger("SettingsIn");
        m_Button.onClick.RemoveAllListeners();
        m_Button.onClick.AddListener(closeSettings);
    }
    public void closeSettings()
    {
        m_Animator.SetTrigger("SettingsOut");
        m_Button.onClick.RemoveAllListeners();
        m_Button.onClick.AddListener(openSettings);
    }
}
