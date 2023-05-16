using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SlowReduceAirPowerUp : PowerUp
{
    public override int PowerUpTime {get;set;} = 15;
    private AirTank m_AirTank;
    public override void activatePowerUp(GameObject player)
    {
        Debug.Log("activate Slow reduce air PowerUp");
        m_AirTank = player.GetComponent<PlayerMovement>().PlayerAirTank;
        m_AirTank.ReduceAirTime *= 2;
    }

    public override void deActivatePowerUp(GameObject player)
    {
        Debug.Log("deactivate slow rduce air PowerUp");
        m_AirTank.ReduceAirTime /= 2; 
    }
}
