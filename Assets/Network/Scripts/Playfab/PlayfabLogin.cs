using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayfabLogin : MonoBehaviour
{
    private const string TITLE_ID = "AA6A1";
    [SerializeField] private string m_Username;

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

    private void loginWithCustomerId()
    {
        Debug.Log($"Login to Playfab as {m_Username}");
        var request = new LoginWithCustomIDRequest {CustomId = m_Username, CreateAccount = true};

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginCustomIdSuccess, OnFailure);
    }

    private void updateDisplayName(string displayName)
    {
         Debug.Log($"Updating Playfab's account display name to {displayName}");
         var request = new UpdateUserTitleDisplayNameRequest {DisplayName = displayName};

         PlayFabClientAPI.UpdateUserTitleDisplayName(request ,OnDisplayNameSuccess, OnFailure);
    }

    public void OnUsernameChanged()
    {
        PlayerPrefs.SetString("USERNAME", m_Username);
    }

    public void Login()
    {
        if(isValidUsername())
        {  
            loginWithCustomerId();
        }
    }

    //-----------------------------------Playfab Callbacks-----------------------------------//

    private void OnLoginCustomIdSuccess(LoginResult result)
    {
        Debug.Log($"You have logged into Playfab using custom id {m_Username}");
        updateDisplayName(m_Username);
    }

    private void OnDisplayNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log($"You have updated the display name of the Playfab account.");
    }

    private void OnFailure(PlayFabError error)
    {
        Debug.Log($"There was an issue with your request {error.GenerateErrorReport()}");
    }

}
