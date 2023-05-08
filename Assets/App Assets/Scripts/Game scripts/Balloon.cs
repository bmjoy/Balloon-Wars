using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;
using System.Linq;

public class Balloon : MonoBehaviour
{
    [SerializeField] private FixedJoint2D m_ConnectingJoint;
    public FixedJoint2D ConnectingJoint { get{return m_ConnectingJoint;} }
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
        OnBalloonLost();
        m_PhotonView.RPC("popBalloon", RpcTarget.All);
    }

    [PunRPC]
    private void popBalloon()
    {
        Debug.Log($"{m_PhotonView.Owner.NickName}'s string broke");
        if(ConnectingJoint != null)
        {
            ConnectingJoint.attachedRigidbody.AddForce(Vector2.up * 1.4f);
        }
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
        ConnectingJoint.connectedBody = playerBody;
        ConnectingJoint.autoConfigureConnectedAnchor = false;
        ConnectingJoint.connectedAnchor = Vector2.zero;
    }

    public void FindAndAttachToPlayer()
    {
        if(m_PhotonView.IsMine)
        {
            m_PhotonView.RPC("AttachToPlayerToAll", RpcTarget.AllBuffered, m_PhotonView.Owner.NickName);
        }
    }

    [PunRPC]
    private void AttachToPlayerToAll(string BalloonOwner)
    {
        Debug.Log($"Ataching {BalloonOwner}'s Balloon on {PhotonNetwork.LocalPlayer.NickName}'s screen");
        List<GameObject> players = GameObject.FindGameObjectsWithTag("Player").ToList();
        GameObject matchingPlayer = players.Find(
            Player => Player.GetComponent<PhotonView>().Owner.NickName == BalloonOwner);
        PlayerBody = matchingPlayer.GetComponent<Rigidbody2D>();
        AttachToPlayer(PlayerBody);
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
