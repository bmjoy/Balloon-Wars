using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameCharacter : MonoBehaviour
{
    private SkinChanger m_SkinChanger;
    private PhotonView m_PhotonView;

    private void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
        m_SkinChanger = GetComponentInChildren<SkinChanger>();
    }

    void Start()
    {
        if (m_PhotonView.IsMine)
        {
            m_PhotonView.RPC("changeGameCharacter", RpcTarget.AllBuffered, CharacterChooser.Instance.CurCharacterIndex);
        }
    }

    [PunRPC]
    private void changeGameCharacter(int characterNumber)
    {
        m_SkinChanger.changeCharacter(characterNumber);
    }
}
