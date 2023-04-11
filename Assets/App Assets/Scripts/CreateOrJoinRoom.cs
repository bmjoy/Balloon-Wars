using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateOrJoinRoom : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_RoomNameTxt;
    [SerializeField] PhotonRoomsConnector m_PhotonRoomsConnector;

    public void CreateOrJoinPhotonRoom()
    {
        m_PhotonRoomsConnector.CreatePhotonRoom(m_RoomNameTxt.text);
    }
}
