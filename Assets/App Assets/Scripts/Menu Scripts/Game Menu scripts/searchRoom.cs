using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Photon.Realtime;

public class searchRoom : MonoBehaviour
{
    private TMP_InputField m_InputField;
    private PhotonRoomsConnector m_PhotonRoomsConnector;
    private RoomsViewList m_RoomsViewList;
    void Start()
    {
        m_PhotonRoomsConnector = FindObjectOfType<PhotonRoomsConnector>();
        m_RoomsViewList = FindObjectOfType<RoomsViewList>();
        m_InputField = GetComponent<TMP_InputField>();
    }

    public void searchRoomInTextBox()
    {
        string roomName = m_InputField.text;
        m_InputField.text = string.Empty;
        Debug.Log($"Looking for room named {roomName}");
        if(m_PhotonRoomsConnector.RoomList.Any(room => room.Name == roomName))
        {
            Debug.Log($"room named {roomName} was found");
            RoomInfo selectedRoom = m_PhotonRoomsConnector.RoomList.Find(room => room.Name == roomName);
            if(selectedRoom.IsOpen && selectedRoom.PlayerCount < selectedRoom.MaxPlayers)
            {
                Debug.Log($"selecting room {roomName}");
                m_RoomsViewList.OnSelectedRoomChanged(roomName);
            }
            else
            {
                Debug.Log("room unavailable!");
                m_InputField.placeholder.GetComponent<TextMeshProUGUI>().SetText("room unavailable!");
            }
        }
        else
        {
            Debug.Log("room was not found");
            m_InputField.placeholder.GetComponent<TextMeshProUGUI>().SetText("room was not found");
        }
    }
}
