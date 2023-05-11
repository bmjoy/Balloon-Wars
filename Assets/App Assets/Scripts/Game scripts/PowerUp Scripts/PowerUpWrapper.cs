using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUpWrapper : MonoBehaviour
{
    [SerializeField] List<GameObject> m_PowerUpPrefabs;
    PowerUp m_CurPowerUp;
    private void generateNewPowerUp()
    {

    }
    
    private void destroyOldPowerUp()
    {

    }

    private void ActivatePowerUp(GameObject player, int PowerUpTime)
    {
        StartCoroutine(StartPowerUpForTime(player, PowerUpTime));
    }

    private IEnumerator StartPowerUpForTime(GameObject player, int time)
    {
        m_CurPowerUp.activatePowerUp(player);
        yield return new WaitForSeconds(time);
        m_CurPowerUp.deActivatePowerUp(player);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
