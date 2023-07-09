using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterName : MonoBehaviour
{
    private TextMeshProUGUI m_CharacterNameText;
    
    void Start()
    {
        CharacterChooser.Instance.CharacterChanged += changeUICharacterName;
        m_CharacterNameText = GetComponent<TextMeshProUGUI>();
        changeUICharacterName(CharacterChooser.Instance.CurCharacterIndex);
    }

    private void changeUICharacterName(int characterNumber)
    {
        characterNumber %= CharacterChooser.Instance.CharactersNames.Count;
        m_CharacterNameText.SetText(CharacterChooser.Instance.CharactersNames[characterNumber]);
    }
}
