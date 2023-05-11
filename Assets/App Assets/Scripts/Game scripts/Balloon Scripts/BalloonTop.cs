using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BalloonTop : MonoBehaviour
{
    [SerializeField] Balloon m_Balloon;
    string[] m_Colors = {"ColorRed", "ColorBlue", "ColorYellow", "ColorPurple", "ColorPink"};
    SpriteRenderer m_SpriteRenderer;
    PhotonView m_PhotonView;
    private void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_PhotonView = GetComponent<PhotonView>();
        if(m_PhotonView.IsMine)
        {
            setRandomColor();
        }
    }
    public void OnStartExploding()
    {
        m_Balloon.playPopSound();
    }    
    public void OnExploded()
    {
        m_Balloon.DestroyBalloon();
    }

    private void setRandomColor()
    {
        int colorIndex = Random.Range(0, m_Colors.Length);
        m_PhotonView.RPC("setColorRPC", RpcTarget.AllBuffered, m_Colors[colorIndex]);
    }

    [PunRPC]
    private void setColorRPC(string colorTrigger)
    {
        GetComponent<Animator>().SetTrigger(colorTrigger);
        Debug.Log("color changed to " + colorTrigger);
    }
}
