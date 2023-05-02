using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonTop : MonoBehaviour
{
    [SerializeField] Balloon m_Balloon;

    public void OnStartExploding()
    {
        m_Balloon.playPopSound();
    }    
    public void OnExploded()
    {
        m_Balloon.DestroyBalloon();
    }
}
