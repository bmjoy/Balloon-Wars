using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SlowReduceAirPowerUp : PowerUp
{
    public override int PowerUpTime {get;set;} = 15;
    private AirTank m_AirTank;
    private float m_AirToReduce = 15f;
    public override void activatePowerUp(GameObject player)
    {
        Debug.Log("activate Slow reduce air PowerUp");
        m_AirTank = player.GetComponent<PlayerMovement>().PlayerAirTank;
        m_AirTank.ReduceAirSpeed = m_AirTank.ReduceAirSpeed <= 10f? 5f : m_AirTank.ReduceAirSpeed - m_AirToReduce;
    }

    public override void deActivatePowerUp(GameObject player)
    {
        Debug.Log("deactivate slow rduce air PowerUp");
        m_AirTank.ReduceAirSpeed = m_AirTank.ReduceAirSpeed < 10f? 10f : m_AirTank.ReduceAirSpeed + m_AirToReduce; 
    }
}
