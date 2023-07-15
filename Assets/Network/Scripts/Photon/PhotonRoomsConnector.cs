using UnityEngine;
using TMPro;
using System;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class PhotonRoomsConnector : MonoBehaviourPunCallbacks
{
    [SerializeField] private Animator m_Animator;
    private bool m_IsCreatedRoom = false;
    public event Action<List<RoomInfo>> RoomListChanged;
    public event Action<Player> PlayerAddedToList;
    public event Action<Player> PlayerRemovedFromList;
    public event Action<string> failedConnectToRoom;
    public event Action PlayerJoinedRoom;
    public event Action MasterPlayerSwiched;
    public List<RoomInfo> RoomList { get; private set; } = new List<RoomInfo>();
    public static SceneNavigator.GameMode GameMode { get; set; }

    private TypedLobby ClassicLobby = new TypedLobby("Classic", LobbyType.Default);
    private TypedLobby PlayOutsideLobby = new TypedLobby("PlayOutside", LobbyType.Default);

    private void Start() 
    {
        JoinPhotonLobby();
        PhotonConnector.Instance.ConnectedToMaster += JoinPhotonLobby;
    }

    // --------- Lobby ---------

    private void JoinPhotonLobby()
    {
        if(!PhotonNetwork.InLobby)
        {
            // Join the appropriate lobby based on the chosen game mode
            switch (GameMode)
            {
                case SceneNavigator.GameMode.Classic:
                    PhotonNetwork.JoinLobby(ClassicLobby);
                    break;
                case SceneNavigator.GameMode.PlayOutside:
                    PhotonNetwork.JoinLobby(PlayOutsideLobby);
                    break;
            }
        }
    }

    public void LeavePhotonLobby()
    {
        if(PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        if (PhotonNetwork.CurrentLobby.Name == ClassicLobby.Name)
        {
            Debug.Log("You have joined the Classic Lobby");
        }
        else if (PhotonNetwork.CurrentLobby.Name == PlayOutsideLobby.Name)
        {
            Debug.Log("You have joined the PlayOutside Lobby");
            // Activate location services
            StartCoroutine(LocationServiceActivator.ActivateLocationServices());
        }
    }

    public override void OnLeftLobby()
    {
        Debug.Log("You have been disconnected from the Photon Lobby");
    }

    // --------- Rooms ---------

    public void CreatePhotonRoom(string roomName, bool isVisible = true, int maxPlayers = 4, int level = 1, bool isOpen = true)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = isOpen;
        roomOptions.IsVisible = isVisible;
        roomOptions.MaxPlayers = (byte)maxPlayers;
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "Level" };
        roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roomOptions.CustomRoomProperties.Add("Level", level);
        if(!PhotonNetwork.InRoom)
        {  
            Debug.Log($"Try create room: {roomName}");
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }
    }

    public void JoinPhotonRoom(string roomName)
    {
        if(!PhotonNetwork.InRoom)
        {  
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    public void LeavePhotonRoom()
    {
        if(PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"You have created a photon room named {PhotonNetwork.CurrentRoom.Name}");
        m_IsCreatedRoom = true;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"You have joined the photon room {PhotonNetwork.CurrentRoom.Name}");
        PlayerJoinedRoom?.Invoke();
        m_Animator.SetTrigger(m_IsCreatedRoom? "CreateRoomToDetails" : "JoinRoomToDetails");
        m_IsCreatedRoom = false;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"You failed to join a Photon room: {message}");
        failedConnectToRoom?.Invoke(message);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("You have left a photon room");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("The room list was changed");
        RoomList = roomList;
        RoomListChanged?.Invoke(roomList);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Another player has joined the room: {newPlayer.UserId}");
        PlayerAddedToList?.Invoke(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"Player has left the room: {otherPlayer.UserId}");
        PlayerRemovedFromList?.Invoke(otherPlayer);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log($"Master player was replaced to: {newMasterClient.NickName}");
        MasterPlayerSwiched?.Invoke();
    }

    private void OnDestroy()
    {
        PhotonConnector.Instance.ConnectedToMaster -= JoinPhotonLobby;
    }
}
