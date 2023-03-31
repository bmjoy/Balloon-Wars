using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameOfPlayer : MonoBehaviour
{
    private TextMeshProUGUI m_NameText;
    private PlayfabLogin m_PlayFab;

    private void Awake()
    {
        m_NameText = GetComponent<TextMeshProUGUI>();
        m_PlayFab = FindAnyObjectByType<PlayfabLogin>();
        m_PlayFab.PlayerLoggedIn += changeNameTextToPlayersName;
        m_PlayFab.PlayerLoggedOut += changeNameTextToPlayersName;
    }

    private void Start()
    {
        changeNameTextToPlayersName();
    }

    private void changeNameTextToPlayersName()
    {
        m_NameText.SetText(m_PlayFab.Username);
    }
}
