using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class PowerUpWrapper : MonoBehaviour
{
    [SerializeField] List<GameObject> m_PowerUpPrefabs;
    [SerializeField][Range(10,60)] int m_PowerUpCoolDown = 30;
    private GameObject m_CurPowerUp;
    private PhotonView m_PhotonView;
    private void Start()
    {
        m_PhotonView = GetComponent<PhotonView>();
        generateNewPowerUp();
    }

    private void generateNewPowerUp()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            string randomPowerName = m_PowerUpPrefabs[UnityEngine.Random.Range(0,m_PowerUpPrefabs.Count)].name;
            PhotonNetwork.Instantiate(randomPowerName,this.transform.position,Quaternion.identity);
        }
    }

    private void destroyOldPowerUp()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(m_CurPowerUp);
        }
    }

    private void ActivatePowerUp(GameObject player, int PowerUpTime, Action<GameObject> activate, Action<GameObject> deactivate)
    {
        StartCoroutine(StartPowerUpForTime(player, PowerUpTime, activate, deactivate));
        m_PhotonView.RPC("destroyOldPowerUp",RpcTarget.MasterClient);
    }

    private IEnumerator StartPowerUpForTime(GameObject player, int time, Action<GameObject> activate, Action<GameObject> deactivate)
    {
        activate(player);
        yield return new WaitForSeconds(time);
        deactivate(player);
    }

    [PunRPC]
    private void RespawnPowerUp()
    {
        StartCoroutine(RespownPowerUpAfterTime(m_PowerUpCoolDown));
    }

    private IEnumerator RespownPowerUpAfterTime(int time)
    {
        destroyOldPowerUp();
        yield return new WaitForSeconds(time);
        generateNewPowerUp();
    }
}