using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField][Range(20,100)] protected int Timeout = 30;
    abstract public int PowerUpTime { get; set; }
    public event Action<GameObject,int> PlayerHitPowerUp;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                OnHitPowerUp(other.gameObject);
            }
            if(PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    private void OnHitPowerUp(GameObject player)
    {
        PlayerHitPowerUp?.Invoke(player,PowerUpTime);
    }
    public abstract void activatePowerUp(GameObject player);
    public abstract void deActivatePowerUp(GameObject player);
}
