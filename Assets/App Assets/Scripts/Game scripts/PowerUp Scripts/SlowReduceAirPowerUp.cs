using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowReduceAirPowerUp : PowerUp
{
    public override int PowerUpTime {get;set;} = 15;

    public override void activatePowerUp(GameObject player)
    {
        Debug.Log("activate High slow reduce air PowerUp");
    }

    public override void deActivatePowerUp(GameObject player)
    {
        Debug.Log("deactivate slow rduce air PowerUp");
    }
}
