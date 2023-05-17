using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalBalloonPowerUp : PowerUp
{
    public override int PowerUpTime {get;set;} = 10;
    private BalloonHolder m_BalloonHolder;
    public override void activatePowerUp(GameObject player)
    {
        m_BalloonHolder = player.GetComponent<BalloonHolder>();
        Debug.Log("activate metal balloons PowerUp");
        m_BalloonHolder.foreachBalloon(balloon => balloon.MakeMetalic());
    }

    public override void deActivatePowerUp(GameObject player)
    {
        Debug.Log("activate metal balloons PowerUp");
        m_BalloonHolder.foreachBalloon(balloon => balloon.UnMakeMetalic());
    }
}
