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
            GameObject powerUp = PhotonNetwork.Instantiate(randomPowerName,this.transform.position,Quaternion.identity);
            m_PhotonView.RPC("registerToPowerUp", RpcTarget.All, powerUp.GetComponent<PhotonView>().ViewID);
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
        destroyOldPowerUp();
        if(player.GetComponent<PhotonView>().IsMine)
        {
            StartCoroutine(StartPowerUpForTime(player, PowerUpTime, activate, deactivate));            
        }
        StartCoroutine(RespownPowerUpAfterTime(m_PowerUpCoolDown));
    }

    private IEnumerator StartPowerUpForTime(GameObject player, int time, Action<GameObject> activate, Action<GameObject> deactivate)
    {
        activate(player);
        yield return new WaitForSeconds(time);
        deactivate(player);
    }

    private IEnumerator RespownPowerUpAfterTime(int time)
    {
        yield return new WaitForSeconds(time);
        generateNewPowerUp();
    }

    [PunRPC]
    private void registerToPowerUp(int viewId)
    {
        m_CurPowerUp = PhotonView.Find(viewId).gameObject;
        m_CurPowerUp.GetComponent<PowerUp>().PlayerHitPowerUp += ActivatePowerUp;
    }
}