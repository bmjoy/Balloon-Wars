using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinButton : MonoBehaviour
{
    public void nextCharacter()
    {
        CharacterChooser.Instance.ChangeToNextCharacter();
    }
    public void prevCharacter()
    {
        CharacterChooser.Instance.ChangeToPrevCharacter();
    }
}
