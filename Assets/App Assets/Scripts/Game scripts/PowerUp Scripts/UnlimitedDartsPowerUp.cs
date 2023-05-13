using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlimitedDartsPowerUp : PowerUp
{
    public override int PowerUpTime {get;set;} = 10;

    public override void activatePowerUp(GameObject player)
    {
        Debug.Log("activate Unlimited darts PowerUp");
    }

    public override void deActivatePowerUp(GameObject player)
    {
        Debug.Log("deactivate Unlimited darts PowerUp");
    }
}
