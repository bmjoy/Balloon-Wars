using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalloonJoiner : MonoBehaviour
{
    [SerializeField] Balloon m_Balloon;

    private void OnJointBreak2D(Joint2D brokenJoint) {
        m_Balloon.OnStringBreak();    
    }
}
