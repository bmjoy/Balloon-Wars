using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BalloonTop : MonoBehaviour
{
    [SerializeField] private Balloon m_Balloon;
    private string[] m_Colors = {"ColorRed", "ColorBlue", "ColorYellow", "ColorPurple", "ColorPink"};
    private SpriteRenderer m_SpriteRenderer;
    private PhotonView m_PhotonView;
    private PhotonView m_BalloonPhotonView;
    private bool popped = false;
    private string m_regularColor;
    
    private void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();    
    }
    private void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_BalloonPhotonView = m_Balloon.GetComponent<PhotonView>();
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
        m_regularColor = m_Colors[Random.Range(0, m_Colors.Length)];
        m_PhotonView.RPC("setColorRPC", RpcTarget.AllBuffered, m_regularColor);
    }

    [PunRPC]
    private void setColorRPC(string colorTrigger)
    {
        GetComponent<Animator>().SetTrigger(colorTrigger);
        Debug.Log("color changed to " + colorTrigger);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (m_PhotonView.IsMine)
        {
            if (collision.gameObject.CompareTag("Trap"))
            {
                if (!popped && m_Balloon.MetalicAmout == 0)
                {
                    popped = true;
                    m_BalloonPhotonView.RPC("TrapPopBalloonRPC", RpcTarget.All, m_PhotonView.Owner.NickName);
                }
            }
        }
    }

    internal void SetMetalicColor()
    {
        m_PhotonView.RPC("setColorRPC", RpcTarget.AllBuffered, "ColorMetal");
    }

    internal void SetRegularColor()
    {
        m_PhotonView.RPC("setColorRPC", RpcTarget.AllBuffered, m_regularColor);
    }
}
