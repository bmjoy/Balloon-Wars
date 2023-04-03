using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class NameOfPlayer : MonoBehaviour
{
    private static readonly string GUEST = "Guest";
    private TextMeshProUGUI m_NameText;

    private void Start()
    {
        m_NameText = GetComponent<TextMeshProUGUI>();   

        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest(), OnGetPlayerProfileSuccess, OnGetPlayerProfileError);
        }
        else
        {
            m_NameText.SetText(GUEST);
        }
    }

    private void OnGetPlayerProfileSuccess(GetPlayerProfileResult result)
    {
        m_NameText.SetText(result.PlayerProfile.DisplayName);
    }

    private void OnGetPlayerProfileError(PlayFabError error)
    {
        Debug.LogError("GetPlayerProfile error: " + error.ErrorMessage);
        m_NameText.SetText(GUEST);
    }
}
