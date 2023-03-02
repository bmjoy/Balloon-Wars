using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    [SerializeField] Animator transition;
    public void MoveToGameMenu()
    {
        StartCoroutine(loadWantedScene("Game menu"));
    }

    public void MoveToMainMenu()
    {
        StartCoroutine(loadWantedScene("Main menu"));
    }

    public void MoveToClassicGame()
    {
        StartCoroutine(loadWantedScene("Classic"));
    }

    private IEnumerator loadWantedScene(string sceneName)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}
