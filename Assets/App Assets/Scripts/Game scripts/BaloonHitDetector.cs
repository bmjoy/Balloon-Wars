using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class BaloonHitDetector : MonoBehaviour
{
    [SerializeField] private Balloon m_Balloon;
    private PhotonView m_PhotonView;

    void Start()
    {
        m_PhotonView = m_Balloon.GetComponent<PhotonView>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Dart"))
        {
            Player dartOwner = other.gameObject.GetComponent<PhotonView>().Owner;
            Player balloonOwner = m_PhotonView.Owner;

            if(dartOwner == balloonOwner)
            {
                if(m_PhotonView.IsMine)
                {
                    Debug.Log("Ignore self dart");
                }
            }
            else
            {
                if(PhotonNetwork.IsMasterClient)
                {
                    m_PhotonView.RPC("popBalloonRPC", RpcTarget.All, balloonOwner.NickName, dartOwner.NickName);
                    other.gameObject.tag = string.Empty;
                    other.GetComponent<PhotonView>().RPC("DartHitBalloonRPC", dartOwner);
                }
            }
        }
    }
}
