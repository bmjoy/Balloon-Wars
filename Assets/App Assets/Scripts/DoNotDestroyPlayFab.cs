using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyPlayFab : MonoBehaviour
{
    private void Awake() 
    {
        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("Playfab");
        if(musicObjects.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
