using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class PlayerAmountTXT : MonoBehaviour
{
    private int m_PlayerAmount;
    private TextMeshProUGUI m_AmountText;
    void Start()
    {
        m_PlayerAmount = PhotonNetwork.CurrentRoom.PlayerCount;
        m_AmountText = GetComponent<TextMeshProUGUI>();
        FindObjectOfType<PhotonRoomInfo>().PlayerDied += ReducePlayerAmount;
        setTextToPlayerAmount();
    }

    private void ReducePlayerAmount(Player deadPlayer)
    {
        Debug.Log($"Reducing player {deadPlayer.NickName}");
        m_PlayerAmount--;
        setTextToPlayerAmount();
    }

    private void setTextToPlayerAmount()
    {
        m_AmountText.SetText($"× {m_PlayerAmount}");
    }
}
