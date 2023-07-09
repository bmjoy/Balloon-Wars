using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;
using System.Linq;
using System;

public class UICharacter : MonoBehaviour
{
    private SkinChanger m_SkinChanger;
    void Start()
    {
        m_SkinChanger = GetComponent<SkinChanger>();
        CharacterChooser.Instance.CharacterChanged += changeUICharacter;
        changeUICharacter(CharacterChooser.Instance.CurCharacterIndex);
    }

    private void OnDestroy()
    {
        CharacterChooser.Instance.CharacterChanged -= changeUICharacter;
    }

    private void changeUICharacter(int characterNumber)
    {
        m_SkinChanger.changeCharacter(characterNumber);
    }
}
