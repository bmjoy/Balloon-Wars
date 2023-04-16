using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateRoom : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_RoomNameTxt;
    [SerializeField] PhotonRoomsConnector m_PhotonRoomsConnector;
    [SerializeField] MapChooser m_MapChooser;
    [SerializeField] Slider m_PlayersSlider;
    [SerializeField] Toggle m_IsPrivateToggle;

    public void CreateRoomButtonClicked()
    {
        string roomName = m_RoomNameTxt.text;
        int level = m_MapChooser.CurMapIndex;
        int playersAmount = (int)m_PlayersSlider.value;
        bool isVisible = !m_IsPrivateToggle.isOn;
        m_PhotonRoomsConnector.CreatePhotonRoom(roomName, isVisible, playersAmount, level);
    }
}
