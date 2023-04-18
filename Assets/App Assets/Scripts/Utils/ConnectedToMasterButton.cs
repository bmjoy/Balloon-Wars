using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ConnectedToMasterButton : MonoBehaviour
{
    Button m_Button;
    PhotonConnector m_PhotonConnector;
    void Start()
    {
        m_Button = GetComponent<Button>();
        m_PhotonConnector = FindObjectOfType<PhotonConnector>();
        m_Button.interactable = m_PhotonConnector.IsConnectedToMaster;
        m_PhotonConnector.ConnectedToMaster += activateButton;
        m_PhotonConnector.DisConnectedFromMaster += deActivateButton;
    }
    protected void activateButton()
    {
        m_Button.interactable = true;
    }
    protected void deActivateButton()
    {
        if (!m_Button.IsDestroyed())
        {
            m_Button.interactable = false;
        }
    }
}
