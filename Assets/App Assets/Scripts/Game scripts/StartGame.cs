using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class StartGame : MonoBehaviourPunCallbacks
{
    [SerializeField] private SceneNavigator m_SceneNavigator;
    private PhotonView m_PhotonView;

    private void Start()   
    {
        m_PhotonView = GetComponent<PhotonView>();
    }
    public void StartGameForAll()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.IsOpen)
        {
            Debug.Log("Starting the game for all players in room");
            PhotonNetwork.CurrentRoom.IsOpen = false;
            photonView.RPC("RPC_StartGame", RpcTarget.All);
        }
    }

    [PunRPC]
    void RPC_StartGame()
    {
        Debug.Log("The master player started the game");
        m_SceneNavigator.LoadGameLevel((int)PhotonNetwork.CurrentRoom.CustomProperties["Level"]);
    }
}
