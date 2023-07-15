using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public enum GameMode
    {
        Classic,
        PlayOutside
    }
    [SerializeField] Animator transition;
    
    public void MoveToGameMenu(string gameMode)
    {
        switch (gameMode)
        {
            case "Classic":
                PhotonRoomsConnector.GameMode = GameMode.Classic;
                break;
            case "PlayOutside":
                PhotonRoomsConnector.GameMode = GameMode.PlayOutside;
                break;
        }
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

    public void MoveToShop()
    {
        StartCoroutine(loadWantedSceneByName("Shop"));
    }

    public void LoadGameLevel(int LevelIndex)
    {
        Debug.Log("loading game");
        StartCoroutine(loadPhotonSceneByIndex(4 + LevelIndex));
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
        Debug.Log("Opening game scene");
        PhotonNetwork.LoadLevel(sceneIndex);
    }
}
