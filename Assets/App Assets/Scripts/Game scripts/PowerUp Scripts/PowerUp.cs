using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField][Range(20,100)] protected int Timeout = 30;
    abstract public int PowerUpTime { get; set; }
    public event Action<GameObject,int,Action<GameObject>,Action<GameObject>> PlayerHitPowerUp;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            OnHitPowerUp(other.gameObject, activatePowerUp, deActivatePowerUp);
        }
    }

    private void OnHitPowerUp(GameObject player, Action<GameObject> activatePowerUp, Action<GameObject> deActivatePowerUp)
    {
        PlayerHitPowerUp?.Invoke(player, PowerUpTime, activatePowerUp, deActivatePowerUp);
    }
    
    public abstract void activatePowerUp(GameObject player);
    public abstract void deActivatePowerUp(GameObject player);
}
