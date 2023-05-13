using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastAddAirPowerUp : PowerUp
{
    public override int PowerUpTime {get;set;} = 15;

    public override void activatePowerUp(GameObject player)
    {
        Debug.Log("activate Fast Add Air PowerUp");
    }

    public override void deActivatePowerUp(GameObject player)
    {
        Debug.Log("deactivate Fast Add Air PowerUp");
    }
}
