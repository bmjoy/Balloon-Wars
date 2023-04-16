using UnityEngine;
using TMPro;
using System;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class PhotonRoomsConnector : MonoBehaviourPunCallbacks
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private MapChooser m_DetailsMapChooser;
    [SerializeField] private GameObject m_StartGameButton;
    [SerializeField] private TextMeshProUGUI m_DetailsLevelName;
    private bool m_IsCreatedRoom = false;
    public event Action<List<RoomInfo>> RoomListChanged;
    public event Action<Player> PlayerAddedToList;
    public event Action<Player> PlayerRemovedFromList;
    public List<RoomInfo> RoomList { get; private set; } = new List<RoomInfo>();

    private void Start() 
    {
        JoinPhotonLoby();
    }

    public void CreatePhotonRoom( 
        string roomName, bool isVisible = true, int maxPlayers = 4, int level = 1, bool isOpen = true)
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
            PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
        }
    }

    private void JoinPhotonLoby()
    {
        if(!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public void JoinPhotonRoom(string roomName)
    {
        if(!PhotonNetwork.InRoom)
        {  
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    public void LeavePhotonLobby()
    {
        if(PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
    }

    public void LeaveRoom()
    {
        if(PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("The room list was changed");
        RoomList = roomList;
        RoomListChanged?.Invoke(roomList);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("You have connected to the Photon Lobby");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"You have created a photon room named {PhotonNetwork.CurrentRoom.Name}");
        m_IsCreatedRoom = true;
    }

    public override void OnJoinedRoom()
    {
        Room curRoom = PhotonNetwork.CurrentRoom;
        Debug.Log($"You have joined the photon room {curRoom.Name}");
        int level = (int)PhotonNetwork.CurrentRoom.CustomProperties["Level"];
        Debug.Log($"room details: visable = {curRoom.IsVisible}, open = {curRoom.IsOpen}," + 
                    $" maxPlayers ={curRoom.MaxPlayers}, Level = {level}");
        m_DetailsMapChooser.setBackImageToLevel(level);
        m_DetailsLevelName.SetText(curRoom.Name);
        m_StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
        m_Animator.SetTrigger(m_IsCreatedRoom? "CreateRoomToDetails" : "JoinRoomToDetails");
        m_IsCreatedRoom = false;
    }

    public override void OnLeftRoom()
    {
        Debug.Log("You have left a photon room");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("You have disConnected from the Photon Lobby");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"You failed to join a Photon room: {message}");
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
        m_StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

}
