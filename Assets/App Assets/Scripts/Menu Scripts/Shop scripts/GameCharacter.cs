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
            m_PhotonView.RPC("changeGameCharacter", RpcTarget.AllBuffered,
            CharacterItemChooser.Instance.CurCharacter.ToString(),
            CharacterItemChooser.Instance.CurHat.ToString(),
            CharacterItemChooser.Instance.CurWeapon.ToString(),
            CharacterItemChooser.Instance.CurShirt.ToString(),
            CharacterItemChooser.Instance.CurPants.ToString());
        }
    }

    [PunRPC]
    private void changeGameCharacter(string character, string hat, string weapon, string shirt, string pants)
    {
        m_SkinChanger.changeItem(Skins.Catagories.Character, character);
        m_SkinChanger.changeItem(Skins.Catagories.Hat, hat);
        m_SkinChanger.changeItem(Skins.Catagories.Weapon, weapon);
        m_SkinChanger.changeItem(Skins.Catagories.Shirt, shirt);
        m_SkinChanger.changeItem(Skins.Catagories.Pants, pants);
    }
}