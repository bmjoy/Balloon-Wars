using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighJumpPowerUp : PowerUp
{
    public override int PowerUpTime {get;set;} = 20;
    private float m_JumpForceToAdd = 20f;
    private PlayerMovement m_PlayerMovement;
    public override void activatePowerUp(GameObject player)
    {
        Debug.Log("activate High Jump PowerUp");
        m_PlayerMovement = player.GetComponent<PlayerMovement>();
        m_PlayerMovement.JumpForce += m_JumpForceToAdd;
    }

    public override void deActivatePowerUp(GameObject player)
    {
        Debug.Log("deactivate High Jump PowerUp");
        m_PlayerMovement.JumpForce -= m_JumpForceToAdd;
    }
}
