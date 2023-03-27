using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private AudioSource m_FinishSound;

    private void Start()
    {
        m_FinishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.name == "Player")
        {
            m_FinishSound.Play();
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        
    }
}
