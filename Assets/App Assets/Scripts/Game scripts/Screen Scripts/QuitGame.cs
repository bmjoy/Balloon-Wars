using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.Linq;

public class QuitGame : MonoBehaviour
{
    [SerializeField] SceneNavigator m_SceneNavigator;
    [SerializeField] Animator m_SettingsAnimator;
    public void QuitRoom()
    {
        m_SettingsAnimator.SetTrigger("SettingsQuit");
        List<GameObject> players= GameObject.FindGameObjectsWithTag("Player").ToList();
        GameObject myPlayer = players.Find(player => player.GetComponent<PhotonView>().IsMine);
        if(myPlayer != null)
        {
            Debug.Log("Killing player");
            myPlayer.GetComponent<PlayerLife>().TrapDie();
        }
        else
        {
            StartCoroutine(QuitRoomDelayed());
        }
    }

    public void returnToGameMenu()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("Left photon room");
        m_SceneNavigator.MoveToGameMenu(PhotonNetwork.CurrentLobby.Name);
        Debug.Log("Moved to game menu");
    }

    private IEnumerator QuitRoomDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        returnToGameMenu();
    }
}
