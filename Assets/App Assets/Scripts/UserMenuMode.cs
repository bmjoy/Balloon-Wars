using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMenuMode : MonoBehaviour
{
    private PlayfabLogin m_PlayFab;
    [SerializeField] private GameObject m_LoginScreen;
    [SerializeField] private GameObject m_LogoutScreen;
    void Start()
    {
        m_PlayFab = FindAnyObjectByType<PlayfabLogin>();
        m_PlayFab.PlayerLoggedIn += changeMenuToLoggedInMode;
        m_PlayFab.PlayerLoggedOut += changeMenuToLoggedOutMode;
    }

    private void changeMenuToLoggedInMode()
    {
        m_LogoutScreen.SetActive(true);
        m_LoginScreen.SetActive(false);
    }

    private void changeMenuToLoggedOutMode()
    {
        m_LoginScreen.SetActive(true);
        m_LogoutScreen.SetActive(false);
    }
}
