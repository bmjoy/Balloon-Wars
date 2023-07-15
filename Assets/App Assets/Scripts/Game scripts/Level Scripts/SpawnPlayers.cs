using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerPrefab;
    [SerializeField] private List<Vector2> m_SpawnPositions;
    private PhotonView m_PhotonView;

    void Start()
    {
        m_PhotonView = GetComponent<PhotonView>();
        if(PhotonNetwork.IsMasterClient && m_PhotonView.IsMine)
        {
            Debug.Log("gonna spawn playeres");
            m_SpawnPositions.Shuffle();
            SpawnAllPlayers();
        }
    }

    private void SpawnAllPlayers()
    {
        int i = 0;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            m_PhotonView.RPC("SpawnPlayerRPC", player, m_SpawnPositions[i]);
            i = i + 1 < m_SpawnPositions.Count ? i + 1 : 0;
        }
    }

    [PunRPC]
    public void SpawnPlayerRPC(Vector2 position)
    {
        PhotonNetwork.Instantiate(m_PlayerPrefab.name, position, Quaternion.identity);
        Debug.Log($"Spawned player");
    }
}

public static class ShuffleListExtension
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
