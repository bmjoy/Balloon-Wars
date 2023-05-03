using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class Balloon : MonoBehaviour
{
    [SerializeField] private HingeJoint2D m_ConnectingHinge;
    public HingeJoint2D ConnectingHinge { get{return m_ConnectingHinge;} }
    private PhotonView m_PhotonView;
    public event Action<GameObject> BalloonLost;
    private AudioSource m_PopAudioSource;

    void Start()
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

    public void DestroyBalloon()
    {
        if(m_PhotonView.IsMine)
        {
            // foreach(Transform child in gameObject.transform)
            // {
            //     PhotonNetwork.Destroy(child.gameObject);    
            // }
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
