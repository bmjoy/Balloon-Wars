using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartLoaderFader : MonoBehaviour
{
    [SerializeField] private Animator m_DartStickAnimation;

    public void fadeOutDartStick()
    {
        m_DartStickAnimation.SetTrigger("DartStickOut");
    }
}
