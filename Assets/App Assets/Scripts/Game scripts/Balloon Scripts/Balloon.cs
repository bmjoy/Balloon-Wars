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
    [SerializeField] [Range(0.5f, 3f)] private float m_JointBreakForce = 1.3f;
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
        Debug.Log($"{m_PhotonView.Owner.NickName}'s string broke");
        OnBalloonLost();
        StartCoroutine(delayExplodeBalloon(2f));
    }

    public void OnBalloonHitTrap()
    {
        Debug.Log($"{m_PhotonView.Owner.NickName}'s Balloon hit trap");
        OnBalloonLost();
        StartCoroutine(delayExplodeBalloon(0f));        
    }

    [PunRPC]
    private void BreakStringForAll()
    {
        Debug.Log("break for all called");
        if(ConnectingJoint != null && ConnectingJoint.attachedRigidbody != null)
        {
            ConnectingJoint.attachedRigidbody.AddForce(Vector2.up * 1.4f);
        }    
    }

    private IEnumerator delayExplodeBalloon(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponentInChildren<Animator>().SetTrigger("explode");
        Debug.Log("balloon exploded");
    }

    [PunRPC]
    private void DartPopBalloonRPC(string BalloonOwnerName, string DartOwnerName)
    {
        Debug.Log($"{BalloonOwnerName}'s balloon was hit by {DartOwnerName}'s dart");
        popBalloon();
    }

    [PunRPC]
    private void TrapPopBalloonRPC(string BalloonOwnerName)
    {
        Debug.Log($"{BalloonOwnerName}'s balloon was hit by a trap");
        popBalloon();
    }

    private void popBalloon()
    {
        OnBalloonLost();
        GetComponentInChildren<Animator>().SetTrigger("explode");
        Debug.Log("Balloon exploded");
    }

    public void AttachBalloonToPlayer(Rigidbody2D playerBody)
    {
        PlayerBody = playerBody;
        ConnectingJoint.connectedBody = playerBody;
        ConnectingJoint.autoConfigureConnectedAnchor = false;
        ConnectingJoint.connectedAnchor = Vector2.zero;
        ConnectingJoint.breakForce = m_JointBreakForce;
    }

    public void AttachBalloonToOwnerForAll()
    {
        if(m_PhotonView.IsMine)
        {
            m_PhotonView.RPC("findOwnerAndAttachBalloon", RpcTarget.AllBuffered, m_PhotonView.Owner.NickName);
        }
    }

    [PunRPC]
    private void findOwnerAndAttachBalloon(string BalloonOwner)
    {
        Debug.Log($"Ataching {BalloonOwner}'s Balloon on {PhotonNetwork.LocalPlayer.NickName}'s screen");
        List<GameObject> players = GameObject.FindGameObjectsWithTag("Player").ToList();
        GameObject matchingPlayer = players.Find(
            Player => Player.GetComponent<PhotonView>().Owner.NickName == BalloonOwner);
        AttachBalloonToPlayer(matchingPlayer.GetComponent<Rigidbody2D>());
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
