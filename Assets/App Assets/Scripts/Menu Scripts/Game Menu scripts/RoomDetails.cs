using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomDetails : MonoBehaviour
{
    [SerializeField] private PhotonRoomsConnector m_PhotonRoomsConnector; 
    [SerializeField] private MapChooser m_MapChooser;
    [SerializeField] private GameObject m_StartGameButton;
    [SerializeField] private TextMeshProUGUI m_LevelName;

    private void Start()
    {
        m_PhotonRoomsConnector.PlayerJoinedRoom += setRoomUiValues;
        m_PhotonRoomsConnector.MasterPlayerSwiched += setRoomMasterUI;
        if(PhotonNetwork.InRoom)
        {
            setRoomUiValues();
        }
    }

    public void setRoomUiValues()
    {
        Room curRoom = PhotonNetwork.CurrentRoom;
        int level = (int)PhotonNetwork.CurrentRoom.CustomProperties["Level"];
        Debug.Log($"room details: visable = {curRoom.IsVisible}, open = {curRoom.IsOpen}," + 
                    $" maxPlayers ={curRoom.MaxPlayers}, Level = {level}");
        m_MapChooser.setBackImageToLevel(level);
        m_LevelName.SetText(curRoom.Name);
        setRoomMasterUI();
    }

    public void setRoomMasterUI()
    {
        m_StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
}
