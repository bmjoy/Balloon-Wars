using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayersViewList : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayersScrollViewContent;
    [SerializeField] private GameObject m_UiPlayerPrefab;
    private PhotonRoomsConnector m_PhotonRoomsConnector;
    
    void Start()
    {
        m_PhotonRoomsConnector = FindObjectOfType<PhotonRoomsConnector>();
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            addPlayerToUIList(player);
        }
        m_PhotonRoomsConnector.PlayerAddedToList += addPlayerToUIList;
        m_PhotonRoomsConnector.PlayerRemovedFromList += removePlayerFromUIList;

    }

    private void addPlayerToUIList(Player playerToAdd)
    {
        Debug.Log($"Adding player {playerToAdd.NickName} to listView");
        GameObject listItem = Instantiate(m_UiPlayerPrefab, m_PlayersScrollViewContent.transform);
        listItem.GetComponentInChildren<TextMeshProUGUI>().SetText(playerToAdd.NickName);
        listItem.gameObject.SetActive(true);
    }

    private void removePlayerFromUIList(Player playerToRemove)
    {
        Debug.Log($"Removing player {playerToRemove.NickName} from listView");
        foreach(Transform listItem in m_PlayersScrollViewContent.transform)
        {
            TextMeshProUGUI nameTxt = listItem.GetComponentInChildren<TextMeshProUGUI>();
            if( nameTxt != null && nameTxt.text == playerToRemove.NickName)
            {
                Destroy(listItem.gameObject);
            }
        }
    }
}
