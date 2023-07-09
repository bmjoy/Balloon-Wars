using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class FastAddAirPowerUp : PowerUp
{
    public override int PowerUpTime {get;set;} = 15;
    private AirTank m_AirTank;
    public override void activatePowerUp(GameObject player)
    {
        Debug.Log("activate Fast Add Air PowerUp");
        m_AirTank = player.GetComponent<PlayerMovement>().PlayerAirTank;
        m_AirTank.AddAirTime /= 2;
    }

    public override void deActivatePowerUp(GameObject player)
    {
        Debug.Log("deactivate Fast Add Air PowerUp");
        m_AirTank.AddAirTime *= 2;
    }
}
