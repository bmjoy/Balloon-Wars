using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighJumpPowerUp : PowerUp
{
    public override int PowerUpTime {get;set;} = 20;

    public override void activatePowerUp(GameObject player)
    {
        Debug.Log("activate High Jump PowerUp");
    }

    public override void deActivatePowerUp(GameObject player)
    {
        Debug.Log("deactivate High Jump PowerUp");
    }
}
