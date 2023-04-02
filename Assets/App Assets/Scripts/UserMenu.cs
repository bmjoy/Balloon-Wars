using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMenu : MonoBehaviour
{
    [SerializeField] GameObject m_LoginScreen;
    [SerializeField] GameObject m_TabMenuScreen;
    void Start()
    {
        bool isGuest = string.IsNullOrEmpty(PlayerPrefs.GetString("USERNAME"));
        m_LoginScreen.SetActive(isGuest);
        m_TabMenuScreen.SetActive(!isGuest);
    }
}
