using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    [SerializeField] Animator transition;
    public void MoveToGameMenu()
    {
        StartCoroutine(loadWantedSceneByName("Game menu"));
    }

    public void MoveToMainMenu()
    {
        StartCoroutine(loadWantedSceneByName("Main menu"));
    }

    public void MoveToLoginScreen()
    {
        StartCoroutine(loadWantedSceneByName("Login screen"));
    }

    public void LoadGameLevel(int LevelIndex)
    {
        StartCoroutine(loadPhotonSceneByIndex(3 + LevelIndex));
    }

    private IEnumerator loadWantedSceneByName(string sceneName)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator loadPhotonSceneByIndex(int sceneIndex)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(1);
        PhotonNetwork.LoadLevel(sceneIndex);
    }
}
