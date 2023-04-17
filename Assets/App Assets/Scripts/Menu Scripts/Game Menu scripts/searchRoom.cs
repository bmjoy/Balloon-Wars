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
        m_PhotonRoomsConnector.failedConnectToRoom += OnFailedToFindActiveRoom;
    }

    public void searchRoomInTextBox()
    {
        string roomName = m_InputField.text;
        m_InputField.text = string.Empty;
        if(string.IsNullOrEmpty(roomName))
        {
            Debug.Log($"Empty room name");
            m_InputField.placeholder.GetComponent<TextMeshProUGUI>().SetText("Empty room name!");
        }
        else
        {
            Debug.Log($"Looking for room named {roomName}");
            m_PhotonRoomsConnector.JoinPhotonRoom(roomName);
        }
    }

    private void OnFailedToFindActiveRoom(string failMsg)
    {
        m_InputField.placeholder.GetComponent<TextMeshProUGUI>().SetText(failMsg);
    }
}
