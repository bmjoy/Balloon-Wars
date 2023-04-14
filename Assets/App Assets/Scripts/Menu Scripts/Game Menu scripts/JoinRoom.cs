using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JoinRoom : MonoBehaviour
{
    [SerializeField] private Button m_JoinRoomButton;
    [SerializeField] private TextMeshProUGUI m_RoomNameText;
    [SerializeField] private TextMeshProUGUI m_RoomNameHeader;
    [SerializeField] private RoomsViewList m_RoomsViewList;

    private void Start() 
    {
        m_RoomsViewList.SelectedRoomChanged += setComponentsToNewRoom;
    }

    private void setComponentsToNewRoom(string roomName)
    {
        bool isRoomAvailable = !string.IsNullOrEmpty(roomName);
        Debug.Log($"room named {roomName} was chosen, is room available ={isRoomAvailable}");
        m_RoomNameText.SetText(isRoomAvailable ? roomName : string.Empty);
        m_JoinRoomButton.interactable = isRoomAvailable;
        m_RoomNameHeader.gameObject.SetActive(isRoomAvailable);
    }
}
