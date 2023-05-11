using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerBalloonJoiner : MonoBehaviour
{
    [SerializeField] Balloon m_Balloon;
    private PhotonView m_PhotonView;
    private void Start()
    {
        m_PhotonView = m_Balloon.GetComponent<PhotonView>();
    }

    private void OnJointBreak2D(Joint2D brokenJoint)
    {
        Debug.Log($"{m_PhotonView.Owner.NickName}'s string is braking");
        if(m_PhotonView.IsMine)
        {
            Debug.Log("breakingForAll");
            m_PhotonView.RPC("BreakStringForAll", RpcTarget.Others);
        }
        m_Balloon.OnStringBreak();    
    }
}
