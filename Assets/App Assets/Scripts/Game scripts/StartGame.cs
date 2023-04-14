using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private MapChooser m_MapChooser;
    [SerializeField] private SceneNavigator m_SceneNavigator;
    
    public void startChosenRoom()
    {
        m_SceneNavigator.LoadGameLevel(m_MapChooser.CurMapIndex);
    }
}
