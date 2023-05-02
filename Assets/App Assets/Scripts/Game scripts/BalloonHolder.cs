using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class BalloonHolder : MonoBehaviour
{
    [SerializeField] private GameObject m_BalloonPrefab;
    [SerializeField] private List<GameObject> m_Balloons;
    public int BalloonAmount { get{ return m_Balloons.Count;}}

    public event Action BallonsFinishd;

    private void Start() 
    {
        foreach (GameObject balloon in m_Balloons)
        {
            balloon.GetComponent<Balloon>().BalloonLost += OnBalloonLost;
        }
    }

    private void OnBallonsFinishd()
    {
        BallonsFinishd?.Invoke();
    }

    public void AddBalloon()
    {
        GameObject balloon = PhotonNetwork.Instantiate(m_BalloonPrefab.name, gameObject.transform.position, Quaternion.identity);
        Balloon balloonScript = balloon.GetComponent<Balloon>();
        balloonScript.ConnectingHinge.connectedBody = GetComponent<Rigidbody2D>();
        m_Balloons.Add(balloon);
        balloonScript.BalloonLost += OnBalloonLost;
    }

    public void OnBalloonLost(GameObject balloon)
    {
        if(balloon != null)
        {
            m_Balloons.Remove(balloon);
        }
        m_Balloons.RemoveAll(balloon => balloon == null);

        Debug.Log($"{BalloonAmount} balloons left");
        if(BalloonAmount == 0)
        {
            OnBallonsFinishd();
            Debug.Log("No Ballons left");
        }

    }

}
