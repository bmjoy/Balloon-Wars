using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.Linq;

public class QuitGame : MonoBehaviour
{
    [SerializeField] SceneNavigator m_SceneNavigator;
    public void QuitRoom()
    {
        List<GameObject> players= GameObject.FindGameObjectsWithTag("Player").ToList();
        GameObject myPlayer = players.Find(player => player.GetComponent<PhotonView>().IsMine);
        myPlayer.GetComponent<PlayerLife>().Die();
        StartCoroutine(QuitRoomDelayed());
    }

    private IEnumerator QuitRoomDelayed()
    {
        yield return new WaitForSeconds(0.2f);
        PhotonNetwork.LeaveRoom();
        m_SceneNavigator.MoveToGameMenu();
    }
}
