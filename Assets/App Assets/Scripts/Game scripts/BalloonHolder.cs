using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

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
    }

    private void generateBalloons()
    {
        for (int i = 0; i < BalloonNum; i++)
        {
            AddBalloon();
        }
    }

    private void OnBallonsFinishd()
    {
        BallonsFinishd?.Invoke();
    }

    public void AddBalloon()
    {
        if(m_PhotonView.IsMine)
        {
            Vector3 playerPos = gameObject.transform.position;
            Vector3 balloonPos = new Vector3(playerPos.x, playerPos.y + 2.5f, playerPos.z);
            GameObject balloon = PhotonNetwork.Instantiate(m_BalloonPrefab.name, balloonPos, Quaternion.identity);
            Balloon balloonScript = balloon.GetComponent<Balloon>();
            balloonScript.AttachToPlayer();
            m_Balloons.Add(balloon);
            balloonScript.BalloonLost += OnBalloonLost;
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
