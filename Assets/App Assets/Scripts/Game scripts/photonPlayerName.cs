using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class photonPlayerName : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private TextMeshProUGUI m_NameText;

    private void Awake()
    {
        m_NameText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        string username = photonView.Owner.NickName;
        m_NameText.SetText(username);
    }
}
