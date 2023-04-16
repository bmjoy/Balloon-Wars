using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class roomNameGenerator : MonoBehaviour
{
    const string charset = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    [SerializeField] private PhotonRoomsConnector m_PhotonRoomsConnector;
    [SerializeField] private TextMeshProUGUI m_RoomNameLabel;

    private string GenerateRandomRoomName()
    {
        Debug.Log("generating name");
        string roomName;
        do
        {
            System.Random random = new System.Random();
            roomName = new string(Enumerable.Repeat(charset, 7)
                                           .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        while (m_PhotonRoomsConnector.RoomList.Any(room => room.Name == roomName));
        Debug.Log($"generated name: {roomName}");

        return roomName;
    }

    public void setRoomName()
    {
        m_RoomNameLabel.SetText(GenerateRandomRoomName());
    }
}
