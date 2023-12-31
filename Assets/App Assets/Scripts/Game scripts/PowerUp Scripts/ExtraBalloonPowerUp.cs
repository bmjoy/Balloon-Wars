using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBalloonPowerUp : PowerUp
{
    public override int PowerUpTime {get;set;} = 0;
    public override void activatePowerUp(GameObject player)
    {
        Debug.Log("activate Extra balloon PowerUp");
        player.GetComponent<BalloonHolder>().AddBalloon();
    }

    public override void deActivatePowerUp(GameObject player){}
}
