using UnityEngine;
using System;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using Photon.Realtime;

public class PhotonConnector : MonoBehaviourPunCallbacks
{
    private void Start() 
    {
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest(), OnGetPlayerProfileSuccess, OnGetPlayerProfileError);
        }
        else
        {
            string GuestName = $"Guest {Guid.NewGuid().ToString()}";
            connectToPhotonMaster(GuestName);
        }
    }

    private void OnGetPlayerProfileSuccess(GetPlayerProfileResult result)
    {
        string nickName = result.PlayerProfile.DisplayName;
        connectToPhotonMaster(nickName);
    }

    private void OnGetPlayerProfileError(PlayFabError error)
    {
        Debug.LogError("GetPlayerProfile error: " + error.ErrorMessage);
        connectToPhotonMaster($"Guest {Guid.NewGuid().ToString()}");
    }

    private void connectToPhotonMaster(string nickName)
    {
        Debug.Log($"Connect to Photon as {nickName}");
        PhotonNetwork.AuthValues = new AuthenticationValues(nickName);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = nickName;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void DisconnectFromPhotonMaster(string nickName)
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"You have disconnected from the Photon Master Server. Cause: {cause.ToString()}");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("You have connected to the Photon Master Server");
    }
}
