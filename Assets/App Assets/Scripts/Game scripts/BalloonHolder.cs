using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.Linq;

public class BalloonHolder : MonoBehaviour
{
    [SerializeField] private GameObject m_BalloonPrefab;
    private List<GameObject> m_Balloons = new List<GameObject>();
    private PhotonView m_PhotonView;
    [SerializeField][Range(1,5)] private int BalloonNum = 3;
    public int BalloonsLeft { get{ return m_Balloons.Count;}}

    public event Action BallonsFinishd;

    private void Start() 
    {
        m_PhotonView = GetComponent<PhotonView>();
        if (m_PhotonView.IsMine)
        {
            generateBalloons();
        }
        atachBalloonsToPlayer();
    }

    private void generateBalloons()
    {
        for (int i = 0; i < BalloonNum; i++)
        {
            CreateBalloon();
        }
    }

    private void OnBallonsFinishd()
    {
        BallonsFinishd?.Invoke();
    }

    public void CreateBalloon()
    {
        if(m_PhotonView.IsMine)
        {
            Vector3 playerPos = gameObject.transform.position;
            Vector3 balloonPos = new Vector3(playerPos.x, playerPos.y + 2.5f, playerPos.z);
            GameObject balloon = PhotonNetwork.Instantiate(m_BalloonPrefab.name, balloonPos, Quaternion.identity);
            balloon.GetComponent<Balloon>().BalloonLost += OnBalloonLost;
            m_Balloons.Add(balloon);
        }
    }

    public void atachBalloonsToPlayer()
    {
        string playerOwner = m_PhotonView.Owner.NickName;
        List<GameObject> balloons = GameObject.FindGameObjectsWithTag("Balloon").ToList();
        balloons.RemoveAll(Balloon => Balloon.GetComponent<PhotonView>().Owner.NickName != playerOwner);
        Debug.Log($"Ataching {balloons.Count} balloons to {playerOwner}");
        foreach(GameObject balloon in balloons)
        {
            balloon.GetComponent<Balloon>().AttachToPlayer(GetComponent<Rigidbody2D>());
        }
    }

    public void OnBalloonLost(GameObject balloon)
    {
        if(balloon != null)
        {
            m_Balloons.Remove(balloon);
        }
        m_Balloons.RemoveAll(balloon => balloon == null);

        Debug.Log($"{BalloonsLeft} balloons left");
        if(BalloonsLeft == 0)
        {
            OnBallonsFinishd();
            Debug.Log("No Ballons left");
        }
    }
}
