using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScreenChooser : MonoBehaviour
{
    [SerializeField] GameObject LoginScreen;
    [SerializeField] GameObject RememberScreen;
    void Start()
    {
        bool haveAutosave = !string.IsNullOrEmpty(PlayerPrefs.GetString("USERNAME"));
        LoginScreen.SetActive(!haveAutosave);
        RememberScreen.SetActive(haveAutosave);
    }
}
