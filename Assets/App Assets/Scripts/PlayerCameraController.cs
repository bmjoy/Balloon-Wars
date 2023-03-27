using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using Photon.Realtime;

public class PlayerCameraController : MonoBehaviour, IPunOwnershipCallbacks
{
    [SerializeField] private CinemachineVirtualCamera m_VirtualCamera = null;

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
