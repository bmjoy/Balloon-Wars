using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

public class PhotonConnector : MonoBehaviourPunCallbacks
{
    [SerializeField] private SceneNavigator m_SceneNavigator;

    private void Start() 
    {
        string GuestName = $"Guest {Guid.NewGuid().ToString()}";
        string Username = PlayerPrefs.GetString("USERNAME");
        connectToPhoton(!string.IsNullOrEmpty(Username)? Username : GuestName);
    }

    private void connectToPhoton(string nickName)
    {
        Debug.Log($"Connect to Photon as {nickName}");
        PhotonNetwork.AuthValues = new AuthenticationValues(nickName);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = nickName;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreatePhotonRoom(string roomName)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    public void LeavePhotonLobby()
    {
        if(PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("You have connected to the Photon Master Server");
        if(!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("You have connected to the Photon Lobby");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"You have created a photon room named {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"You have joined the photon room {PhotonNetwork.CurrentRoom.Name}");
        m_SceneNavigator.MoveToClassicGame();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("You have left a photon room");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"You failed to join a Photon room: {message}");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Another player has joined the room: {newPlayer.UserId}");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"Player has left the room: {otherPlayer.UserId}");
    }
}
