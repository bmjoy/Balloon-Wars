using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Realtime;
using TMPro;
using System.Linq;

public class RoomsViewList : MonoBehaviour
{
    [SerializeField] private GameObject m_RoomsScrollViewContent;
    [SerializeField] private TextMeshProUGUI m_NoRoomsAvailableTxt;
    [SerializeField] private GameObject m_UiRoomPrefab;
    private PhotonRoomsConnector m_PhotonRoomsConnector;
    public event Action<string> SelectedRoomChanged;
    public string SelectedRoom { get; private set; } = null;

    public void OnSelectedRoomChanged(string selectdRoom)
    {
        SelectedRoom = selectdRoom;
        SelectedRoomChanged?.Invoke(selectdRoom);
    }
    
    void Start()
    {
        m_PhotonRoomsConnector = FindObjectOfType<PhotonRoomsConnector>();
        m_PhotonRoomsConnector.RoomListChanged += UpdateRoomsList;
        UpdateRoomsList(m_PhotonRoomsConnector.RoomList);
    }

    private void UpdateRoomsList(List<RoomInfo> roomList)
    {
        if(roomList == null)
        {
            return;
        }

        Debug.Log("updating list");
        roomList = roomList.Where(room => room.IsOpen && room.IsVisible && room.PlayerCount < room.MaxPlayers).ToList();
        roomList = roomList.OrderBy( room => room.Name).ToList();
        m_NoRoomsAvailableTxt.gameObject.SetActive(roomList.Count == 0);

        if(SelectedRoom !=null && !roomList.Any(room => room.Name == SelectedRoom))
        {
            OnSelectedRoomChanged(null);
        }

        foreach (Transform child in m_RoomsScrollViewContent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (RoomInfo room in roomList)
        {
            Debug.Log($"adding room {room.Name} to listView");
            GameObject listItem = Instantiate(m_UiRoomPrefab, m_RoomsScrollViewContent.transform);
            listItem.GetComponentInChildren<TextMeshProUGUI>().SetText(room.Name);
            listItem.gameObject.SetActive(true);
            listItem.GetComponent<UIRoom>().Clicked += OnSelectedRoomChanged;
        }
    }
}
