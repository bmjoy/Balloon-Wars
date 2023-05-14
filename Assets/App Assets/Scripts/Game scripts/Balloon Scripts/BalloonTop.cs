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
    PhotonView m_BalloonPhotonView;
    private bool popped = false;

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
        int colorIndex = Random.Range(0, m_Colors.Length);
        m_PhotonView.RPC("setColorRPC", RpcTarget.AllBuffered, m_Colors[colorIndex]);
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
                if (!popped)
                {
                    popped = true;
                    m_BalloonPhotonView.RPC("TrapPopBalloonRPC", RpcTarget.All, m_PhotonView.Owner.NickName);
                }
            }
        }
    }
}
