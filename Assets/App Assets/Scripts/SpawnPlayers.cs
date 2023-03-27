using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    [SerializeField]private float m_minX;
    [SerializeField]private float m_maxX;
    [SerializeField]private float m_minY;
    [SerializeField]private float m_maxY;
    
    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(m_minX, m_maxX), Random.Range(m_minY, m_maxY));

        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }
}
