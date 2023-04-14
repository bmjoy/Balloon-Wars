using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class PhotonRoomsConnector : MonoBehaviourPunCallbacks
{
    [SerializeField] private Animator m_Animator;
    private bool m_IsCreatedRoom = false;
    public event Action<List<RoomInfo>> RoomListChanged;
    public List<RoomInfo> RoomList { get; private set; }

    private void Start()
    {
        if(!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public void CreatePhotonRoom(string roomName,bool isOpen = true, bool isVisible = true, byte maxPlayers = 4)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = isOpen;
        roomOptions.IsVisible = isVisible;
        roomOptions.MaxPlayers = maxPlayers;
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
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
        Debug.Log("update room list");
        RoomList = roomList;
        RoomListChanged?.Invoke(roomList);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("You have connected to the Photon Lobby");
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"You have created a photon room named {PhotonNetwork.CurrentRoom.Name}");
        m_IsCreatedRoom = true;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"You have joined the photon room {PhotonNetwork.CurrentRoom.Name}");
        Debug.Log($"is created = {m_IsCreatedRoom}");
        if(m_IsCreatedRoom)
        {
            m_Animator.SetTrigger("CreateRoomToDetails");
        }
        else
        {
            m_Animator.SetTrigger("JoinRoomToDetails");
        }
        m_IsCreatedRoom = false;
    }

    public override void OnLeftRoom()
    {
        Debug.Log("You have left a photon room");
        if(!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
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
