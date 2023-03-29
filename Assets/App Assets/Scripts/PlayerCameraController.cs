using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using Photon.Realtime;

public class PlayerCameraController : MonoBehaviour, IPunOwnershipCallbacks
{
    private CinemachineVirtualCamera m_VirtualCamera = null;
    private Transform m_PlayerTransform;
    private PhotonView m_View;

    private void Awake()
    {
         m_View = GetComponent<PhotonView>();
    }


    private void Start() 
    {
        if(m_View.IsMine)
        {
            m_PlayerTransform = GetComponent<Transform>();
            m_VirtualCamera = GameObject.FindAnyObjectByType<CinemachineVirtualCamera>();
            m_VirtualCamera.Follow = m_PlayerTransform;
            m_VirtualCamera.LookAt = m_PlayerTransform;
        }
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if(targetView.IsMine)
        {
            m_VirtualCamera.gameObject.SetActive(true);
            enabled = true;
        }
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        if(targetView.IsMine)
        {
            m_VirtualCamera.gameObject.SetActive(true);
            enabled = true;
        }
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        
    }
}
