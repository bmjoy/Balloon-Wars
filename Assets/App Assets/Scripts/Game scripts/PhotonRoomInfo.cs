using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Linq;

public class PhotonRoomInfo : MonoBehaviourPunCallbacks
{
    public int AlivePlayersAmount{get{return m_PlayersAlive.Count;}}
    public event Action<Player> PlayerWon;
    public event Action<Player> PlayerDied;
    private List<Player> m_PlayersAlive;
    private void Start() 
    {
        m_PlayersAlive = PhotonNetwork.PlayerList.ToList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"Player has left the room: {otherPlayer.NickName}");
        RemovePlayerFromGame(otherPlayer);
    }

    public void RemovePlayerFromGame(Player playerToRemove)
    {
        if (m_PlayersAlive.Contains(playerToRemove))
        {
            m_PlayersAlive.Remove(playerToRemove);
            OnPlayerDied(playerToRemove);
            if(AlivePlayersAmount == 1)
            {
                OnPlayerWon(m_PlayersAlive[0]);
            }
        }
    }

    private void OnPlayerWon(Player winningPlayer)
    {
        PlayerWon?.Invoke(winningPlayer);
        Debug.Log($"Player {winningPlayer.NickName} won!");
    }

    private void OnPlayerDied(Player player)
    {
        Debug.Log($"Player has died: {player.NickName}");
        PlayerDied?.Invoke(player);
    }
}
