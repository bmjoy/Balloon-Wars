using System;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.PfEditor.EditorModels;
using UnityEngine;

public class PlayfabLogin : MonoBehaviour
{
    private const string TITLE_ID = "AA6A1";
    [SerializeField] private string m_Username;
    [SerializeField] private string m_Password;

    void Start()
    {
        if(string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = TITLE_ID;
        }

        Username = "TestAccount"; // Temporary until we implement the login scene.
    }

    public string Username 
    {
        get { return m_Username; }
        set 
        {
            m_Username = value;
        }
    }

    private bool isValidUsername()
    {
        bool isValid = false;

        if (m_Username.Length >= 3 && m_Username.Length <= 24)
        {
            isValid = true;
        }

        return isValid;
    }

    private void RegisterToPlayFab()
    {
         Debug.Log($"Registering to Playfab as {m_Username}");
         var request = new RegisterPlayFabUserRequest {TitleId = TITLE_ID, Username = m_Username, Password = m_Password, RequireBothUsernameAndEmail=false};

         PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterPlayFabSuccess, OnFailure);
    }

    private void LoginWithPlayFabAccount()
    {
        Debug.Log($"Login to Playfab as {m_Username}");
        var request = new LoginWithPlayFabRequest {TitleId = TITLE_ID, Username = m_Username, Password = m_Password};

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginPlayFabSuccess, OnFailure);
    }

    private void updateDisplayName(string displayName)
    {
         Debug.Log($"Updating Playfab's account display name to {displayName}");
         var request = new UpdateUserTitleDisplayNameRequest {DisplayName = displayName};

         PlayFabClientAPI.UpdateUserTitleDisplayName(request ,OnDisplayNameSuccess, OnFailure);
    }

    public void OnUsernameChanged(string username)
    {
        m_Username = username;
        PlayerPrefs.SetString("USERNAME", m_Username);
    }

    public void OnPasswordChanged(string password)
    {
        m_Password = password;
        PlayerPrefs.SetString("PASSWORD", m_Password);
    }

    public void Login()
    {
        if(isValidUsername())
        {  
            LoginWithPlayFabAccount();
        }
    }

    public void Signup()
    {
        if(isValidUsername())
        {  
            RegisterToPlayFab();
        }
    }

    //-----------------------------------Playfab Callbacks-----------------------------------//

    private void OnLoginPlayFabSuccess(PlayFab.ClientModels.LoginResult result)
    {
        Debug.Log($"You have logged into Playfab using custom id {m_Username}");
        updateDisplayName(m_Username);
    }

    private void OnRegisterPlayFabSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log($"You have registered a new Playfab account: {m_Username}");
        Login();
    }

    private void OnDisplayNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log($"You have updated the display name of the Playfab account.");
    }

    private void OnFailure(PlayFab.PlayFabError error)
    {
        Debug.Log($"There was an issue with your request {error.GenerateErrorReport()}");
    }

}
