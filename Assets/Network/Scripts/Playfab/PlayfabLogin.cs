using PlayFab;
using System;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections;
using TMPro;

public class PlayfabLogin : MonoBehaviour
{
    public static readonly string GUEST = "Guest";
    [SerializeField] private TextMeshProUGUI ErrorText;
    [SerializeField] [Range(2, 10)] private int ErrorTextTime;
    [SerializeField] [Range(10, 50)] private int maxErrorLen;
    public event Action PlayerLoggedIn;
    public event Action PlayerLoggedOut;
    private IEnumerator errorTextCoroutine;
    private const string TITLE_ID = "AA6A1";
    public string Username{get; set;} = GUEST;
    public string Password{get; set;}

    void Start()
    {
        if(string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = TITLE_ID;
        }
    }

    protected void OnPlayerLoggedOut()
    {
        PlayerLoggedOut?.Invoke();
    }

    protected void OnPlayerLoggedIn()
    {
        PlayerLoggedIn?.Invoke();
    }

    private bool isValidUsername()
    {
        bool isValid = false;

        if (!string.IsNullOrWhiteSpace(Username) && Username.Length >= 3 && Username.Length <= 24)
        {
            isValid = true;
        }

        return isValid;
    }

    private void RegisterToPlayFab()
    {
         Debug.Log($"Registering to Playfab as {Username}");
         var request = new RegisterPlayFabUserRequest {TitleId = TITLE_ID, Username = Username, Password = Password, RequireBothUsernameAndEmail=false};

         PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterPlayFabSuccess, OnFailure);
    }

    private void LoginWithPlayFabAccount()
    {
        Debug.Log($"Login to Playfab as {Username}");
        var request = new LoginWithPlayFabRequest {TitleId = TITLE_ID, Username = Username, Password = Password};

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginPlayFabSuccess, OnFailure);
    }

    public void LoginWithGoogleAccount()
    {
        Debug.Log($"Login to Playfab with google");
        LoginWithGoogleAccountRequest request = new LoginWithGoogleAccountRequest();
        PlayFabClientAPI.LoginWithGoogleAccount(request, OnLoginPlayFabSuccess, OnFailure);
    }

    public void LoginWithFacebookAccount()
    {
        Debug.Log($"Login to Playfab with facebook");
        LoginWithFacebookRequest request = new LoginWithFacebookRequest();
        PlayFabClientAPI.LoginWithFacebook(request, OnLoginPlayFabSuccess, OnFailure);
    }

    private void updateDisplayName(string displayName)
    {
         Debug.Log($"Updating Playfab's account display name to {displayName}");
         var request = new UpdateUserTitleDisplayNameRequest {DisplayName = displayName};

         PlayFabClientAPI.UpdateUserTitleDisplayName(request ,OnDisplayNameSuccess, OnFailure);
    }

    public void OnUsernameChanged(string username)
    {
        Username = username;
        PlayerPrefs.SetString("USERNAME", Username);
    }

    public void OnPasswordChanged(string password)
    {
        Password = password;
        PlayerPrefs.SetString("PASSWORD", Password);
    }

    public void Login()
    {
        if(isValidUsername())
        {  
            LoginWithPlayFabAccount();
        }
        else
        {
            showErrorMsg("invalid username");
        }
    }

    public void LogOut()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        Username = GUEST;
        OnPlayerLoggedOut();
    }

    public void Signup()
    {
        if(isValidUsername())
        {  
            RegisterToPlayFab();
        }
        else
        {
            showErrorMsg("invalid username");
        }
    }

    private void showErrorMsg(string errorMsg)
    {
        if(errorTextCoroutine != null)
        {
            StopCoroutine(errorTextCoroutine);
        } 
        errorTextCoroutine = setError(errorMsg);
        StartCoroutine(errorTextCoroutine);
    }

    private IEnumerator setError(string errorMsg)
    {
        string errorToDisplay = "Error - " + errorMsg;
        errorToDisplay = errorToDisplay.Substring(Math.Max(errorToDisplay.IndexOf(':') + 1, 0));
        if (errorToDisplay.Length >= maxErrorLen)
        {
            errorToDisplay = errorToDisplay.Substring(0, maxErrorLen - 4);
            errorToDisplay += "...";
        }
        Debug.Log(errorMsg);
        ErrorText.SetText(errorToDisplay);
        ErrorText.gameObject.SetActive(true);
        yield return new WaitForSeconds(ErrorTextTime);
        ErrorText.gameObject.SetActive(false);
    }

    //-----------------------------------Playfab Callbacks-----------------------------------//

    private void OnLoginPlayFabSuccess(LoginResult result)
    {
        Debug.Log($"You have logged into Playfab using custom id {Username}");
        updateDisplayName(Username);
        OnPlayerLoggedIn();
    }

    private void OnRegisterPlayFabSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log($"You have registered a new Playfab account: {Username}");
        Login();
    }

    private void OnDisplayNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log($"You have updated the display name of the Playfab account.");
    }

    private void OnFailure(PlayFab.PlayFabError error)
    {
        string ErrorMsg = error.GenerateErrorReport();
        showErrorMsg(ErrorMsg);
    }
}
