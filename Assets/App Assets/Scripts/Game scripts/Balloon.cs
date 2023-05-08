using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;
using System.Linq;

public class Balloon : MonoBehaviour
{
    [SerializeField] private HingeJoint2D m_ConnectingHinge;
    public HingeJoint2D ConnectingHinge { get{return m_ConnectingHinge;} }
    private PhotonView m_PhotonView;

    public Rigidbody2D PlayerBody { get; private set; }

    public event Action<GameObject> BalloonLost;
    private AudioSource m_PopAudioSource;

    private void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
        m_PopAudioSource = GetComponent<AudioSource>();
    }

    private void OnBalloonLost()
    {
        BalloonLost?.Invoke(this.gameObject);
    }

    public void OnStringBreak()
    {
        Debug.Log("string broke");
        OnBalloonLost();
        StartCoroutine(delayExplodeBalloon(2f));
    }

    private IEnumerator delayExplodeBalloon(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponentInChildren<Animator>().SetTrigger("explode");
        Debug.Log("balloon exploded");
    }
    public void AttachToPlayer(Rigidbody2D playerBody)
    {
        PlayerBody = playerBody;
        ConnectingHinge.connectedAnchor = Vector2.zero;
        ConnectingHinge.autoConfigureConnectedAnchor = true;
        ConnectingHinge.connectedBody = playerBody;
    }

    public void FindAndAttachToPlayer()
    {
        if(m_PhotonView.IsMine)
        {
            m_PhotonView.RPC("AttachToPlayerToAll", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName);
        }
    }

    [PunRPC]
    private void AttachToPlayerToAll(string BalloonOwner)
    {
        Debug.Log($"Ataching {BalloonOwner}'s Balloon on {PhotonNetwork.LocalPlayer.NickName}'s screen");
        List<GameObject> players = GameObject.FindGameObjectsWithTag("Player").ToList();
        GameObject matchingPlayer = players.Find(Player => Player.GetComponent<PhotonView>().Owner.NickName == BalloonOwner);
        PlayerBody = matchingPlayer.GetComponent<Rigidbody2D>();
        ConnectingHinge.connectedAnchor = new Vector2(0,0);
        ConnectingHinge.autoConfigureConnectedAnchor = true;
        ConnectingHinge.connectedBody = PlayerBody;
    }

    public void DestroyBalloon()
    {
        if(m_PhotonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    public void playPopSound()
    {
        if(m_PopAudioSource != null)
        {
            m_PopAudioSource.Play();
        }
    }
}
