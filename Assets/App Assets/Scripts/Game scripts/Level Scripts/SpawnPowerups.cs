using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;

public class SpawnPowerups : MonoBehaviourPunCallbacks
{
    // public GameObject m_PlayerPrefab;
    // [SerializeField] public List<Vector2> m_SpawnPositions;
    // private Dictionary<Vector2, float> disabledPositions = new Dictionary<Vector2, float>();
    // [SerializeField] private float m_DisableTime = 5f;

    // void Start()
    // {
    //     Debug.Log("gonna spawn player");
    //     SpawnPlayer();
    // }

    // // Function to get a random non-disabled position
    // private Vector2 GetRandomSpawnPosition()
    // {
    //     // Shuffle the spawn position list
    //     m_SpawnPositions.Shuffle();

    //     // Loop through the shuffled positions
    //     foreach (Vector2 position in m_SpawnPositions)
    //     {
    //         // Check if the position is currently disabled
    //         if (disabledPositions.ContainsKey(position))
    //         {
    //             // Check if the disable time has passed
    //             if (Time.time > disabledPositions[position] + m_DisableTime)
    //             {
    //                 // Remove the position from the disabled list
    //                 disabledPositions.Remove(position);

    //                 // Return the position
    //                 return position;
    //             }
    //         }
    //         else
    //         {
    //             // Return the position
    //             return position;
    //         }
    //     }

    //     // If all positions are disabled, return the first position in the list
    //     return m_SpawnPositions[0];
    // }

    // // Function to disable a spawn position for a certain amount of time
    // private void DisableSpawnPosition(Vector2 position)
    // {
    //     // Add the position and disable time to the disabledPositions dictionary
    //     disabledPositions[position] = Time.time;
    // }

    // public void SpawnPlayer()
    // {
        
    //     Vector2 freePosition = GetRandomSpawnPosition();

    //     PhotonNetwork.Instantiate(m_PlayerPrefab.name, freePosition, Quaternion.identity);
    //     Debug.Log($"Spawnd player");
    // }

    // public void RespawnPlayer()
    // {
    //     SpawnPlayer();
    // }
}