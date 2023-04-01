using PlayFab;
using System;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayfabLogin : MonoBehaviour
{
    [SerializeField] SceneNavigator m_SceneNavigator;
    [SerializeField] private TextMeshProUGUI m_ErrorText;
    [SerializeField] [Range(2, 10)] private int m_ErrorTextTime;
    [SerializeField] [Range(10, 50)] private int m_maxErrorLen;
    [SerializeField] Button m_LoginButton;
    [SerializeField] Button m_SignupButton;
    private IEnumerator m_errorTextCoroutine;
    private const string TITLE_ID = "AA6A1";
    public string Username{get; set;}
    public string Password{get; set;}

    void Start()
    {
        if(string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = TITLE_ID;
        }
    }

    //------------------ set username and password ------------------

    public void saveUsername(string username)
    {
        PlayerPrefs.SetString("USERNAME", username);
    }
    public void savePassword(string password)
    {
        PlayerPrefs.SetString("PASSWORD", password);
    }

    private void saveUsernameAndPassword()
    {
        saveUsername(Username);
        savePassword(Password);
    }

    public void OnUsernameChanged(string username)
    {
        Username = username;
        setButtonsInteractableIfValidInput();
    }
    
    public void OnPasswordChanged(string password)
    {
        Password = password;
        setButtonsInteractableIfValidInput();
    }

    private bool isValidUsername(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && name.Length >= 3 && name.Length <= 12;
    }

    private bool isValidPassword(string password)
    {
        return !string.IsNullOrWhiteSpace(password) && password.Length >= 6 && password.Length <= 15;
    }

    private void setButtonsInteractableIfValidInput()
    {
        if (isValidUsername(Username) && isValidPassword(Password))
        {
            m_LoginButton.interactable = true;
            m_SignupButton.interactable = true;
        }
        else
        {
            m_LoginButton.interactable = false;
            m_SignupButton.interactable = false;
        }
    }

    //------------------ Login and Signup ------------------

    private void RegisterToPlayFab(string username, string password)
    {
         Debug.Log($"Registering to Playfab as {username}");
         var request = new RegisterPlayFabUserRequest {TitleId = TITLE_ID, Username = username, Password = password, RequireBothUsernameAndEmail=false};

         PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterPlayFabSuccess, OnFailure);
    }

    private void LoginWithPlayFabAccount(string username, string password)
    {
        Debug.Log($"Login to Playfab as {username}");
        var request = new LoginWithPlayFabRequest {TitleId = TITLE_ID, Username = username, Password = password};

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

    public void Login()
    {
        LoginWithPlayFabAccount(Username, Password);
    }

    public void LoginWithSavedValues()
    {
        Username = PlayerPrefs.GetString("USERNAME");
        Password = PlayerPrefs.GetString("PASSWORD");
        Login();
    }

    public void Signup()
    { 
        RegisterToPlayFab(Username, Password);
    }

    //------------------ error msg ------------------
    private void showErrorMsg(string errorMsg)
    {
        if(m_errorTextCoroutine != null)
        {
            StopCoroutine(m_errorTextCoroutine);
        } 
        m_errorTextCoroutine = setError(errorMsg);
        StartCoroutine(m_errorTextCoroutine);
    }

    private IEnumerator setError(string errorMsg)
    {
        string errorToDisplay = "Error - " + errorMsg;
        errorToDisplay = errorToDisplay.Substring(Math.Max(errorToDisplay.IndexOf(':') + 1, 0));
        if (errorToDisplay.Length >= m_maxErrorLen)
        {
            errorToDisplay = errorToDisplay.Substring(0, m_maxErrorLen - 4);
            errorToDisplay += "...";
        }
        Debug.Log(errorMsg);
        m_ErrorText.SetText(errorToDisplay);
        m_ErrorText.gameObject.SetActive(true);
        yield return new WaitForSeconds(m_ErrorTextTime);
        m_ErrorText.gameObject.SetActive(false);
    }

    //-----------------------------------Playfab Callbacks-----------------------------------//

    private void OnLoginPlayFabSuccess(LoginResult result)
    {
        saveUsernameAndPassword();
        Debug.Log($"You have logged into Playfab using custom id {Username}");
        updateDisplayName(Username);
        m_SceneNavigator.MoveToMainMenu();
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
