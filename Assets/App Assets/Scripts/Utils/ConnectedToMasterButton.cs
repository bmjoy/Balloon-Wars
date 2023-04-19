using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ConnectedToMasterButton : MonoBehaviour
{
    Button m_Button;
    void Start()
    {
        m_Button = GetComponent<Button>();
        m_Button.interactable = PhotonConnector.Instance.IsConnectedToMaster;
        PhotonConnector.Instance.ConnectedToMaster += activateButton;
        PhotonConnector.Instance.DisConnectedFromMaster += deActivateButton;
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
